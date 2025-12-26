using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CaveStory : MonoBehaviour
{
    public void TitanCutscene()
    {
        var executer = StoryExecuter.Instance;
        var system = executer.GetSystem();
        var characters = StoryHelper.GatherCharacters("Before Titan Fight");
        var titan = characters["Titan"];
        
        system.SetUp();

        executer.SetChapter("Titan Intro");
        system.SwapPlayerState<DefaultMovementState, CameraStateInPlace>();
        system.ShowMovieBars();
        
        // cutscene
        system.SwapCamera(CutscenesHelper.LocateCamera("TitanCam"), continueStoryOverCamera: false);
        titan.Say("Mortal...");
        titan.Say("You still cling to the illusion of freedom in this broken realm...");
        titan.Say("Once, I believed the same... a world worth guarding, worth enduring for.");
        titan.Say("I learned the truth: hope is the cruelest lie of all.");
        titan.Say("letting you escape to the deeper cave... no.");
        titan.Say("unfortunately, this is where your short journey ends.");
        titan.Say("Le fario.");

        IGoapAgent titanAgent = titan.GetComponent<IGoapAgent>();
        titan.DelayedAction(() =>
        {
            titanAgent.EnableGoap();
        });
        
        system.HideMovieBars();
        system.SwapPlayerState<cc_fpState, FP_CameraState>();
        executer.startChapter();
    }

    public void ActivateTitanFight()
    {
        var characters = StoryHelper.GatherCharacters("Before Titan Fight");
        var titan = characters["Titan"];
        IGoapAgent titanAgent = titan.GetComponent<IGoapAgent>();
        
        if (titanAgent.IsEnabled())
            return;
        titanAgent.EnableGoap();
    }
    
    public void AfterBattle()
    {
        // setup
        var executer = StoryExecuter.Instance;
        var system = executer.GetSystem();
        var characters = StoryHelper.GatherCharacters("Before Titan Fight");
        Dictionary<string, CutsceneCamera> cutsceneCameras = CutscenesHelper.GatherCutsceneCameras();
        var titan = characters["Titan"];
        var gate = StoryHelper.GatherStoryObjects()["Gate"].GetComponent<OpenGate>();
        
        system.SetUp();

        executer.SetChapter("Titan After Fight");
        system.SwapPlayerState<DefaultMovementState, CameraStateInPlace>();
        system.ShowMovieBars();
        system.SwapCamera(cutsceneCameras["AfterFightTitanCam"], titan.transform);
        
        // story
        titan.Say("you are not like the rest of them");
        titan.Say("I should have known...");
        titan.Say("how... how could I have known, that you are different");
        titan.Say("will you save us...");
        titan.Say("...");
        titan.Say("I'm sorry father, I will let this one through.");

        system.SwapCamera(cutsceneCameras["Gate"]);
        
        system.DelayedAction(() => { gate.Open(); });
        system.Delay(5f);

        system.SwapCamera(cutsceneCameras["AfterFightTitanCam"], titan.transform);

        titan.Say("I trust you, get out of here, OUT OUT of here,");
        titan.Say("and tell them there are entities still alive in here!");
        titan.Say("tell them! you must!");
        titan.Say("don't disappoint me...");

        // start story
        system.HideMovieBars();
        system.SwapPlayerState<cc_fpState, FP_CameraState>();
        executer.startChapter();
    }
}
