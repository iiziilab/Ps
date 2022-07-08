
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PS.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PS
{
    public static class clsHelper
    {
        private static readonly string _awsAccessKey = ConfigurationHelper.config.GetSection("AWSAccessKey").Value;

        private static readonly string _awsSecretKey = ConfigurationHelper.config.GetSection("AWSSecretKey").Value;

        private static readonly string _bucketName = ConfigurationHelper.config.GetSection("Bucketname").Value;

        private static readonly string _regionName = ConfigurationHelper.config.GetSection("AWSRegion").Value;



        //[HttpPost]
        //public static bool UploadToS3(IFormFile file, string s3DirectoryName)
        //{

        //    Stream st = file.InputStream;
        //    //string name = Path.GetFileNameWithoutExtension(file.FileName);
        //    //string extenstion = Path.GetExtension(file.FileName);
        //    //name = name + DateTime.Now.ToString("yymmssfff") + extenstion;
        //    string name = string.Format(file.FileName);
        //    string myBucketName = _bucketName; //your s3 bucket name goes here  
        //                                       // string s3DirectoryName = "";
        //    string s3FileName = @name;
        //    bool a;
        //    AmazonUploader myUploader = new AmazonUploader();
        //    a = myUploader.sendMyFileToS3(st, myBucketName, s3DirectoryName, s3FileName);
        //    if (a == true)
        //    {
        //        return true;

        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    // return View();
        //}
        //[HttpPost]

        //public static bool UploadToS3WithCustomFilename(IFormFile file, string s3DirectoryName, string filename)
        //{
        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //    Stream st = file.InputStream;
        //    //string name = Path.GetFileNameWithoutExtension(file.FileName);
        //    //string extenstion = Path.GetExtension(file.FileName);
        //    //string name = filename + extenstion;
        //    // string name = string.Format(file.FileName);
        //    string myBucketName = _bucketName; //your s3 bucket name goes here  
        //                                       // string s3DirectoryName = "";
        //    string s3FileName = @filename;
        //    bool a;
        //    AmazonUploader myUploader = new AmazonUploader();
        //    a = myUploader.sendMyFileToS3(st, myBucketName, s3DirectoryName, s3FileName);
        //    if (a == true)
        //    {
        //        return true;

        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    // return View();
        //}

        //public static bool UploadEnctryptToS3(String filename, string s3DirectoryName)
        //{
        //    using (FileStream source = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    {

        //        Stream st = source;
        //        //string name = Path.GetFileNameWithoutExtension(file.FileName);
        //        //string extenstion = Path.GetExtension(file.FileName);
        //        //name = name + DateTime.Now.ToString("yymmssfff") + extenstion;
        //        string name = Path.GetFileName(filename);
        //        string myBucketName = _bucketName; //your s3 bucket name goes here  
        //                                           // string s3DirectoryName = "";
        //        string s3FileName = @name;
        //        bool a;
        //        AmazonUploader myUploader = new AmazonUploader();
        //        a = myUploader.sendMyFileToS3(st, myBucketName, s3DirectoryName, s3FileName);
        //        if (a == true)
        //        {
        //            return true;

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        // return View();
        //    }
        //}

        //public class AmazonUploader
        //{

        //    public bool sendMyFileToS3(System.IO.Stream localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        //    {
        //        // IAmazonS3 client = new AmazonS3Client(RegionEndpoint.APSoutheast1);
        //        var credentials = new BasicAWSCredentials(_awsAccessKey, _awsSecretKey);
        //        IAmazonS3 client = new AmazonS3Client(credentials, RegionEndpoint.APSouth1);
        //        TransferUtility utility = new TransferUtility(client);
        //        TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

        //        if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
        //        {
        //            request.BucketName = bucketName; //no subdirectory just bucket name  
        //        }
        //        else
        //        {   // subdirectory and bucket name  
        //            request.BucketName = bucketName + @"/" + subDirectoryInBucket;
        //        }
        //        request.Key = fileNameInS3; //file name up in S3  
        //        request.InputStream = localFilePath;
        //        utility.Upload(request); //commensing the transfer  

        //        return true; //indicate that the file was sent  
        //    }
        //}
        //public static Stream ReadObjectData(String key, String fileName)
        //{

        //    try
        //    {
        //        using (var client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.APSouth1))
        //        {
        //            var request = new GetObjectRequest
        //            {
        //                BucketName = _bucketName,
        //                // InputStream = fileName,
        //                Key = key + "/" + fileName,
        //                //Key =key,
        //                //UseChunkEncoding = false
        //            };

        //            using (var getObjectResponse = client.GetObject(request))
        //            {
        //                using (var responseStream = getObjectResponse.ResponseStream)
        //                {
        //                    var stream = new MemoryStream();
        //                    responseStream.CopyToAsync(stream);
        //                    stream.Position = 0;
        //                    return stream;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception("Read object operation failed.", exception);
        //    }

        //}

        //[HttpGet()]
        //public static Stream GetFileFromS3(String path)
        //{
        //    // var user = await GetUserFromBearerToken();
        //    //image = "submarine.jpg";

        //    //  string path = "C:\\Program Files\\Program\\fatih.gurdal.docx";
        //    string fileName = path.Substring(path.LastIndexOf(((char)92)) + 1);
        //    int index = fileName.LastIndexOf('.');
        //    string onyName = fileName.Substring(0, index);
        //    string fileExtension = fileName.Substring(index + 1);
        //    //Console.WriteLine("Full File Name: " + fileName);
        //    //Console.WriteLine("Full File Ony Name: " + onyName);
        //    //Console.WriteLine("Full File Extension: " + fileExtension);
        //    string filepath2 = @path;
        //    FileInfo fileinfo = new FileInfo(filepath2);
        //    string foldername = fileinfo.Directory.Name;
        //    string name = fileinfo.Name;

        //    // Console.WriteLine(foldername);

        //    Stream imageStream = ReadObjectData(foldername, name);
        //    HttpContext.Current.Response.Headers.Add("Content-Disposition", new ContentDisposition
        //    {
        //        FileName = name,
        //        Inline = true // false = prompt the user for downloading; true = browser to try to show the file inline
        //    }.ToString());

        //    //return File(imageStream,"image/jpeg",name);
        //    return imageStream;
        //}
    }
}
