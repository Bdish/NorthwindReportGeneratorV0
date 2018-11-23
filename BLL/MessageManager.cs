using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BLL
{
    public class MessageManager
    {

      /*  public MessageManager()
        {

        }*/

        public void SendMsgWithFile(string email,string subject,string textBody ,Attachment attachment)
        {
            SmtpClient client = new SmtpClient(GlobalSettingMessage.Host, GlobalSettingMessage.Port);
            client.EnableSsl = GlobalSettingMessage.EnableSsl; 
            client.Timeout = GlobalSettingMessage.Timeout;
            client.DeliveryMethod = GlobalSettingMessage.DeliveryMethod;
            client.UseDefaultCredentials = GlobalSettingMessage.UseDefaultCredentials;
            client.Credentials = new System.Net.NetworkCredential(GlobalSettingMessage.UserName, GlobalSettingMessage.Password);
            MailMessage msg = new MailMessage();
            msg.To.Add(email);
            msg.From = new MailAddress(GlobalSettingMessage.UserName);
            msg.Subject = subject;
            msg.Body = textBody;
            msg.Attachments.Add(attachment);
            client.Send(msg);
        }
    }
}
