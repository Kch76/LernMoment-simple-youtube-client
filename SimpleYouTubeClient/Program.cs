using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleYouTubeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            YouTubeServerProxy proxy;
            Video[] uploadedVideos;

            Console.WriteLine("### Willkommen zum SYT-Client! ###");
            Console.WriteLine();

            Console.Write("Bitte gebe deinen API-Key ein: ");
            string apiKey = Console.ReadLine();
            proxy = new YouTubeServerProxy(apiKey);

            Console.Write("Bitte gebe eine Channel-Id ein: ");
            string channelId = Console.ReadLine();
            uploadedVideos = proxy.GetAllUploadedVideos(channelId);
            Console.ReadLine();
        }
    }
}
