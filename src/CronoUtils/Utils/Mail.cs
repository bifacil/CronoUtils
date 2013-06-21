using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


namespace CronoUtils
{

        public class Mail
        {

            private static string host;
            private static int portNumber;
            private static Boolean EnableSsl = true;
            private static NetworkCredential credentials;
            private static MailAddress fromAddress;
            private static string[] toAddresses;
            

            public static void SetGMailCredentials(string name, string password)
            {
                SetCredentials("smtp.gmail.com:587", name, password);
            }

            public static void SetCredentials(string hostAndPort, string name, string password)
            {
                host = hostAndPort.Split(':').First();
                portNumber = int.Parse(hostAndPort.Split(':').Last());
                credentials = new NetworkCredential(name, password);
            }


            public static void SetFromAddress(string address, string displayName)
            {
                fromAddress = new MailAddress(address, displayName);
            }


            public static void SetToAddresses(params string[] addresses)
            {
                toAddresses = addresses;
            }


 

            public static void SendMail(string subject, string body, Boolean isbodyhtml)
            {
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = portNumber,
                    EnableSsl = EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = credentials
                };
                
                using (var message = new MailMessage()
                {
                    From=fromAddress,
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isbodyhtml
                })
                {
                    toAddresses.ToList().ForEach(s => message.To.Add(new MailAddress(s)));
                    smtp.Send(message);
                }
            }


      

            public static void SendMail(string subject, string body)
            {
                SendMail(subject, body, false);
            }


            public static void SendHtmlMail(string subject, string body)
            {
                SendMail(subject, body, true);
            }

            public static void SendMarkdownMail(string subject, string body)
            {
                body = CronoUtils.Markdown.Transform(body);
                SendMail(subject, body, true);
            }






        }







    }



