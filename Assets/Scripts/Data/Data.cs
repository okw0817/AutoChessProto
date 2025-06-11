
#region WorldState
public enum WorldState { None, Init, Login, Main}
#endregion

#region Command

public enum UIPageString { None, Login, Main, Store, Synergy};
public enum UIPopupString { None, OneButton};
public enum ReciverType { None, UIManager, SceneLoadManager}
public enum SceneName { None, Init, Main}

public enum CommandDataString { None, HeroList, Exe, AddSynergy, DeleteSynergy, SynergyCount }
public enum CommandCallbackString { None, Callback, LevelUp }
#endregion

#region DataType
public enum UIDataType { None, Title, Content, Callback, Data, Exe, Level, Probability}
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
    #endregion
}
#endregion

#region Addressabels
public enum AddressablesLabel { None, Hero }
#endregion

#region Tile
public enum TileType { None, WatingRoom, Stage}
#endregion