using System;

namespace Linkediner.Models
{
    [Serializable]
    public class PersonToSkill
    {

        public PersonToSkill()
        {
            
        }

        public PersonToSkill(string id, string skill)
        {
            Id = id;
            Skill = skill;
        }

        public string Id { get; set; }

        public string Skill { get; set; }
    }
}