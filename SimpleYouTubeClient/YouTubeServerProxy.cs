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

        public List<Video> GetAllUploadedVideos(string channelId)
        {
            // Get ids of all uploaded videos from the channel
            List<string> videoIds = GetIdsOfAllUploadedVideos(channelId);

            // Get core information of each video specified by id
            List<Video> allUploadedVideos = GetVideoDataForAllIds(videoIds);

            return allUploadedVideos;
        }

        private List<Video> GetVideoDataForAllIds(IList<string> videoIds)
        {
            // Die API erlaubt im VideoListRequest die gewünschten VideoIds in einem string
            // anzugeben. Dabei müssen die einzelnen Ids durch Komma getrennt sein.
            // ACHTUNG: Es können nicht mehr als 50 Ids pro Execute verwendet werden!!!
            List<Video> result = new List<Video>();
            int remainingVideosToLoad = videoIds.Count;

            while (remainingVideosToLoad > 0)
            {
                int amountOfVideosForThisIteration = 0;
                if (remainingVideosToLoad > 50)
                {
                    amountOfVideosForThisIteration = 50;
                    remainingVideosToLoad -= 50;
                }
                else
                {
                    amountOfVideosForThisIteration = remainingVideosToLoad;
                    remainingVideosToLoad = 0;
                }

                // ACHTUNG: Join mit startIndex und Count geht nur auf einem Array und
                // nicht auf einer IList, obwohl es keine Fehler vom Compiler gibt!!!
                string allVideoIds = String.Join(",", videoIds.ToArray(), remainingVideosToLoad, amountOfVideosForThisIteration);

                // Anfrage erstellen um die Video-Daten zu erhalten
                var request = ytService.Videos.List("snippet,status");
                request.Id = allVideoIds;

                // Anfrage zum Server schicken
                var response = request.Execute();

                // Videos aus der Response holen und dem Resultat hinzufügen
                result.AddRange(response.Items);
            }

            return result;
        }

        private List<string> GetIdsOfAllUploadedVideos(string channelId)
        {
            List<string> result = new List<string>();

            // Alle Videos eines Kanals befinden sich in der UploadsPlaylist. Davon benötigen
            // wir erstmal die entsprechende Playlist-Id.
            string uploadPlaylistId = GetUploadsPlaylistId(channelId);

            // Eine Playlist besteht aus Playlist-Items. Jedes PlaylistItem stellt einen Eintrag
            // in der Playliste dar und enthält die Video-Id des referenzierten Videos.
            List<PlaylistItem> items = GetAllItems(uploadPlaylistId);

            foreach (var playlistItem in items)
            {
                result.Add(playlistItem.Snippet.ResourceId.VideoId);
            }

            return result;
        }

        private List<PlaylistItem> GetAllItems(string uploadPlaylistId)
        {
            List<PlaylistItem> result = new List<PlaylistItem>();
             
            var nextPageToken = "";
            while (nextPageToken != null)
            {
                // wir stellen eine Anfrage für die ersten/nächsten 50 Items in der Playlist
                var playlistItemsListRequest = ytService.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = uploadPlaylistId;
                playlistItemsListRequest.MaxResults = 50;
                playlistItemsListRequest.PageToken = nextPageToken;

                // Anfrage ausführen
                var playlistItemsListResponse = playlistItemsListRequest.Execute();

                // Die erhaltenen Items fügen wir der Liste hinzu.
                result.AddRange(playlistItemsListResponse.Items);

                // Nun können bei Bedarf die nächsten Items abgefragt werden
                nextPageToken = playlistItemsListResponse.NextPageToken;
            }

            return result;
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
