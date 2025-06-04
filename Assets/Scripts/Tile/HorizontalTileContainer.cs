using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTileContainer : MonoBehaviour, IInitializer
{
    #region Members : Private
    [SerializeField]
    private List<Tile> tiles = new List<Tile>();
    #endregion

    #region Members : Property
    public IEnumerator Tiles { get => tiles.GetEnumerator(); }
    #endregion

    #region Methods : Interface
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

    #region Methods : Public
    public Tile GetFirstEmptyTile()
    {
        foreach(var tile in tiles)
        {
            if (!tile.HasHero)
                return tile;
        }

        return null;
    }
    #endregion
}
