using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorrData", menuName = "Scriptable Object/ColorData", order = int.MaxValue)]
public class ColorObjectable : ScriptableObject
{
    #region Members : Public
    [SerializeField]
    private Color synergyActive;
    public Color SynergySynergy { get { return synergyActive; } }

    [SerializeField]
    private Color synergyInActive;
    public Color SynergyInActive { get { return synergyInActive; } }
    #endregion
}
