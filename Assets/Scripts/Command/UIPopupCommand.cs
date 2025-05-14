
public class UIPopupCommand : UICommand
{
    #region Methods : Constructor
    public UIPopupCommand(UIPopupString uIPopupString, bool active)
    {
        Id = uIPopupString.ToString();
        reciverType = ReciverType.UIManager;
        isActive = active; //Command 요청이 Open인지 Close인지 확인하는 파라미터
    }
    #endregion
}
 