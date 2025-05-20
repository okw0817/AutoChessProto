using System.Collections;
using System.Collections.Generic;
using System;

public class SceneCommand : Command
{
    #region Members : Private
    private Dictionary<string, object> commandData = new Dictionary<string, object>();
    private Dictionary<string, Action> callbacks = new Dictionary<string, Action>();
    #endregion

    #region Members : Public
    public bool isAddictive { get; protected set; }
    public bool isClosePrevScene { get; protected set; }
    #endregion

    #region Members : Protected
    public string Id { get; protected set; }
    #endregion

    #region Methods : Constructor
    public SceneCommand(SceneName sceneName, bool isAddictive, bool isClosePrevScene = true)
    {
        Id = sceneName.ToString();
        this.isAddictive = isAddictive;
        this.isClosePrevScene = isClosePrevScene;
        reciverType = ReciverType.SceneLoadManager;

    }
    #endregion

    #region Methods : Public
    public void SetData(string key, object data)
    {
        if (!commandData.ContainsKey(key))
            commandData.Add(key, data);
    }

    public Dictionary<string, object> GetData()
    {
        return commandData;
    }

    public void SetCallback(string key, Action callback)
    {
        if (!callbacks.ContainsKey(key))
            callbacks.Add(key, callback);
    }

    public Dictionary<string, Action> GetCallbacks()
    {
        return callbacks;
    }
    #endregion
}
