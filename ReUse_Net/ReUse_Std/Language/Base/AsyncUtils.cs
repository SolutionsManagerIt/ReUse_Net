using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReUse_Std.Base
{
    public static class AsyncUtils
    {
        /// <summary>
        /// Run current TasksToRun using Task.WhenAll 
        /// </summary>
        public static async Task<IEnumerable<T>> R<T>(this IEnumerable<Task<T>> TasksToRun)
        {
            return await Task.WhenAll(TasksToRun);
        }

        /// <summary>
        /// Run current TasksToRun using Task.WhenAll 
        /// </summary>
        public static async Task R(this IEnumerable<Task> TasksToRun)
        {
            await Task.WhenAll(TasksToRun);
        }

        /// <summary>
        /// Run current async TaskToRun synchronously using Wait 
        /// </summary>
        public static void r(this Task TaskToRun)
        {
            TaskToRun.Wait();
        }

        /// <summary>
        /// Run current async TaskToRun synchronously using Result
        /// </summary>
        public static T r<T>(this Task<T> TaskToRun)
        {
            return TaskToRun.Result;
        }

    }
}
