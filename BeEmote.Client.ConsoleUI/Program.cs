using BeEmote.Core;
using BeEmote.Services;
using System;

namespace BeEmote.Client.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            AppManager app = new AppManager();
            app.Init();
            app.StartTextAnalytics();
            app.StartEmotion();

            Console.ReadLine();
        }
    }
}
