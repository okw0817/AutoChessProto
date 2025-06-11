using UnityEngine;

public class SynergyObjectable : ScriptableObject, IInitializer
{
    #region Members : Private
    protected string synergyName;
    #endregion

    #region Methods : Virtual
    public virtual void ActiveSynergy(int level, Hero hero)
    {
        Debug.Log($"{level}: {hero.HeroData.name}");
    }
    #endregion

    #region Methods : Interface
    public virtual void Init()
    {
    }
    #endregion

    #region Methods : Public
    public bool IsSame(string synergyName)
    {
        return this.synergyName == synergyName;
    }
    #endregion
}
