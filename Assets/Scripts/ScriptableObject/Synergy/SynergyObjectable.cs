using UnityEngine;

public class SynergyObjectable : ScriptableObject, IInitializer
{
    #region Methods : Virtual
    public virtual void ActiveSynergy(int level, Hero hero)
    {

    }
    #endregion

    #region Methods : Interface
    public virtual void Init()
    {
    }
    #endregion
}
