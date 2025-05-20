using UnityEngine;

public class AutoChessMaster : SigletoneBase<AutoChessMaster>
{
    #region Members : Private
    private TileController tileController;
    private HeroWatingRoom heroWatingRoom;
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Methods : Override
    public override void Init()
    {
        tileController = GetComponentInChildren<TileController>();
        heroWatingRoom = GetComponentInChildren<HeroWatingRoom>();
    }
    #endregion

    #region Methods : Public
    public Vector3 GetTiltePosition(Vector3 pos)
    {
        return Vector3.zero;
    }
    #endregion
}
