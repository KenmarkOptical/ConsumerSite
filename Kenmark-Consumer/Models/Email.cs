using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class Email
    {
        public static bool SendEmail(string sFromAddress, List<string> lToAddress, List<string> lCCAddress, string sSubject, string sBody)
        {
            try
            {
                System.Net.Mail.MailMessage MM = new System.Net.Mail.MailMessage(sFromAddress, lToAddress[0], sSubject, sBody);
                MM.IsBodyHtml = true;

                // Add each of the rest of the to addresses, ccaddresses, bccaddresses
                foreach (string sToAddress in lToAddress)
                {
                    if (sToAddress != lToAddress[0])
                    {
                        MM.To.Add(sToAddress);
                    }
                }

                foreach (string sCCAddress in lCCAddress)
                {
                    MM.CC.Add(sCCAddress);
                }


                System.Net.Mail.SmtpClient SMTP = new System.Net.Mail.SmtpClient();
                SMTP.Send(MM);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}