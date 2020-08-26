using ShopHere.Database;
using ShopHere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHere.Services
{
    public class ConfigurationService
    {
        private readonly ProjectDbContext db = new ProjectDbContext();
        #region: Singleton Design Pattern
        private static ConfigurationService privateInMemoryObject { get; set; }
        public static ConfigurationService ClassObject
        {
            get
            {
                if (privateInMemoryObject == null)
                    return privateInMemoryObject = new ConfigurationService();

                return privateInMemoryObject;
            }
        }

        private ConfigurationService()
        {
        }
        #endregion


        public Config GetSingleConfiguration(string query)
        {
            var getSingleConfigurationData = db.Configs.FirstOrDefault(s => s.Key == query);

            if (getSingleConfigurationData != null)
            {
                return getSingleConfigurationData;
            }

            else
            {
                return new Config();
            }

        }


        public int PageSize()
        {
            var pazeSizeConfig = db.Configs.Find("PageSize");

            return pazeSizeConfig != null ? int.Parse(pazeSizeConfig.Value) : 5;
        }

        public int ShopPageSize()
        {
            var pazeSizeConfig = db.Configs.Find("ShopPageSize");

            return pazeSizeConfig != null ? int.Parse(pazeSizeConfig.Value) : 6;
        }
    }
}
