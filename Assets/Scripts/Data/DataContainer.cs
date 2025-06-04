
#region Character
[System.Serializable]
public class HeroContatiner
{
    public HeroData[] heroes;
}

[System.Serializable]
public class HeroData
{
    public string name;
    public string icon;
    public int level;
    public string[] synergies;
}
#endregion

#region Probability
[System.Serializable]
public class ProbabilityContatiner
{
    public ProbabilityData[] levels;
}

[System.Serializable]
public class ProbabilityData
{
    public int one;
    public int two;
    public int three;
    public int four;
}
#endregion


#region Probability
[System.Serializable]
public class ExperienceContatiner
{
    public RequireExperienceData[] requireExperiences;
}

[System.Serializable]
public class RequireExperienceData
{
    public int level;
    public int requiringAmount;
}
#endregion
