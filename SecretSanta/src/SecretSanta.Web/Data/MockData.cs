using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        public static List<UserViewModel> Users = new List<UserViewModel>{
            new UserViewModel {Id = 0, FirstName = "James", LastName = "Holden"},
            new UserViewModel {Id = 1, FirstName = "Naomi", LastName = "Nagata"},
            new UserViewModel {Id = 2, FirstName = "Amos", LastName = "Burton"},
            new UserViewModel {Id = 3, FirstName = "Alex", LastName = "Kamal"}
        };
    }
}
