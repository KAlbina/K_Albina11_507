namespace Kozlova_Albina;
//  2
public class TaskDispatcher
{
    private readonly List<Action> _tasks = new();

    private readonly Semaphore _semaphore = new(5, 5);

    private int _currentRunning;
    private int _maxObservedConcurrency;

    public int MaxObservedConcurrency => _maxObservedConcurrency;

    public void AddTask(Action task)
    {
        ArgumentNullException.ThrowIfNull(task);
        _tasks.Add(task);
    }

    public void Run()
    {
        var threads = new List<Thread>(_tasks.Count);

        foreach (Action task in _tasks)
        {
            var thread = new Thread(() =>
            {
                _semaphore.WaitOne();
                try
                {
                    int running = Interlocked.Increment(ref _currentRunning);
                    UpdateMax(running);

                    task();
                }
                finally
                {
                    Interlocked.Decrement(ref _currentRunning);
                    _semaphore.Release();
                }
            });

            threads.Add(thread);
            thread.Start();
        }

        foreach (Thread thread in threads)
            thread.Join();
    }

    private void UpdateMax(int running)
    {
        int observed;
        do
        {
            observed = _maxObservedConcurrency;
            if (running <= observed)
                return;
        }
        while (Interlocked.CompareExchange(ref _maxObservedConcurrency, running, observed) != observed);
    }
}
