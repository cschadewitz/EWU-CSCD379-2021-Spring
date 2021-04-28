using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class DataCollection<T> : List<T> where T : IData
    {
        public int NextId { get; set; }

        public DataCollection() : base()
        {
            NextId = 0;
        }
#pragma warning disable CS8603 // Possible null reference return.
        public new T this[int Id]
        {
            get
            {
                return this.SingleOrDefault(g => g.Id == Id);
            }
            set
            {
                base[this.FindIndex(g => g.Id == Id)] = value;
            }
        }
#pragma warning restore CS8603 // Possible null reference return.

        public new void Add(T data)
        {
            if (data is null)
                return;
            data.Id = NextId;
            base.Add(data);
            NextId++;
        }
    }
}
