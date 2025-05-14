
public class UIPage : UIBase
{
    #region Members : Public
    public string Id { get; protected set; }
    #endregion

    #region Methods : Override
    public override void Open(UICommand uICommand)
    {
        if (uICommand.isActive)
        {
            gameObject.SetActive(uICommand.isActive);
            SetData(uICommand.GetData());
            SetCallback(uICommand.GetCallbacks());
            OpenPage();
        }
        else
            Close();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Dispose();
    }
    #endregion

    #region Methods : Virtual
    public virtual void OpenPage() { }
    #endregion

}
