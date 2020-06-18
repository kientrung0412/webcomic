using System;
using System.IO;
using System.Web;

namespace Model.Models
{
    public class UploadFile
    {
        public static Messenger Upload(HttpPostedFileBase file, String filePath, String fileName)
        {
            Messenger messenger = new Messenger();

            try
            {
                var typeFile = file.ContentType;

                if (typeFile.Contains("image"))
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

                    messenger.Mss = "Có lỗi tải lên";
                    messenger.Code = 0;

                    return messenger;
                }
                
                messenger.Mss = "Định dạng file không đúng";
                messenger.Code = 0;
                
                return messenger;
                
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