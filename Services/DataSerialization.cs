using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TaskManager.Models;
using Type = System.Type;

namespace TaskManager.Services
{
    public class DataSerialization
    {
        public static void Serialize<T>(T data, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new Type[] { typeof(ToDoList) });
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
            }

        }

        public static T Deserialize<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new Type[] { typeof(ToDoList) });
            FileInfo fileInfo = new FileInfo(filePath);
            if (File.Exists(filePath) && fileInfo.Length != 0)
            {
                using (TextReader reader = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            else
            {
                File.Create(filePath);
                return default;
            }

        }
    }

}
