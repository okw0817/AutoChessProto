using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Members : Private
    private Hero standingHero = null;
    #endregion

    #region Members : Property
    public Hero StandingHero { get => standingHero; set => standingHero = value;}
    public TileType type { get; set; }
    #endregion

}
