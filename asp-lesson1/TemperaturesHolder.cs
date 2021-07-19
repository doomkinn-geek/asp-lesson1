using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_lesson1
{
    public class TemperaturesHolder
    {
        private readonly List<Temperature> _values;
        public List<Temperature> Values => _values;      
        public TemperaturesHolder()
        {
            _values = new List<Temperature>();
        }
        public bool Add(string date, int temperature, out string err)
        {
            try
            {
                Values.Add(new Temperature { Date = Convert.ToDateTime(date), Value = temperature });
            }
            catch(Exception e)
            {
                err = e.Message;
                return false;
            }

            err = null;
            return true;
        }
        public int Change(string date, int temperature, out string errMessage)
        {
            try
            {
                int foundIndex = Values.FindIndex(index => index.Date == Convert.ToDateTime(date));
                if (foundIndex == -1)
                {
                    errMessage = "Requested date is not found";
                    return -1;
                }
                else
                {
                    Values[foundIndex].Value = temperature;
                    errMessage = null;
                    return 0;
                }
            }
            catch (Exception e)
            {
                errMessage = e.Message;
                return -1;
            }
        }

        public List<Temperature> Get(DateTime start, DateTime end, out string err)
        {
            //Воевал с лямбда выражениями, так и не смог заставить работать
            /*err = null;            
            return Values
                    .Where(x => (x.Date > start) && (x.Date < end))
                        as List<Temperature>;
            */

            try
            {
                List<Temperature> result = new List<Temperature>();
                foreach (Temperature t in Values)
                {
                    if (t.Date >= start && t.Date <= end)
                    {
                        result.Add(t);
                    }
                }

                err = null;
                return result;
            }
            catch (Exception e)
            {
                err = e.Message;
                return null;
            }
        }

        public List<Temperature> SelectRecords(DateTime start, DateTime end, out string err)
        {
            try
            {
                err = null;
                return Values
                    .Where(x => (x.Date > start) && (x.Date < end))
                    .GroupBy(x => x.Date.Date).Select(y => y.ToList()) as List<Temperature>;
            }
            catch (Exception e)
            {
                err = e.Message;
                return null;
            }

        }
    }
}
