using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractier : MonoBehaviour, Interactable
{
    [SerializeField] private Shop shop;
    [SerializeField] private GameObject interactIcon;
    
    private StoryExecuter _storyExecuter;

    private void Start()
    {
        _storyExecuter = StoryExecuter.Instance;
        
        StopMarking();
    }

    public void MarkAsInteractable()
    {
        DisplayInteractGui();
    }

    private void DisplayInteractGui()
    {
        interactIcon.SetActive(true);
    }

    public void StopMarking()
    {
        interactIcon.SetActive(false);
    }

    public void Interact()
    {
        if (!shop || shop.IsShopUIOpen)
        {
            Debug.LogError("error opening shop! assign shop to this interactor in the inspector.");
            return;
        }

        shop.IsShopUIOpen = true;
        shop.DisableShopMenuHidingObjects();
        
        // setup enter shop
        _storyExecuter = StoryExecuter.Instance;
        
        Dictionary<Characters, StoryCharacter> storyCharacters = StoryHelper.GatherCharacters();
        StoryCharacter player = storyCharacters[Characters.Player];
        var system = _storyExecuter.GetSystem();
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
}
