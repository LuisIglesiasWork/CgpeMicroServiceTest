using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Cgpe.Du.Domain
{

    public class IntegrationDomainService
    {

        private IUnitOfWork uow;
        private IProcuratorRepository procuratorRepository;
        private IAssociationRepository associationRepository;

        public IntegrationDomainService(IUnitOfWork uow, IProcuratorRepository procuratorRepository, IAssociationRepository associationRepository)
        {
            this.uow = uow;
            this.procuratorRepository = procuratorRepository;
            this.associationRepository = associationRepository;

        }

        public List<Procurator> GetProcuratorsListForBoe()
        {
            return procuratorRepository.GetProcuratorsListForBoe();
        }

        public List<Association> GetAssociationsListForAcu()
        {
            return associationRepository.GetAssociations();
        }

        public List<Procurator> GetProcuratorsListForNotary()
        {
            return procuratorRepository.GetProcuratorsListForNotary();
        }

        public List<Procurator> GetProcuratorsListForRegistrators()
        {
            return procuratorRepository.GetProcuratorsListForRegistrators();
        }

        public List<Procurator> GetProcuratorsListForAcu()
        {
            return procuratorRepository.GetProcuratorsListForNotary();
        }


        public Procurator GetProcuratorByNif(string nif)
        {
            return procuratorRepository.GetProcuratorByNif(nif);
        }


        public Association GetAssociationByCif(string cif)
        {
            return associationRepository.ReadByCif(cif);
        }

        public List<Procurator> GetProcuratorsListForCgpj()
        {
            return procuratorRepository.GetProcuratorsListForCgpj();
        }
    }

}
