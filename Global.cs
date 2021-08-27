namespace SnipForMammal
{
    public static class Global
    {
        public static SnipForMammal snipForMammal
        {
            get;
            set;
        }

        public static Log log
        {
            get;
            set;
        }

        public static Settings settings
        {
            get;
            set;
        }

        public static CustomTextEntryForm customTextEntryForm
        {
            get;
            set;
        }

        public static Spotify spotify
        {
            get;
            set;
        }

        public static int updateAuthTokenInterval
        {
            get;
            set;
        }

        public static int updateSpotifyTrackInfoInterval
        {
            get;
            set;
        }

        public static int updateCurrentTrackPlayingInterval
        {
            get;
            set;
        }

        public static bool autoCloseAuthWindow
        {
            get;
            set;
        }

        public static string outputFormat
        {
            get;
            set;
        }

        public static string browser
        {
            get;
            set;
        }
    
        public static bool IsTextOverriden
        {
            get;
            set;
        }

        public static SpotifyTrack currentTrack
        {
            get;
            set;
        }

        public static SpotifyTrack lastTrack
        {
            get;
            set;
        }
    }
}
