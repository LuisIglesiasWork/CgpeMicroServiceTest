//using Cgpe.Du.CrossCuttings;
using Cgpe.Du.Domain;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure;
using Cgpe.Du.Ministry.WcfApi.Contracts;
using Cgpe.Du.Ministry.WcfApi.Maps;
using Cgpe.Du.Ministry.WcfApi.Properties;
//using Cgpe.LogClient;
using Cgpe.Security.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Configuration;
using System.IO;
using Ubiety.Dns.Core;
using System.Diagnostics;

namespace Cgpe.Du.Ministry.WcfApi
{

    public class MinistryAppService : IDisposable
    {

        #region Constants

        private const string pageSizeName = "PageSize";
        private const string notificationEmailName = "NotificationEmail";
        private const string notificatorEmailName = "NotificatorEmail";
        private const string notificatorEmailPass = "NotificatorEmailPass";
        private const string smtpServerName = "SmtpServer";

        #endregion

        private MinistryIntegrationDomainService ministryDomainService;
        private Cgpe.Du.Domain.SecurityDomainService securityDomainService;
        //private CgpeLogClient logWriter;
        private IUnitOfWork uow;
        private string ministryCif;
        private MinistryMapper ministryMapper;

        private static object sync = new object();

        public MinistryAppService()
        {
            //StringBuilder sb1 = new StringBuilder();
            //sb1.Append('C');
            //File.AppendAllText(@"c:\log.txt", sb1.ToString());
            //sb1.Clear();
            // this.logWriter = new CgpeLogClient(DuMessageBusManager.MessageBus);
            try
            {
                this.uow = new DuUnitOfWork();
                this.ministryCif = ConfigurationManager.AppSettings["MinistryCif"];// ConfigurationManager.AppSettings["MinistryCif"];
                this.ministryMapper = new MinistryMapper();
                this.securityDomainService = new SecurityDomainService(this.uow, new DirectoryUserRepository(this.uow));
                this.ministryDomainService = new MinistryIntegrationDomainService(uow, new ProcuratorRepository(uow), new IntegrationWorkflowRepository(uow), new AuditRepository(uow));
            }
            catch (Exception ex)
            {
                //this.logWriter.Error(Resources.MinistryAppServiceStartError, null, null, ex);
            }
        }

        public ColegiadosResponse HandleMinistryRequest(ColegiadosRequest colegiadosRequest)
        {
            try
            {
                this.Login();
                if (colegiadosRequest.destinoPeticion != colegiadosRequestDestinoPeticion.PRO)
                    throw this.HandleException(Resources.DestinoPeticionError, ErrorType.FUN_1006);
                IntegrationWorkflow workflowInstance = this.GetWorkflow(colegiadosRequest.numeroPeticion, colegiadosRequest.pagina);
                int pageSize = int.Parse(ConfigurationManager.AppSettings[pageSizeName]);
                if (pageSize <= 0)
                    throw new ConfigurationErrorsException(string.Format(Resources.ParameterError, pageSizeName));
                ColegiadosResponse response = new ColegiadosResponse()
                {
                    fechaDesde = colegiadosRequest.fechaDesde,
                    numeroPeticion = workflowInstance.WorkflowId.ToString(),
                    origenRespuesta = "PRO",
                    colegiados = new colegiadosResponseColegiados()
                }; // HASTA AQUÍ PARECE QUE ESTÁ BIEN (2018-11-15 14:29)
                switch (workflowInstance.CurrentState)
                {
                    case IntegrationWorkflowStatesEnum.INICIADO:
                        response.codigoRetorno = "INI";
                        response.descripcionRetorno = "INICIADO";
                        this.ministryDomainService.CreateIntegrationWorkflowInstanceCommit();
                        if (colegiadosRequest.numeroPeticion.Trim().ToLower() == "inicio_carga_inicial")
                            workflowInstance.CurrentState = IntegrationWorkflowStatesEnum.CARGA_INICIAL;
                        else
                            workflowInstance.CurrentState = IntegrationWorkflowStatesEnum.PENDIENTE;
                        this.ministryDomainService.SaveIntegrationWorkflowInstance(workflowInstance);
                        break; // HASTA AQUÍ PARECE QUE ESTÁ BIEN (2018-11-15 14:33)
                    case IntegrationWorkflowStatesEnum.CARGA_INICIAL:
                    case IntegrationWorkflowStatesEnum.PENDIENTE:
                        int recordsNumber = this.ministryDomainService.SyncInitTransaction(workflowInstance.WorkflowId, colegiadosRequest.fechaDesde, DateTime.Now, colegiadosRequest.codigoColegio, workflowInstance.CurrentState == IntegrationWorkflowStatesEnum.CARGA_INICIAL);
                        if (recordsNumber <= 0)
                        {
                            response.codigoRetorno = "FIN";
                            response.descripcionRetorno = "FINALIZADO";
                            workflowInstance.CurrentState = IntegrationWorkflowStatesEnum.FINALIZADO;
                            this.ministryDomainService.SaveIntegrationWorkflowInstance(workflowInstance);
                            // ESTÁ BIEN
                        }
                        else
                        {
                            response.codigoRetorno = "PEN";
                            response.descripcionRetorno = "PENDIENTE";
                            response.totalPaginas = ((int)Math.Ceiling((decimal)recordsNumber / pageSize)).ToString();
                            workflowInstance.CurrentState = IntegrationWorkflowStatesEnum.PROCESANDO;
                            workflowInstance.TotalRecordsNumber = recordsNumber;
                            this.ministryDomainService.SaveIntegrationWorkflowInstance(workflowInstance);
                            // ESTÁ BIEN
                        }
                        break;
                    case IntegrationWorkflowStatesEnum.PROCESANDO:
                        return this.HandleMinistryPageRequest(colegiadosRequest, response, workflowInstance, pageSize);
                    case IntegrationWorkflowStatesEnum.FINALIZADO:
                        throw this.HandleException(Resources.MessageTypeError, ErrorType.FUN_9100x);
                    // PARECE QUE ESTÁ BIEN
                    default:
                        throw this.HandleException(string.Format(Resources.WorkflowStateError, Enum.GetName(typeof(IntegrationWorkflowStatesEnum), workflowInstance.CurrentState)), ErrorType.FUN_9100x);
                        // PARECE QUE ESTÁ BIEN
                }
                return response;
            }
            catch (ServiceUnavailableException suex)
            {
                throw this.HandleException(suex, ErrorType.TEC_1001);
            }
            catch (FaultException<ProfesionalesFaultContract>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw this.HandleException(ex, ErrorType.TEC_9100x);
            }
        }

