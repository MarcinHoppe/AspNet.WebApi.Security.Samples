using System;
using Microsoft.Owin.Hosting;

namespace OwinMiddleware
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            using (WebApp.Start<Startup>("http://localhost:9000"))
            {
                Console.WriteLine("Started. Press ENTER to exit.");
                Console.ReadLine();
            }
        }
    }
}