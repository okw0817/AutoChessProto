using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class SceneLoadManager : ManagerBase<SceneLoadManager>, IReceiver
{
    #region Members : Private
    private ReciverType reciverType;
    private string prevSceneName;
    private Action callback;
    #endregion

    #region Methods : Override
    public override void Init()
    {
        prevSceneName = SceneManager.GetActiveScene().name;
        reciverType = ReciverType.SceneLoadManager;
        CommandManager.Instance.AddObserver(this);
        base.Init();
    }

    public ReciverType GetReciverType()
    {
        return reciverType;
    }

    public async void Receive(Command command)
    {
        var sceneCommand = command as SceneCommand;

        if(sceneCommand.GetCallbacks().ContainsKey(CommandCallbackString.Callback.ToString()))
        {
            callback = sceneCommand.GetCallbacks()[CommandCallbackString.Callback.ToString()];
        }

        await LoadScene(sceneCommand.Id, sceneCommand.isAddictive);
    }
    #endregion

    #region Methods : Private
    private async UniTask LoadScene(string sceneName, bool isAdditive)
    {
        LoadSceneMode mode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        await SceneManager.LoadSceneAsync(sceneName + "Scene", mode);
        await SceneManager.UnloadSceneAsync(prevSceneName);
        prevSceneName = sceneName;

        if (callback != null) callback.Invoke();

        callback = null;
    }
    #endregion
}
