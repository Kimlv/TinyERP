namespace App.Security.Aggregate
{
    using System;
    using System.Collections.Generic;
    public class Roles : List<Role>
    {
        public IList<string> ToList()
        {
            IList<string> keys = new List<string>();
            foreach (Role role in this)
            {
                keys.Add(role.Key);
            }
            return keys;
        }
    }
}
