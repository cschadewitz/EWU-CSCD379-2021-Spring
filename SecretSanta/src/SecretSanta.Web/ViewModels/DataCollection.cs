using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Web.ViewModels
{
    public class DataCollection<T> : List<T> where T : IDataViewModel
    {
        public int NextId { get; set; }

        public DataCollection(IEnumerable<T> items) : base(items)
        {
            NextId = base.Count;
        }
        public new T this[int Id]
        {
            get
            {
                try
                {
                    return this.Find(g => g.Id == Id);
                }
                catch (ArgumentNullException x)
                {
                    return null;
                }
            }
            set
            {
                base[this.FindIndex(g => g.Id == Id)] = value;
            }
        }

        public new void Add(T data)
        {
            data.Id = NextId;
            base.Add(data);
            NextId++;
        }
    }
}
