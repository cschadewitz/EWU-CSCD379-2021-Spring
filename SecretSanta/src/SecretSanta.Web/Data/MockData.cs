using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        private static int usersNextId = 3;
        private static int groupsNextId = 3;

        public static List<UserViewModel> Users = new List<UserViewModel>{
            new UserViewModel {Id = 0, FirstName = "James", LastName = "Holden"},
            new UserViewModel {Id = 1, FirstName = "Naomi", LastName = "Nagata"},
            new UserViewModel {Id = 2, FirstName = "Amos", LastName = "Burton"},
            new UserViewModel {Id = 3, FirstName = "Alex", LastName = "Kamal"}
        };
        public static int UsersNextId()
        {
            return ++usersNextId;
        }
        public static List<GroupViewModel> Groups = new List<GroupViewModel>{
            new GroupViewModel {Id = 0, GroupName = "Rocinante"},
            new GroupViewModel {Id = 1, GroupName = "Tycho Station"},
            new GroupViewModel {Id = 2, GroupName = "Medina Station"},
            new GroupViewModel {Id = 3, GroupName = "Inaros faction"}
        };
        public static int GroupsNextId()
        {
            return ++groupsNextId;
        }
    }
}
