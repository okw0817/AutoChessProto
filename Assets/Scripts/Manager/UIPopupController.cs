using System.Collections.Generic;
using UnityEngine;

public class UIPopupController : MonoBehaviour
{
    #region Mebers : Private
    private List<UIPopup> popupList = new List<UIPopup>();
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        var popupArr = GetComponentsInChildren<UIPopup>(true);

        foreach (var popup in popupArr)
        {
            popupList.Add(popup);
            popup.Init();
        }
    }
    #endregion

    #region Methods : Public
    public void OpenPopup(UIPopupCommand uIPopupCommand)
    {
        var popup = popupList.Find((page) => page.Id == uIPopupCommand.Id);
        popup.Open(uIPopupCommand);
    }
    #endregion
}
