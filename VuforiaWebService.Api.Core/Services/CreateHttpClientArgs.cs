using System.Collections.Generic;
using System.Net;

namespace VuforiaWebService.Api.Core.Services
{
    /// <summary>Arguments for creating a HTTP client.</summary>
    public class CreateHttpClientArgs
    {
        /// <summary>Gets or sets the application name that is sent in the User-Agent header.</summary>
        public string ApplicationName { get; set; }

        /// <summary>Gets a list of initializers to initialize the HTTP client instance.</summary>
        public IList<IConfigurableHttpClientInitializer> Initializers { get; private set; }

        /// <summary>Gets or sets the network credential.</summary>
        public NetworkCredential NetworkCredential { get; set; }

        /// <summary>Constructs a new argument instance.</summary>
        public CreateHttpClientArgs()
        {
            this.Initializers = new List<IConfigurableHttpClientInitializer>();
        }
    }
}
