namespace Tyxel;

class Watcher : IDisposable
{
    Dictionary<string, bool> Buffer { get; } = new Dictionary<string, bool>();
    FileSystemWatcher FileSystemWatcher { get; }

    Action<string> Work { get; }
    int MillisecondsTimeout { get; }

    public Watcher(string filename, Action<string> work, int millisecondsTimeout = 10)
    {
        var path = Path.GetFullPath(filename);
        if (path == null)
            throw new NullReferenceException(nameof(path));

        Work = work;
        MillisecondsTimeout = millisecondsTimeout;
        FileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(path), Path.GetFileName(path)) { EnableRaisingEvents = true };
        ScheduleWork(path);
    }

    public void WaitForChange()
    {
        var change = FileSystemWatcher.WaitForChanged(WatcherChangeTypes.Changed);
        var filename = Path.GetFullPath(change.Name ?? throw new NullReferenceException(nameof(change.Name)));
        ScheduleWork(filename);
    }

    private void ScheduleWork(string filename)
    {
        if (!Buffer.TryGetValue(filename, out var scheduled) || !scheduled)
            lock (Buffer)
            {
                Buffer[filename] = true;

                ThreadPool.QueueUserWorkItem(state =>
                {
                    Thread.Sleep(MillisecondsTimeout);
                    lock (Buffer)
                    {
                        Buffer[filename] = false;
                        Work(filename);
                    }
                });
            }
    }

    public void Dispose()
    {
        ((IDisposable)FileSystemWatcher).Dispose();
    }
}
