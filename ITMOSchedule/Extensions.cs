using System.Threading;
using System.Threading.Tasks;

namespace ITMOSchedule
{
    //TODO: move to folder
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