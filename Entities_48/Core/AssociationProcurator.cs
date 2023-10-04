using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;


namespace Cgpe.Du.Domain.Entities
{

    public class AssociationProcurator
    {
        /// <summary>
        /// Identificador de la colegiación
        /// </summary>
        public string AssociationProcuratorId { get; set; }

        /// <summary>
        /// Número de socio o colegiado del procurador dentro del colegio
        /// </summary>
        public string MemberNumber { get; set; }

        /// <summary>
        /// Fecha en la que el procurador pasó a su situación actual
        /// </summary>
        public DateTime CurrentSituationDate { get; set; }

        /// <summary>
        /// Situación actual del procurador (en el ejercicio de su profesión dentro de la colegiación)
        /// </summary>
        public ProcuratorSituation CurrentSituation { get; set; }

        /// <summary>
        /// Historial de cambios de la situación del procurador (en el ejercicio de su profesión dentro de la colegiación)
        /// </summary>
        public List<SituationChange> SituationHistory { get; set; }

        /// <summary>
        /// Fecha de alta del procurador como colegiado.
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// "Fecha en que insta colegiación"
        /// </summary>
        public DateTime? RegistrationRequestDate { get; set; }

        /// <summary>
        /// Fecha de baja del procurador como colegiado.
        /// </summary>
        public DateTime? DeregistrationDate { get; set; }

        /// <summary>
        /// Si es la colegiación predeterminada.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Indica si es la primera colegiación, es decir, la que se ha introducido cuando
        /// se ha dado de alta el procurador.
        /// </summary>
        public bool IsFirst { get; set; }

        /// <summary>
        /// Colegio en el que el procurador está colegiado.
        /// </summary>
        [IgnoreDataMember]
        public Association Association { get; set; }

        /// <summary>
        /// Identificador del procurador colegiado.
        /// </summary>
        public string ProcuratorId { get; set; }

        public List<Address> AssociationProcuratorAddresses { get; set; }

        /// <summary>
        /// Indica si la colegiación está actualmente cancelada. Esto ocurrirá si 
        /// en el historial de cancelaciones hay alguna que esté vigente en la actualidad.
        /// </summary>
        public bool IsCancelled { get; set; }

        // <summary>
        /// (Historial de) cancelaciones de la colegiación. Potencialmente, podrían ser varias a lo largo del tiempo,
        /// aunque esto fuese una circunstancia extremadamente rara.
        /// </summary>
        public List<Cancellation> Cancellations { get; set; }

        /// <summary>
        /// Procurador colegiado
        /// </summary>
        public Procurator Procurator { get; set; }

        #region Creation request

        /// <summary>
        /// Estado actual de la colegiación (enumerado). La creaación de la colegiación sigue un flujo de aprobación si la creación 
        /// la lleva a cabo un colegio en lugar del CGPE.
        /// </summary>
        //public CreationStateEnum CreationStateId { get; set; }
        /// <summary>
        /// Fecha en la que se solicitó la creación (es decir, en la que se hizo la "prealta") de la colegiación.
        /// </summary>
        public DateTime? CreationRequestDate { get; set; }
        /// <summary>
        /// Fecha en la que ha ocurrido el último cambio de estado en el flujo de creación de la colegiación.
        /// </summary>
        public DateTime LatestCreationStateDate { get; set; }
        /// <summary>
        /// Identificador del usuario que ha creado la colegiación.
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// Razón de que la creación de la colegiación se encuentre en un determinado estado dentro del flujo.
        /// En la práctica, sólo se usa cuando se rechaza la creación de la colegiación.
        /// </summary>
        public string CreationStateReason { get; set; }

