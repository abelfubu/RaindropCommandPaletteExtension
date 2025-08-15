namespace Raindrops.Helpers;

internal sealed partial class Debouncer : IDisposable
{
    private CancellationTokenSource cts = new();

    public async Task DebounceAsync(Func<Task> action, int delayMs)
    {
        cts.Cancel();
        cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(delayMs, cts.Token);
            await action();
        }
        catch (TaskCanceledException)
        {

        }
    }

    public CancellationToken GetToken()
    {
        return cts.Token;
    }

    public void Dispose()
    {
        cts.Dispose();
    }
}