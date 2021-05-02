using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public interface IUserRepository : IEntityRepository
    {
        ICollection<User> List();
        User? GetItem(int id);
        bool Remove(int id);
        User Create(User item);
        void Save(User item);
    }
}
