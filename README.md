Usage

        private static void Main()
        {
            try
            {
                DatabaseAccessKeys keys = GetKeys();
                TargetListResource resource = new TargetListResource(GetService());
                var vuforiaGetAllResponse = resource.List(keys).Execute();
                var vuforiaGetDatabaseSummaryReportResponse = resource.GetDatabaseSummaryReport(keys).Execute();
                var vuforiaCheckSimilarResponse = resource.CheckSimilar(keys, "TARGET_ID").Execute();
                var vuforiaDeleteResponse = resource.Delete(keys, "TARGET_ID").Execute();
                var vuforiaRetrieveResponse = resource.Get(keys, "TARGET_ID").Execute();
                var vuforiaPostResponse = resource.Insert(keys, new PostTrackableRequest()).Execute();
                var vuforiaRetrieveTargetSummaryReportResponse = resource.RetrieveTargetSummaryReport(keys, "TARGET_ID").Execute();
                var vuforiaUpdateResponse = resource.Update(keys, new PostTrackableRequest(), "TARGET_ID").Execute();
            }
            catch (VuforiaPortalApiException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Error.ResultCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
