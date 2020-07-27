using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Desktop.LogInterface
{
    public interface ILogController
    {
        void saveInLog(string msgToSave);

        List<LogEntity> displayLog();

        List<LogEntity> displayLogBetweenDates(DateTime from, DateTime to);
    } 
}
