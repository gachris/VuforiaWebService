using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VuforiaWebService.Api.Core.Extensions;
using VuforiaWebService.Api.Core.Logger;
using VuforiaWebService.Api.Core.Utils;

namespace VuforiaWebService.Api.Core;

public class ConfigurableMessageHandler : DelegatingHandler
{
    /// <summary>The class logger.</summary>
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ConfigurableMessageHandler>();
    /// <summary>The current API version of this client library.</summary>
    private static readonly string ApiVersion = Utilities.GetLibraryVersion();
    /// <summary>The User-Agent suffix header which contains the <see cref="F:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.ApiVersion" />.</summary>
    private static readonly string UserAgentSuffix = "VuforiaPortal-api-dotnet-client/" + ApiVersion;
    private readonly object unsuccessfulResponseHandlersLock = new object();
    private readonly object exceptionHandlersLock = new object();
    private readonly object executeInterceptorsLock = new object();
    /// <summary>A list of <see cref="T:VuforiaPortal.Apis.Http.IHttpUnsuccessfulResponseHandler" />.</summary>
    private readonly IList<IHttpUnsuccessfulResponseHandler> unsuccessfulResponseHandlers = new List<IHttpUnsuccessfulResponseHandler>();
    /// <summary>A list of <see cref="T:VuforiaPortal.Apis.Http.IHttpExceptionHandler" />.</summary>
    private readonly IList<IHttpExceptionHandler> exceptionHandlers = new List<IHttpExceptionHandler>();
    /// <summary>A list of <see cref="T:VuforiaPortal.Apis.Http.IHttpExecuteInterceptor" />.</summary>
    private readonly IList<IHttpExecuteInterceptor> executeInterceptors = new List<IHttpExecuteInterceptor>();
    /// <summary>Number of tries. Default is <c>3</c>.</summary>
    private int numTries = 3;
    /// <summary>Number of redirects allowed. Default is <c>10</c>.</summary>
    private int numRedirects = 10;
    /// <summary>Maximum allowed number of tries.</summary>
    public const int MaxAllowedNumTries = 20;

    /// <summary>Adds the specified handler to the list of unsuccessful response handlers.</summary>
    public void AddUnsuccessfulResponseHandler(IHttpUnsuccessfulResponseHandler handler)
    {
        lock (unsuccessfulResponseHandlersLock)
            unsuccessfulResponseHandlers.Add(handler);
    }

    /// <summary>Removes the specified handler from the list of unsuccessful response handlers.</summary>
    public void RemoveUnsuccessfulResponseHandler(IHttpUnsuccessfulResponseHandler handler)
    {
        lock (unsuccessfulResponseHandlersLock)
            unsuccessfulResponseHandlers.Remove(handler);
    }

    /// <summary>
    /// Gets a list of <see cref="T:VuforiaPortal.Apis.Http.IHttpExceptionHandler" />s.
    /// <remarks>
    /// Since version 1.10, <see cref="M:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.AddExceptionHandler(VuforiaPortal.Apis.Http.IHttpExceptionHandler)" /> and <see cref="M:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.RemoveExceptionHandler(VuforiaPortal.Apis.Http.IHttpExceptionHandler)" /> were added
    /// in order to keep this class thread-safe.  More information is available on
    /// <a href="https://github.com/VuforiaPortal/VuforiaPortal-api-dotnet-client/issues/592">#592</a>.
    /// </remarks>
    /// </summary>
    [Obsolete("Use AddExceptionHandler or RemoveExceptionHandler instead.")]
    public IList<IHttpExceptionHandler> ExceptionHandlers => exceptionHandlers;

    /// <summary>Adds the specified handler to the list of exception handlers.</summary>
    public void AddExceptionHandler(IHttpExceptionHandler handler)
    {
        lock (exceptionHandlersLock)
            exceptionHandlers.Add(handler);
    }

    /// <summary>Removes the specified handler from the list of exception handlers.</summary>
    public void RemoveExceptionHandler(IHttpExceptionHandler handler)
    {
        lock (exceptionHandlersLock)
            exceptionHandlers.Remove(handler);
    }

    /// <summary>
    /// Gets a list of <see cref="T:VuforiaPortal.Apis.Http.IHttpExecuteInterceptor" />s.
    /// <remarks>
    /// Since version 1.10, <see cref="M:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.AddExecuteInterceptor(VuforiaPortal.Apis.Http.IHttpExecuteInterceptor)" /> and <see cref="M:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.RemoveExecuteInterceptor(VuforiaPortal.Apis.Http.IHttpExecuteInterceptor)" /> were
    /// added in order to keep this class thread-safe.  More information is available on
    /// <a href="https://github.com/VuforiaPortal/VuforiaPortal-api-dotnet-client/issues/592">#592</a>.
    /// </remarks>
    /// </summary>
    [Obsolete("Use AddExecuteInterceptor or RemoveExecuteInterceptor instead.")]
    public IList<IHttpExecuteInterceptor> ExecuteInterceptors => executeInterceptors;

