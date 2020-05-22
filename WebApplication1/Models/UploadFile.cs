using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace Model.Models
{
    public class UploadFile
    {
        public FileUpload Upload(HttpPostedFileBase file, String filePath)
        {
            FileUpload fileUpload = new FileUpload();
            
            if (file.ContentLength > 0)
            {
                String fileName = String.Format("{0}_{1}", DateTime.Now.ToString("ddMMyy_hhmmss"), file.FileName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                try
                {
                    file.SaveAs(Path.Combine(filePath, fileName));
                    fileUpload.Code = 1;
                    fileUpload.Mss = String.Format("{0}/{1}", filePath, fileName);

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
    }
}