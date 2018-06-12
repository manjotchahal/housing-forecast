using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Housing.Forecast.Context
{
    public class Poller
    {
        private readonly ForecastContext _context;
        private readonly int IdleCycle;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Task mainTask = null;

        public Poller(ForecastContext context)
        {
            _context = context;
        }

        public void OnStart()
        {
            mainTask = new Task(Poll, cts.Token, TaskCreationOptions.LongRunning);
            mainTask.Start();
        }

        public void OnStop()
        {
            cts.Cancel();
            mainTask.Wait();
        }

        public void Update()
        {

        }

        public void Poll()
        {
            CancellationToken cancellation = cts.Token;
            TimeSpan interval = TimeSpan.FromDays(1);
            while (!cancellation.WaitHandle.WaitOne(interval))
            {
                try
                {
                    Update();
                    if (cancellation.IsCancellationRequested)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    // Log the exception
                    //string fromTimeString = result.ToString(“hh’:’mm”);
                    //interval = Convert.ToDouble(ConfigurationSettings.AppSettings[“WaitAfterSuccessInterval”]);
                    //interval = TimeSpan.FromMinutes((ConfigurationSettings.AppSettings[“WaitAfterSuccessInterval”]));
                    //interval = TimeSpan.FromMinutes((Convert.ToDouble(ConfigurationSettings.AppSettings[“WaitAfterSuccessInterval”])));
                    //TimeSpan result = TimeSpan.FromMinutes(interval);
                }
            }
        }

    }
}


