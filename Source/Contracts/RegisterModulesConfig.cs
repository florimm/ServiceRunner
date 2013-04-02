using System.Configuration;

namespace Contracts
{
    public class RegisterModulesConfig
        : ConfigurationSection
    {

        public static RegisterModulesConfig GetConfig()
        {
            return (RegisterModulesConfig)ConfigurationManager.GetSection("RegisterModules") ?? new RegisterModulesConfig();
        }

        [ConfigurationProperty("Modules")]
        public Modules Modules
        {
            get
            {
                object o = this["Modules"];
                return o as Modules;
            }
        }

    }
}