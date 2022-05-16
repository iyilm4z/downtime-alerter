namespace DowntimeAlerter.Notification
{
    public interface INotifier
    {
        void Nofify(NotifyModel notifyModel);
    }
}