using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

public class UIBase : MonoBehaviour, IUIOpen, IUIClose, IInitializer, IDispose
{
    #region Members : Private
    private Dictionary<string, object> commandData;
    private Dictionary<string, Action> callbacks;

    [SerializeField]
    private TextMeshProUGUI text_title;
    [SerializeField]
    private TextMeshProUGUI text_content;

    #endregion

    #region Methods : Interface
    public virtual void Open(UICommand uICommand) { }
    public virtual void Close() { }

    public virtual void Init() { }

    public virtual void Dispose()
    {
        commandData = null;
        callbacks = null;
    }
    #endregion

    #region Methods : Virtual
    public virtual void SetData(Dictionary<string, object> data) { this.commandData = data; }
    public virtual void SetCallback(Dictionary<string, Action> callbacks) { this.callbacks = callbacks; }
    #endregion

    #region Methods : Public
    public void SetTitle(string title)
    {
        if (text_title != null)
            text_title.text = title;
    }

    public void SetContent(string content)
    {
        if (text_content != null)
            text_content.text = content;
    }
    #endregion
}
