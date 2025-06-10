using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWatingRoom : MonoBehaviour, IInitializer
{
    #region Members : Private
    private HorizontalTileContainer horizontalTileContainer;
    #endregion

    #region Methods : Interface
    public void Init()
    {
        horizontalTileContainer = GetComponent<HorizontalTileContainer>();
        horizontalTileContainer.TileType = TileType.WatingRoom;
        horizontalTileContainer.Init();
    }
    #endregion

    #region Methods : Private

    #endregion

    #region Methods : Public
    public bool AddHero(Hero hero)
    {
        var tile = horizontalTileContainer.GetFirstEmptyTile();

        if (tile == null)
            return false;
        else
        {
            hero.transform.position = tile.transform.position;
            tile.StandingHero = hero;
            return true;
        }
    }

    public void MoveHero()
    {

    }

    public void DeleteHero()
    {

    }
    #endregion
}
