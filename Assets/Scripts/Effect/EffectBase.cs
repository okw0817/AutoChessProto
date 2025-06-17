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
    public string EffectName { 
        get => name;
        set => name = value;
    }
    #endregion


    #region Methods : Public
    public async virtual void PlayParticle()
    {
        cts = new CancellationTokenSource();
        await UniTask.WaitForSeconds(particle.time, cancellationToken: cts.Token);

        AutoChessMaster.Instance.PushPrefabPool(effectName, this.gameObject);
    }
    #endregion

    #region Methods : Mono
    private void OnDestroy()
    {
        cts.Cancel();
    }
    #endregion
}

