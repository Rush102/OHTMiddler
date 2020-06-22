using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;

namespace OHTM.StatusMachine
{
    public class ConfigClass
    {

        #region private parameter
        private double same_AddressTimeout;
        private double same_CmdTimeout;
        private int delete_DueDate;
        private XDocument targetConfig;
        #endregion

        #region public parameter
        public double sameAddressTimeout { get => same_AddressTimeout; set => same_AddressTimeout = value; }
        public double sameCmdTimeout { get => same_CmdTimeout; set => same_CmdTimeout = value; }
        public int deleteDueDate { get => delete_DueDate; set => delete_DueDate = value; }
        #endregion

        public ConfigClass()
        {
            targetConfig = set_DirectPathOfConfig("timeSetting.xml");

            sameAddressTimeout = configTargetContent(targetConfig, "sameAddressTimeout");
            sameCmdTimeout = configTargetContent(targetConfig, "sameCmdTimeout");
            deleteDueDate = (int)configTargetContent(targetConfig, "deleteDueDate");
        }

        private double configTargetContent(XDocument target_Config , string element)
        {
            string targetValue_string = target_Config.Element("Domain").Element(element).Value;
            double targetValue = Convert.ToDouble(targetValue_string);
            return targetValue;
        }

        private static XDocument set_DirectPathOfConfig(string filename)
        {
            string configPath = "D:\\UserData\\Veh" + "\\config\\";
            return XDocument.Load(configPath + filename);
        }
    }
}
