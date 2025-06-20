using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorrData", menuName = "Scriptable Object/ColorData", order = int.MaxValue)]
public class ColorObjectable : ScriptableObject
{
    #region Members : Public
    [SerializeField]
    private Color synergyActive;
    public Color SynergyActive { get { return synergyActive; } }

    [SerializeField]
    private Color synergyInActive;
    public Color SynergyInActive { get { return synergyInActive; } }

    [SerializeField]
    private List<Color> heroColor;
    #endregion

    #region Methods
    public Color GetHeroColor(int grade)
    {
        if (grade > heroColor.Count)
            return heroColor[0];

        return heroColor[grade - 1];
    }
    #endregion
}
