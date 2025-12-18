using System.Collections.Generic;
using UnityEngine;

public class DrillInteract : RanchInteractable
{
    public void Interact()
    {
        // story: about the drill
        var storyExecuter = StoryExecuter.Instance;
        
        Dictionary<Characters, StoryCharacter> storyCharacters = StoryHelper.GatherCharacters();
        var system = storyExecuter.GetSystem();
        Dictionary<string, CutsceneCamera> cutsceneCameras = CutscenesHelper.GatherCutsceneCameras();
        
        // chapter
        _storyExecuter.SetChapter("EnterShop");
        
        // show intro cutscene
        system.DisableInput();
        system.SwapPlayerState<DefaultMovementState, CameraStateInPlace>();
        system.ShowMovieBars();
        system.SwapCamera(cutsceneCameras["ShopCamera"], player.transform);
        system.ShowCursor();
        
        // end
        _storyExecuter.startChapter();
    }

    public void MarkAsInteractable()
    {
        
    }

    public void StopMarking()
    {
        
    }
}
