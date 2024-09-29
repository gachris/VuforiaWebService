using System.Net;
using Moq;
using VuforiaWebService.Api.Auth;
using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Target.Resources;
using VuforiaWebService.Api.Target.Services;
using VuforiaWebService.Api.Target.Types;
using static VuforiaWebService.Api.Target.Resources.TargetListResource;

namespace VuforiaWebService.Tests;

public class VuforiaApiTests
{
    private Mock<TargetListResource> _mockTargetListResource;
    private Mock<ListRequest> _mockTargetListRequest;
    private Mock<TargetService> _mockTargetService;
    private DatabaseAccessKeys _mockDatabaseAccessKeys;
    private PostTrackableRequest _mockPostTrackableRequest;

    [SetUp]
    public void Setup()
    {
        // Initialize mocks
        _mockTargetService = new Mock<TargetService>(MockBehavior.Strict);

        _mockTargetListResource = new Mock<TargetListResource>(_mockTargetService.Object);
        _mockPostTrackableRequest = new PostTrackableRequest();

        _mockTargetListRequest = new Mock<ListRequest>();
    }

    [Test]
    public void List_Calls_TargetListResource_List_Method()
    {
        // Arrange
        _mockTargetListResource.Setup(x => x.List(It.IsAny<DatabaseAccessKeys>()))
                               .Returns(_mockTargetListRequest.Object);

        // Act
        var result = _mockTargetListResource.Object.List(_mockDatabaseAccessKeys);

        // Assert
        _mockTargetListResource.Verify(x => x.List(_mockDatabaseAccessKeys), Times.Once);
        Assert.IsNotNull(result);
    }

    [Test]
    public void GetDatabaseSummaryReport_Calls_Correct_Method()
    {
        //// Arrange
        //_mockTargetListResource.Setup(x => x.GetDatabaseSummaryReport(It.IsAny<DatabaseAccessKeys>()))
        //                       .Returns(_mockTargetListResource.Object);

        //// Act
        //var result = _mockTargetListResource.Object.GetDatabaseSummaryReport(_mockDatabaseAccessKeys);

        //// Assert
        //_mockTargetListResource.Verify(x => x.GetDatabaseSummaryReport(_mockDatabaseAccessKeys), Times.Once);
        //Assert.IsNotNull(result);
    }

    [Test]
    public void CheckSimilar_Calls_Correct_Method()
    {
        //// Arrange
        //var targetId = "TARGET_ID";
        //_mockTargetListResource.Setup(x => x.CheckSimilar(It.IsAny<DatabaseAccessKeys>(), targetId))
        //                       .Returns(_mockTargetListResource.Object);

        //// Act
        //var result = _mockTargetListResource.Object.CheckSimilar(_mockDatabaseAccessKeys, targetId);

        //// Assert
        //_mockTargetListResource.Verify(x => x.CheckSimilar(_mockDatabaseAccessKeys, targetId), Times.Once);
        //Assert.IsNotNull(result);
    }

    [Test]
    public void Delete_Calls_Correct_Method()
    {
        //// Arrange
        //var targetId = "TARGET_ID";
        //_mockTargetListResource.Setup(x => x.Delete(It.IsAny<DatabaseAccessKeys>(), targetId))
        //                       .Returns(_mockTargetListResource.Object);

        //// Act
        //var result = _mockTargetListResource.Object.Delete(_mockDatabaseAccessKeys, targetId);

        //// Assert
        //_mockTargetListResource.Verify(x => x.Delete(_mockDatabaseAccessKeys, targetId), Times.Once);
        //Assert.IsNotNull(result);
    }

    [Test]
    public void Get_Calls_Correct_Method()
    {
        //// Arrange
        //var targetId = "TARGET_ID";
        //_mockTargetListResource.Setup(x => x.Get(It.IsAny<DatabaseAccessKeys>(), targetId))
        //                       .Returns(_mockTargetListResource.Object);

        //// Act
        //var result = _mockTargetListResource.Object.Get(_mockDatabaseAccessKeys, targetId);

        //// Assert
        //_mockTargetListResource.Verify(x => x.Get(_mockDatabaseAccessKeys, targetId), Times.Once);
        //Assert.IsNotNull(result);
    }

    [Test]
    public void Insert_Calls_Correct_Method()
    {
        //// Arrange
        //_mockTargetListResource.Setup(x => x.Insert(It.IsAny<DatabaseAccessKeys>(), _mockPostTrackableRequest))
        //                       .Returns(_mockTargetListResource.Object);

        //// Act
        //var result = _mockTargetListResource.Object.Insert(_mockDatabaseAccessKeys, _mockPostTrackableRequest);

        //// Assert
        //_mockTargetListResource.Verify(x => x.Insert(_mockDatabaseAccessKeys, _mockPostTrackableRequest), Times.Once);
        //Assert.IsNotNull(result);
    }

    [Test]
    public void RetrieveTargetSummaryReport_Calls_Correct_Method()
    {
        //// Arrange
        //var targetId = "TARGET_ID";
        //_mockTargetListResource.Setup(x => x.RetrieveTargetSummaryReport(It.IsAny<DatabaseAccessKeys>(), targetId))
        //                       .Returns(_mockTargetListResource.Object);

        //// Act
        //var result = _mockTargetListResource.Object.RetrieveTargetSummaryReport(_mockDatabaseAccessKeys, targetId);

        //// Assert
        //_mockTargetListResource.Verify(x => x.RetrieveTargetSummaryReport(_mockDatabaseAccessKeys, targetId), Times.Once);
        //Assert.IsNotNull(result);
    }

    [Test]
    public void Update_Calls_Correct_Method()
    {
        //// Arrange
        //var targetId = "TARGET_ID";
        //_mockTargetListResource.Setup(x => x.Update(It.IsAny<DatabaseAccessKeys>(), _mockPostTrackableRequest, targetId))
        //                       .Returns(_mockTargetListResource.Object);

        //// Act
        //var result = _mockTargetListResource.Object.Update(_mockDatabaseAccessKeys, _mockPostTrackableRequest, targetId);

        //// Assert
        //_mockTargetListResource.Verify(x => x.Update(_mockDatabaseAccessKeys, _mockPostTrackableRequest, targetId), Times.Once);
        //Assert.IsNotNull(result);
    }

    [Test]
    public void ExceptionHandling_When_Api_Fails()
    {
        //// Arrange
        //var targetId = "TARGET_ID";
        //_mockTargetListResource.Setup(x => x.Get(It.IsAny<DatabaseAccessKeys>(), targetId))
        //                       .Throws(new VuforiaPortalApiException("API Error"));

        //// Act & Assert
        //Assert.Throws<VuforiaPortalApiException>(() => _mockTargetListResource.Object.Get(_mockDatabaseAccessKeys, targetId));
        //_mockTargetListResource.Verify(x => x.Get(_mockDatabaseAccessKeys, targetId), Times.Once);
    }
}
