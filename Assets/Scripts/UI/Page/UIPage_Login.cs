using System;
using System.Collections.Generic;

public class UIPage_Login : UIPage
{
    #region Methods : Mono

    public override void Init()
    {
        Id = UIPageString.Login.ToString();
    }
    #endregion

    #region Methods : Override
    public override void SetData(Dictionary<string, object> data)
    {
        base.SetData(data);

        string title = UIDataType.Title.ToString();
        if(data.ContainsKey(title))
        {
            SetTitle((string)data[title]);
        }

        string content = UIDataType.Content.ToString();
        if(data.ContainsKey(content))
        {
            SetContent((string)data[content]);
        }

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
