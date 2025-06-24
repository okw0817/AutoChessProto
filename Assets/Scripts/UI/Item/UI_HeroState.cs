using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HeroState : MonoBehaviour
{
    #region Members : Private
    [SerializeField]
    private UI_ProgressBar ui_hp;

    [SerializeField]
    private UI_ProgressBar ui_mp;
    #endregion

    #region Methods : Public
    public void FollowHero(Vector3 target)
    {
        if (target == null)
            return;

        transform.position = target;
        transform.forward = Camera.main.transform.forward;
    }

    public void SetHp(float percent)
    {
        ui_hp.SetPercent(percent);
    }

    public void SetMP(float percent)
    {
        ui_mp.SetPercent(percent);
    }

    public void DivisionTeamColor(Color color)
    {
        ui_hp.SetImageColor(color);
        //ui_hp.SetImageColor(color);
    }
    #endregion
}
