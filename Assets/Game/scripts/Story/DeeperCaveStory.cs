using UnityEngine;

public class DeeperCaveStory : MonoBehaviour
{
    [SerializeField] private IGoapAgent[] titans;

    public void CaveEntrance()
    {
        // cutscene display cave
        
        
        // activate titans
        foreach (var titan in titans)
            titan.EnableGoap();
    }
}
