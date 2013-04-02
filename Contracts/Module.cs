using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;

namespace Contracts
{
    public class Module : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }
        [ConfigurationProperty("scheduler", IsRequired = true)]
        public string Scheduler
        {
            get
            {
                return this["scheduler"] as string;
            }
        }
        [ConfigurationProperty("interval", IsRequired = true)]
        public int Interval
        {
            get
            {
                try
                {
                    var interval = this["interval"].ToString();
                    if (string.IsNullOrEmpty(interval))
                    {
                        return 1;
                    }
                    return Convert.ToInt32(interval);
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
        }
        [ConfigurationProperty("other", IsRequired = true)]
        public string AdditionalConfig
        {
            get
            {
                var data = this["other"] as string;

                return data;
            }
        }
    }

    public class AdditionalConfig : DynamicObject
    {
        private Dictionary<string, object> _members =
            new Dictionary<string, object>();
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_members.ContainsKey(binder.Name))
            {
                result = _members[binder.Name];
                return true;
            }
            else
            {
                return base.TryGetMember(binder, out result);
            }
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (_members.ContainsKey(binder.Name)
                      && _members[binder.Name] is Delegate)
            {
                result = (_members[binder.Name] as Delegate).DynamicInvoke(args);
                return true;
            }
            else
            {
                return base.TryInvokeMember(binder, args, out result);
            }
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _members.Keys;
        }
    }
}