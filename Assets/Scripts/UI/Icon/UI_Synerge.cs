using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Synerge : MonoBehaviour
{
    #region Members : Private
    [SerializeField]
    private TextMeshProUGUI txt_SynergeName;

    [SerializeField]
    private Image img_Icon;
    #endregion

    #region Methods : Public
    public void SetSynergeName(string name)
    {
        txt_SynergeName.text = name;
    }

    public void SetIcon(Sprite sprite)
    {
        img_Icon.sprite = sprite;    
    }
    #endregion
}
