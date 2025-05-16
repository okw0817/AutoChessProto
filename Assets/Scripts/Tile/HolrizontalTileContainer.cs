using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolrizontalTileContainer : MonoBehaviour, IInitializer
{
    #region Members : Private
    private List<Tile> tiles = new List<Tile>();
    #endregion

    #region Methods : Public
    public void Init()
    {
        var arrTile = GetComponentsInChildren<Tile>();

        foreach (var tile in arrTile)
        {
            tiles.Add(tile);
        }

        tiles.Sort((Tile a, Tile b) =>
        {
            return a.transform.position.x < b.transform.position.x ? -1 : 1;
        });
    }

    #endregion
}
