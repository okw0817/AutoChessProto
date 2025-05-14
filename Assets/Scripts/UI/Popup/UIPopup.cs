using UnityEngine;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    #region Members : Private
    [SerializeField]
    Button closeButton;
    #endregion
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
            OpenPopup();

            closeButton.onClick.AddListener(() => Close());
        }
        else
            Close();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Dispose();
    }

    public override void Dispose()
    {
        closeButton.onClick.RemoveAllListeners();
        base.Dispose();
    }
    #endregion

    #region Methods : Virtual
    public virtual void OpenPopup() { }
    #endregion
}