        private ColegiadosResponse HandleMinistryPageRequest(ColegiadosRequest colegiadosRequest, ColegiadosResponse response, IntegrationWorkflow workflowInstance, int pageSize)
        {
            List<string> changeIds = null;
            try
            {
                workflowInstance.CurrentPage++;
                List<Procurator> updateSet = new List<Procurator>();
                List<Procurator> changeSet = this.ministryDomainService.SyncGetPageOfRecords(workflowInstance.WorkflowId, pageSize);
                changeIds = (from cs in changeSet select cs.ProcuratorId).ToList();

                response.codigoRetorno = "PRO";
                response.descripcionRetorno = "PROCESANDO";
                response.pagina = workflowInstance.CurrentPage.ToString();
                response.totalPaginas = ((int)Math.Ceiling((decimal)workflowInstance.TotalRecordsNumber / pageSize)).ToString();

                response.colegiados.actualizacionColegiado = this.ministryMapper.MapProcurators(changeSet, updateSet);



                if (workflowInstance.CurrentPage == int.Parse(response.totalPaginas))
                {
                    response.codigoRetorno = "FIN";
                    response.descripcionRetorno = "FINALIZADO";
                }
                bool isSentOk = true;
                try
                {
                    return response;
                }
                catch (Exception exSnd)
                {
                    isSentOk = false;
                    throw new Exception(string.Format(Resources.PageSendingError, workflowInstance.CurrentPage), exSnd);
                }
                finally
                {
                    if (isSentOk && workflowInstance.CurrentPage == int.Parse(response.totalPaginas))
                    {
                        workflowInstance.CurrentState = IntegrationWorkflowStatesEnum.FINALIZADO;
                        workflowInstance.CurrentPage = 0;
                    }
                    if (changeIds != null && changeIds.Count > 0)
                    {
                        this.ministryDomainService.SyncMarkProcessedPage(workflowInstance.WorkflowId, isSentOk, changeIds);
                    }
                    if (updateSet != null && updateSet.Count > 0)
                    {
                        this.ministryDomainService.UpdateProcuratorsSentState(updateSet);
                    }
                    this.ministryDomainService.SaveIntegrationWorkflowInstance(workflowInstance);
                }
            }
            catch (Exception exPro)
            {
                workflowInstance.CurrentPage--;
                throw new Exception(string.Format(Resources.PageGenerationError, workflowInstance.CurrentPage), exPro);
            }
        }

