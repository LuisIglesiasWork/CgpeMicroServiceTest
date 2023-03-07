using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cgpe.Du.Domain.Entities;

namespace Cgpe.Du.Domain
{

    public class FileDomainService
    {
        #region Fields & Properties
        private IFileStorageProxy fileStorageProxy;
        #endregion

        public FileDomainService(IFileStorageProxy fileStorageProxy)
        {
            this.fileStorageProxy = fileStorageProxy;
        }

        public void UploadBlob(IdentificationDocumentFile document, byte[] fileContent)
        {

            this.fileStorageProxy.UploadBlob(document.ExternalFileFullName, fileContent);

            var hashProvider = new SHA1Managed();
            byte[] hash = hashProvider.ComputeHash(fileContent);
            document.Hash = BitConverter.ToString(hash).Replace("-", String.Empty);
        }

        public byte[] DownloadBlob(string blobName)
        {
            return this.fileStorageProxy.DownloadBlob(blobName);
        }

    }

}
