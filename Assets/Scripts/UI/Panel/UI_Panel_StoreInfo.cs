using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Panel_StoreInfo : MonoBehaviour
{
    #region Members : Private
    [SerializeField]
    private TextMeshProUGUI txt_Level;

    [SerializeField]
    private Slider slider_Level;

    [SerializeField]
    private TextMeshProUGUI txt_Experience;

    [SerializeField]
    private List<TextMeshProUGUI> synergiesProbabilities;
    #endregion

    #region Methods : Public
    public void SetSlider(float rate)
    {
        slider_Level.value = rate;
    }

    public void SetTextLevel(int CurLevel)
    {
        txt_Level.text = CurLevel.ToString() + " lvl";
    }

    public void SetExe(int CurExe, int TotalExe)
    {
        txt_Experience.text = CurExe.ToString() + "/" + TotalExe.ToString();
    }

    public void SetSynergies(ProbabilityData probabilityData)
    {
        synergiesProbabilities[0].text = probabilityData.one.ToString();
        synergiesProbabilities[1].text = probabilityData.two.ToString();
        synergiesProbabilities[2].text = probabilityData.three.ToString();
        synergiesProbabilities[3].text = probabilityData.four.ToString();
    }
    #endregion
}
