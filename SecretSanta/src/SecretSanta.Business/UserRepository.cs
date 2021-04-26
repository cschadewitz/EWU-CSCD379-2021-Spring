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
        public static DataCollection<User> _Users = new DataCollection<User>
        {
            new User { Id = 0, FirstName = "Inigo", LastName = "Montoya" },
            new User { Id = 1, FirstName = "Princess", LastName = "Buttercup" },
            new User { Id = 2, FirstName = "Prince", LastName = "Humperdink" },
            new User { Id = 3, FirstName = "Count", LastName = "Rugen" },
            new User { Id = 4, FirstName = "Miracle", LastName = "Max" },
        };
        public User Create(User item)
        {
            _Users.Add(item);
            return item;
        }

        public User? GetItem(int id)
        {
            return _Users[id];
        }

        public ICollection<User> List()
        {
            return _Users;
        }

        public bool Remove(int id)
        {
            if (_Users[id] is null)
                return false;
            _Users.Remove(_Users[id]);
            return true;
        }

        public void Save(User item)
        {
            if (item is not null && _Users[item.Id] is null)
                return;
            _Users[item.Id] = item;
        }
    }
}
