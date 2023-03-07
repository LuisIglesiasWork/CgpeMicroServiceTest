using System;
using System.Collections.Generic;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cgpe.Du.Domain;

namespace Cgpe.Du.Infrastructure
{
    //public class AzureStorageProxy : IFileStorageProxy
    //{
    //    private CloudStorageAccount storageAccount;
    //    private CloudBlobClient blobClient;
    //    private CloudBlobContainer container;

    //    public AzureStorageProxy()
    //    {
    //        this.storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["AzureStorageConnectionString"]);
    //        this.blobClient = this.storageAccount.CreateCloudBlobClient();
    //        this.container = this.blobClient.GetContainerReference(ConfigurationManager.AppSettings["AzurePrivateContainer"]);

    //        if (this.container.CreateIfNotExistsAsync().Result)
    //        {
    //            this.container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }).Wait();
    //        }
    //    }

    //    public void UploadBlob(string blobName, byte[] fileContent)
    //    {
    //        CloudBlockBlob blockBlob = this.container.GetBlockBlobReference(blobName);

    //        bool successfullyUploaded = false;
    //        int tries = 5;

    //        while (!successfullyUploaded)
    //        {
    //            tries++;
    //            try
    //            {
    //                blockBlob.UploadFromByteArrayAsync(fileContent, 0, fileContent.Length).Wait();
    //            }
    //            catch
    //            {
    //                if (tries >= 5)
    //                {
    //                    throw;
    //                }
    //            }
    //            successfullyUploaded = true;
    //        }

    //    }



    //    public void DeleteBlob(string blobName)
    //    {
    //        CloudBlockBlob blockBlob = this.container.GetBlockBlobReference(blobName);
    //        blockBlob.DeleteIfExistsAsync().Wait();
    //    }

    //    public byte[] DownloadBlob(string blobName)
    //    {
    //        CloudBlockBlob blockBlob = this.container.GetBlockBlobReference(blobName);
    //        blockBlob.FetchAttributesAsync().Wait();
    //        byte[] file = new byte[blockBlob.Properties.Length];
    //        blockBlob.DownloadToByteArrayAsync(file, 0).Wait();
    //        return file;
    //    }
    //}
}
