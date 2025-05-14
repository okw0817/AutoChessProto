
public class Command : ICommand
{
    #region Members : Public
    public ReciverType reciverType { get; protected set; }
    #endregion

    #region Methods : Constructor
    public Command()
    {
    }
    #endregion

    #region Methods : Interface
    public void Excute()
    {
        CommandManager.Instance.Excute(this);
    }
    #endregion
}
