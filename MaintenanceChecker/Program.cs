using System;
using System.Net.Http;
using System.Timers;
using System.Media;

namespace MaintenanceChecker
{
    class Program
    {
        private static Timer timer;
        public static string server;
        public static string template;
        
        static void Main(string[] args)
        {
            timer = new Timer(5 * 1000);
            timer.Elapsed += FiveSecondsElapsed;
            timer.Start();
            
            Console.Write("Enter the server name: ");
            server = Console.ReadLine() ?? string.Empty;

            template =
                " <div class=\"ags-ServerStatus-content-responses-response-server\">\n                                    " +
                "<div class=\"ags-ServerStatus-content-responses-response-server-status-wrapper\">\n                                        " +
                "<div class=\"ags-ServerStatus-content-responses-response-server-status ags-ServerStatus-content-responses-response-server-status--maintenance\">\n   " +
                " " +
                "<svg class=\"\" focusable=\"false\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 221.4 222.3\" xml:space=\"preserve\" fill=\"currentColor\">\n        " +
                "<g>\n            " +
                "<path d=\"M205.3,152.5c-11.8-11.8-22.4-22.4-33.6-33.6c-4.7-4.7-9.5-9.5-14.5-14.5l-0.2-0.2c-0.4-0.4-0.8-0.8-1.3-1.2\n                " +
                "c-3.4-2.6-8-2.4-11.1,0.6c-3.1,3-3.4,7.7-0.9,11c0.6,0.8,1.3,1.5,1.9,2.1l10.7,10.7c12.3,12.3,23.8,23.9,36.8,36.8\n                " +
                "c4.1,4.1,6.2,8.8,6.5,14.3c0.3,5.8-2,11.6-6.3,15.7c-4.2,4.1-10,6.2-15.8,5.7c-5.1-0.4-9.6-2.7-14-7.1\n                " +
                "c-11.2-11.2-22.7-22.5-33.8-33.5c-7-7-14.1-14-21.1-20.9c-1.6-1.6-3.2-3.2-4.9-4.9c6.5-3.4,12-7.4,16.6-12.3\n                " +
                "c9.7-10.1,16-21.1,18.7-32.7c2.8-11.9,2-24.5-2.6-37.5c-4.1-11.9-10.7-21.8-19.4-29.4C108.6,14.2,97.9,8.9,85.4,6\n                " +
                "c-1.7-0.4-3.3-0.7-4.9-0.9c-0.7-0.1-1.4-0.2-2.1-0.4l-0.9-0.1c-3.2-0.3-6.2-0.5-10.6,0.1l-0.1,0C65.9,4.9,64.9,5,64,5.2\n                " +
                "c-2.1,0.3-4.3,0.7-6.5,1.3c-4.2,1.1-8.3,2.4-12.2,3.8c-3,1.1-5,3.4-5.7,6.2c-0.7,2.9,0.3,6,2.6,8.3l4.4,4.4\n                " +
                "c10.2,10.3,20.7,20.9,31,31.5c4.8,4.9,5.3,11.5,1.3,16.4c-2.1,2.6-5.4,4.2-8.9,4.4c-3.7,0.2-7.3-1.2-9.9-3.8\n               " +
                " C49.2,66.7,38.4,55.7,26.4,43.5c-3.9-4-7.5-4.3-9.7-3.8c-2.2,0.5-5.2,2.1-7.2,7.1c-0.2,0.6-0.5,1.2-0.7,1.8\n                " +
                "c-1.5,3.9-3.1,8-3.6,12.4c0,0.3-0.1,0.6-0.1,1.1C4.7,65.4,3.6,73,5.3,83.8c0.7,4.7,2,9.4,3.9,14.1C15.4,113,26,125.1,40,132.7\n                " +
                "c13.9,7.6,29.7,10,45.7,7.1c0.1,0,0.1,0,0.1,0c0,0,0.1,0.1,0.1,0.1c16.7,16.6,33.7,33.5,50.2,49.9l15.5,15.4\n                " +
                "c3.1,3.1,7,6.5,11.9,8.5c3.6,1.6,7.9,3.3,12.9,3.6c0.8,0.1,1.7,0.1,2.5,0.1c9,0,16.8-4.3,18.4-5.2c10.4-6.1,17.3-16.2,18.8-27.8\n                " +
                "C217.9,172.7,213.8,161.1,205.3,152.5z M95.3,83.8c6.8-11.7,4.5-26.4-5.8-36.4c-4.3-4.2-8.6-8.5-13-12.7c-4.1-4-8.2-8-12.3-12\n                " +
                "c15.8-2.6,35.5,1.5,49.3,20.4c6.7,9.1,10.2,20.1,9.9,31c-0.3,11.5-4.8,22.5-12.9,31.8c-7.9,9-17.8,15-28.6,17.2\n                " +
                "c-11.3,2.3-23,0.4-33.9-5.5c-10.1-5.5-17.8-13.8-22.3-24c-3.9-9-5.1-18.9-3.6-29.1c1.5,1.4,2.9,2.8,4.3,4.2\n                " +
                "c2.4,2.4,4.8,4.7,7.2,7.1c4.8,4.7,9.7,9.6,14.6,14.3c6.9,6.8,16.2,9.9,25.2,8.7C82.6,97.5,90.5,92.1,95.3,83.8z\"/>\n            " +
                "<path d=\"M189.7,179.5c0-5.9-4.6-10.5-10.5-10.6c-5.8-0.1-10.6,4.6-10.6,10.5c0,5.9,4.6,10.6,10.5,10.6\n                " +
                "C184.9,190,189.7,185.2,189.7,179.5z\"/>\n        " +
                "</g>\n    " +
                "</svg>\n                                        " +
                "</div>\n                                    " +
                "</div>\n                                    " +
                "<div class=\"ags-ServerStatus-content-responses-response-server-name\">\n                                        " +
                server + "\n                                    " +
                "</div>\n                                " +
                "</div>";

                CheckPage(template);

                Console.ReadKey();
        }


        static void FiveSecondsElapsed(object sender, ElapsedEventArgs e)
        {
            CheckPage(template);
        }
        
        static void CheckPage(string temp)
        {
            using (HttpClient web = new HttpClient())
            {
                using (HttpResponseMessage response = web.GetAsync("https://www.playlostark.com/en-gb/support/server-status").Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        string source = content.ReadAsStringAsync().Result;

                        if (!source.Contains(server))
                        {
                            Console.WriteLine("Server list not available");
                        }
                        else
                        {
                            if (source.Contains(temp))
                            {
                                Console.Clear();
                                Console.WriteLine("Maintenance");
                            }
                            else
                            {
                                Console.Clear();
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine("Up!!1!11 POGGERS");
                                timer.Stop();
                            }
                        }
                    }
                }
            }

        }
    }
}
