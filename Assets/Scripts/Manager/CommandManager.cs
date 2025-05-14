using System.Collections.Generic;

public class CommandManager : ManagerBase<CommandManager>, ISubject<IReceiver>
{
    #region Members : Private
    private List<IReceiver> observers = new List<IReceiver>();
    #endregion

    #region Methods : Override
    public override void Init()
    {
        base.Init();
    }
    #endregion

    #region Methods : Public
    public void Excute(Command command) //Command Type�� �´� Command�� ����
    {
        foreach(var receiver in observers)
        {
            if(receiver is IObserver<Command>)
            {
                if (receiver.GetReciverType() == command.reciverType)
                {
                    (receiver as IObserver<Command>).Notify(command);
                }
            }
        }
    }

    public void AddObserver(IReceiver observer) //Oberserver �߰�
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void DeleteObserver(IReceiver observer) //Observer ����
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }
    #endregion
}
