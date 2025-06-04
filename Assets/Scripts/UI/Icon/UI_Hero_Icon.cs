using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Hero_Icon : MonoBehaviour, IPointerClickHandler
{
    #region Members : Private
    [SerializeField]
    private TextMeshProUGUI heroName;

    [SerializeField]
    private TextMeshProUGUI cost;

    [SerializeField]
    private Image outline;

    [SerializeField]
    private List<UI_Synerge> synergies;
    #endregion

    #region Methods : Public
    public void InActiveSynergies()
    {
        foreach(var synerge in synergies)
        {
            synerge.gameObject.SetActive(false);
        }
    }

    public void SetSynergies(HeroData heroData)
    {

        for(int i=0; i<heroData.synergies.Length; ++i)
        {
            synergies[i].gameObject.SetActive(true);
            synergies[i].SetSynergeName(heroData.synergies[i]);
            heroName.text = heroData.name;
        }
    }

    public void SetCost(int cost)
    {
        this.cost.text = cost.ToString();
    }

    #endregion

    #region Methods : Interface
    public void OnPointerClick(PointerEventData eventData)
    {
        AutoChessMaster.Instance.AddHeroPrefab(heroName.text, this);
    }
    #endregion
}
