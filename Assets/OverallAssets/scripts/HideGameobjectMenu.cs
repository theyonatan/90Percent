using UnityEditor;

/// <summary>
/// does not work currently :(
/// </summary>
public class HideGameobjectMenu
{
    [MenuItem("GameObject/Other/Spline", true)]
    static bool MoveSpline() => false;
    
    [MenuItem("GameObject/Other/Volume", true)]
    static bool MoveVolume() => false;
    
    [MenuItem("GameObject/UI Toolkit", true)]
    static bool HideUIToolkit() => false;
    
    [MenuItem("Assets/Create/UI Toolkit", true)]
    static bool HideUIT_Create() => false;
}
