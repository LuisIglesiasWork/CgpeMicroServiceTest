using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class ContactTypes
    {
        public static readonly string Phone = new Guid("{077FEC40-56BD-4F6A-90A6-000687259336}").ToString();
        public static readonly string Fax = new Guid("{367F6900-E4A5-4B46-9807-000D0EADAA83}").ToString();
        public static readonly string Email = new Guid("{FDE6781F-2D2B-4E93-B74F-00135F13B2A2}").ToString();
        public static readonly string Web = new Guid("{A56773A3-323C-41F3-8A1F-0019C0634BFB}").ToString();
        public static readonly string Mobile = new Guid("{ED8E8E65-8A5E-4FF5-852E-001E0F685FF1}").ToString();
        public static readonly string Switchboard = new Guid("{EBD12DB4-923F-44D9-B8E6-00200BF79039}").ToString();
    }

}
