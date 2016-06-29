using System;
using System.Collections.Generic;
using Linkediner.Models;

namespace Linkediner.Interfaces
{
    public interface IDataAccessor : IDisposable
    {
        void Init();
        void InsertProfile(LinkedinProfile profile);
        LinkedinProfile GetProfile(string id);
        List<LinkedinProfile> GetProfilesBySkills(List<string> skills);
    }
}