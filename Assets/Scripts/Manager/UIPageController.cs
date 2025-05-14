using UnityEngine;
using System.Collections.Generic;

public class UIPageController : MonoBehaviour
{
    #region Mebers : Private
    private List<UIPage> pageList = new List<UIPage>();
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        var pageArr = GetComponentsInChildren<UIPage>(true);

        foreach(var page in pageArr)
        {
            pageList.Add(page);
            page.Init();
        }
    }
    #endregion

    #region Methods : Public
    public void OpenPage(UIPageCommand uIPageCommand)
    {
        var page = pageList.Find((page) => page.Id == uIPageCommand.Id);
        page.Open(uIPageCommand);

    }
    #endregion
}
