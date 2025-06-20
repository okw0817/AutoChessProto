using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Members : Private
    [SerializeField]
    private Hero standingHero = null;
    private (int, int) index;
    #endregion

    #region Members : Property
    public (int, int) Index { get => index; }
    public Hero StandingHero { get => standingHero; set => standingHero = value;}
    public TileType type { get; set; }
    #endregion

    #region Methods : Public
    public void SetIndex((int, int) index) { this.index = index; }
    #endregion

}
