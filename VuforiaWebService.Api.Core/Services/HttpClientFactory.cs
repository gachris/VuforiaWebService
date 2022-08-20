using System.Net.Http;

namespace VuforiaWebService.Api.Core.Services;

/// <summary>The default implementation of the HTTP client factory.</summary>
public class HttpClientFactory : IHttpClientFactory
{
    /// <summary>The class logger.</summary>
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<HttpClientFactory>();

    public ConfigurableHttpClient CreateHttpClient(CreateHttpClientArgs args)
    {
        ConfigurableHttpClient httpClient = new ConfigurableHttpClient(new ConfigurableMessageHandler(CreateHandler(args))
        {
            ApplicationName = args.ApplicationName
        });
        foreach (IConfigurableHttpClientInitializer initializer in args.Initializers)
            initializer.Initialize(httpClient);
        return httpClient;
    }

    /// <summary>Creates a HTTP message handler. Override this method to mock a message handler.</summary>
    protected virtual HttpMessageHandler CreateHandler(CreateHttpClientArgs args)
    {
        HttpClientHandler httpClientHandler = new HttpClientHandler();

        httpClientHandler.UseDefaultCredentials = true;
        httpClientHandler.PreAuthenticate = true;
        httpClientHandler.Credentials = args.NetworkCredential;

        if (httpClientHandler.SupportsRedirectConfiguration)
            httpClientHandler.AllowAutoRedirect = false;
        HttpClientFactory.Logger.Debug("Handler was created. SupportsRedirectConfiguration={0}, SupportsAutomaticDecompression={1}", httpClientHandler.SupportsRedirectConfiguration, httpClientHandler.SupportsAutomaticDecompression);
        return httpClientHandler;
    }
}
