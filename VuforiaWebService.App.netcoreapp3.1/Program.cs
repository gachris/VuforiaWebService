using System.Net;
using VuforiaWebService.Api.Auth;
using VuforiaWebService.Api.Core.Services;
using VuforiaWebService.Api.Core.Types;
using VuforiaWebService.Api.Target;

namespace VuforiaWebService.App.netcoreapp3_1
{
    internal class Program
    {
        private static void Main()
        {
            TargetListResource resource = new TargetListResource(GetService());
            var vuforiaGetAllResponse = resource.List(GetKeys()).Execute();
            var vuforiaGetDatabaseSummaryReportResponse = resource.GetDatabaseSummaryReport(GetKeys()).Execute();
            var vuforiaCheckSimilarResponse = resource.CheckSimilar(GetKeys(), "TARGET_ID").Execute();
            var vuforiaDeleteResponse = resource.Delete(GetKeys(), "TARGET_ID").Execute();
            var vuforiaRetrieveResponse = resource.Get(GetKeys(), "TARGET_ID").Execute();
            var vuforiaPostResponse = resource.Insert(GetKeys(), new Api.Target.Types.PostTrackableRequest()).Execute();
            var vuforiaRetrieveTargetSummaryReportResponse = resource.RetrieveTargetSummaryReport(GetKeys(), "TARGET_ID").Execute();
            var vuforiaUpdateResponse = resource.Update(GetKeys(), new Api.Target.Types.PostTrackableRequest(), "TARGET_ID").Execute();
        }

        private static TargetService GetService()
        {
            UserCredential Credential = AuthorizationBroker.AuthorizeAsync(new NetworkCredential("EMAIL", "PASSWORD"));
            return new TargetService(new BaseClientService.Initializer()
            {
                ApplicationName = "APPLICATION_NAME",
                HttpClientInitializer = Credential
            });
        }

        private static DatabaseAccessKeys GetKeys()
        {
            return new DatabaseAccessKeys("ACCESS_KEY", "SECRET_KEY");
        }
    }
}
