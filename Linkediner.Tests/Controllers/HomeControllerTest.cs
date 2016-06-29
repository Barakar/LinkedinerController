using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Linkediner.DAL;
using Linkediner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkediner;
using Linkediner.Controllers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Linkediner.Tests.Controllers
{

    [TestFixture]
    public class SQLiteDataAccessorTests
    {
        [Test]
        public void someTest()
        {
            var sqLiteDataAccessor = new SQLiteDataAccessor("Test.sqlite");
            sqLiteDataAccessor.Init();

            var linkedinProfile = new LinkedinProfile("8","barak","A","B","",new List<string>(){"C"},new List<Experience>(){new Experience()
            {
                CompanyName = "C", Description = "hey", Position = "T", StartTime = DateTime.Now, EndTime = DateTime.Now
            }},  new List<School>()
            {
                new School(){ Title = "SA",DegreeName = "BA",Summary = "HAHA"}
            });

            
            sqLiteDataAccessor.InsertProfile(linkedinProfile);
            var id = linkedinProfile.Id;
            var profile = sqLiteDataAccessor.GetProfile(id);
            var serializeObject = JsonConvert.SerializeObject(profile);
            
            sqLiteDataAccessor.Dispose();
            int x = 6;
        }

        [Test]
        public void someTest2()
        {
            var sqLiteDataAccessor = new SQLiteDataAccessor("Test.sqlite");
            sqLiteDataAccessor.Init();

            var profile = sqLiteDataAccessor.GetProfilesBySkills(new List<string> {"C"});
            var serializeObject = JsonConvert.SerializeObject(profile);

            sqLiteDataAccessor.Dispose();
            int x = 6;
        }
    }

    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
