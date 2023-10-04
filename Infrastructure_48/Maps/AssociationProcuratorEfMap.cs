using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class AssociationProcuratorEfMap
    {

        public void Map(AssociationProcuratorEntity source, AssociationProcurator target, bool mapProcurator = false)
        {
            if (target.AssociationProcuratorAddresses == null)
            {
                target.AssociationProcuratorAddresses = new List<Address>();
            }

            if (target.SituationHistory == null)
            {
                target.SituationHistory = new List<SituationChange>();
            }

            if (target.Cancellations == null)
            {
                target.Cancellations = new List<Cancellation>();
            }

          

            target.AssociationProcuratorId = source.AssociationProcuratorId;
            target.MemberNumber = source.MemberNumber;

            target.IsDefault = source.IsDefault;
            target.IsFirst = source.IsFirst;
            target.RegistrationRequestDate = source.RegistrationRequestDate;
            target.RegistrationDate = source.RegistrationDate;

            target.IsCancelled = source.IsCancelled;
            target.ProcuratorId = source.ProcuratorId;

            if (source.CurrentSituation != null)
            {
                ProcuratorSituationEfMap situationMapper = new ProcuratorSituationEfMap();
                target.CurrentSituation = new ProcuratorSituation();
                situationMapper.Map(source.CurrentSituation, target.CurrentSituation);
            }
            target.CurrentSituationDate = source.CurrentSituationDate;

            if (source.Association != null)
            {
                target.Association = new Association();
                AssociationEfMap assoMapper = new AssociationEfMap();
                assoMapper.Map(source.Association, target.Association);
            }

            if (source.AssociationProcuratorAddresses != null && source.AssociationProcuratorAddresses.Count > 0)
            {
                AddressEfMap addressMapper = new AddressEfMap();
                Address address;
                foreach (AddressEntity addressEntity in source.AssociationProcuratorAddresses)
                {
                    address = new Address();
                    addressMapper.Map(addressEntity, address);
                    target.AssociationProcuratorAddresses.Add(address);
                }
            }

            if (source.SituationHistory != null && source.SituationHistory.Count > 0)
            {
                SituationChangeEfMap situationChangeMapper = new SituationChangeEfMap();
                SituationChange situationChange = null;
                foreach (SituationChangeEntity situationChangeEntity in source.SituationHistory)
                {
                    situationChange = new SituationChange();
                    situationChangeMapper.Map(situationChangeEntity, situationChange);
                    target.SituationHistory.Add(situationChange);
                }
            }

            if (source.Procurator != null && mapProcurator)
            {
                target.Procurator = new Procurator();
                new ProcuratorEfMap().Map(source.Procurator, target.Procurator);
            }

            target.IsCancelled = source.IsCancelled;

            if (source.Cancellations != null && source.Cancellations.Count > 0)
            {
                Cancellation can;
                CancellationEfMap canMapper = new CancellationEfMap();

                foreach (CancellationEntity canEnt in source.Cancellations)
                {
                    can = new Cancellation();
                    canMapper.Map(canEnt, can);
                    target.Cancellations.Add(can);
                }
            }

            //target.CreationStateId = source.CreationStateId;
            target.CreatorUserId = source.CreatorUserId;
            target.CreationStateReason = source.CreationStateReason;
            target.CreationRequestDate = source.CreationRequestDate;
            target.LatestCreationStateDate = source.LatestCreationStateDate;
        }

        public void ShallowMap(AssociationProcuratorEntity source, AssociationProcurator target)
        {
            if (target.AssociationProcuratorAddresses == null)
            {
                target.AssociationProcuratorAddresses = new List<Address>();
            }

            if (target.SituationHistory == null)
            {
                target.SituationHistory = new List<SituationChange>();
            }

            target.AssociationProcuratorId = source.AssociationProcuratorId;
            target.MemberNumber = source.MemberNumber;

            target.IsDefault = source.IsDefault;
            target.IsFirst = source.IsFirst;
            target.RegistrationRequestDate = source.RegistrationRequestDate;
            target.RegistrationDate = source.RegistrationDate;

            target.IsCancelled = source.IsCancelled;

            

            target.ProcuratorId = source.ProcuratorId;

         
            if (source.Association != null)
            {
                target.Association = new Association();
                AssociationEfMap assoMapper = new AssociationEfMap();
                assoMapper.Map(source.Association, target.Association);
            }
         
            if (source.Procurator != null)
            {
                target.Procurator = new Procurator();
                new ProcuratorEfMap().ShallowMap(source.Procurator, target.Procurator);
            }

           
           // target.CreationStateId = source.CreationStateId;
            target.CreatorUserId = source.CreatorUserId;
            target.CreationStateReason = source.CreationStateReason;
            target.CreationRequestDate = source.CreationRequestDate;
            target.LatestCreationStateDate = source.LatestCreationStateDate;

        }

        public void Map(AssociationProcurator source, AssociationProcuratorEntity target, string procuratorId, bool isNew = false)
        {
            if (target.AssociationProcuratorAddresses == null)
            {
                target.AssociationProcuratorAddresses = new List<AddressEntity>();
            }
            if (target.SituationHistory == null)
            {
                target.SituationHistory = new List<SituationChangeEntity>();
            }

            if(target.Cancellations == null)
            {
                target.Cancellations = new List<CancellationEntity>();
            }

            if (isNew)
            {
                source.AssociationProcuratorId = Guid.NewGuid().ToString();
            }
            target.AssociationProcuratorId = source.AssociationProcuratorId;

            if (procuratorId != null)
            {
                target.ProcuratorId = procuratorId;
            }
            target.MemberNumber = source.MemberNumber;

            target.CurrentSituationId = source.CurrentSituation.ProcuratorSituationId;
            target.CurrentSituationDate = source.CurrentSituationDate;

            target.IsDefault = source.IsDefault;
            target.RegistrationRequestDate = source.RegistrationRequestDate;
            target.RegistrationDate = source.RegistrationDate;
            // La deregistration date no la mapeo porque es calculada.
            target.AssociationId = source.Association.AssociationId;
            //target.ProcuratorId = source.Procurator.ProcuratorId;



            target.IsCancelled = source.IsCancelled;
            target.CurrentCancellationDate = source.GetCurrentCancellationDate();

            target.IsFirst = source.IsFirst;

           // target.CreationStateId = source.CreationStateId;
            target.CreatorUserId = source.CreatorUserId;
            target.CreationStateReason = source.CreationStateReason;
            target.CreationRequestDate = source.CreationRequestDate;
            target.LatestCreationStateDate = source.LatestCreationStateDate;

        }


    }

}
