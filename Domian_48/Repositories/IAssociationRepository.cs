using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IAssociationRepository
    {

        void Create(Association association);

        Association Read(string associationId);

        Association ReadByCif(string associationNif);

        void Update(Association association);

        List<Association> GetAssociations();
   
        bool CheckIfAssociationIdDocumentExists(string cif, string associationId);
    }

}
