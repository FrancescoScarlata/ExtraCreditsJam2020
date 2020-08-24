using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for the general elements of the Game
/// </summary>
public class GameManager : MonoBehaviour
{
    // the element that you call to get this item
    public static GameManager instance;

    public SupermarketList sMList;
    public SupermarketManager sMMan;
    public MarketListUIManager mListUIMan;
    public GameMusicManager musicManager;

    public _PhaseManager[] phases = new _PhaseManager[3]; // these will be be linked to the actal 3 phases

    public int currDay;
    public int currPhase = 0;
    public DayInfo [] dayInfos;


    public bool isPlayerWithMask = false;
    public bool isPlayerInfected = false;
    //

    /// <summary>
    /// An enum to track current game state.  MENU - main menu, RUNNING - game proper, PAUSED - pause menu
    /// </summary>
    public enum GameState
    {
        RUNNING,
        PAUSED
    }

    public GameState CurrentGameState { get; private set; } = GameState.RUNNING;
    public Events.EventGameState OnGameStateChanged;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start() // i don't know it this is correct, but for now i guess it's enough
    {
        NewGame();
    }

    /// <summary>
    /// Method to initialize the things at the start of a run.
    /// For now it's just to reset the member elements
    /// </summary>
    public void NewGame()
    {
        foreach(MemberManager member in ((PhaseManager1_PreShopping)phases[0]).familyMembers)
        {
            member.myMember.NewGame(); // this will clear the item lists, so that no item will be there
        }
        currPhase = 0;
        currDay = 0;
        phases[currPhase].StartPhase();
    }

    public void PlayerInfection()
    {
        isPlayerInfected = true;
    }


    /// <summary>
    /// Method called to move to the next phase
    /// </summary>
    public void NextPhase()
    {
        StartCoroutine(NextPhaseWithPauses());
       

    }


    /// <summary>
    /// Method called by the section interactions
    /// </summary>
    /// <param name="newItem">the item that was picked up</param>
    public void InsertItem(Item newItem)
    {
        int resultPickUp;
        if (newItem != null)
        {
            resultPickUp= sMList.PickUpItem(newItem);
            //update the supermarket UI list in some way
            if (resultPickUp > 0)
                mListUIMan.UpdateUIList(newItem.myType);
        }
        else
        {
            // feedback that not item pick up
            
        }
    }

    /// <summary>
    /// A method to update the current GameState
    /// </summary>
    /// <param name="state"></param>
    public void UpdateState(GameState state)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        // Dispatch current game state
        OnGameStateChanged.Invoke(CurrentGameState, previousGameState);

        Debug.Log("Current GameState is " + CurrentGameState);
    }

    
    protected IEnumerator NextPhaseWithPauses()
    {
        // the next phase will open after a fade in/out
        phases[currPhase].EndPhase();
        StartCoroutine(ChangeMusicBetweenPhases());
        yield return new WaitForSeconds(1.2f);
        // next phase called
        currPhase = (currPhase + 1) % phases.Length;
        if (currPhase == 0)
        {
            if (currDay < dayInfos.Length-1)
            {
                currDay++;

                isPlayerInfected = false;
                isPlayerWithMask = false;

                phases[currPhase].StartPhase();
            }    
            else
            {
                Debug.Log("Days finished! Go to the ending scene");
                // finish game / ending scene
                SceneManager.LoadScene("02_GameOver");
            }
        }
        else
        {
            phases[currPhase].StartPhase();
        }
    }


    IEnumerator ChangeMusicBetweenPhases()
    {
        float startingSound;
        float finalValue;
        if (currPhase == 1)
        {
            startingSound = 1;
            finalValue = 0;
        }
        else
        {
            startingSound = 0;
            finalValue = 1;
        }

            
        float currTime = 0;
        while (currTime < 1)
        {
            musicManager.UpdatePhaseMusicValue(Mathf.Lerp(startingSound, finalValue, currTime));
                yield return null;
            currTime += Time.deltaTime;
        }
    }
}
