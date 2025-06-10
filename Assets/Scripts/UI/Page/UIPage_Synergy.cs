using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIPage_Synergy : UIPage
{
    #region Members : Private
    [SerializeField]
    private UI_Synergy_Item synergy_Prefab;

    private Dictionary<string, UI_Synergy_Item> Dic_Synergy;

    [SerializeField]
    private RectTransform active_rect;

    [SerializeField]
    private RectTransform inActive_rect;

    [SerializeField]
    private PrefabPool prefabPool;
    #endregion

    #region Methods : Mono

    public override void Init()
    {
        Id = UIPageString.Synergy.ToString();
        Dic_Synergy = new Dictionary<string, UI_Synergy_Item>();
    }
    #endregion

    #region Methods : Override

    public override void Dispose()
    {
        base.Dispose();
    }
    public override void SetData(Dictionary<string, object> data)
    {
        base.SetData(data);

        if (data.ContainsKey(UIDataType.Title.ToString()))
            SetTitle((string)data[UIDataType.Title.ToString()]);

        if (data.ContainsKey(CommandDataString.AddSynergy.ToString()))
        {
            var heroData = (HeroData)data[CommandDataString.AddSynergy.ToString()];
            SetSynergy(heroData, 1);
            Debug.Log($"AddSynergy : {heroData.name}");
        }

        if (data.ContainsKey(CommandDataString.DeleteSynergy.ToString()))
        {
            var heroData = (HeroData)data[CommandDataString.DeleteSynergy.ToString()];
            SetSynergy(heroData, -1);
            Debug.Log($"DeleteSynergy : {heroData.name}");
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

    #region Methods : Private
    private void SetSynergy(HeroData heroData, int amount)
    {
        foreach(var synergy in heroData.synergies)
        {
            if (!Dic_Synergy.ContainsKey(synergy))
            {
                var obj = prefabPool.PopPool(ResorucesName.Synergy);
                if (obj == null)
                {
                    synergy_Prefab.gameObject.SetActive(true);
                    Dic_Synergy.Add(synergy, Instantiate<UI_Synergy_Item>(synergy_Prefab, inActive_rect, false));
                    obj = Dic_Synergy[synergy].gameObject;
                    synergy_Prefab.gameObject.SetActive(false);
                }
 
                var item = Dic_Synergy[synergy];
                item.SetName(synergy);
                item.Count += amount;

                Debug.Log($"{synergy} Count : {item.Count}");
                if (item.Count == 1)
                {
                    item.SetParent(inActive_rect);
                }
            }
            else
            {
                var item = Dic_Synergy[synergy];
                item.Count += amount;

                Debug.Log($"{synergy} Count : {item.Count}");
                if (item.Count == 0)
                {
                    prefabPool.PushPool(synergy, Dic_Synergy[synergy].gameObject);
                    Dic_Synergy.Remove(synergy);
                }
                else if(item.Count == 1)
                {
                    item.SetParent(inActive_rect);
                }
                else if (item.Count >= 2)
                {
                    item.SetParent(active_rect);
                }
            }
        }

    }
    #endregion
}
