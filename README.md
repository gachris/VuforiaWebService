# VuforiaWebService

VuforiaWebService

        private static void Main()
        {
            TargetListResource resource = new TargetListResource(GetService());
            var vuforiaGetAllResponse = resource.List(GetKeys()).Execute();
            var vuforiaGetDatabaseSummaryReportResponse = resource.GetDatabaseSummaryReport(GetKeys()).Execute();
            var vuforiaCheckSimilarResponse = resource.CheckSimilar(GetKeys(), "TARGET_ID").Execute();
            var vuforiaDeleteResponse = resource.Delete(GetKeys(), "TARGET_ID").Execute();
            var vuforiaRetrieveResponse = resource.Get(GetKeys(), "TARGET_ID").Execute();
            var vuforiaPostResponse = resource.Insert(GetKeys(), new PostTrackableRequest()).Execute();
            var vuforiaRetrieveTargetSummaryReportResponse = resource.RetrieveTargetSummaryReport(GetKeys(), "TARGET_ID").Execute();
            var vuforiaUpdateResponse = resource.Update(GetKeys(), new PostTrackableRequest(), "TARGET_ID").Execute();
        }

        private static TargetService _service;
        private static TargetService GetService()
        {
            if (_service == null)
            {
                UserCredential Credential = AuthorizationBroker.AuthorizeAsync(new NetworkCredential("EMAIL", "PASSWORD"));
                _service = new TargetService(new BaseClientService.Initializer()
                {
                    ApplicationName = "APPLICATION_NAME",
                    HttpClientInitializer = Credential
                });
            }
            return _service;
        }

        private static DatabaseAccessKeys _keys;
        private static DatabaseAccessKeys GetKeys()
        {
            if (_keys == null)
            {
                _keys = new DatabaseAccessKeys("ACCESS_KEY", "SECRET_KEY");
            }
            return _keys;
        }
