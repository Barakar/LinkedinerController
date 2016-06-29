using System;
using System.Collections.Generic;

namespace Linkediner.Models
{
    [Serializable]
    public class LinkedinProfile
    {
        public LinkedinProfile(string id, string name, string title, string position, string summary, List<string> skills, List<Experience> experience, List<School> education)
        {
            Id = id;
            Name = name;
            Title = title;
            Position = position;
            Summary = summary;
            Skills = skills;
            Experience = experience;
            Education = education;
        }

        public LinkedinProfile()
        {
            
        }

        public string Id { get; set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Position { get; private set; }
        public string Summary { get; private set; }
        public List<string> Skills { get; private set; }
        public List<Experience> Experience { get; private set; }
        public List<School> Education { get; private set; }
    }
}