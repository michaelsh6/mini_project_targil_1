using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BE
{
    public static class Tools
    {
        public static readonly string BankAccuntPath = "atm.xml";
        public static readonly string configPath = "config.xml";
        public static readonly string HostingUnitPath = "HostingUnit.xml";
        public static readonly string OrderPath = "Order.xml";
        public static readonly string GuestPath = "Guest.xml";

        public static XElement ConfigRoot;


        public static void DownloadBankXml()
        {
            configurition.BanksXmlFinish = false;

            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, BankAccuntPath);
                
            }
            catch (Exception)
            {
                try
                {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, BankAccuntPath);
                }
                catch(Exception)
                {
                    
                }
            }
            finally
            {
                wc.Dispose();
                configurition.BanksXmlFinish = true;
            }

        }

        public static List<BankAccunt> XmlToBankAccunt(XElement bankAccuntsRoot)
        {
            try
            {
                return (from bankAccunt in bankAccuntsRoot.Elements()
                        select new BankAccunt()
                        {
                            BankName = bankAccunt.Element("שם_בנק").Value.Trim(),
                            BankNumber = Convert.ToInt32(bankAccunt.Element("קוד_בנק").Value.Trim()),
                            BranchAddress = bankAccunt.Element("כתובת_ה-ATM").Value.Trim(),
                            BranchCity = bankAccunt.Element("ישוב").Value.Trim(),
                            BranchNumber = Convert.ToInt32(bankAccunt.Element("קוד_סניף").Value.Trim())
                        }
                        ).Distinct().ToList();
            }
            catch (Exception ex)
            {
                // throw new Exception("file_problem_Order");
                throw ex;
            }
        }

        public static void SaveConfigToXml()
        {
            try
            {
                ConfigRoot = new XElement("config");
                ConfigRoot.Add(
                    new XElement("GuestRequestKey", configurition.GuestRequestKey),
                    new XElement("HostingUnitKey", configurition.HostingUnitKey),
                    new XElement("OrderKey", configurition.OrderKey),
                    new XElement("commission", configurition.commission),
                    new XElement("LastApdate", configurition.LastApdate));
                ConfigRoot.Save(configPath);
            }
            catch (Exception) { }
        }


        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }


        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }



        public static bool sendMail(string To, string Subject, string Body, bool isHtml)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress("israelhostingservice@gmail.com");
            mail.Subject = Subject;
            mail.Body = Body;

            mail.IsBodyHtml = isHtml;
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("israelhostingservice@gmail.com", "israel0000");

            smtp.EnableSsl = true;
            try
            {
                //שליחת ההודעה //
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(1);
            int columns = arr.GetLength(0);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    var test = arr[i, j];
                    arrFlattened[i * rows + j] = arr[i, j];
                }
            }
            return arrFlattened;
        }
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[columns, rows];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrExpanded[i, j] = arr[i * rows + j ];
                }
            }
            return arrExpanded;
        }

        public static T Clone<T>(this T source)
        {
            var isNotSerializable = !typeof(T).IsSerializable;
            if (isNotSerializable)
                throw new ArgumentException("The type must be serializable.", "source");
            var sourceIsNull = ReferenceEquals(source, null);
            if (sourceIsNull)
                return default(T);
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}
