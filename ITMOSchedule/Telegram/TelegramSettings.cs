using System;
using System.Collections.Generic;
using System.Text;
using MihaZupan;

namespace ITMOSchedule.Telegram
{
    public static class TelegramSettings
    {
        public static string Key { get; set; } = "935172409:AAF9sZuWrEo_AgePRVD-WC-lzNYa8fxdAbw";
        public static HttpToSocks5Proxy Proxy { get; set; } = new HttpToSocks5Proxy("orbtl.s5.opennetwork.cc", 999, "409428213", "OqU8K9W4");
    }
}