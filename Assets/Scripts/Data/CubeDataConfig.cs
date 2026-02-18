using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CubeStyle
{
    public int Number;
    public Texture2D Texture; 
}

[CreateAssetMenu(fileName = "CubeDataConfig", menuName = "Game/Cube Data Config")]
public class CubeDataConfig : ScriptableObject
{
    [SerializeField] private List<CubeStyle> _styles;
    [SerializeField] private CubeStyle _defaultStyle;

    public CubeStyle GetStyle(int number)
    {
        foreach (var style in _styles)
        {
            if (style.Number == number) return style;
        }
        return _defaultStyle;
    }
}