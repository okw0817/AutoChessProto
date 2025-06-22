using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ProgressBar : MonoBehaviour
{
    #region Members : Private
    [SerializeField]
    private RectTransform rect_Bar;

    [SerializeField]
    private TextMeshProUGUI txt_percent;

    [SerializeField]
    private Image background;
    #endregion

    #region Methoes : Public
    public void SetPercent(float percent)
    {
        rect_Bar.anchorMax = new Vector2(percent, 1);
        txt_percent.text = $"{percent * 100}%";
    }

    public void SetImageColor(Color color)
    {
        background.color = color;
    }
    #endregion
}
