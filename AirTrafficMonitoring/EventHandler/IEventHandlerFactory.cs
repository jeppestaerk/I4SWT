using AirTrafficMonitoring.EventPublisher;
using AirTrafficMonitoring.Validator;

namespace AirTrafficMonitoring.EventHandler
{
  public interface IEventHandlerFactory
  {
    IEventHandler CreateEventHandler(IEventObj typeOfEventObj, IEventListGenerator elg);
  }
}