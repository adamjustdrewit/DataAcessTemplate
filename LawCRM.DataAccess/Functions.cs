using Template.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace Template.DataAccess
{
    internal class Functions
    {
        public static Func<KeyValuePair<string, EntityStatus>, bool> StatusIs(EntityStatus status)
        {
            return s => s.Value == status;
        }

        public static Func<KeyValuePair<string, EntityStatus>, string> Key()
        {
            return s => s.Key;
        }
    }
}
