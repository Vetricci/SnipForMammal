using System;
using System.Net;

namespace SnipForMammal
{
    class WebClientWithShortTimeout : WebClient
    {
        // How many seconds before webclient times out and moves on.
        private const int WebClientTimeoutSeconds = 10;

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest webRequest = base.GetWebRequest(address);
            webRequest.Timeout = WebClientTimeoutSeconds * 60 * 1000;
            return webRequest;
        }
    }
}
