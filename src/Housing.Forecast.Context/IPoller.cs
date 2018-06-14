using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Forecast.Context
{
    public interface IPoller
    {
        void OnStart();
        void OnStop();
        void Update();
        void Poll();
    }
}
