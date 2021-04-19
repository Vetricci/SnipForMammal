namespace SnipForMammal
{
    public class SpotifyTrack
    {
        public string artists;
        public string id;
        public string name;
        public string uri;
        public string fullTitle;

        public SpotifyTrack(dynamic jsonObject)
        {
            this.id = jsonObject.item.id;
            this.artists = GenerateArtistsFromJson(jsonObject.item.artists);
            this.name = jsonObject.item.name;
            this.uri = jsonObject.item.uri;
            this.fullTitle = Global.outputFormat.Replace("$song", this.name).Replace("$artist", this.artists);
        }

        public SpotifyTrack(string trackId, string trackArtists, string trackName, string trackUri)
        {
            this.id = trackId;
            this.artists = trackArtists;
            this.name = trackName;
            this.uri = trackUri;
            this.fullTitle = Global.outputFormat.Replace("$song", this.name).Replace("$artist", this.artists);
        }

        public SpotifyTrack(string customText)
        {
            // Used by Custom Text option
            this.id = string.Empty;
            this.artists = customText;
            this.name = string.Empty;
            this.uri = string.Empty;
            this.fullTitle = customText;
        }


        private string GenerateArtistsFromJson(dynamic input)
        {
            string artists = string.Empty;

            // Loop through artists and join strings together.
            foreach (dynamic artist in input)
            {
                artists += artist.name.ToString() + ", ";
            }

            // Strip extra ',' at end.
            artists = artists.Substring(0, artists.LastIndexOf(','));

            return artists;
        }

        public override string ToString()
        {
            return this.fullTitle;
        }
    }
}
