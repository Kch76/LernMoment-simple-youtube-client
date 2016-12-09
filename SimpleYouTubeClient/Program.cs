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

            string apiKey = RequestApiKeyFromUser();
            proxy = new YouTubeServerProxy(apiKey);

            string channelId = RequestChannelIdFromUser();
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

        private static string RequestApiKeyFromUser()
        {
            string key = Properties.Settings.Default.ApiKey;

            Console.WriteLine("*** API Key Eingabe ***");
            Console.Write("Soll der gespeichert ApiKey: {0} verwendet werden? (ja/nein): ", key);
            string retrieveNewKey = Console.ReadLine();

            if (retrieveNewKey.ToLower().Equals("nein"))
            {
                Console.Write("Bitte gib einen neuen ApiKey ein: ");
                key = Console.ReadLine();

                // neuen Wert in den Settings speichern
                Properties.Settings.Default.ApiKey = key;
                Properties.Settings.Default.Save();
            }

            return key;
        }

        private static string RequestChannelIdFromUser()
        {
            string id = Properties.Settings.Default.ChannelId;

            Console.WriteLine("*** Channel Id Eingabe ***");
            Console.Write("Soll die gespeicherte Channel Id: {0} verwendet werden? (ja/nein): ", id);
            string retrieveNewId = Console.ReadLine();

            if (retrieveNewId.ToLower().Equals("nein"))
            {
                Console.Write("Bitte gib eine neue ChannelId ein: ");
                id = Console.ReadLine();

                // neuen Wert in den Settings speichern
                Properties.Settings.Default.ChannelId = id;
                Properties.Settings.Default.Save();
            }

            return id;
        }
    }
}
