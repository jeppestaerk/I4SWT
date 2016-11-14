using AirTrafficMonitoring.EventHandler;
using AirTrafficMonitoring.EventPublisher;
using AirTrafficMonitoring.Output;
using AirTrafficMonitoring.Receiver;
using AirTrafficMonitoring.Validator;
using TransponderReceiver;

namespace AirTrafficMonitoring.ConsoleApplication
{
  class Program
  {
    static void Main(string[] args)
    {
      var receiver = new TransponderDataReceiver(TransponderReceiverFactory.CreateTransponderDataReceiver(), "yyyyMMddHHmmssFFFF");
      var validator = new AirSpaceValidator(receiver, 10000, 90000, 10000, 90000, 500, 20000);
      var eventListGenerator = new EventListGenerator();
      var eventPublisherEbteryLeft = new EventPublisherEntryLeft(eventListGenerator, validator);
      var eventPublisherSeperation= new EventPublisherSeperation(eventListGenerator, validator);
      var eventContext = new EventContext(eventListGenerator, validator);
      var log = new Log(eventListGenerator);
      var screen = new Screen(eventListGenerator, validator);

      while(true)
      {

      }
    }
  }
}
