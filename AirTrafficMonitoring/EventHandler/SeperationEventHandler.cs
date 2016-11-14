using System.Threading;
using AirTrafficMonitoring.EventPublisher;

namespace AirTrafficMonitoring.EventHandler
{

  public class SeperationEventHandler : IEventHandler
  {

    public IEventListGenerator elg { get; set; }

    public SeperationEventHandler(IEventListGenerator elge)
    {
      elg = elge;
    }

    public void HandleEventThreadFunc(IEventObj eventObj)
    {
      //elg.DeleteEventFromList(eventObj);
    }

    public void Handle(IEventObj eventObj)
    {
      Thread handleThread = new Thread(() => HandleEventThreadFunc(eventObj));
      handleThread.Start();
    }

  }
}
