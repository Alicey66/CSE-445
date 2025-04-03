using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://www.public.asu.edu/~1224829749/Hotels.xml";
        public static string xmlErrorURL = "https://www.public.asu.edu/~1224829749/HotelsErrors.xml";
        public static string xsdURL = "https://www.public.asu.edu/~1224829749/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, xsdUrl);
                settings.ValidationType = ValidationType.Schema;

                string errorMsg = "No Error";

                settings.ValidationEventHandler += (sender, e) =>
                {
                    errorMsg = $"Validation Error: {e.Message}";
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) ;
                }

                return errorMsg;
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        public static string Xml2Json(string xmlUrl)
        {
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            try
            {
                string xmlContent = new WebClient().DownloadString(xmlUrl);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);

                string jsonText = JsonConvert.SerializeXmlNode(doc.DocumentElement, Newtonsoft.Json.Formatting.Indented, true);

                return jsonText;
            }
            catch (Exception ex)
            {
                return $"Exception during XML to JSON conversion: {ex.Message}";
            }
        }
    }

}
