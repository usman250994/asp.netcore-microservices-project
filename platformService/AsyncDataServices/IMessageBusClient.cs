using platformService.Dtos;

namespace platformService.AsyncDataServices

{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto platformPublishDto);
    }

}