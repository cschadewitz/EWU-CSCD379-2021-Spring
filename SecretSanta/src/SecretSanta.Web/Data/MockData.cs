using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        private static DataCollection<UserViewModel> _Users = new DataCollection<UserViewModel>{
            new UserViewModel {FirstName = "James", LastName = "Holden"},
            new UserViewModel {FirstName = "Naomi", LastName = "Nagata"},
            new UserViewModel {FirstName = "Amos", LastName = "Burton"},
            new UserViewModel {FirstName = "Alex", LastName = "Kamal"},
            new UserViewModel {FirstName = "Josephus", LastName = "Miller"}
        };
        private static DataCollection<GroupViewModel> _Groups = new DataCollection<GroupViewModel>{
            new GroupViewModel {GroupName = "Rocinante"},
            new GroupViewModel {GroupName = "Tycho Station"},
            new GroupViewModel {GroupName = "Medina Station"},
            new GroupViewModel {GroupName = "Inaros faction"}
        };
        private static DataCollection<GiftViewModel> _Gifts = new DataCollection<GiftViewModel>{
            new GiftViewModel {Title = "FN SCAR-L CQC", Description = "Close quarters combat automatic shotgun", Priority = 1, Url = "https://www.test.com", UserId = 2},
            new GiftViewModel {Title = "Chiappa Rhino 50DS", Description = "Standard issue side arm for UN Black-Ops team", Priority = 1, Url = "https://www.test.com", UserId = 4}
        };

        public static DataCollection<UserViewModel> Users { get => _Users; }
        public static DataCollection<GroupViewModel> Groups { get => _Groups; }
        public static DataCollection<GiftViewModel> Gifts { get => _Gifts; }


    }
}
