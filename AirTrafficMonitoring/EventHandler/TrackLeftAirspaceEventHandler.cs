using System.Threading;
using AirTrafficMonitoring.EventPublisher;

namespace AirTrafficMonitoring.EventHandler
{
  public class TrackLeftAirspaceEventHandler : IEventHandler
  {
    public IEventListGenerator elg { get; set; }

    public TrackLeftAirspaceEventHandler(IEventListGenerator elge)
    {
      elg = elge;
    }

    public void HandleEventThreadFunc(IEventObj eventObj)
    {
      Thread.Sleep(5000);
      elg.DeleteEventFromList(eventObj);
    }


    public void Handle(IEventObj eventObj)
    {
      Thread handleThread = new Thread(() => HandleEventThreadFunc(eventObj));
      handleThread.Start();
    }
  }
}
