using Cgpe.Du.CrossCuttings;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Ministry.WcfApi.Contracts;
using Cgpe.Security.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Cgpe.Du.Ministry.WcfApi
{

    [ServiceBehavior(Namespace = "http://mju.ntj.ecd.profesionales", Name = "profesionales")]
    public class Profesionales : IProfesionales
    {

        #region Actions

        public ColegiadosResponse actualiza(ColegiadosRequest colegiadosRequest)
        {
            MinistryAppService appService = new MinistryAppService();
            return appService.HandleMinistryRequest(colegiadosRequest);
        }

        public ReportResponse actualizaReport(ColegiadosReport colegiadosReport)
        {
            MinistryAppService appService = new MinistryAppService();
            return appService.HandleMinistryReport(colegiadosReport);
        }

        #endregion

    }

}
