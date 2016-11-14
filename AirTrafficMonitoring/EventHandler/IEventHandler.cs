using AirTrafficMonitoring.EventPublisher;

namespace AirTrafficMonitoring.EventHandler
{
  public interface IEventHandler
  {
    IEventListGenerator elg { get; set; }

    void Handle(IEventObj eventObj);


  }
}
