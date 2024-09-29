using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Core.Types;
using VuforiaWebService.Api.Core.Util;
using VuforiaWebService.Api.Target.Requests;
using VuforiaWebService.Api.Target.Types;

namespace VuforiaWebService.Api.Target.Resources;

/// <summary>
/// Represents a resource on Vuforia web service for accessing targets.
/// </summary>
public class TargetListResource
{
    private readonly IClientService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="TargetListResource"/> class.
    /// </summary>
    /// <param name="service">The client service used for making requests.</param>
    public TargetListResource(IClientService service) => _service = service;

    /// <summary>
    /// Creates a new <see cref="ListRequest"/> instance for retrieving the list of targets.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <returns>A new <see cref="ListRequest"/> instance for retrieving the list of targets.</returns>
    public virtual ListRequest List(DatabaseAccessKeys keys) => new ListRequest(_service, keys);

    /// <summary>
    /// Creates a new <see cref="GetRequest"/> instance for retrieving a specific target by ID.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <param name="targetId">The ID of the target to get. To retrieve target IDs, call the <see cref="List"/> method.</param>
    /// <returns>A new <see cref="GetRequest"/> instance for retrieving a specific target by ID.</returns>
    public virtual GetRequest Get(DatabaseAccessKeys keys, string targetId) => new GetRequest(_service, keys, targetId);

    /// <summary>
    /// Creates a new <see cref="InsertRequest"/> instance that can insert a new target.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <param name="body">The target to insert.</param>
    /// <returns>A new <see cref="InsertRequest"/> instance that can insert a new target.</returns>
    public virtual InsertRequest Insert(DatabaseAccessKeys keys, PostTrackableRequest body) => new InsertRequest(_service, keys, body);

    /// <summary>
    /// Creates a new <see cref="UpdateRequest"/> instance that can update an existing target.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <param name="body">The body of the request containing the updated target information.</param>
    /// <param name="targetId">The ID of the target to update. To retrieve target IDs, call the <see cref="List"/> method.</param>
    /// <returns>A new <see cref="UpdateRequest"/> instance that can update an existing target.</returns>
    public virtual UpdateRequest Update(DatabaseAccessKeys keys, PostTrackableRequest body, string targetId) => new UpdateRequest(_service, keys, body, targetId);

    /// <summary>
    /// Creates a new <see cref="DeleteRequest"/> instance that can delete an existing target by ID.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <param name="targetId">The ID of the target to delete. To retrieve target IDs, call the <see cref="List"/> method.</param>
    /// <returns>A new <see cref="DeleteRequest"/> instance that can delete an existing target by ID.</returns>
    public virtual DeleteRequest Delete(DatabaseAccessKeys keys, string targetId) => new DeleteRequest(_service, keys, targetId);

    /// <summary>
    /// Creates a new <see cref="CheckSimilarRequest"/> instance that can check for similar targets.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <param name="targetId">The ID of the target to check for similarity. To retrieve target IDs, call the <see cref="List"/> method.</param>
    /// <returns>A new <see cref="CheckSimilarRequest"/> instance for checking similar targets.</returns>
    public virtual CheckSimilarRequest CheckSimilar(DatabaseAccessKeys keys, string targetId) => new CheckSimilarRequest(_service, keys, targetId);

    /// <summary>
    /// Creates a new <see cref="RetrieveTargetSummaryReportRequest"/> instance to retrieve a target's summary report.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <param name="targetId">The ID of the target to retrieve the summary report for. To retrieve target IDs, call the <see cref="List"/> method.</param>
    /// <returns>A new <see cref="RetrieveTargetSummaryReportRequest"/> instance to retrieve a target's summary report.</returns>
    public virtual RetrieveTargetSummaryReportRequest RetrieveTargetSummaryReport(DatabaseAccessKeys keys, string targetId) => new RetrieveTargetSummaryReportRequest(_service, keys, targetId);

    /// <summary>
    /// Creates a new <see cref="GetDatabaseSummaryReportRequest"/> instance to retrieve the database summary report.
    /// </summary>
    /// <param name="keys">The database access keys required for authorization.</param>
    /// <returns>A new <see cref="GetDatabaseSummaryReportRequest"/> instance for retrieving the database summary report.</returns>
    public virtual GetDatabaseSummaryReportRequest GetDatabaseSummaryReport(DatabaseAccessKeys keys) => new GetDatabaseSummaryReportRequest(_service, keys);

    /// <summary>
    /// Represents a request to get the database summary report from the Vuforia web service.
    /// </summary>
    public class GetDatabaseSummaryReportRequest : TargetBaseServiceRequest<VuforiaGetDatabaseSummaryReportResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="GetDatabaseSummaryReportRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        public GetDatabaseSummaryReportRequest(IClientService service, DatabaseAccessKeys keys) : base(service, keys)
        {
        }

        /// <inheritdoc/>
        public override string MethodName => "summary";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.GET;

