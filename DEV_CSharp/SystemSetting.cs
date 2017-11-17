using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DEV_CSharp
{
    [XmlRoot("configuration")]
    public class SystemSetting
    {
        [XmlElement("username")]
        public string _username { get; set; }
        [XmlElement("password")]
        public string _password { get; set; }
        public static int ReadXML<Type>(out Type pClass, string pPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            using (FileStream stream = new FileStream(pPath, FileMode.Open))
            {
                pClass = (Type)serializer.Deserialize(stream);
            }
            return 0;
        }
        public static int WriteXML<Type>(Type pClass, string pPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            using (FileStream stream = new FileStream(pPath, FileMode.Create))
            {
                serializer.Serialize((Stream)stream, pClass);
            }
            return 0;
        }
    }
}
