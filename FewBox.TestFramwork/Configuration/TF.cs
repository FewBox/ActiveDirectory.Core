using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FewBox.TestFramwork.Configuration
{
    public static class TF
    {
        private static Dictionary<string, string> properties;

        static TF()
        {
            properties = new Dictionary<string, string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(String.Format(@"{0}\TestFramework.tfconfig.xml", Assembly.GetExecutingAssembly().GetDirectoryPath()));
            if (xmlDocument.DocumentElement != null)
            {
                foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
                {
                    if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == "Properties")
                    {
                        foreach (XmlNode propertyXmlNode in xmlNode.ChildNodes)
                        {
                            if (propertyXmlNode.NodeType == XmlNodeType.Element && propertyXmlNode.Name == "Property")
                            {
                                if (propertyXmlNode.Attributes != null)
                                {
                                    string attributeKey = propertyXmlNode.Attributes["Key"].Value;
                                    string attributeValue = propertyXmlNode.Attributes["Value"].Value;
                                    if (properties.ContainsKey(attributeKey))
                                    {
                                        throw new KeyReduplicateException();
                                    }
                                    else
                                    {
                                        properties.Add(attributeKey, attributeValue);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static Dictionary<string, string> PS
        {
            get
            {
                
                return properties;
            }
        }

    }

    public static class AssemblyExtension
    {
        public static string GetDirectoryPath(this Assembly assembly)
        {
            string filePath = new Uri(assembly.CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
    }
}
