using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Core.Types;
using VuforiaWebService.Api.Core.Util;
using VuforiaWebService.Api.Target.Requests;
using VuforiaWebService.Api.Target.Types;

namespace VuforiaWebService.Api.Target.Resources;

/// <summary>The "calendarList" collection of methods.</summary>
public class TargetListResource
{
    private const string Resource = "targetList";

    /// <summary>The service which this resource belongs to.</summary>
    private readonly IClientService _service;

    /// <summary>Constructs a new resource.</summary>
    public TargetListResource(IClientService service) => _service = service;

    /// <summary>Removes a target from the user's target list.</summary>
    /// <param name="targetId">target identifier. To retrieve target IDs call the targetList.list method. If you
    /// want to access the primary target of the currently logged in user, use the "primary" keyword.</param>
    public virtual DeleteRequest Delete(DatabaseAccessKeys keys, string targetId) => new DeleteRequest(_service, keys, targetId);

    /// <summary>Returns a target from the user's target list.</summary>
    /// <param name="targetId">target identifier. To retrieve target IDs call the targetList.list method. If you
    /// want to access the primary target of the currently logged in user, use the "primary" keyword.</param>
    public virtual GetRequest Get(DatabaseAccessKeys keys, string targetId) => new GetRequest(_service, keys, targetId);

    /// <summary>Inserts an existing target into the user's target list.</summary>
    /// <param name="body">The body of the request.</param>
    public virtual InsertRequest Insert(DatabaseAccessKeys keys, PostTrackableRequest body) => new InsertRequest(_service, keys, body);

    /// <summary>Returns the targets on the user's targets list.</summary>
    public virtual ListRequest List(DatabaseAccessKeys keys) => new ListRequest(_service, keys);

    /// <summary>Updates an existing target on the user's target list.</summary>
    /// <param name="body">The body of the request.</param>
    /// <param name="targetId">Target identifier. To retrieve target IDs call the targetList.list method. If you
    /// want to access the primary target of the currently logged in user, use the "primary" keyword.</param>
    public virtual UpdateRequest Update(DatabaseAccessKeys keys, PostTrackableRequest body, string targetId) => new UpdateRequest(_service, keys, body, targetId);

    public virtual CheckSimilarRequest CheckSimilar(DatabaseAccessKeys keys, string targetId) => new CheckSimilarRequest(_service, keys, targetId);

    public virtual RetrieveTargetSummaryReportRequest RetrieveTargetSummaryReport(DatabaseAccessKeys keys, string targetId) => new RetrieveTargetSummaryReportRequest(_service, keys, targetId);

    public virtual GetDatabaseSummaryReportRequest GetDatabaseSummaryReport(DatabaseAccessKeys keys) => new GetDatabaseSummaryReportRequest(_service, keys);

    public class GetDatabaseSummaryReportRequest : TargetBaseServiceRequest<VuforiaGetDatabaseSummaryReportResponse>
    {
        /// <summary>Constructs a new Delete request.</summary>
        public GetDatabaseSummaryReportRequest(IClientService service, DatabaseAccessKeys keys) : base(service, keys) => InitParameters();

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "summary";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.GET;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/summary";

        /// <summary>Initializes Delete parameter list.</summary>
        protected override void InitParameters() => base.InitParameters();
    }

    public class RetrieveTargetSummaryReportRequest : TargetBaseServiceRequest<VuforiaRetrieveTargetSummaryReportResponse>
    {
        /// <summary>Constructs a new Delete request.</summary>
        public RetrieveTargetSummaryReportRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
            InitParameters();
        }

        /// <summary>target identifier. To retrieve target IDs call the targetList.list method. If you want to
        /// access the primary target of the currently logged in user, use the "primary" keyword.</summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; private set; }

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "summary";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.GET;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/summary/{targetId}";

