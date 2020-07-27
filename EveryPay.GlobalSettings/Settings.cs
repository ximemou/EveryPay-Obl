using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.GlobalSettings
{
    public class Settings
    {


        private static Settings settings =null;

        private int moneyForPoint;

        public static Settings GetInstance()
        {
            if (settings == null)
            {
                return new Settings();
            }
            return settings;

        }

        private Settings()
        {
            moneyForPoint = 150;
        }


        public int GetMoneyForPoint()
        {
            return moneyForPoint;
        }
        public void SetMoneyForPoint(int money)
        {
            this.moneyForPoint = money;
        }

    }
}
