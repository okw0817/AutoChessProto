using System;
using System.Collections.Generic;

public class UIPage_Store : UIPage
{
    #region Methods : Mono

    public override void Init()
    {
        Id = UIPageString.Store.ToString();
    }
    #endregion

    #region Methods : Override
    public override void SetData(Dictionary<string, object> data)
    {
        base.SetData(data);

        SetTitle((string)data[UIDataType.Title.ToString()]);
        SetContent((string)data[UIDataType.Content.ToString()]);

    }

    public override void SetCallback(Dictionary<string, Action> callbacks)
    {
        base.SetCallback(callbacks);

        string callbackKey = UIDataType.Callback.ToString();
        if (callbacks.ContainsKey(callbackKey))
        {
            callbacks[callbackKey].Invoke();
        }
    }

    #endregion
}
