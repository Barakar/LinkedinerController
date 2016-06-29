using System.Collections.Generic;
using Linkediner.Models;
using Newtonsoft.Json;

namespace Linkediner.DAL
{
    public class JsonLinkedinProfile
    {

        public JsonLinkedinProfile()
        {
            
        }
        public JsonLinkedinProfile(LinkedinProfile profile)
        {
            Id = profile.Id;
            Name = profile.Name;
            Title = profile.Title;
            Position = profile.Position;
            Summary = profile.Summary;

            Skills = JsonConvert.SerializeObject(profile.Skills);
            Experience = JsonConvert.SerializeObject(profile.Experience);
            Education = JsonConvert.SerializeObject(profile.Education);
        }

        public LinkedinProfile ToLinkedinProfile()
        {
            var skills = JsonConvert.DeserializeObject<List<string>>(Skills);
            var experience = JsonConvert.DeserializeObject<List<Experience>>(Experience);
            var education = JsonConvert.DeserializeObject<List<School>>(Education);

            return new LinkedinProfile(Id, Name, Title, Position, Summary, skills, experience, education);
        }


        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Summary { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
    }
}