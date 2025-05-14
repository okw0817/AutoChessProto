
public class UIPageCommand : UICommand
{
    #region Methods : Constructor
    public UIPageCommand(UIPageString uIPageString, bool active)
    {
        Id = uIPageString.ToString();
        reciverType = ReciverType.UIManager;
        isActive = active; //Command 요청이 Open인지 Close인지 확인하는 파라미터
    }
    #endregion
}
