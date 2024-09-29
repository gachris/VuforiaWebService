using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Target.Services;
using VuforiaWebService.Api.Target.Types;

namespace VuforiaWebService.Api.Target.Tests.Resource;

public class TargetListTestResource : ITargetListTestResource
{
    private readonly TargetService _service;

    public TargetListTestResource(TargetService service)
    {
        _service = service;
    }

    public virtual VuforiaGetAllResponse List(ServerAccessKeys keys)
    {
        return _service.TargetList.List(keys).Execute();
    }

    public virtual VuforiaRetrieveResponse Get(ServerAccessKeys keys, string targetId)
    {
        return _service.TargetList.Get(keys, targetId).Execute();
    }

    public virtual VuforiaPostResponse Insert(ServerAccessKeys keys, PostTrackableRequest request)
    {
        return _service.TargetList.Insert(keys, request).Execute();
    }

    public virtual VuforiaUpdateResponse Update(ServerAccessKeys keys, PostTrackableRequest request, string targetId)
    {
        return _service.TargetList.Update(keys, request, targetId).Execute();
    }

    public virtual VuforiaDeleteResponse Delete(ServerAccessKeys keys, string targetId)
    {
        return _service.TargetList.Delete(keys, targetId).Execute();
    }

    public virtual VuforiaCheckSimilarResponse CheckSimilar(ServerAccessKeys keys, string targetId)
    {
        return _service.TargetList.CheckSimilar(keys, targetId).Execute();
    }

    public virtual VuforiaRetrieveTargetSummaryReportResponse RetrieveTargetSummaryReport(ServerAccessKeys keys, string targetId)
    {
        return _service.TargetList.RetrieveTargetSummaryReport(keys, targetId).Execute();
    }

    public virtual VuforiaGetDatabaseSummaryReportResponse GetDatabaseSummaryReport(ServerAccessKeys keys)
    {
        return _service.TargetList.GetDatabaseSummaryReport(keys).Execute();
    }
}
