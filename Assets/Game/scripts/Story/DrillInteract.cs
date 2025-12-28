using System;
using System.Collections.Generic;
using UnityEngine;

public class DrillInteract : MonoBehaviour, RanchInteractable
{
    [SerializeField] private DrillLocation location;
    
    
    public void Interact()
    {
        switch (location)
        {
            case DrillLocation.Overworld:
                Overworld();
                break;
            case DrillLocation.Level1:
            case DrillLocation.Watercave:
                TeleportUpwards();
                break;
            default:
                Debug.LogError("no default");
                break;
        }
    }

    private void TeleportUpwards()
    {
        var player = FindFirstObjectByType<InputDirector>();
        var spawnPoint = FindFirstObjectByType<SpawnPoint>();

        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
        Physics.SyncTransforms();
    }

    public void Overworld()
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

public enum DrillLocation
{
    Overworld,
    Level1,
    Watercave,
}
