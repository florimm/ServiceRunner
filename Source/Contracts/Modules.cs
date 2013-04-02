using System.Collections.Generic;
using System.Configuration;

namespace Contracts
{
    public class Modules
        : ConfigurationElementCollection
    {
        public Module this[int index]
        {
            get
            {
                return base.BaseGet(index) as Module;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public List<Module> ToList()
        {
            var m = new List<Module>();
            for (int i = 0; i < this.BaseGetAllKeys().Length; i++)
            {
                m.Add(this[i]);
            }
            return m;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Module();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Module)element).Name;
        }
    }
}