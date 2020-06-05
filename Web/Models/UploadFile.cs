using System;
using System.IO;
using System.Web;

namespace Model.Models
{
    public class UploadFile
    {
        public String SetNameFile(HttpPostedFileBase file)
        {
            String fileName = String.Format("{0}_{1}", DateTime.Now.ToString("ddMMyy_hhmmss"), file.FileName);
            return fileName;
        }

        public static FileUpload Upload(HttpPostedFileBase file, String filePath, String fileName)
        {
            FileUpload fileUpload = new FileUpload();

            try
            {
                if (file.ContentLength > 0)
                {

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    try
                    {
                        file.SaveAs(Path.Combine(filePath, fileName));
                        fileUpload.Code = 1;
                        fileUpload.Mss = fileName;

                        return fileUpload;
                    }
                    catch (Exception e)
                    {
                        fileUpload.Mss = String.Format("Tải lên thất bại!, {0}", e);
                        fileUpload.Code = 0;

                        return fileUpload;
                    }
                }
                else
                {
                    fileUpload.Mss = "Có lỗi tải lên";
                    fileUpload.Code = 0;

                    return fileUpload;
                }
            }
            catch (Exception e)
            {
                fileUpload.Mss = "Có lỗi: " + e;
                fileUpload.Code = 0;

                return fileUpload;
            }
        }
    }
}