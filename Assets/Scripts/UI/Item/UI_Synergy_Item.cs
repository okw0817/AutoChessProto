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

    public void ActiveSynergy(int count)
    {
        foreach(var tmp in synergies_count)
        {
            tmp.color = colorObjectable.SynergyInActive;
        }
    }
    #endregion
}
