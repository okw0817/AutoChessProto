
#region WorldState
public enum WorldState { None, Init, Login, Main}
#endregion

#region Command

public enum UIPageString { None, Login, Main, Store, Synergy};
public enum UIPopupString { None, OneButton};
public enum ReciverType { None, UIManager, SceneLoadManager}
public enum SceneName { None, Init, Main}

public enum CommandDataString { None, HeroList, Exe, AddSynergy, DeleteSynergy, SynergyCount, PlayButton }
public enum CommandCallbackString { None, Callback, LevelUp, Refresh }
#endregion

#region DataType
public enum UIDataType { None, Title, Content, Callback, Data, Exe, Level, Probability, Money}
#endregion

#region CharacterAbillity
public enum CharacterSynergy { None, Warrior, Rider, Magician, Shielder, Archer, Healer, HeavyArmor, ClothArmor }
#endregion

#region Jsonfiles
public static class ResorucesName
{
    #region Members : public
    public static readonly string CharactersProperties = "CharactersProperties";
    public static readonly string ProbabilityLevels = "ProbabilityLevels";
    public static readonly string RequireExperiences = "RequireExperiences";
    public static readonly string Synergy = "Synergy";
    public static readonly string ProjectileEffect = "Effects";
    public static readonly string Rounds = "Rounds";
    public static readonly string UI_HeroState = "UI_HeroState";
    #endregion
}
#endregion

#region Addressabels
public enum AddressablesLabel { Hero, Particle, Projectile, UI, Icon }
#endregion

#region Tile
public enum TileType { None, WatingRoom, Stage}
#endregion

#region Hero
public enum Team { None, Friendly, Enemy}
#endregion