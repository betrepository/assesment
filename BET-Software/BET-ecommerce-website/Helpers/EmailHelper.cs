using System.Net.Mail;

namespace BET_ecommerce_website.Helpers
{
    public static class EmailHelper
    {
        public static void sendEmail(int ordernumber,string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("testingbetassessment@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Assessment";
            mail.Body = "Dear Reader, Kindly not that you have checked in successfully, and your order number is " + ordernumber.ToString();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("testingbetassessment@gmail.com", "!Q@W3e4r");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
     }
}