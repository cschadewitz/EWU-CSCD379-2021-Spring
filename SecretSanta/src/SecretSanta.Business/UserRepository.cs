using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class UserRepository : IUserRepository
    {
        public User Create(User item)
        {
            StaticData.Users.Add(item);
            return item;
        }

        public User? GetItem(int id)
        {
            return StaticData.Users[id];
        }

        public ICollection<User> List()
        {
            return StaticData.Users;
        }

        public bool Remove(int id)
        {
            if (StaticData.Users[id] is null)
                return false;
            StaticData.Users.Remove(StaticData.Users[id]);
            return true;
        }

        public void Save(User item)
        {
            if (item is null || (item is not null && StaticData.Users[item.Id] is null))
                return;
            StaticData.Users[item.Id] = item;
        }
    }
}