        /// <summary>Initializes Delete parameter list.</summary>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add(
                "targetId", new Parameter
                {
                    Name = "targetId",
                    IsRequired = true,
                    ParameterType = "path",
                    DefaultValue = null,
                    Pattern = null,
                });
        }
    }

    public class CheckSimilarRequest : TargetBaseServiceRequest<VuforiaCheckSimilarResponse>
    {
        /// <summary>Constructs a new Delete request.</summary>
        public CheckSimilarRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
            InitParameters();
        }

        /// <summary>target identifier. To retrieve target IDs call the targetList.list method. If you want to
        /// access the primary target of the currently logged in user, use the "primary" keyword.</summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; private set; }

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "duplicates";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.GET;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/duplicates/{targetId}";

        /// <summary>Initializes Delete parameter list.</summary>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add(
                "targetId", new Parameter
                {
                    Name = "targetId",
                    IsRequired = true,
                    ParameterType = "path",
                    DefaultValue = null,
                    Pattern = null,
                });
        }
    }

    /// <summary>Removes a target from the user's target list.</summary>
    public class DeleteRequest : TargetBaseServiceRequest<VuforiaDeleteResponse>
    {
        /// <summary>Constructs a new Delete request.</summary>
        public DeleteRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
            InitParameters();
        }

        /// <summary>target identifier. To retrieve target IDs call the targetList.list method. If you want to
        /// access the primary target of the currently logged in user, use the "primary" keyword.</summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; private set; }

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "delete";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.DELETE;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/targets/{targetId}";

        /// <summary>Initializes Delete parameter list.</summary>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add(
                "targetId", new Parameter
                {
                    Name = "targetId",
                    IsRequired = true,
                    ParameterType = "path",
                    DefaultValue = null,
                    Pattern = null,
                });
        }
    }

    /// <summary>Returns a target from the user's target list.</summary>
    public class GetRequest : TargetBaseServiceRequest<VuforiaRetrieveResponse>
    {
        /// <summary>Constructs a new Get request.</summary>
        public GetRequest(IClientService service, DatabaseAccessKeys keys, string targetId) : base(service, keys)
        {
            TargetId = targetId;
            InitParameters();
        }

        /// <summary>target identifier. To retrieve target IDs call the targetList.list method. If you want to
        /// access the primary target of the currently logged in user, use the "primary" keyword.</summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; private set; }

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "get";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.GET;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/targets/{targetId}";

        /// <summary>Initializes Get parameter list.</summary>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add("targetId", new Parameter
            {
                Name = "targetId",
                IsRequired = true,
                ParameterType = "path",
                DefaultValue = null,
                Pattern = null,
            });
        }
    }

    /// <summary>Inserts an existing target into the user's target list.</summary>
    public class InsertRequest : TargetBaseServiceRequest<VuforiaPostResponse>
    {
        /// <summary>Constructs a new Insert request.</summary>
        public InsertRequest(IClientService service, DatabaseAccessKeys keys, PostTrackableRequest body) : base(service, keys)
        {
            Body = body;
            InitParameters();
        }

        /// <summary>Gets or sets the body of this request.</summary>
        private PostTrackableRequest Body { get; set; }

        ///<summary>Returns the body of the request.</summary>
        protected override object GetBody() => Body;

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "insert";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.POST;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/targets";

        /// <summary>Initializes Insert parameter list.</summary>
        protected override void InitParameters() => base.InitParameters();
    }

    /// <summary>Returns the calendars on the user's calendar list.</summary>
    public class ListRequest : TargetBaseServiceRequest<VuforiaGetAllResponse>
    {
        /// <summary>Constructs a new List request.</summary>
        public ListRequest(IClientService service, DatabaseAccessKeys keys) : base(service, keys) => InitParameters();

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "list";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.GET;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/targets";

        /// <summary>Initializes List parameter list.</summary>
        protected override void InitParameters() => base.InitParameters();
    }

    /// <summary>Updates an existing target on the user's target list.</summary>
    public class UpdateRequest : TargetBaseServiceRequest<VuforiaUpdateResponse>
    {
        /// <summary>Constructs a new Update request.</summary>
        public UpdateRequest(IClientService service, DatabaseAccessKeys keys, PostTrackableRequest body, string targetId) : base(service, keys)
        {
            TargetId = targetId;
            Body = body;
            InitParameters();
        }

        /// <summary>Target identifier. To retrieve target IDs call the targetList.list method. If you want to
        /// access the primary target of the currently logged in user, use the "primary" keyword.</summary>
        [RequestParameter("targetId", RequestParameterType.Path)]
        public virtual string TargetId { get; private set; }

        /// <summary>Gets or sets the body of this request.</summary>
        private PostTrackableRequest Body { get; set; }

        ///<summary>Returns the body of the request.</summary>
        protected override object GetBody() => Body;

        ///<summary>Gets the method name.</summary>
        public override string MethodName => "update";

        ///<summary>Gets the HTTP method.</summary>
        public override string HttpMethod => ApiMethodConstants.PUT;

        ///<summary>Gets the REST path.</summary>
        public override string RestPath => "/targets/{targetId}";

        /// <summary>Initializes Update parameter list.</summary>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add(
                "targetId", new Parameter
                {
                    Name = "targetId",
                    IsRequired = true,
                    ParameterType = "path",
                    DefaultValue = null,
                    Pattern = null,
                });
        }
    }
}
