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
            List<Video> uploadedVideos;

            Console.WriteLine("### Willkommen zum SYT-Client! ###");
            Console.WriteLine();

            Console.Write("Bitte gebe deinen API-Key ein: ");
            string apiKey = Console.ReadLine();
            proxy = new YouTubeServerProxy(apiKey);

            Console.Write("Bitte gebe eine Channel-Id ein: ");
            string channelId = Console.ReadLine();
            uploadedVideos = proxy.GetAllUploadedVideos(channelId);

            Console.WriteLine();
            Console.WriteLine("*** Hier die gefundenen Videos: ***");
            for (int i = 0; i < uploadedVideos.Count; i++)
            {
                Video current = uploadedVideos[i];
                Console.WriteLine("{0}: Id: {1}, Name: {2}", i, current.Id, current.Snippet.Title);
            }


            Console.ReadLine();
        }
    }
}
