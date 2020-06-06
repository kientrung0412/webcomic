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

        public static Messenger Upload(HttpPostedFileBase file, String filePath, String fileName)
        {
            Messenger messenger = new Messenger();

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
                        messenger.Code = 1;
                        messenger.Mss = fileName;

                        return messenger;
                    }
                    catch (Exception e)
                    {
                        messenger.Mss = String.Format("Tải lên thất bại!, {0}", e);
                        messenger.Code = 0;

                        return messenger;
                    }
                }
                else
                {
                    messenger.Mss = "Có lỗi tải lên";
                    messenger.Code = 0;

                    return messenger;
                }
            }
            catch (Exception e)
            {
                messenger.Mss = "Có lỗi: " + e;
                messenger.Code = 0;

                return messenger;
            }
        }
    }
}