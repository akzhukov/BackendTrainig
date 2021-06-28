using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Shared.Extensions
{
    public static class ParallelExtention
    {

        public static async Task RunInParallel(this IEnumerable<Func<Task>> tasks, ILogger logger, int numParallelTasks = 4)
        {
            var semaphoreSlim = new SemaphoreSlim(numParallelTasks, numParallelTasks);

            await Task.WhenAll(tasks.Select(task => resolveTask(task)));

            async Task resolveTask(Func<Task> task)
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    await task().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error on execution async method.");
                }

                semaphoreSlim.Release();
            }
        }

    }
}
