using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public interface IRepository<T> : IEntityRepository
    {
        ICollection<T> List();
        T? GetItem(int id);
        bool Remove(int id);
        T Create(T item);
        void Save(T item);
    }
}