        /// <inheritdoc/>
        public override string RestPath => "/summary";
    }

    /// <summary>
    /// Represents a request to retrieve a target's summary report from the Vuforia web service.
    /// </summary>
    public class RetrieveTargetSummaryReportRequest : TargetBaseServiceRequest<VuforiaRetrieveTargetSummaryReportResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="RetrieveTargetSummaryReportRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        /// <param name="targetId">The ID of the target to retrieve the summary report for. To retrieve target IDs, call the <see cref="List"/> method.</param>
        public RetrieveTargetSummaryReportRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
        }

        /// <summary>
        /// Gets the target ID.
        /// </summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; }

        /// <inheritdoc/>
        public override string MethodName => "summary";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.GET;

        /// <inheritdoc/>
        public override string RestPath => "/summary/{targetId}";

        /// <inheritdoc/>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add("targetId", new Parameter("targetId", "path", true));
        }
    }

    /// <summary>
    /// Represents a request to check for similar targets in the Vuforia web service.
    /// </summary>
    public class CheckSimilarRequest : TargetBaseServiceRequest<VuforiaCheckSimilarResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="CheckSimilarRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        /// <param name="targetId">The ID of the target to check for similarity. To retrieve target IDs, call the <see cref="List"/> method.</param>
        public CheckSimilarRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
        }

        /// <summary>
        /// Gets the target ID.
        /// </summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; }

        /// <inheritdoc/>
        public override string MethodName => "duplicates";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.GET;

        /// <inheritdoc/>
        public override string RestPath => "/duplicates/{targetId}";

        /// <inheritdoc/>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add("targetId", new Parameter("targetId", "path", true));
        }
    }

    /// <summary>
    /// Represents a request to delete a target by ID from the Vuforia web service.
    /// </summary>
    public class DeleteRequest : TargetBaseServiceRequest<VuforiaDeleteResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="DeleteRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        /// <param name="targetId">The ID of the target to delete. To retrieve target IDs, call the <see cref="List"/> method.</param>
        public DeleteRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
        }

        /// <summary>
        /// Gets the target ID.
        /// </summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; }

        /// <inheritdoc/>
        public override string MethodName => "delete";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.DELETE;

        /// <inheritdoc/>
        public override string RestPath => "/targets/{targetId}";

        /// <inheritdoc/>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add("targetId", new Parameter("targetId", "path", true));
        }
    }

    /// <summary>
    /// Represents a request to retrieve a specific target by ID from the Vuforia web service.
    /// </summary>
    public class GetRequest : TargetBaseServiceRequest<VuforiaRetrieveResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="GetRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        /// <param name="targetId">The ID of the target to get. To retrieve target IDs, call the <see cref="List"/> method.</param>
        public GetRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
        }

        /// <summary>
        /// Gets the target ID.
        /// </summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; }

        /// <inheritdoc/>
        public override string MethodName => "get";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.GET;

        /// <inheritdoc/>
        public override string RestPath => "/targets/{targetId}";

        /// <inheritdoc/>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add("targetId", new Parameter("targetId", "path", true));
        }
    }

    /// <summary>
    /// Represents a request to insert a new target into the Vuforia web service.
    /// </summary>
    public class InsertRequest : TargetBaseServiceRequest<VuforiaPostResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="InsertRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        /// <param name="body">The target to insert.</param>
        public InsertRequest(IClientService service, DatabaseAccessKeys keys, PostTrackableRequest body) : base(service, keys)
        {
            Body = body;
        }

        /// <summary>
        /// Gets the body of the request containing the target to insert.
        /// </summary>
        private PostTrackableRequest Body { get; }

        /// <inheritdoc/>
        public override string MethodName => "insert";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.POST;

        /// <inheritdoc/>
        public override string RestPath => "/targets";

        /// <inheritdoc/>
        protected override object GetBody() => Body;
    }

    /// <summary>
    /// Represents a request to retrieve a list of targets from the Vuforia web service.
    /// </summary>
    public class ListRequest : TargetBaseServiceRequest<VuforiaGetAllResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="ListRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        public ListRequest(IClientService service, DatabaseAccessKeys keys) : base(service, keys)
        {
        }

        /// <inheritdoc/>
        public override string MethodName => "list";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.GET;

        /// <inheritdoc/>
        public override string RestPath => "/targets";
    }

    /// <summary>
    /// Represents a request to update an existing target in the Vuforia web service.
    /// </summary> 
    public class UpdateRequest : TargetBaseServiceRequest<VuforiaUpdateResponse>
    {
        /// <summary>
        /// Constructs a new <see cref="UpdateRequest"/> instance.
        /// </summary>
        /// <param name="service">The client service used for making requests.</param>
        /// <param name="keys">The database access keys required for authorization.</param>
        /// <param name="body">The body of the request containing the updated target information.</param>
        /// <param name="targetId">The ID of the target to update. To retrieve target IDs, call the <see cref="List"/> method.</param>
        public UpdateRequest(IClientService service, DatabaseAccessKeys keys, PostTrackableRequest body, string targetId) : base(service, keys)
        {
            TargetId = targetId;
            Body = body;
        }

        /// <summary>
        /// Gets the target ID.
        /// </summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; }

        /// <summary>
        /// Gets the body of the request containing the updated target information.
        /// </summary>
        private PostTrackableRequest Body { get; }

        /// <inheritdoc/>
        public override string MethodName => "update";

        /// <inheritdoc/>
        public override string HttpMethod => ApiMethodConstants.PUT;

        /// <inheritdoc/>
        public override string RestPath => "/targets/{targetId}";

        /// <inheritdoc/>
        protected override object GetBody() => Body;

        /// <inheritdoc/>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add("targetId", new Parameter("targetId", "path", true));
        }
    }
}
