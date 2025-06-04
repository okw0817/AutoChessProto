using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class ResourceManager : ManagerBase<ResourceManager>
{
    #region Members : Private
    private readonly string resourcesPath = "/resources/";
    private List<HeroData> heroData = new List<HeroData>();
    private Dictionary<int, ProbabilityData> ProbabilityData = new Dictionary<int, ProbabilityData>();
    private Dictionary<int, int> ExperienceData = new Dictionary<int, int>();
    #endregion

    #region Members : Properties
    public IEnumerator<HeroData> Heroes { get => heroData.GetEnumerator(); }
    public Dictionary<int, ProbabilityData>.Enumerator Probabilities { get => ProbabilityData.GetEnumerator(); }
    public Dictionary<int, int>.Enumerator ExperienceAmounts { get => ExperienceData.GetEnumerator(); }
    #endregion

    #region Methods : Override
    public override async void Init()
    {
        base.Init();

        await Addressables.InitializeAsync();
        await Addressables.LoadResourceLocationsAsync(AddressablesLabel.Hero.ToString());

        //Characters
        var asset = Readfile(ResorucesName.CharactersProperties);
        HeroContatiner container = JsonUtility.FromJson<HeroContatiner>(asset.ToString());

        foreach(var hero in container.heroes)
        {
            heroData.Add(hero);
        }

        //Probability
        asset = Readfile(ResorucesName.ProbabilityLevels);
        ProbabilityContatiner probabilityContatiner = JsonUtility.FromJson<ProbabilityContatiner>(asset.ToString());

        int index = 0;
        foreach (var level in probabilityContatiner.levels)
        {
            ProbabilityData.Add(++index, level);
        }

        //RequireExprience
        asset = Readfile(ResorucesName.RequireExperiences);
        ExperienceContatiner experienceContatiner = JsonUtility.FromJson<ExperienceContatiner>(asset.ToString());

        foreach (var requireExperience in experienceContatiner.requireExperiences)
        {
            ExperienceData.Add(requireExperience.level, requireExperience.requiringAmount);
        }
    }
    #endregion

    #region Methods : Public
    public ProbabilityData GetProbabilityLevel(int lvl)
    {
        if (ProbabilityData.ContainsKey(lvl))
            return ProbabilityData[lvl];

        return null;
    }

    public int GetRequireExperience(int level)
    {
        return ExperienceData[level];
    }

    public async UniTask<GameObject> GetHeroRasources(string heroName)
    {
        return await Addressables.InstantiateAsync(heroName);
    }
    #endregion

    #region Methods : Private
    private TextAsset Readfile(string fileName)
    {
        return Resources.Load<TextAsset>(fileName);
    }
    #endregion
}
