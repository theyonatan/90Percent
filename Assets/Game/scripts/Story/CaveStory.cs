using UnityEngine;

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

    public void AfterBattle()
    {
        // you are not like the rest of them
        // I should of known...
        // how... how could I have known, that you are different
        // will you save us...
        // I'm sorry father, I will let this one through.
        
        // goes to door
        // punch (.behave, continue=true)
        // doorObject.behave(door break)
        
        // don't disappoint me...
        
        // goes deeper to the inner cave
        // despawn titan
        
        // end cutscene
    }
}
