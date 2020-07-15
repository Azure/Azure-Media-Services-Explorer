using System;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AMSExplorer.ManifestGeneration
{
    public class XmlManifestUtils
    {
        public static string AddIsmcToIsm(string ismXmlContent, string newIsmcFileName)
        {
            // Example head tag for the ISM on how to include the ISMC.
            // <head>
            //   < meta name = "clientManifestRelativePath" content = "GOPR0881.ismc" />
            //   < meta name = "formats" content = "mp4" />
            // </ head >

            string manPath = "clientManifestRelativePath";

            byte[] array = Encoding.ASCII.GetBytes(ismXmlContent);
            // Checking and removing Byte Order Mark (BOM) for UTF-8 if present.
            if (array[0] == 63)
            {
                byte[] tempArray = new byte[array.Length - 1];
                Array.Copy(array, 1, tempArray, 0, tempArray.Length);
                ismXmlContent = Encoding.UTF8.GetString(tempArray);
            }

            XDocument doc = XDocument.Parse(ismXmlContent);
            XNamespace ns = "http://www.w3.org/2001/SMIL20/Language";

            // If the node is already there we should skip this asset.  Maybe.  Or maybe update it?
            if (doc != null)// && ismXmlContent.IndexOf("clientManifestRelativePath") < 0)
            {
                XElement bodyhead = doc.Element(ns + "smil").Element(ns + "head");
                var element = new XElement(ns + "meta", new XAttribute("name", manPath), new XAttribute("content", newIsmcFileName));

                var manifestRelPath = bodyhead.Elements(ns + "meta").Where(e => e.Attribute("name").Value == manPath).FirstOrDefault();
                if (manifestRelPath != null)
                {
                    manifestRelPath.ReplaceWith(element);
                }
                else
                {
                    bodyhead.Add(element);
                }
            }
            else
            {
                throw new Exception("Xml document cannot be read or is empty.");
            }
            return doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
        }

        public static string RemoveXmlNode(string ismcContentXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ismcContentXml);
            XmlNode node = doc.SelectSingleNode("//SmoothStreamingMedia");
            XmlNode child = doc.SelectSingleNode("//Protection");
            node.RemoveChild(child);
            return doc.OuterXml;
        }
    }
}