    /// <summary>Adds the specified interceptor to the list of execute interceptors.</summary>
    public void AddExecuteInterceptor(IHttpExecuteInterceptor interceptor)
    {
        lock (executeInterceptorsLock)
            executeInterceptors.Add(interceptor);
    }

    /// <summary>Removes the specified interceptor from the list of execute interceptors.</summary>
    public void RemoveExecuteInterceptor(IHttpExecuteInterceptor interceptor)
    {
        lock (executeInterceptorsLock)
            executeInterceptors.Remove(interceptor);
    }

    /// <summary>
    /// Gets or sets the number of tries that will be allowed to execute. Retries occur as a result of either
    /// <see cref="T:VuforiaPortal.Apis.Http.IHttpUnsuccessfulResponseHandler" /> or <see cref="T:VuforiaPortal.Apis.Http.IHttpExceptionHandler" /> which handles the
    /// abnormal HTTP response or exception before being terminated.
    /// Set <c>1</c> for not retrying requests. The default value is <c>3</c>.
    /// <remarks>
    /// The number of allowed redirects (3xx) is defined by <see cref="P:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.NumRedirects" />. This property defines
    /// only the allowed tries for &gt;=400 responses, or when an exception is thrown. For example if you set
    /// <see cref="P:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.NumTries" /> to 1 and <see cref="P:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.NumRedirects" /> to 5, the library will send up to five redirect
    /// requests, but will not send any retry requests due to an error HTTP status code.
    /// </remarks>
    /// </summary>
    public int NumTries
    {
        get => numTries;
        set
        {
            if (value > 20 || value < 1)
                throw new ArgumentOutOfRangeException(nameof(NumTries));
            numTries = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of redirects that will be allowed to execute. The default value is <c>10</c>.
    /// See <see cref="P:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.NumTries" /> for more information.
    /// </summary>
    public int NumRedirects
    {
        get => numRedirects;
        set
        {
            if (value > 20 || value < 1)
                throw new ArgumentOutOfRangeException(nameof(NumRedirects));
            numRedirects = value;
        }
    }

    /// <summary>
    /// Gets or sets whether the handler should follow a redirect when a redirect response is received. Default
    /// value is <c>true</c>.
    /// </summary>
    public bool FollowRedirect { get; set; }

    /// <summary>Gets or sets whether logging is enabled. Default value is <c>true</c>.</summary>
    public bool IsLoggingEnabled { get; set; }

    /// <summary>Gets or sets the application name which will be used on the User-Agent header.</summary>
    public string ApplicationName { get; set; }

    /// <summary>Constructs a new configurable message handler.</summary>
    public ConfigurableMessageHandler(HttpMessageHandler httpMessageHandler)
      : base(httpMessageHandler)
    {
        FollowRedirect = true;
        IsLoggingEnabled = true;
    }

    /// <summary>
    /// The main logic of sending a request to the server. This send method adds the User-Agent header to a request
    /// with <see cref="P:VuforiaPortal.Apis.Http.ConfigurableMessageHandler.ApplicationName" /> and the library version. It also calls interceptors before each attempt,
    /// and unsuccessful response handler or exception handlers when abnormal response or exception occurred.
    /// </summary>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Exception lastException;
        HttpResponseMessage response = null;
        string dateFormat = "ddd, dd MMM yyy HH:mm:ss";
        bool loggable = IsLoggingEnabled && Logger.IsDebugEnabled;
        int triesRemaining = NumTries;
        int redirectRemaining = NumRedirects;
        request.Headers.Add("User-Agent", (ApplicationName == null ? "" : ApplicationName + " ") + UserAgentSuffix);
        request.Headers.TryAddWithoutValidation("Date", SystemClock.Default.UtcNow.ToString(dateFormat, CultureInfo.CreateSpecificCulture("en-US")) + " GMT");
        do
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (response != null)
            {
                response.Dispose();
                response = null;
            }
            lastException = null;
            IEnumerable<IHttpExecuteInterceptor> httpExecuteInterceptors;
            lock (executeInterceptorsLock)
                httpExecuteInterceptors = executeInterceptors.ToList();
            foreach (IHttpExecuteInterceptor executeInterceptor in httpExecuteInterceptors)
            {
                await executeInterceptor.InterceptAsync(request, cancellationToken).ConfigureAwait(false);
            }
            try
            {
                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                lastException = ex;
            }
            if (response == null || response.StatusCode >= HttpStatusCode.BadRequest || response.StatusCode < HttpStatusCode.OK)
                --triesRemaining;
            bool flag;
            if (response == null)
            {
                bool flag1 = false;
                IEnumerable<IHttpExceptionHandler> list2;
                lock (exceptionHandlersLock)
                    list2 = exceptionHandlers.ToList();
                foreach (IHttpExceptionHandler exceptionHandler in list2)
                {
                    flag = flag1;
                    int num = await exceptionHandler.HandleExceptionAsync(new HandleExceptionArgs()
                    {
                        Request = request,
                        Exception = lastException,
                        TotalTries = NumTries,
                        CurrentFailedTry = NumTries - triesRemaining,
                        CancellationToken = cancellationToken
                    }).ConfigureAwait(false) ? 1 : 0;
                    flag1 = flag | num != 0;
                }
                if (!flag1)
                {
                    Logger.Error(lastException, "Exception was thrown while executing a HTTP request and it wasn't handled");
                    throw lastException;
                }
                if (loggable)
                    Logger.Debug("Exception {0} was thrown, but it was handled by an exception handler", lastException.Message);
            }
            else if (response.IsSuccessStatusCode)
            {
                triesRemaining = 0;
            }
            else
            {
                bool flag1 = false;
                IEnumerable<IHttpUnsuccessfulResponseHandler> list2;
                lock (unsuccessfulResponseHandlersLock)
                    list2 = unsuccessfulResponseHandlers.ToList();
                foreach (IHttpUnsuccessfulResponseHandler unsuccessfulResponseHandler in list2)
                {
                    flag = flag1;
                    int num = await unsuccessfulResponseHandler.HandleResponseAsync(new HandleUnsuccessfulResponseArgs()
                    {
                        Request = request,
                        Response = response,
                        TotalTries = NumTries,
                        CurrentFailedTry = NumTries - triesRemaining,
                        CancellationToken = cancellationToken
                    }).ConfigureAwait(false) ? 1 : 0;
                    flag1 = flag | num != 0;
                }
                if (!flag1)
                {
                    if (FollowRedirect && HandleRedirect(response))
                    {
                        if (redirectRemaining-- == 0)
                            triesRemaining = 0;
                        if (loggable)
                            Logger.Debug("Redirect response was handled successfully. Redirect to {0}", response.Headers.Location);
                    }
                    else
                    {
                        if (loggable)
                            Logger.Debug("An abnormal response wasn't handled. Status code is {0}", response.StatusCode);
                        triesRemaining = 0;
                    }
                }
                else if (loggable)
                    Logger.Debug("An abnormal response was handled by an unsuccessful response handler. Status Code is {0}", response.StatusCode);
            }
        }
        while (triesRemaining > 0);
        if (response == null)
        {
            Logger.Error(lastException, "Exception was thrown while executing a HTTP request");
            throw lastException;
        }
        if (!response.IsSuccessStatusCode)
            Logger.Debug("Abnormal response is being returned. Status Code is {0}", response.StatusCode);
        return response;
    }

