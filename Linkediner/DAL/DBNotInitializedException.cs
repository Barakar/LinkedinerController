using System;

namespace Linkediner.DAL
{
    public class DBNotInitializedException : Exception
    {
        public DBNotInitializedException(string message)
            : base(message)
        {

        }

        public DBNotInitializedException()
        {

        }
    }
}