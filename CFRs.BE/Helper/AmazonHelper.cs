using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using CFRs.BLL.CONFIG;
using System.Data;

namespace CFRs.BE.Helper
{
    public static class AmazonHelper
    {
        public static string UploadFileToS3(Stream localFilePath, string fileNameInS3, string PathInS3)
        {
            try
            {
                DataTable dtConfig = ConfigBLL.Instance.GetConfigBLL($" AND CONFIG_NAME = 'S3'");
                if(dtConfig.Rows.Count == 0)
                    throw new Exception("Not found config S3.");

                string BucketName = dtConfig.Rows[0]["CONFIG_VALUE_1"].ToString();
                string BucketFolder = dtConfig.Rows[0]["CONFIG_VALUE_2"].ToString();

                string FixRootPath = "CFRs";

                IAmazonS3 client = new AmazonS3Client(
                      dtConfig.Rows[0]["CONFIG_VALUE_3"].ToString()
                    , dtConfig.Rows[0]["CONFIG_VALUE_4"].ToString()
                    , RegionEndpoint.APSoutheast1);

                // create a TransferUtility instance passing it the IAmazonS3 created in the first step
                TransferUtility utility = new TransferUtility(client);
                // making a TransferUtilityUploadRequest instance
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                if (BucketFolder == "" || BucketFolder == null)
                {
                    request.BucketName = BucketName; //no subdirectory just bucket name
                }
                else
                {   // subdirectory and bucket name
                    request.BucketName = BucketName + @"/" + BucketFolder;
                }

                request.BucketName = (request.BucketName + "/" + FixRootPath + "/" + PathInS3).Replace("//", "/");

                request.Key = fileNameInS3; //file name up in S3
                                            //request.FilePath = localFilePath; //local file name
                request.InputStream = localFilePath;
                //request.CannedACL = S3CannedACL.PublicRead;
                utility.Upload(request); //commensing the transfer

                return request.BucketName.ToString() + "/" + request.Key.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}