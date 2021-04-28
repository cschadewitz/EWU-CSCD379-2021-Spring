using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Business;

namespace SecretSanta.Data
{
    public static class StaticData
    {
        public static DataCollection<User> Users { get; } = new()
        {
            new User { Id = 0, FirstName = "Inigo", LastName = "Montoya" },
            new User { Id = 1, FirstName = "Princess", LastName = "Buttercup" },
            new User { Id = 2, FirstName = "Prince", LastName = "Humperdink" },
            new User { Id = 3, FirstName = "Count", LastName = "Rugen" },
            new User { Id = 4, FirstName = "Miracle", LastName = "Max" },
        };
    }
}
