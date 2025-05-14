public interface IReceiver
{
    public void Receive(Command command);
    public ReciverType GetReciverType();
}
