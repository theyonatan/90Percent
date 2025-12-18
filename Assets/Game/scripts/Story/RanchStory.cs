using System;
using UnityEngine;

public class RanchStory : MonoBehaviour
{
    private void Awake()
    {
        // init player pickaxe
        var playerPickaxe = PlayerPickaxe.Instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var executer = StoryExecuter.Instance;
        var system = executer.GetSystem();
        system.SetUp();

        executer.SetChapter("player walk");
        
        system.SwapPlayerState<cc_fpState, FP_CameraState>();
        
        executer.startChapter();
    }
}
