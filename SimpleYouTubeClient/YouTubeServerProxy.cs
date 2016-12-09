using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleYouTubeClient
{
    class YouTubeServerProxy
    {
        private YouTubeService ytService;

        public YouTubeServerProxy(string apiKey)
        {
            ytService = new YouTubeService(new BaseClientService.Initializer() { ApiKey = apiKey });
        }

        public Video[] GetAllUploadedVideos(string channelId)
        {
            // Get ids of all uploaded videos from the channel
            string[] videoIds = GetIdsOfAllUploadedVideos(channelId);

            // Get core information of each video specified by id
            Video[] allUploadedVideos = GetVideoDataForAllIds(videoIds);

            return allUploadedVideos;
        }

        private Video[] GetVideoDataForAllIds(string[] videoIds)
        {
            Console.WriteLine("GetVideoDataForAllIds to be implemented!");

            return null;
        }

        private string[] GetIdsOfAllUploadedVideos(string channelId)
        {
            Console.WriteLine("GetIdsOfAllUploadedVideos to be implemented!");

            return null;
        }
    }
}
