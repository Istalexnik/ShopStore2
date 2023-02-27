using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ShopStore2.Tools
{
    public class Emailing
    {
        public static void SendEmail(string email, string subject, string body)
        {
            using(MailMessage mm = new MailMessage("brianmanson1231@gmail.com", email))
            {
                mm.Body = body;
                mm.Subject = subject;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                NetworkCredential networkCredential = new NetworkCredential("brianmanson1231@gmail.com", "kivixklhyrgzgtmr");
                smtp.Credentials = networkCredential;
                smtp.EnableSsl = true;
                smtp.Port= 587;
                smtp.Send(mm);
            }
        }

    }
}