using Microsoft.WindowsAzure.Storage;
using ReportingProvisioning;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPCopyToBlob
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncPump.Run(async delegate
            {
                    await Run();
                
            });
        }

        static async Task Run()
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ftpUri"]);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                // This example assumes the FTP site uses anonymous logon.  
                request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpLogin"], ConfigurationManager.AppSettings["ftpPassword"]);
                request.EnableSsl = true;
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string line = reader.ReadLine();
                List<string> files = new List<string>();
                while (line!=null)
                {
                    files.Add(line);
                    line = reader.ReadLine();
                }

                //Console.WriteLine("Download Complete, status {0}", response.StatusDescription);

                reader.Close();
                response.Close();

                CloudStorageAccount account = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("thinkcxftpdatastorage", ConfigurationManager.AppSettings["azureKey"]), true);
                var blobClient=account.CreateCloudBlobClient();
                var container=blobClient.GetContainerReference("ftpfiles");
                foreach (var file in files)
                {
                    var blob = container.GetBlockBlobReference(file);
                    if (!blob.Exists())
                    {
                        request = (FtpWebRequest)WebRequest.Create($"{ConfigurationManager.AppSettings["ftpUri"]}/{file}");
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        // This example assumes the FTP site uses anonymous logon.  
                        request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpLogin"], ConfigurationManager.AppSettings["ftpPassword"]);
                        request.EnableSsl = true;
                        request.UsePassive = true;
                        request.UseBinary = true;
                        request.KeepAlive = false;
                        request.Timeout = Timeout.Infinite;

                        response = (FtpWebResponse)request.GetResponse();

                        responseStream = response.GetResponseStream();
                        responseStream.WriteTimeout = Timeout.Infinite;
                        responseStream.ReadTimeout = Timeout.Infinite;

                        blob.UploadFromStream(responseStream);
                        response.Close();
                    }
                }
            }
            catch(Exception ex)
            {

            }

        }
    }

}
