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

    public _PhaseManager[] phases = new _PhaseManager[3]; // these will be be linked to the actal 3 phases

    public int currDay;
    public int currPhase = 0;
    public bool hasPlayerGotInfectiousToday = false;
    public DayInfo [] dayInfos;
    //

    /// <summary>
    /// An enum to track current game state.  MENU - main menu, RUNNING - game proper, PAUSED - pause menu
    /// </summary>
    public enum GameState
    {
        MENU,
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

    private void Update()
    {
        if (CurrentGameState == GameState.MENU)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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
    }

    /// <summary>
    /// Method called to move to the next phase
    /// </summary>
    public void NextPhase()
    {
        // the next phase will open after a fade in/out
        
        // next phase called
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
                mListUIMan.UpdateUIList(UpdateType.addInCorrect, resultPickUp);
            else
                mListUIMan.UpdateUIList(UpdateType.addInWrong, -resultPickUp);
        }
        else
        {
            // feedback that not item pick up
            
        }
    }

    /// <summary>
    /// Method called from the SupermarketUI manager when a item is removed
    /// </summary>
    /// <param name="itemToRemove"></param>
    public void RemoveItem(int index)
    {
        Item itemRemoved = sMList.GetItemFromWrongAt(index);
        sMList.RemoveItem(index);
        sMMan.TakeBackItem(itemRemoved.myType);
        // update the supermarket list UI
        mListUIMan.UpdateUIList(UpdateType.removeFromWrong, index);
    }

    /// <summary>
    /// A method to update the current GameState
    /// </summary>
    /// <param name="state"></param>
    void UpdateState(GameState state)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.MENU:
                Time.timeScale = 1.0f;
                break;

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

    public void StartMenu()
    {
        UpdateState(GameState.MENU);
    }

    public void StartGame()
    {
        // Add logic to wait for FadeOut to complete

        LoadLevel("01_Phases");
    }

    public void TogglePause()
    {
        UpdateState(CurrentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {
        // Add logic to wait for FadeOut to complete

        LoadLevel("00_MainMenu");
    }

    public void QuitGame()
    {
        // implement features for quitting (e.g. autosaving)?

        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}