        private void UpdateSentProcurators(List<Procurator> sentProcs)
        {
            this.ministryDomainService.UpdateSentProcurators(sentProcs);
        }

        public ReportResponse HandleMinistryReport(ColegiadosReport colegiadosReport)
        {
            try
            {
                this.Login();
                if (colegiadosReport.destinoPeticion != colegiadosReportDestinoPeticion.PRO)
                    throw this.HandleException("Field \"destinoPeticion\" is not valid.", ErrorType.FUN_1006);
                IntegrationWorkflow workflowInstance = this.GetWorkflow(colegiadosReport.numeroPeticion, colegiadosReport.pagina);
                switch (workflowInstance.CurrentState)
                {
                    case IntegrationWorkflowStatesEnum.INICIADO:
                    case IntegrationWorkflowStatesEnum.PENDIENTE:
                    case IntegrationWorkflowStatesEnum.PROCESANDO:
                        throw this.HandleException("Invalid message type. \"ColegiadosRequest\" is expected.", ErrorType.FUN_9100x);
                    case IntegrationWorkflowStatesEnum.FINALIZADO:
                        try
                        {
                            List<string> procuratorsNifs = null;
                            workflowInstance.CurrentPage++;
                            if (colegiadosReport.colegiadosConError.colegiadoConError != null && colegiadosReport.colegiadosConError.colegiadoConError.Length > 0)
                            {
                                StringBuilder writer = new StringBuilder("Errores de sincronización de Directorio Único de los Procuradores con el Ministerio de Justicia:\r\n");
                                bool write = true;
                                procuratorsNifs = new List<string>(colegiadosReport.colegiadosConError.colegiadoConError.Length);
                                foreach (colegiadosReportColegiadosConErrorColegiadoConError procurator in colegiadosReport.colegiadosConError.colegiadoConError)
                                {
                                    procuratorsNifs.Add(procurator.colegiado.numeroIdentificacion.ToLower());
                                    if (write)
                                    {
                                        writer.AppendLine(string.Format(
                                            @"
    El procurador:
        Tipo de documento:      ""{0}""
        Número del documento:   ""{1}"" 
    Error:
        Tipo de error:          ""{2}""
        Codigo de error:        ""{3}""
        Causa del error:        ""{4}""
        Descripción:            ""{5}""
        Acción requerida:       ""{6}""
"
                                            , procurator.colegiado.tipoIdentificacion, procurator.colegiado.numeroIdentificacion, procurator.Error.TipoError, procurator.Error.codigoError, procurator.Error.causaError, procurator.Error.descripcionError, procurator.Error.accion));
                                        if (writer.Length > 4000)
                                        {
                                            writer.AppendLine("Hay mas errores...");
                                            write = false;
                                        }
                                    }
                                }
                                this.ministryDomainService.SyncMarkProcuratorsWithError(procuratorsNifs);
                                if (writer.Length > 0)
                                    this.SendEmail(writer.ToString());
                            }
                            if (workflowInstance.CurrentPage == colegiadosReport.totalPaginas)
                            {
                                workflowInstance.CurrentState = IntegrationWorkflowStatesEnum.INFORME_RECIBIDO;
                                // MOMENTO CLAVE: AQUÍ SE REGISTRAN LOS NIFS QUE SE HAN ENVIADO Y SE HAN ACEPTADO.
                                this.ministryDomainService.SyncMarkAcceptedPage(workflowInstance.WorkflowId, true, procuratorsNifs);
                            }
                            this.ministryDomainService.SaveIntegrationWorkflowInstance(workflowInstance);
                        }
                        catch (Exception ex)
                        {
                            this.ministryDomainService.Rollback();
                            throw ex;
                        }
                        break;
                    case IntegrationWorkflowStatesEnum.INFORME_RECIBIDO:
                        throw this.HandleException("Transaction is terminated and the Report was received.", ErrorType.FUN_1003);
                    default:
                        throw this.HandleException("The Workflow State \"" + Enum.GetName(typeof(IntegrationWorkflowStatesEnum), workflowInstance.CurrentState) + "\" is not supported.", ErrorType.FUN_9100x);
                }
                ReportResponse response = new ReportResponse();
                response.numeroPeticion = colegiadosReport.numeroPeticion;
                return response;
            }
            catch (ServiceUnavailableException suex)
            {
                throw this.HandleException(suex, ErrorType.TEC_1001);
            }
            catch (FaultException<ProfesionalesFaultContract>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw this.HandleException(ex, ErrorType.TEC_9100x);
            }
        }