    /// <summary>
    /// Handles redirect if the response's status code is redirect, redirects are turned on, and the header has
    /// a location.
    /// When the status code is <c>303</c> the method on the request is changed to a GET as per the RFC2616
    /// specification. On a redirect, it also removes the <c>Authorization</c> and all <c>If-*</c> request headers.
    /// </summary>
    /// <returns> Whether this method changed the request and handled redirect successfully. </returns>
    private bool HandleRedirect(HttpResponseMessage message)
    {
        Uri location = message.Headers.Location;
        if (!message.IsRedirectStatusCode() || location == null)
            return false;
        HttpRequestMessage requestMessage = message.RequestMessage;
        requestMessage.RequestUri = new Uri(requestMessage.RequestUri, location);
        if (message.StatusCode == HttpStatusCode.RedirectMethod)
            requestMessage.Method = HttpMethod.Get;
        requestMessage.Headers.Remove("Authorization");
        requestMessage.Headers.IfMatch.Clear();
        requestMessage.Headers.IfNoneMatch.Clear();
        requestMessage.Headers.IfModifiedSince = new DateTimeOffset?();
        requestMessage.Headers.IfUnmodifiedSince = new DateTimeOffset?();
        requestMessage.Headers.Remove("If-Range");
        return true;
    }
}
