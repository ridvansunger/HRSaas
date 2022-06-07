using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HRSaas.Core.Helpers
{
    public static class MailSender
    {
        public static void Send(string mailtosend)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.To.Add(mailtosend);
            mail.Priority = MailPriority.High;
            mail.From = new MailAddress("infohrsaas@gmail.com", "Şifre Güncelleme",System.Text.Encoding.UTF8);
            mail.Subject = "HRSaas Platformu - Personel Giriş Bilgileri";
           mail.Body= $"Sayın şirketi otomasyon giriş işlemleriniz tamamlanmıştır. {DateTime.Now.ToShortDateString()} tarihinden itibaren sisteme giriş yapabilirsiniz.<br> <hr /> <br> <strong>Giriş Bilgileriniz: </strong> <br>Mail: https://easyhrmanagement.azurewebsites.net/Login/ActivateUser <br> <br/><br/><b>Güvenliğiniz için lüfen bu maili kimseyle paylaşmayınız.</b><br/><b><a href=\"https://igoldenshot.azurewebsites.net\">Otomasyona Giriş</a></b>";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential("infohrsaas@gmail.com", "Hrsaas123_");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;

            smtpClient.Send(mail);
        }
    }
}
