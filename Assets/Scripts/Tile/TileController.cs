using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    #region Members
    private List<HorizontalTileContainer> allTiles = new List<HorizontalTileContainer>();
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
    }
    #endregion

    #region Methods : Public

    #endregion

}
