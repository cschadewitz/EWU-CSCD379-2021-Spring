using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        public static DataCollection<UserViewModel> Users = new DataCollection<UserViewModel>{
            new UserViewModel {Id = 0, FirstName = "James", LastName = "Holden"},
            new UserViewModel {Id = 1, FirstName = "Naomi", LastName = "Nagata"},
            new UserViewModel {Id = 2, FirstName = "Amos", LastName = "Burton"},
            new UserViewModel {Id = 3, FirstName = "Alex", LastName = "Kamal"},
            new UserViewModel {Id = 4, FirstName = "Josephus", LastName = "Miller"}
        };
        public static DataCollection<GroupViewModel> Groups = new DataCollection<GroupViewModel>{
            new GroupViewModel {Id = 0, GroupName = "Rocinante"},
            new GroupViewModel {Id = 1, GroupName = "Tycho Station"},
            new GroupViewModel {Id = 2, GroupName = "Medina Station"},
            new GroupViewModel {Id = 3, GroupName = "Inaros faction"}
        };
        public static DataCollection<GiftViewModel> Gifts = new DataCollection<GiftViewModel>{
            new GiftViewModel {Id = 0, Title = "FN SCAR-L CQC", Description = "Close quarters combat automatic shotgun", Priority = 1, Url = "https://www.test.com", UserId = 2},
            new GiftViewModel {Id = 1, Title = "Chiappa Rhino 50DS", Description = "Standard issue side arm for UN Black-Ops team", Priority = 1, Url = "https://www.test.com", UserId = 4}
        };
    }
}
