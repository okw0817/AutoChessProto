
public class UIPopupCommand : UICommand
{
    #region Methods : Constructor
    public UIPopupCommand(UIPopupString uIPopupString, bool active)
    {
        Id = uIPopupString.ToString();
        reciverType = ReciverType.UIManager;
        isActive = active; //Command ��û�� Open���� Close���� Ȯ���ϴ� �Ķ����
    }
    #endregion
}
 