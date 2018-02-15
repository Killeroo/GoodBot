using System;

using GoodBot.Core;
using GoodBot.Logger;

namespace GoodBot
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Args check
            if (args.Length < 1) {
                Log.Warning("Not enough arguments: GoodBot /listen [OR] /send");
                Environment.Exit(1);
            }

            if (args[0].ToLower() == "/listen") {
                Listen();
            } else if (args[0].ToLower() == "/send") {
                Send();
            } else {
                Log.Warning("Unrecognised arguments \"" + args[0] + "\" use: GoodBot /listen [OR] /send");
                Environment.Exit(1);
            }
        }

        /*
         * Send commands to GoodBot
         */
        public static void Send() 
        {
            Log.Info("Sending...");

        }

        /*
         * GoodBot listens for commands
         */
        private static void Listen() 
        {
            Console.WriteLine("Hi my name is GoodBot or you can call me by my machinee given name ********");
            Console.WriteLine();
            Console.WriteLine("I hope I do a good job! You just leave me too it and send me commands by typing ‘goodbot /send *IP_ADDR* *COMMAND*' and I’ll see what I can do :)");
            Console.WriteLine();
            Console.WriteLine("If your stuck, type 'goodbot /examples' for help");
            Console.WriteLine();
        }
    }
}
