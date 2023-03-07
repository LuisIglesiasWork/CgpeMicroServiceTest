using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class LanguageEfMap
    {
        public void Map(LanguageEntity source, Language target)
        {
            target.LanguageId = source.LanguageId;
            target.LanguageCode = source.LanguageCode;
            target.LanguageName = source.LanguageName;
        }

        public void Map(Language source, LanguageEntity target)
        {
            target.LanguageId = source.LanguageId;
            target.LanguageCode = source.LanguageCode;
            target.LanguageName = source.LanguageName;
        }
    }

}
