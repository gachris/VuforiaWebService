using Moq;
using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Core.Response;
using VuforiaWebService.Api.Target.Types;
using VuforiaWebService.Tests.Resource;

namespace VuforiaWebService.Tests;

[TestFixture]
public class VuforiaApiTests
{
    private Mock<ITargetListTestResource> _mockTargetListResource;
    private ServerAccessKeys _mockServerAccessKeys;
    private const string TARGET_ID = "TARGET_ID";
    private const string TRANSACTION_ID = "TRANSACTION_ID";

    [SetUp]
    public void Setup()
    {
        _mockTargetListResource = new Mock<ITargetListTestResource>();
        _mockServerAccessKeys = new ServerAccessKeys("ACCESS_KEY", "SECRET_KEY");
    }

    [Test]
    public void List_ValidResponse_ReturnsExpectedResult()
    {
        // Arrange
        var response = CreateValidGetAllResponse();

        _mockTargetListResource.Setup(x => x.List(It.IsAny<ServerAccessKeys>()))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.List(_mockServerAccessKeys);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.ResultCode, Is.EqualTo(VuforiaBaseResponse.ResultCodeEnum.Success));
        Assert.That(result.Results, Has.Length.EqualTo(1));
        Assert.That(result.Results[0], Is.EqualTo(TARGET_ID));

