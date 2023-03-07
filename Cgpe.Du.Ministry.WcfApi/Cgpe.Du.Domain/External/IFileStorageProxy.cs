using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cgpe.Du.Domain
{

    public interface IFileStorageProxy
    {
        void UploadBlob(string blobName, byte[] fileContent);
        void DeleteBlob(string blobName);
        byte[] DownloadBlob(string blobName);
    }
}
