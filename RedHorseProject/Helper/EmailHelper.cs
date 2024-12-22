using System.Net;
using System.Net.Mail;


public class EmailHelper
{
    public static void SendEmail(string toEmail, string subject, string body)
    {
        var fromAddress = new MailAddress("erayyciftci@gmail.com", "Red Horse");
        var toAddress = new MailAddress(toEmail);
        const string fromPassword = "kxfgocvjdndogleb"; 

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
    }
}


