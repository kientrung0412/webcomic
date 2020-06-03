using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Model.Models
{
    public class MyConvert
    {
        public static String ListDictionaryToString(Dictionary<string, string> dictionary)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();

            try
            {
                binFormatter.Serialize(mStream, dictionary);

                var byteArray = mStream.ToArray();

                string result = HttpServerUtility.UrlTokenEncode(byteArray);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Dictionary<string, string> StringToListDictionary(String s)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();

            try
            {
                var bytes = HttpServerUtility.UrlTokenDecode(s);

                mStream.Write(bytes, 0, bytes.Length);
                mStream.Position = 0;

                Dictionary<string, string> listDictionary =
                    binFormatter.Deserialize(mStream) as Dictionary<string, string>;

                return listDictionary;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}