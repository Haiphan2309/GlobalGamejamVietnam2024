using System.Linq;
using System.Reflection;

namespace Shun_State_Machine
{
    public interface IStateParameter
    {
        public T Get<T>() where T : class, IStateParameter
        {
            return this as T;
        }
    }
}