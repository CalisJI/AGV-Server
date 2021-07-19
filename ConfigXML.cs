using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Windows.Forms;


namespace Call_AGV
{
    public class ConfigXML
    {
        public static string Write_txt = "Write.txt";
        public static string Read_txt = "Read.txt";
        private static string Xml_path = Application.StartupPath + @"\" + "System Configuration.xml";
        public static string Create_MapFile(string FileName)
        {
            string MapFile = Application.StartupPath + @"\" + FileName + ".xml";
            return MapFile;
        }
        #region Con_figuration APP
        public static Call_AGV_Configue GetSystem_Config()
        {
            if (File.Exists(Xml_path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Call_AGV_Configue));
                Stream stream = new FileStream(Xml_path, FileMode.Open);
                Call_AGV_Configue Call_AGV_Configue = (Call_AGV_Configue)serializer.Deserialize(stream);
                stream.Close();
                return Call_AGV_Configue;
            }
            else
            {
                Call_AGV_Configue Call_AGV_Configue = new Call_AGV_Configue();
                Call_AGV_Configue.Sel_travel = "";
                Call_AGV_Configue.File_data = new List<string>();
                Call_AGV_Configue.Selected_Station = string.Empty;
                Call_AGV_Configue.Path_Communicate = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(Call_AGV_Configue));
                Stream stream = new FileStream(Xml_path, FileMode.Create);
                XmlWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                serializer.Serialize(writer, Call_AGV_Configue);
                writer.Close();
                stream.Close();
                return Call_AGV_Configue;
            }
        }
        public static void UpdateSystem_Config(string nodeName, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Xml_path);
            XmlElement xml_elm = xmlDoc.DocumentElement;
            foreach (XmlNode node in xml_elm.ChildNodes)
            {
                if (node.Name == nodeName) node.InnerText = value;
            }
            xmlDoc.Save(Xml_path);
        }
        public static void UpdateFullSystem_Config(Call_AGV_Configue call_AGV_Configue) 
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Call_AGV_Configue));
            using (TextWriter textWriter = new StreamWriter(Xml_path))
            {
                xmlSerializer.Serialize(textWriter, call_AGV_Configue);
                textWriter.Close();
            }
        }
        #endregion
        #region Mapping Configuration
        public static Mapping GetMapping(string Mapping_path) 
        {
            if (File.Exists(Create_MapFile(Mapping_path))) 
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Mapping));
                Stream stream = new FileStream(Create_MapFile(Mapping_path), FileMode.Open);
                Mapping mapping = (Mapping)xmlSerializer.Deserialize(stream);
                stream.Close();
                return mapping;
            }
            else 
            {
                Mapping Mapping = new Mapping();
                Mapping.Rectangles = new List<System.Drawing.Rectangle>();
                Mapping.Position = new List<System.Drawing.Point>();
                Mapping.Rectangles_2_Fill = new List<System.Drawing.Rectangle>();
                Mapping.Route = new List<System.Drawing.Point>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Mapping));
                Stream stream = new FileStream(Create_MapFile(Mapping_path), FileMode.Create);
                XmlWriter xmlwriter = new XmlTextWriter(stream, Encoding.UTF8);
                xmlSerializer.Serialize(xmlwriter, Mapping);
                xmlwriter.Close();
                stream.Close();
                return Mapping;
            }
        }
        public static void Update_Mapping(Mapping mapping, string MapFile_Path) 
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Mapping));
            using (TextWriter textWriter = new StreamWriter(Create_MapFile(MapFile_Path))) 
            {
                xmlSerializer.Serialize(textWriter, mapping);
                textWriter.Close();
            }
        }
        #endregion
    }
}
