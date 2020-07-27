using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Desktop.LogInterface;
using EveryPay.Data.Repository;
using EveryPay.Data.Entities;

namespace EveryPay.LogManager
{
    public class LogController : ILogController
    {
        private readonly IUnitOfWork unitOfWork;

        public LogController()
        {
            unitOfWork = new UnitOfWork();
        }

        public void saveInLog(string msgToSave)
        {
            string[] splitted = msgToSave.Split('|');

            string action = splitted[0];
            DateTime date = convertStringToDate(splitted[1]);
            string userName = splitted[2];

            Log log = new Log(action, date, userName);
            unitOfWork.LogRepository.Insert(log);
            unitOfWork.Save();
        }

        public List<LogEntity> displayLog()
        {
            List<Log> logsAsList = unitOfWork.LogRepository.Get().ToList();
            return convertListToEntities(logsAsList);
        }

        public List<LogEntity> displayLogBetweenDates(DateTime from, DateTime to)
        {

            
            List<Log> logsAsList = unitOfWork.LogRepository.Get(s => s.Date >= from && s.Date<to).ToList();
            return convertListToEntities(logsAsList);
        }

     
        private List<LogEntity> convertListToEntities(List<Log> logsAsList)
        {
            List<LogEntity> logsAsEntities = new List<LogEntity>();

            foreach (Log log in logsAsList)
            {
                LogEntity entity = new LogEntity(convertStringToEnum(log.Action), log.Date, log.UserName);
                logsAsEntities.Add(entity);
            }

            return logsAsEntities;
        }

        private LogAction convertStringToEnum(string action)
        {
            LogAction converted = (LogAction)Enum.Parse(typeof(LogAction), action);
            return converted;
        }

        private DateTime convertStringToDate(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
