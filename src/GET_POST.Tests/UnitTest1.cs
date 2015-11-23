using System;
using GET_POST.Models;
using GET_POST.Services;
using GET_POST.Controllers;
using System.Web;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using System.Net;

namespace GET_POST.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        Mock<ContactRepository> _contactRepositoryMock;
        ContactController _contactController;
        Contact contact = new Contact { Id = 222, Name = "Stefano" };
        HttpRequestMessage request = new HttpRequestMessage();

        [SetUp]
        public void SetUp()
        {
            _contactRepositoryMock = new Mock<ContactRepository>();

            _contactRepositoryMock.Setup(x => x.GetAllContact()).Returns(new Contact[]
                     {
                        new Contact
                        {
                             Id = 1, Name = "George"
                        },
                        new Contact
                        {
                            Id = 2, Name = "Dan"
                        }
                     });
            _contactRepositoryMock.Setup(x => x.SaveContact(It.IsAny<Contact>(), It.IsAny<HttpRequestMessage>())).Returns(
                 new HttpResponseMessage(HttpStatusCode.OK));

            _contactController = new ContactController(_contactRepositoryMock.Object);
        }

        [Test]
        public void GetTest()
        {

            // Act
            var result = _contactController.Get(); // <2>

            // Assert // <3>
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Dan", result);
            Assert.AreEqual(2, result.Length);
            //Assert.AreEqual("1", result);
            //Assert.AreEqual("2", result);
        }

        [Test]
        public void PostTest()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var result = _contactController.Post(contact);

            Assert.AreEqual(response.StatusCode, result.StatusCode);
        }

    }
}