        private void Login()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("Entro En el Login");
            //File.AppendAllText(@"c:\log.txt", sb.ToString());
            //sb.Clear();
            //if (OperationContext.Current.ServiceSecurityContext.AuthorizationContext.ClaimSets == null || OperationContext.Current.ServiceSecurityContext.AuthorizationContext.ClaimSets.Count <= 0)
            //throw new SecurityException("No claimset. Service configuration error.");
            CgpeCertificateTool certTool = new CgpeCertificateTool();
            X509Certificate2 clientCertificate = ((System.IdentityModel.Claims.X509CertificateClaimSet)OperationContext.Current.ServiceSecurityContext.AuthorizationContext.ClaimSets[0]).X509Certificate;
            //if (clientCertificate == null)
              //  throw new SecurityException("Access denied.");
            // TODO :: Llamar a FNMT
            ClaimsIdentity identity = new ClaimsIdentity("Custom", ClaimTypes.Name, ClaimTypes.Role);
            string nif = certTool.GetUserNif(clientCertificate);
            identity.AddClaim(new Claim(ClaimTypes.Name, nif));
            //if (identity.Name.ToUpper() != this.ministryCif.ToUpper())
               // throw new SecurityException("Access denied.");

            var user = new DirectoryUser(identity, clientCertificate);
            if (!user.IsInitialized)
            {
                this.securityDomainService.ReadSecurityData(ref user, clientCertificate);
            }
            //lock (sync)
            //{
            //    if (!user.IsInitialized)
            //    {
            //        securityDomainService.ReadSecurityData(ref user, clientCertificate);
            //    }
            //}

            Thread.CurrentPrincipal = user;

            Thread.CurrentPrincipal = new DirectoryUser(identity, clientCertificate);
        }

        private void SendEmail(string message)
        {
            SmtpClient mailClient = new SmtpClient(ConfigurationManager.AppSettings[smtpServerName]);
            mailClient.EnableSsl = true;
            mailClient.Port = 587;
            mailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings[notificatorEmailName], ConfigurationManager.AppSettings[notificatorEmailPass]);
            MailMessage mail = new MailMessage(ConfigurationManager.AppSettings[notificatorEmailName], ConfigurationManager.AppSettings[notificationEmailName], "Directorio Único 3.0 :: Errores de integración", message);
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            try
            {
                mailClient.Send(mail);
            }
            catch (Exception ex)
            {
                //this.logWriter.Error("Cgpe.Du.Ministry.WcfApi Send Email error.", null, null, ex);
            }
        }



        private IntegrationWorkflow GetWorkflow(string requestNumber, int requestedPage)
        {
            IntegrationWorkflow workflowInstance = null;
            if (!string.IsNullOrWhiteSpace(requestNumber) && requestNumber.Trim().ToLower() != "inicio_carga_inicial")
            {
                //Guid workflowInstanceId = Guid.Empty;
                //if (Guid.TryParse(requestNumber, out workflowInstanceId))
                //   workflowInstance = this.ministryDomainService.GetIntegrationWorkflowInstance(workflowInstanceId.ToString());
                //string workflowInstanceId = requestNumber.Trim();
                workflowInstance = this.ministryDomainService.GetIntegrationWorkflowInstance(requestNumber);
            }
            else
            {
                workflowInstance = this.ministryDomainService.CreateIntegrationWorkflowInstance(false);
            }
            if (workflowInstance == null)
                throw this.HandleException("Field \"numeroPeticion\" is not valid.", ErrorType.FUN_1003);
            int currentPage = workflowInstance.CurrentPage + 1;
            if (requestedPage > 0 && requestedPage != currentPage)
                throw this.HandleException("Field \"pagina\" is not valid.", ErrorType.FUN_1004);
            return workflowInstance;
        }



        #region Error Handling

        private FaultException<ProfesionalesFaultContract> HandleException(Exception ex, ErrorType type)
        {
            return this.HandleException(ex.ToString(), type);
        }

        private FaultException<ProfesionalesFaultContract> HandleException(string errorMessage, ErrorType type)
        {
            //this.logWriter.Error(errorMessage);
            return new FaultException<ProfesionalesFaultContract>(
                    new ProfesionalesFaultContract()
                    {
                        CodigoError = Enum.GetName(type.GetType(), type)
                    },
                    new FaultReason(errorMessage),
                    new FaultCode("500"));
        }

        #endregion

        public void Dispose()
        {
            if (this.uow != null)
                this.uow.Dispose();
        }

    }

}