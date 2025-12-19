using System.Collections.Generic;
using UnityEngine;

public class DrillInteract : MonoBehaviour, RanchInteractable
{
    public void Interact()
    {
        // story: about the drill
        var storyExecuter = StoryExecuter.Instance;
        var system = storyExecuter.GetSystem();
        
        // chapter
        storyExecuter.SetChapter("DrillInteract");
        
        system.DisableInput();
        system.SwapPlayerState<DefaultMovementState, CameraStateInPlace>();
        system.SwapCamera(CutscenesHelper.LocateCamera("DrillCam"), null);

        system.Say("This bad boy can save your ass at any depth!");
        system.Say("*insurance not included");
        
        system.EnableInput();
        system.SwapPlayerState<cc_fpState, FP_CameraState>();
        
        // end
        storyExecuter.startChapter();
    }

    public void MarkAsInteractable()
    {
        
    }

    public void StopMarking()
    {
        
    }
}
