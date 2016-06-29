using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;
using Linkediner.Interfaces;
using Linkediner.Models;

namespace Linkediner.DAL
{
    public class SQLiteDataAccessor : IDataAccessor
    {
        private readonly SQLiteConnection _sqLiteConnection;
        private LinkedinDatabase _linkedinDatabase;
        private bool _init;
        private readonly object _lockObj = new object();
        private const string ConnectionString = "Data Source='{0}';Version=3;";

        public SQLiteDataAccessor(string dbFile)
        {
            _init = false;
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }

            _sqLiteConnection = new SQLiteConnection(string.Format(ConnectionString, dbFile));
        }

        public void Init()
        {
            if (_init) return;

            _sqLiteConnection.Open();

            CreateDBTables();

            CreateDBIndexes();

            _linkedinDatabase = LinkedinDatabase.Init(_sqLiteConnection, 2);

            _init = true;
        }

        private void CreateDBTables()
        {
            _sqLiteConnection.Execute(@"create table if not exists Profiles (
                                      Id varchar(500) PRIMARY KEY, Name varchar(50), Title varchar(100), 
                                      Position varchar(100), Summary varchar(1000), Skills varchar(100), Experience varchar(100), 
                                      Education varchar(100))");

            _sqLiteConnection.Execute("create table if not exists PersonToSkill (" +
                                      "Id varchar(500), Skill varchar(50))");
        }

        private void CreateDBIndexes()
        {
            _sqLiteConnection.Execute("CREATE INDEX if not exists person_id_idx ON Profiles (Id)");
        }

        public void InsertProfile(LinkedinProfile profile)
        {
            CheckInitalization();

            var jsonProfile = new JsonLinkedinProfile(profile);
            lock (_lockObj)
            {
                _linkedinDatabase.BeginTransaction();

                try
                {
                    _linkedinDatabase.Profiles.InsertOrUpdate(jsonProfile);

                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@param", profile.Id);

                    _linkedinDatabase.Query("delete from PersonToSkill where Id = @param", dynamicParameters);
                    foreach (var skill in profile.Skills)
                    {
                        _linkedinDatabase.PersonToSkill.Insert(new PersonToSkill(profile.Id, skill));
                    }

                    _linkedinDatabase.CommitTransaction();
                }
                catch (Exception)
                {
                    //TODO: logs.
                    _linkedinDatabase.RollbackTransaction();
                    throw;
                }
            }
        }

        public LinkedinProfile GetProfile(string id)
        {
            CheckInitalization();

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@param", id);

            return _linkedinDatabase.Query<JsonLinkedinProfile>("select * from Profiles where Id = @param", dynamicParameters)
                .First().ToLinkedinProfile();
        }

        public List<LinkedinProfile> GetProfilesBySkills(List<string> skills)
        {
            CheckInitalization();

            var ids = skills.Aggregate("", (current, skill) => current + ("'" + skill + "',"), result => result.TrimEnd(','));

            var format = string.Format("select * from Profiles p where p.Id in " +
                                       "(select Id from PersonToSkill pts where pts.Skill in ({0}))", ids);

            var profiles = _linkedinDatabase.Query<JsonLinkedinProfile>(format).ToList();

            return profiles.Select(profile => profile.ToLinkedinProfile()).ToList();
        }

        private void CheckInitalization()
        {
            if (!_init)
            {
                throw new DBNotInitializedException();
            }
        }

        public void Dispose()
        {
            _sqLiteConnection.Dispose();
        }
    }
}