        // Verify mock method was called once
        _mockTargetListResource.Verify(x => x.List(_mockServerAccessKeys), Times.Once);
    }

    [Test]
    public void Get_ValidTargetId_ReturnsCorrectData()
    {
        // Arrange
        var response = CreateValidRetrieveResponse();

        _mockTargetListResource.Setup(x => x.Get(It.IsAny<ServerAccessKeys>(), TARGET_ID))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.Get(_mockServerAccessKeys, TARGET_ID);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.ResultCode, Is.EqualTo(VuforiaBaseResponse.ResultCodeEnum.Success));
        Assert.That(result.Status, Is.EqualTo(VuforiaRetrieveResponse.StatusEnum.Success));

        // Assert TargetRecord properties
        var targetRecord = result.TargetRecord;
        Assert.That(targetRecord, Is.Not.Null);
        Assert.That(targetRecord.TargetId, Is.EqualTo(TARGET_ID));
        Assert.That(targetRecord.ActiveFlag, Is.EqualTo("true"));
        Assert.That(targetRecord.TrackingRating, Is.EqualTo(5));
        Assert.That(targetRecord.Width, Is.EqualTo(1));

        // Verify mock method was called once
        _mockTargetListResource.Verify(x => x.Get(_mockServerAccessKeys, TARGET_ID), Times.Once);
    }

    [Test]
    public void Insert_ValidRequest_CallsInsertOnce()
    {
        // Arrange
        var request = CreatePostTrackableRequest();
        var response = CreatePostResponse();

        _mockTargetListResource.Setup(x => x.Insert(It.IsAny<ServerAccessKeys>(), request))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.Insert(_mockServerAccessKeys, request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.ResultCode, Is.EqualTo(VuforiaBaseResponse.ResultCodeEnum.Success));
        Assert.That(result.TargetId, Is.EqualTo(TARGET_ID));

        // Verify the Insert method was called once
        _mockTargetListResource.Verify(x => x.Insert(_mockServerAccessKeys, request), Times.Once);
    }

    [Test]
    public void Update_ValidRequest_CallsUpdateOnce()
    {
        // Arrange
        var request = CreatePostTrackableRequest();
        var response = CreateUpdateResponse();

        _mockTargetListResource.Setup(x => x.Update(It.IsAny<ServerAccessKeys>(), request, TARGET_ID))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.Update(_mockServerAccessKeys, request, TARGET_ID);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.ResultCode, Is.EqualTo(VuforiaBaseResponse.ResultCodeEnum.Success));

        // Verify the Update method was called once
        _mockTargetListResource.Verify(x => x.Update(_mockServerAccessKeys, request, TARGET_ID), Times.Once);
    }

    [Test]
    public void Delete_ValidTargetId_CallsDeleteOnce()
    {
        // Arrange
        var response = CreateDeleteResponse();

        _mockTargetListResource.Setup(x => x.Delete(It.IsAny<ServerAccessKeys>(), TARGET_ID))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.Delete(_mockServerAccessKeys, TARGET_ID);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.ResultCode, Is.EqualTo(VuforiaBaseResponse.ResultCodeEnum.Success));

        // Verify the Delete method was called once
        _mockTargetListResource.Verify(x => x.Delete(_mockServerAccessKeys, TARGET_ID), Times.Once);
    }

    [Test]
    public void CheckSimilar_ValidResponse_CallsCheckSimilarOnce()
    {
        // Arrange
        var response = CreateCheckSimilarResponse();

        _mockTargetListResource.Setup(x => x.CheckSimilar(It.IsAny<ServerAccessKeys>(), TARGET_ID))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.CheckSimilar(_mockServerAccessKeys, TARGET_ID);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.SimilarTargets, Is.Not.Empty);
        Assert.That(result.SimilarTargets[0], Is.EqualTo(TARGET_ID));
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.ResultCode, Is.EqualTo(VuforiaBaseResponse.ResultCodeEnum.Success));

        // Verify the CheckSimilar method was called once
        _mockTargetListResource.Verify(x => x.CheckSimilar(_mockServerAccessKeys, TARGET_ID), Times.Once);
    }

    [Test]
    public void RetrieveTargetSummaryReport_ValidResponse_CallsRetrieveOnce()
    {
        // Arrange
        var response = CreateRetrieveTargetSummaryReportResponse();

        _mockTargetListResource.Setup(x => x.RetrieveTargetSummaryReport(It.IsAny<ServerAccessKeys>(), TARGET_ID))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.RetrieveTargetSummaryReport(_mockServerAccessKeys, TARGET_ID);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.TotalRecos, Is.EqualTo(100));
        Assert.That(result.PreviousMonthRecos, Is.EqualTo(50));
        Assert.That(result.CurrentMonthRecos, Is.EqualTo(100)); // Added
        Assert.That(result.ActiveFlag, Is.True); // Added
        Assert.That(result.Status, Is.EqualTo(VuforiaRetrieveTargetSummaryReportResponse.StatusEnum.Success)); // Added

        _mockTargetListResource.Verify(x => x.RetrieveTargetSummaryReport(_mockServerAccessKeys, TARGET_ID), Times.Once);
    }

    [Test]
    public void GetDatabaseSummaryReport_ValidResponse_CallsGetDatabaseSummaryOnce()
    {
        // Arrange
        var response = CreateDatabaseSummaryReportResponse();

        _mockTargetListResource.Setup(x => x.GetDatabaseSummaryReport(It.IsAny<ServerAccessKeys>()))
                               .Returns(response);

        // Act
        var result = _mockTargetListResource.Object.GetDatabaseSummaryReport(_mockServerAccessKeys);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TransactionId, Is.EqualTo(TRANSACTION_ID));
        Assert.That(result.ActiveImages, Is.EqualTo(5));
        Assert.That(result.FailedImages, Is.EqualTo(5));
        Assert.That(result.InactiveImages, Is.EqualTo(5));
        Assert.That(result.ResultCode, Is.EqualTo(VuforiaBaseResponse.ResultCodeEnum.Success)); // Added

        _mockTargetListResource.Verify(x => x.GetDatabaseSummaryReport(_mockServerAccessKeys), Times.Once);
    }

    [Test]
    public void ExceptionHandling_WhenApiFails_ThrowsVuforiaPortalApiException()
    {
        // Arrange
        _mockTargetListResource.Setup(x => x.Get(It.IsAny<ServerAccessKeys>(), TARGET_ID))
                               .Throws(new VuforiaPortalApiException(nameof(VuforiaApiTests), "API Error"));

        // Act & Assert
        Assert.Throws<VuforiaPortalApiException>(() => _mockTargetListResource.Object.Get(_mockServerAccessKeys, TARGET_ID));

        // Verify the Get method was called once
        _mockTargetListResource.Verify(x => x.Get(_mockServerAccessKeys, TARGET_ID), Times.Once);
    }

    // Helper methods to create responses
    private static VuforiaGetAllResponse CreateValidGetAllResponse()
    {
        return new VuforiaGetAllResponse
        {
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success,
            Results = [TARGET_ID]
        };
    }

    private static VuforiaRetrieveResponse CreateValidRetrieveResponse()
    {
        return new VuforiaRetrieveResponse
        {
            Status = VuforiaRetrieveResponse.StatusEnum.Success,
            TargetRecord = new TargetRecordModel
            {
                ActiveFlag = "true",
                TrackingRating = 5,
                TargetId = TARGET_ID,
                Width = 1
            },
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success
        };
    }

    private static PostTrackableRequest CreatePostTrackableRequest()
    {
        return new PostTrackableRequest
        {
            ActiveFlag = true,
            ApplicationMetadata = "Target Metadata",
            Image = "/9j/4AAQSkZJRgABAQEAAAAAAAD...",
            Name = "Sample Target",
            Width = 1
        };
    }

    private static VuforiaPostResponse CreatePostResponse()
    {
        return new VuforiaPostResponse
        {
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success,
            TargetId = TARGET_ID
        };
    }

    private static VuforiaUpdateResponse CreateUpdateResponse()
    {
        return new VuforiaUpdateResponse
        {
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success
        };
    }

    private static VuforiaDeleteResponse CreateDeleteResponse()
    {
        return new VuforiaDeleteResponse
        {
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success
        };
    }

    private static VuforiaCheckSimilarResponse CreateCheckSimilarResponse()
    {
        return new VuforiaCheckSimilarResponse
        {
            SimilarTargets = [TARGET_ID],
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success
        };
    }

    private static VuforiaRetrieveTargetSummaryReportResponse CreateRetrieveTargetSummaryReportResponse()
    {
        return new VuforiaRetrieveTargetSummaryReportResponse
        {
            ActiveFlag = true,
            CurrentMonthRecos = 100,
            DatabaseName = "TestDB",
            PreviousMonthRecos = 50,
            Status = VuforiaRetrieveTargetSummaryReportResponse.StatusEnum.Success,
            TargetName = "Sample Target",
            TotalRecos = 100,
            TrackingRating = 5,
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success
        };
    }

    private static VuforiaGetDatabaseSummaryReportResponse CreateDatabaseSummaryReportResponse()
    {
        return new VuforiaGetDatabaseSummaryReportResponse
        {
            ActiveImages = 5,
            FailedImages = 5,
            InactiveImages = 5,
            TransactionId = TRANSACTION_ID,
            ResultCode = VuforiaBaseResponse.ResultCodeEnum.Success
        };
    }
}
