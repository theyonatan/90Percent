using UnityEngine;

public class OverworldStory : MonoBehaviour
{
    public bool NewGame = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (NewGame)
            NewGameStory();
    }

    #region NewGame
    
    private void NewGameStory()
    {
        // hello?
        
        // you there, ok, ok! I think I caught a signal
        
        // anyone on the other side?
        // hello???
        // no one, I told you it was a waste of time.
        // if anyone is actually in that piece of an area, it's their problem now...
        
        // open eyes
        
        // wha...
        // hey, hey! I'm here! I'm...
        // where the hell am I???
        
        // goes to steampunk guy
    }

    public void MetSteampunk()
    {
        // oh, good morning dear!
        
        // were you just talking to me?
        
        // me? hahahaha you wanna hear my voice, first thing up?
        
        // no, wait, who even are you
        
        // oh, unfortunately there are a lot of possible answers to that question.
        // I'm afraid that will reveal itself.
        // I do have a request from you. I am an old man, I cannot swing a pickaxe, I'm too weak for that
        // would you do me a favor and go down the mines? I will come rescure you with my drill safely once you collected enough.
        
        // hmmmmmmmmmmmmmmmmmmmmmmmmm
        // no.
        // ok now where's the exit, where is my home
        
        // oh, you thought that was a question?
        
        // yes?
        
        // HAHAHAHAHAHAHAHAHA
        // let me know when you collected enough, punk
        
        // but!
        
        // goes to shop
    }

    #endregion

    #region Steampunk

    public void InteractSteampunk()
    {
        // on interact with him at shop:
        // if storycharacter.NearLocation(storyobjects[shopLocation])
        // playShop
        
        // else if near drill
        // drill
    }
    
    private void ShopDialogue()
    {
        // if first time
        // for your really hard work, I will reward you with some prizes.
        // come exchange here the diamonds when you have enough!
        
        // else
        // enjoying the goodies? WORK HARDER!!!
    }
    
    private void DrillDialogue()
    {
        // if first time
        // you know, coming to rescue you is a lot of effort.
        
        // wait, so you are old enough to run this thing, but not to mine yourself?
        
        // stop asking questions, PUNK
        
        // else
        // hey, no tip?
    }
    
    private void SteampunkGeneralDialogue()
    {
        // this ranch, oh what a lovely place, don't you wanna stay stuck here forever?
        
        // there is only one way out. have you met my friend yet? I think she is still stuck down there.
        
        // oh well...
    }
    #endregion

    public void InteractGirlRanch()
    {
        // I can't believe you are working for this crazy man.
        // and that you brought me here, after I finally got away.
        
        // again, got away where? you can't survive down there!
        
        // I am capable of surviving by myself!
        // I'm not some little child.
        // Besides, this place isn't what you think it is.
        // I can't explain it
        
        // earlier, I heard a voice, I think someone is looking for me.
        
        // like on the phone?
        
        // no, I think it was coming from above, forget it
        
        // okay, let me know if you find more what's going on.
    }
}
