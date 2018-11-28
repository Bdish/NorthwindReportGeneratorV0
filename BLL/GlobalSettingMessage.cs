using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BLL
{
    public static class GlobalSettingMessage
    {
        public static string Host { get; set; } = "smtp.gmail.com";

        public static int Port { get; set; } = 587;

        public static bool EnableSsl { get; set; } = true;

        public static int Timeout { get; set; } = 10000;

        public static SmtpDeliveryMethod DeliveryMethod { get; set; } = SmtpDeliveryMethod.Network;

        public static bool UseDefaultCredentials { get; set; } = false;

        public static string UserName { get; set; } = "***********@gmail.com";

        public static string Password { get; set; } = "***********";
    }
}
