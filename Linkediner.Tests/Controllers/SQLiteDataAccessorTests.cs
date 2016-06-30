using System;
using System.Collections.Generic;
using System.Linq;
using Linkediner.DAL;
using Linkediner.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Linkediner.Tests.Controllers
{
    [TestFixture]
    public class SQLiteDataAccessorTests
    {
        private SQLiteDataAccessor _sqLiteDataAccessor;
        private int _counterId;

        [TestFixtureSetUp]
        public void Init()
        {
            _sqLiteDataAccessor = new SQLiteDataAccessor("Test.sqlite");
            _sqLiteDataAccessor.Init();

            _counterId = 0;
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            _sqLiteDataAccessor.Dispose();
        }

        [Test]
        public void Insert_And_Retrieve_Should_Be_Equal()
        {
            //Arrange
            var linkedinProfile = CreateLinkedinProfile(_counterId);

            //Act
            _sqLiteDataAccessor.InsertProfile(linkedinProfile);
            var id = linkedinProfile.Id;
            var profile = _sqLiteDataAccessor.GetProfile(id);

            //Assert
            Assert.AreEqual(id, profile.Id);
        }

        [Test]
        public void Get_Profile_By_Skill_Of_C_Should_Return_Profile()
        {
            //Act
            var profiles = _sqLiteDataAccessor.GetProfilesBySkills(new List<string> { "C" });

            //Assert
            Assert.NotNull(profiles.FirstOrDefault(profile => profile.Skills.Any(skill => skill == "C")));
        }

        private LinkedinProfile CreateLinkedinProfile(int counterId)
        {
            var linkedinProfile = new LinkedinProfile(counterId.ToString(), "barak", "A", "B", "", new List<string>() { "C" },
                new List<Experience>()
                {
                    new Experience()
                    {
                        CompanyName = "C",
                        Description = "hey",
                        Position = "T",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now
                    }
                }, new List<School>()
                {
                    new School() {Title = "SA", DegreeName = "BA", Summary = "HAHA"}
                });

            return linkedinProfile;
        }
    }
}