namespace DowntimeAlerter
{
    public interface IStartupTask
    {
        void Execute();

        int Order { get; }
    }
}