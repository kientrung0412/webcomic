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

        public static Messenge Upload(HttpPostedFileBase file, String filePath, String fileName)
        {
            Messenge messenge = new Messenge();

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
                        messenge.Code = 1;
                        messenge.Mss = fileName;

                        return messenge;
                    }
                    catch (Exception e)
                    {
                        messenge.Mss = String.Format("Tải lên thất bại!, {0}", e);
                        messenge.Code = 0;

                        return messenge;
                    }
                }
                else
                {
                    messenge.Mss = "Có lỗi tải lên";
                    messenge.Code = 0;

                    return messenge;
                }
            }
            catch (Exception e)
            {
                messenge.Mss = "Có lỗi: " + e;
                messenge.Code = 0;

                return messenge;
            }
        }
    }
}