using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Cgpe.Du.CrossCuttings
{
    public static class FileHttpResponseMessage
    {

        /// <summary>   Type of the CSV media. </summary>
        public const string CSV_MEDIA_TYPE = "text/csv";
        /// <summary>   Type of the XSLX media. </summary>
        public const string XLSX_MEDIA_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        /// <summary>   Type of the APP media. </summary>
        public const string APP_MEDIA_TYPE = "application/octet-stream";
        /// <summary>   The download mode inline. </summary>
        private const string STR_DOWNLOAD_MODE_INLINE = "inline";

        /// <summary>   The download mode attachment. </summary>
        private const string STR_DOWNLOAD_MODE_ATTACHMENT = "attachment";
        public static HttpResponseMessage AddStreamContentToHttpResponseMessage(DataTable result,
            string mediaTypeHeaderValue,
            bool forceDownload,
            string fileName, Encoding encoding = null)
        {
            return AddStreamContentToHttpResponseMessage(result, result, mediaTypeHeaderValue, forceDownload, fileName,
                encoding);
        }
        public static HttpResponseMessage AddStreamContentToHttpResponseMessage(object result,
            DataTable content,
            string mediaTypeHeaderValue,
            bool forceDownload,
            string fileName, Encoding encoding = null)
        {
            var stream = new MemoryStream();

            var writer = encoding == null ? new StreamWriter(stream) : new StreamWriter(stream, encoding);
            foreach (DataRow contentRow in content.Rows)
            {
                writer.Write(contentRow.ItemArray.Select(x => x.ToString()).ToArray());
            }
            writer.Flush();
            stream.Position = 0;

            var resHttp = HttpJsonResponse.CreateResponse(HttpStatusCode.OK, result);
            resHttp.Content.Headers.Add("x-filename", fileName);
            resHttp.Content = new ByteArrayContent(stream.ToArray());
            resHttp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaTypeHeaderValue);

            var downloadType = forceDownload ? STR_DOWNLOAD_MODE_ATTACHMENT : STR_DOWNLOAD_MODE_INLINE;


            resHttp.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(downloadType)
            {
                FileName = fileName
            };
            
            return resHttp;
        }
        public static HttpResponseMessage AddStreamContentToHttpResponseMessage(string mediaTypeHeaderValue,
            bool forceDownload,
            string fileName, string requestLocation, Encoding encoding = null)
        {
            return AddStreamContentToHttpResponseMessage(requestLocation, "", mediaTypeHeaderValue, forceDownload, fileName,
                encoding);
        }
        public static HttpResponseMessage AddStreamContentToHttpResponseMessage(string requestLocation, string filePath,
            string mediaTypeHeaderValue,
            bool forceDownload,
            string fileName, Encoding encoding = null)
        {
            byte[] file = { };
            if (File.Exists(Path.Combine(filePath, fileName)))
                using (var fs = new FileStream(Path.Combine(filePath, fileName), FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(fs))
                    {
                        file = reader.ReadBytes((int) fs.Length);
                    }
                }

            var resHttp = HttpJsonResponse.CreateResponse(HttpStatusCode.OK, new ByteArrayContent(file));
            resHttp.Content.Headers.Add("x-filename", fileName);
            resHttp.Content = new ByteArrayContent(file);
            resHttp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaTypeHeaderValue);

            var downloadType = forceDownload ? STR_DOWNLOAD_MODE_ATTACHMENT : STR_DOWNLOAD_MODE_INLINE;


            resHttp.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(downloadType)
            {
                FileName = requestLocation//Path.Combine(filePath, fileName)
            };
            
            return resHttp;
        }

        public static void AddStreamContentToHttpResponseMessage(ref HttpResponseMessage resHttp,
            StringBuilder content,
            string mediaTypeHeaderValue,
            bool forceDownload,
            string fileName, Encoding encoding = null)
        {
            var stream = new MemoryStream();

            var writer = encoding == null ? new StreamWriter(stream) : new StreamWriter(stream, encoding);

            writer.Write(content.ToString());
            writer.Flush();
            stream.Position = 0;

            resHttp.Content = new StreamContent(stream);
            resHttp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaTypeHeaderValue);

            var downloadType = forceDownload ? STR_DOWNLOAD_MODE_ATTACHMENT : STR_DOWNLOAD_MODE_INLINE;


            resHttp.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(downloadType)
            {
                FileName = fileName
            };


        }
    }
}
