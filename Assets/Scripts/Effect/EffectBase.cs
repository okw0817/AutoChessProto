using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EffectBase : MonoBehaviour
{
    #region Members
    private ParticleSystem particle;
    private CancellationTokenSource cts;

    private string effectName;
    #endregion

    #region Members : Properties
    public string EffectName
    {
        get => effectName;
        set => effectName = value;
    }
    #endregion

    #region Metdhos : Mono
    private void Awake()
    {
        if (particle == null)
            particle = GetComponent<ParticleSystem>();
    }

    private void OnDestroy()
    {
        cts.Cancel();
    }
    #endregion


    #region Methods : Public
    public async virtual void PlayParticle()
    {
        cts = new CancellationTokenSource();
        await UniTask.WaitForSeconds(particle.time + 0.5f, cancellationToken: cts.Token);

        AutoChessMaster.Instance.PushPrefabPool(effectName, this.gameObject);
    }

    #endregion
}

