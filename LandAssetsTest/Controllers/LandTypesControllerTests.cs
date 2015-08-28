using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandAssets.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LandAssets.Models;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
namespace LandAssets.Controllers.Tests
{
    [TestClass()]
    public class LandTypesControllerTests
    {
        static LandTypeViewModel testItem;
        static int newId;
        
        // Use ClassInitialize to run code before running the first test in the class

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            testItem = new LandTypeViewModel { Id = 0, Type = "地目A" };
            newId = 0;
        }

        [TestMethod()]
        public void PostTest()
        {
            // arrange
            LandTypesController controller = new LandTypesController();

            // act
            IHttpActionResult actionResult = controller.Post(testItem);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<LandTypeViewModel>;
            newId = Convert.ToInt32(createdResult.RouteValues["Id"]);

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsTrue(newId > 0, "新增資料編號應大於0");
        }


        [TestMethod()]
        public void GetByIdTest()
        {
            // arrange
            LandTypesController controller = new LandTypesController();

            // Act
            IHttpActionResult actionResult = controller.Get(newId);
            var contentResult = actionResult as OkNegotiatedContentResult<LandTypeViewModel>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(newId, contentResult.Content.Id);
            Assert.IsNotNull(newId, contentResult.Content.TimestampString);
            testItem.TimestampString = contentResult.Content.TimestampString;
        }

        [TestMethod()]
        public void PutTest()
        {
            // arrange
            LandTypesController controller = new LandTypesController();

            // Act
            testItem.Id = newId;
            testItem.Type = "地目B";            
            IHttpActionResult actionResult = controller.Put(newId, testItem);
            var contentResult = actionResult as NegotiatedContentResult<LandTypeViewModel>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(testItem.Type, contentResult.Content.Type);
        }

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            LandTypesController controller = new LandTypesController();

            // Act
            IHttpActionResult actionResult = controller.Get(-1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetTest()
        {
            // arrange
            LandTypesController controller = new LandTypesController();

            // act
            var result = controller.Get();

            // assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<LandTypeViewModel>));
            Assert.IsTrue(result.Count() >= 0);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            LandTypesController controller = new LandTypesController();

            // Act
            IHttpActionResult actionResult = controller.Delete(newId);
            var contentResult = actionResult as NegotiatedContentResult<LandTypeViewModel>;

            // Assert
            //Assert.IsInstanceOfType(actionResult, typeof(OkResult));
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(testItem.Id, contentResult.Content.Id);
        }

    }
}
