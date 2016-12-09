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
            string uploadPlaylistId = GetUploadsPlaylistId(channelId);

            Console.WriteLine("Die Upload-Playlist des Kanals {0} hat die Id {1}", channelId, uploadPlaylistId);

            return null;
        }

        private string GetUploadsPlaylistId(string channelId)
        {
            // Details des Channels abrufen
            var channelsListRequest = ytService.Channels.List("contentDetails");
            channelsListRequest.Id = channelId;
            var channelsListResponse = channelsListRequest.Execute();

            // Die Antwort könnte theoretisch mehrere Kanäle enthalten, aber wir haben
            // nach einer konkreten channelId gefragt. Also sollte auch nur ein channel
            // in der Antwort enthalten sein!
            var channel = channelsListResponse.Items[0];
            if (channel == null)
            {
                throw new ApplicationException("Es wurde kein Kanal mit der ID: '" + channelId + "' gefunden!");
            }
            string uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;

            return uploadsListId;
        }
    }
}
