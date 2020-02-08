using System;
using HtmlAgilityPack;
using ITMOSchedule.Bot.Interfaces;

namespace ITMOSchedule.Bot
{
    public class VkPrinter : IPrinter<string>
    {
        public void PrintData(string data)
        {
            throw new NotImplementedException();
        }
    }

    public class ConsolePrinter : IPrinter<string>
    {
        public void PrintData(string data)
        {
            Console.WriteLine(data);
        }
    }
}