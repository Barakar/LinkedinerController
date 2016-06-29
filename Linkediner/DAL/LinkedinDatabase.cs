using Dapper;
using Linkediner.Models;

namespace Linkediner.DAL
{
    public class LinkedinDatabase : Database<LinkedinDatabase>
    {
        public Table<LinkedinProfile> Profiles { get; set; }
        public Table<PersonToSkill> PersonToSkill { get; set; }

    }
}