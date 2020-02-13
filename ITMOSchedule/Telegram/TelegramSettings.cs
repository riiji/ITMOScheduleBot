using System;
using System.Collections.Generic;
using System.Text;
using MihaZupan;

namespace ITMOSchedule.Telegram
{
    public static class TelegramSettings
    {
        public static string Key { get; set; };
        public static HttpToSocks5Proxy Proxy { get; set; };
    }
}