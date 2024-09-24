using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutters.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Add(string key, object value, int duration);
        object Get(string key);
        bool IsAdd(string key); //cahe de varmı?
        void Remove(string key);
        void RemoveByPattern(string pattern);//pattern içerenleri cacheden sil.
    }
}
