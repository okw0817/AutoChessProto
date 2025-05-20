using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    #region Members
    private List<HolrizontalTileContainer> allTiles = new List<HolrizontalTileContainer>();
    #endregion

    #region Methods : Mono
    void Start()
    {
        var holizontalTileContainer = GetComponentsInChildren<HolrizontalTileContainer>();

        foreach(var container in holizontalTileContainer)
        {
            container.Init();
            allTiles.Add(container);
        }

        allTiles.Sort((HolrizontalTileContainer containerA, HolrizontalTileContainer containerB) => 
        {
            return containerA.transform.position.z > containerB.transform.position.z ? 1 : -1;
        });
    }
    #endregion

    #region Methods : Public

    #endregion

}
