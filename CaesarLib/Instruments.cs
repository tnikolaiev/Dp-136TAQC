using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CaesarLib
{
    public static class Instruments
    {
        public static IEnumerable<object[]> ReadXML(String fileName, String elementName, params String[] attributes)
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestData\" + fileName;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList testDataNodes = xRoot.SelectNodes(elementName);

            List<object[]> result = new List<object[]>();
            List<String> temp = new List<String>();
            foreach (XmlNode node in testDataNodes)
            {
                foreach (String attr in attributes)
                {
                    temp.Add(node.SelectSingleNode("@" + attr).Value);
                }
                result.Add(temp.ToArray());
                temp.Clear();
            }
            return result.ToArray();
        }
    }
}
