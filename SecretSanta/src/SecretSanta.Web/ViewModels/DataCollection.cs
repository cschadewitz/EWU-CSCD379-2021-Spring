using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Web.ViewModels
{
    public class DataCollection<T> : List<T> where T : IDataViewModel
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
                try
                {
                    return this.Find(g => g.Id == Id);
                }
                catch (ArgumentNullException x)
                {
                    return default(T);
                }
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
