using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Target.Types;

namespace VuforiaWebService.Tests.Resource;

public interface ITargetListTestResource
{
    VuforiaGetAllResponse List(ServerAccessKeys keys);

    VuforiaRetrieveResponse Get(ServerAccessKeys keys, string targetId);

    VuforiaPostResponse Insert(ServerAccessKeys keys, PostTrackableRequest request);

    VuforiaUpdateResponse Update(ServerAccessKeys keys, PostTrackableRequest request, string targetId);

    VuforiaDeleteResponse Delete(ServerAccessKeys keys, string targetId);

    VuforiaCheckSimilarResponse CheckSimilar(ServerAccessKeys keys, string targetId);

    VuforiaRetrieveTargetSummaryReportResponse RetrieveTargetSummaryReport(ServerAccessKeys keys, string targetId);

    VuforiaGetDatabaseSummaryReportResponse GetDatabaseSummaryReport(ServerAccessKeys keys);
}