        #endregion Creation request




        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.MemberNumber))
            {
                throw new Exception(Resources.NumberOfAssociatedRequiredValidation);
            }
            if(this.Association == null || this.Association.AssociationId == null || this.Association.AssociationId == string.Empty)
            {
                throw new Exception(Resources.AssociationProcuratorAssociationRequiredValidation);
            }
            if (this.CurrentSituationDate == null || this.CurrentSituationDate == DateTime.MinValue)
            {
                throw new Exception(Resources.StateDateRequiredValidation);
            }
            if(this.CurrentSituation == null)
            {
                throw new Exception(Resources.CurrentSituationRequiredValidation);
            }

            Regex regex = new Regex(@"^\s*\d{6}\s*$");
            if (!regex.IsMatch(this.MemberNumber))
            {
                throw new Exception(Resources.NumberOfAssociatedInvalidValidation);
            }
        }

        /// <summary>
        /// Ordena el historial de situaciones profesionales y, además, calcula si cada cambio ha supuesto un "alta" (si el procurador
        /// ha pasado a estar de "no ejerciente" a "ejerciente"), "baja" (si ha pasado de "ejerciente" a "no ejerciente") o modificación
        /// (si se ha quedado en el mismo estado de actividad, "ejerciente" o "no ejerciente", según el caso).
        /// </summary>
        public void OrderSituationChanges()
        {
            bool firstOperationSet = false;

            if (this.SituationHistory != null)
            {
                this.SituationHistory = this.SituationHistory.OrderBy(sc => sc.OperationDate).ThenBy(sc => sc.CreationDate).ToList();

                bool wasPreviouslyPractising = true;

                for (int i = 0; i < this.SituationHistory.Count; i++)
                {
                    if (!firstOperationSet && !this.SituationHistory[i].IsDeleted)
                    {
                        if (this.SituationHistory[i].ProcuratorSituation.ProcuratorSituationId == ProcuratorSituationEnum.Practising
                            || this.SituationHistory[i].ProcuratorSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredTemporarily)
                        {
                            this.SituationHistory[i].OperationType = new OperationType()
                            {
                                TypeId = OperationTypes.Add
                            };

                            wasPreviouslyPractising = true;

                        }
                        else
                        {
                            this.SituationHistory[i].OperationType = new OperationType()
                            {
                                TypeId = OperationTypes.Delete
                            };
                            wasPreviouslyPractising = false;

                        }
                    }
                    else if (!this.SituationHistory[i].IsDeleted)
                    {
                        if (this.SituationHistory[i].ProcuratorSituation.ProcuratorSituationId == ProcuratorSituationEnum.Practising
                            || this.SituationHistory[i].ProcuratorSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredTemporarily)
                        {
                            if (!wasPreviouslyPractising)
                            {
                                this.SituationHistory[i].OperationType = new OperationType()
                                {
                                    TypeId = OperationTypes.Add
                                };
                            }
                            else
                            {
                                this.SituationHistory[i].OperationType = new OperationType()
                                {
                                    TypeId = OperationTypes.Update
                                };
                            }
                            wasPreviouslyPractising = true;

                        }
                        else
                        {
                            if (wasPreviouslyPractising)
                            {
                                this.SituationHistory[i].OperationType = new OperationType()
                                {
                                    TypeId = OperationTypes.Delete
                                };
                            }
                            else
                            {
                                this.SituationHistory[i].OperationType = new OperationType()
                                {
                                    TypeId = OperationTypes.Update
                                };
                            }

                            wasPreviouslyPractising = false;

                        }
                    }
                }

            }
        }

        /// <summary>
        /// Calcula si el procurador puede utilizar las plataformas de software que hay a disposición de los procuradores 
        /// (ejemplo: LexNET).
        /// </summary>
        /// <returns>Si el procurador puede utilizar las plataformas de software</returns>
        public bool ChekIfCanUseSoftwarePlatforms()
        {
            return (this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.Practising
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedPersonalAffairs
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedClaimWages
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.RetiredClosing
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredTemporarily);
        }

        /// <summary>
        /// Calcula si el procurador puede utilizar la website privada que hay a disposición de los procuradores.
        /// </summary>
        /// <returns>Si el procurador puede utilizar la website privada</returns>
        public bool CheckIfCanUsePrivateWebsite()
        {
            return (this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.Practising
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.NonPractising
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedPersonalAffairs
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedClaimWages
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.RetiredClosing
                || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredTemporarily);
        }

        /// <summary>
        /// Calcula si el procurador está "ejerciente" dentro de la colegiación.
        /// </summary>
        /// <returns>Si el procurador está "ejerciente"</returns>
        public bool CheckIfPractisingWithinAssociation()
        {
            return (this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.Practising
                       || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredTemporarily);
        }

        /// <summary>
        /// Calcula si el procurador está con el despacho en liquidación.
        /// </summary>
        /// <returns>Si el procurador está con al despacho en liquidación</returns>
        public bool CheckIfClosingWithinAssociation()
        {
            return (this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.RetiredClosing);
        }

        /// <summary>
        /// Cancula si la colegiación está "cerrada"-
        /// </summary>
        /// <returns>Si la colegiación está "cerrada"</returns>
        public bool CheckIfAssociationMembershipClosed()
        {
            return (
                this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.None
            || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.PassedAway
            || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredForever
            || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredNotPaying
            || this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.Expelled);
        }

        /// <summary>
        /// Calcula si la colegiación está suspendida (por decisión del propio colegio que gestiona la colegiación).
        /// </summary>
        /// <returns>Si la colegiación está suspendida</returns>
        public bool CheckIfAssociationMembershipSuspended()
        {
            return this.CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.Suspended;
        }

        /// <summary>
        /// Calcula si la colegiación está cancelada a partir del historial/registro de cancelaciones
        /// (mira si hay alguna que esté vigente).
        /// </summary>
        /// <returns>Si la colegiación está cancelada</returns>
        public bool CheckIfAssociationMembershipCancelled()
        {
            DateTime today = new DateTime();
            return this.Cancellations != null && this.Cancellations.Any(s => s.StartDate <= today && (!s.EndDate.HasValue || s.EndDate >= today));
        }

        /// <summary>
        /// Calcula la fecha de la cancelación vigente, si la colegiación está cancelada
        /// </summary>
        /// <returns>Fecha de la cancelación vigente, si la hay</returns>
        public DateTime? GetCurrentCancellationDate()
        {

            if (CheckIfAssociationMembershipCancelled())
            {
                DateTime today = new DateTime();
                return this.Cancellations.Where(s => s.StartDate <= today && (!s.EndDate.HasValue || s.EndDate >= today)).Select(c => c.StartDate).FirstOrDefault();
            }

            return null;
        }


        /// <summary>
        /// Calcula la fecha de baja de la colegiación a partir del historial de situaciones
        /// </summary>
        /// <returns>Fecha de baja de la colegiación (si se ha producido baja)</returns>
        public DateTime? GetDeregistrationDate()
        {

            DateTime? deregistrationDate = null;

            SituationChange maxPastDeregistration = null;
            SituationChange minFutureDeregistration = null;
            DateTime? currentDateTime = new DateTime();

            if (this.SituationHistory != null && this.SituationHistory.Count > 0)
            {
                foreach (SituationChange situationChangeEntity in this.SituationHistory)
                {
                    if (situationChangeEntity.ProcuratorSituation.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredForever
                       || situationChangeEntity.ProcuratorSituation.ProcuratorSituationId == ProcuratorSituationEnum.PassedAway
                       || situationChangeEntity.ProcuratorSituation.ProcuratorSituationId == ProcuratorSituationEnum.Expelled)
                    {
                        if (situationChangeEntity.OperationDate > currentDateTime)
                        {
                            if (minFutureDeregistration == null || minFutureDeregistration.OperationDate > situationChangeEntity.OperationDate)
                            {
                                minFutureDeregistration = situationChangeEntity;
                            }
                        }
                        else if (situationChangeEntity.OperationDate <= currentDateTime)
                        {
                            if (maxPastDeregistration == null || maxPastDeregistration.OperationDate < situationChangeEntity.OperationDate)
                            {
                                maxPastDeregistration = situationChangeEntity;
                            }
                        }
                    }
                }
            }

            if (maxPastDeregistration != null)
            {
                deregistrationDate = maxPastDeregistration.OperationDate;
            }
            else if (minFutureDeregistration != null)
            {
                deregistrationDate = minFutureDeregistration.OperationDate;
            }

            return deregistrationDate;

        }


    }

}
