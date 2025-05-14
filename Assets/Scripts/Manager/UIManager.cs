
public class UIManager : ManagerBase<UIManager>, IReceiver, IObserver<Command>
{
    #region Members : Private
    private UIController uiController;
    private ReciverType reciverType;
    #endregion

    #region Methods : Override
    public override void Init()
    {
        CommandManager.Instance.AddObserver(this); //CommandManager�� Observer�� ���
        reciverType = ReciverType.UIManager;

        base.Init();
    }
    #endregion

    #region Methods : Interface
    public void Receive(Command command)
    {
        if(command is UIPageCommand)
        {
            uiController.UIPageCrl.OpenPage(command as UIPageCommand);
        }
        else
        {
            uiController.UIPopupCrl.OpenPopup(command as UIPopupCommand);
        }
    }

    public ReciverType GetReciverType()
    {
        return reciverType;
    }
    #endregion

    #region Methods : Public
    public void RegisterUIController(UIController uiController) //UIController�� UIPage�� IUPop�� ����.
    {
        this.uiController = uiController;
    }

    public void Notify(Command observer)
    {
        Receive(observer);
    }

    #endregion
}
