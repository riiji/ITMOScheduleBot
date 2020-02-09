using ITMOSchedule.Vk;

namespace ITMOSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            VkAuthorize auth = new VkAuthorize();
            auth.Login().Build();

        }
    }
}
