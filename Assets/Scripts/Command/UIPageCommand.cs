
public class UIPageCommand : UICommand
{
    #region Methods : Constructor
    public UIPageCommand(UIPageString uIPageString, bool active)
    {
        Id = uIPageString.ToString();
        reciverType = ReciverType.UIManager;
        isActive = active; //Command ��û�� Open���� Close���� Ȯ���ϴ� �Ķ����
    }
    #endregion
}
