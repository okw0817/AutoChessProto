using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    #region Members
    private List<HorizontalTileContainer> allTiles = new List<HorizontalTileContainer>();
    #endregion

    #region Members
    public IEnumerator<HorizontalTileContainer> AllTiles { get => allTiles.GetEnumerator(); }
    #endregion

    #region Methods : Mono
    void Start()
    {
        var holizontalTileContainer = GetComponentsInChildren<HorizontalTileContainer>();

        foreach(var container in holizontalTileContainer)
        {
            container.TileType = TileType.Stage;
            container.Init();
            allTiles.Add(container);
        }

        allTiles.Sort((HorizontalTileContainer containerA, HorizontalTileContainer containerB) => 
        {
            return containerA.transform.position.z > containerB.transform.position.z ? 1 : -1;
        });

        for (int i = 0; i < allTiles.Count; ++i)
        {
            var colTiles = allTiles[i].Tiles;
            int colIndex = 0;
            while (colTiles.MoveNext())
            {
                var tile = colTiles.Current;
                tile.SetIndex((colIndex++, i));
            }
        }
    }
    #endregion

    #region Methods : Private
    #endregion

    #region Methods : Public
    public Tile GetTile(int horizontalIndex, int virticalIndex)
    {
        if (virticalIndex < 0 || allTiles.Count <= virticalIndex)
            return null;

        return allTiles[virticalIndex].GethorizontalTile(horizontalIndex);
    }
    #endregion

}
