using System.Threading.Tasks;

namespace ITMOSchedule.Extensions
{
    public static class Extensions
    {
        public static void WaitSafe(this Task task)
        {
            Task.WaitAny(task);
        }

        public static void WaitSafe<T>(this Task<T> task)
        {
            Task.WaitAny(task);
        }
    }
}