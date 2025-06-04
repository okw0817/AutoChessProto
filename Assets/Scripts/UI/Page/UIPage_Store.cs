using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIPage_Store : UIPage
{
    #region Members : Private
    [SerializeField]
    private Button refreshBtn;

    [SerializeField]
    private Button levelUpBtn;

    [SerializeField]
    private RectTransform heroList_rect;

    [SerializeField]
    private UI_Panel_StoreInfo uI_Panel_StoreInfo;

    private List<UI_Hero_Icon> heroIcons = new List<UI_Hero_Icon>();

    #endregion

    #region Methods : Mono

    public override void Init()
    {
        Id = UIPageString.Store.ToString();

        var icons = heroList_rect.GetComponentsInChildren<UI_Hero_Icon>(true);

        foreach(var icon in icons)
        {
            heroIcons.Add(icon);
        }
    }
    #endregion

    #region Methods : Override

    public override void Dispose()
    {
        base.Dispose();
        refreshBtn.onClick.RemoveAllListeners();
        levelUpBtn.onClick.RemoveAllListeners();
    }
    public override void SetData(Dictionary<string, object> data)
    {
        base.SetData(data);

        if (data.ContainsKey(UIDataType.Title.ToString())) 
            SetTitle((string)data[UIDataType.Title.ToString()]);

        if(data.ContainsKey(UIDataType.Content.ToString()))
            SetContent((string)data[UIDataType.Content.ToString()]);

        if(data.ContainsKey(CommandDataString.HeroList.ToString()))
        {
            var heroList = (List<HeroData>)data[CommandDataString.HeroList.ToString()];
            SetHeroData(heroList);
        }

        if(data.ContainsKey(UIDataType.Level.ToString()))
        {
            int lvl = (int)data[UIDataType.Level.ToString()];
            uI_Panel_StoreInfo.SetTextLevel(lvl);
        }

        if (data.ContainsKey(UIDataType.Exe.ToString()))
        {
            var experieces = (List<int>)data[UIDataType.Exe.ToString()];
            uI_Panel_StoreInfo.SetSlider((float)experieces[0] / (float)experieces[1]);
            uI_Panel_StoreInfo.SetExe(experieces[0], experieces[1]);
        }

        if (data.ContainsKey(UIDataType.Probability.ToString()))
        {
            var probability = (ProbabilityData)data[UIDataType.Probability.ToString()];
            uI_Panel_StoreInfo.SetSynergies(probability);
        }

    }

    public override void SetCallback(Dictionary<string, Action> callbacks)
    {
        base.SetCallback(callbacks);

        refreshBtn.onClick.RemoveAllListeners();
        refreshBtn.onClick.AddListener(() =>
        {
            RefreshList();
        });

        levelUpBtn.onClick.RemoveAllListeners();
        levelUpBtn.onClick.AddListener(() =>
        {
            AutoChessMaster.Instance.RaiseExperience(2);
        });

        string callbackKey = UIDataType.Callback.ToString();
        if (callbacks.ContainsKey(callbackKey))
        {
            callbacks[callbackKey].Invoke();
        }
    }

    #endregion

    #region Private : Methods
    private void SetHeroData(List<HeroData> heroList)
    {
        foreach(var uiIcon in heroIcons)
        {
            uiIcon.InActiveSynergies();
            uiIcon.IsSale = false; 
        }

        for(int i=0; i< heroList.Count; ++i)
        {
            heroIcons[i].SetSynergies(heroList[i]);
            heroIcons[i].SetCost(heroList[i].level);
        }
    }

    private void RefreshList()
    {
        if (AutoChessMaster.Instance == null)
            return;

        var chessMaster = AutoChessMaster.Instance;
        var probabilities = chessMaster.GetProbabilityLevel(chessMaster.CurLevel);
        var list = chessMaster.GetStoreList(probabilities);

        SetHeroData(list);
    }
    #endregion
}
