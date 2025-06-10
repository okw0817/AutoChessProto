using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Synergy_Item : MonoBehaviour
{
    #region Members : Private
    [SerializeField]
    private Image img_Icon;

    [SerializeField]
    private TextMeshProUGUI text_Name;

    [SerializeField]
    private List<TextMeshProUGUI> synergies_count;

    [SerializeField]
    private ColorObjectable colorObjectable;

    private int count = 0;
    #endregion

    #region Members : Property
    public int Count
    {
        get => count;
        set
        {
            count = value;
            ActiveSynergy();
        }
    }
    #endregion

    #region Methods : Public
    public void SetIcon(Sprite icon)
    {
        img_Icon.sprite = icon;
    }

    public void SetName(string name)
    {
        text_Name.text = name;
    }

    public void ActiveSynergy()
    {
        foreach(var tmp in synergies_count)
        {
            tmp.color = colorObjectable.SynergyInActive;
        }
        text_Name.color = colorObjectable.SynergyInActive;

        if (count < 2)
            return;

        synergies_count[(count-1)/2].color = colorObjectable.SynergyActive;
        text_Name.color = colorObjectable.SynergyActive;
    }

    public void SetParent(Transform parent)
    {
        this.transform.SetParent(parent, true);
    }

    public bool isActiveSynergy()
    {
        return count >= 2 ? true : false;
    }
    #endregion

    #region Methods : Private
    #endregion
}
