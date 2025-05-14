using UnityEngine;

public class UIController : MonoBehaviour, IInitializer
{
    #region Members : Public
    public UIPopupController UIPopupCrl { get; private set; }
    public UIPageController UIPageCrl { get; private set; }
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        Init();
    }
    #endregion


    #region Methods : Interface
    public void Init()
    {
        UIPopupCrl = GetComponentInChildren<UIPopupController>(true);
        UIPageCrl = GetComponentInChildren<UIPageController>(true);

        UIManager.Instance.RegisterUIController(this);
    }
    #endregion
}
