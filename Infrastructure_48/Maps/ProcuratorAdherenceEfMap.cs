using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cgpe.Du.Infrastructure
{

    public class ProcuratorAdherenceEfMap
    {

        public void Map(ProcuratorAdherence source, ProcuratorAdherenceEntity target, string procuratorId, string addressId, string phoneId, string mobileId, string emailId, bool isNew = false)
        {
            if (isNew)
            {
                source.ProcuratorAdherenceId = Guid.NewGuid().ToString();
            }
            target.ProcuratorAdherenceId = source.ProcuratorAdherenceId;

            if (source.Association != null)
            {
                target.AssociationId = source.Association.AssociationId;
            }

            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.BankAccount = source.BankAccount;
            target.OtherData = source.OtherData;

            target.AddressId = addressId;
            target.PhoneContactId = phoneId;
            target.MobileContactId = mobileId;
            target.EmailContactId = emailId;

            target.AgreementId = source.Agreement.AgreementId;

        }

        public void Map(ProcuratorAdherenceEntity source, ProcuratorAdherence target)
        {
            target.ProcuratorAdherenceId = source.ProcuratorAdherenceId;
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.BankAccount = source.BankAccount;
            target.OtherData = source.OtherData;

            target.Agreement = new Agreement();
            AgreementEfMap agreeMapper = new AgreementEfMap();
            agreeMapper.Map(source.Agreement, target.Agreement);

            if (source.Address != null)
            {
                target.Address = new Address();
                AddressEfMap addrMapper = new AddressEfMap();
                addrMapper.Map(source.Address, target.Address);

            }

            if (source.Association != null)
            {
                target.Association = new Association();
                AssociationEfMap assoMapper = new AssociationEfMap();
                assoMapper.Map(source.Association, target.Association);

            }

            ContactEfMap contactMapper = new ContactEfMap();
            if (source.Phone != null)
            {
                target.Phone = new Contact();
                contactMapper.Map(source.Phone, target.Phone);
            }

            if (source.Mobile != null)
            {
                target.Mobile = new Contact();
                contactMapper.Map(source.Mobile, target.Mobile);
            }

            if (source.Email != null)
            {
                target.Email = new Contact();
                contactMapper.Map(source.Email, target.Email);
            }
        }

    }

}
