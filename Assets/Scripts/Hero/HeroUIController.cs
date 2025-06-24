using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class HeroUIController
{
    #region Members : private
    private PrefabPool prefabPool;
    #endregion

    #region Methods : Constructor
    public HeroUIController(PrefabPool prefabPool)
    {
        this.prefabPool = prefabPool;
    }
    #endregion

    #region Methods : Public
    public async UniTask<T> GetUI<T>(string uiName)
    {
        var obj = prefabPool.PopPool(uiName);

        if (obj == null)
        {
            obj = await ResourceManager.Instance.GetAddressablesRasources(uiName);
        }

        if(obj.TryGetComponent<T>(out T component))
        {
            return component;
        }

        return default;
    }

    public void PushPool(string uiName, GameObject obj)
    {
        prefabPool.PushPool(uiName, obj);
    }

    public void UpdateHeroUI(IEnumerator<Hero> heroes, IEnumerator<Hero> enemies)
    {
        while(heroes.MoveNext())
        {
            heroes.Current.UpdateUITransform();
        }

        while (enemies.MoveNext())
        {
            enemies.Current.UpdateUITransform();
        }
    }
    #endregion
}
