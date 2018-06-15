using Housing.Forecast.Context.ApiAccessors;
using Housing.Forecast.Context.Models;

namespace Housing.Forecast.Context
{
    public interface IPoller
    {
        void OnStart();
        void OnStop();
        void Update();
        void Poll();
        void UpdateAddress(Address check);
        void UpdateBatches(ApiMethods api);
        void UpdateName(Name check);
        void UpdateRooms(ApiMethods api);
        void UpdateUsers(ApiMethods api);
    }
}
