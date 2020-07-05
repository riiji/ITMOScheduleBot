using System;

namespace MessengerBotFramework.Common
{
    public static class Utilities
    {
        private static readonly Random Random = new Random();

        public static int GetRandom()
        {
            return Random.Next();
        }
    }
}