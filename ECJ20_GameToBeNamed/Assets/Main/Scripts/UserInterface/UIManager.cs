using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// A class to manage consistent UI throughout the game.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject pauseMenu;

    [SerializeField] Image fadeImage;
    [SerializeField] float fadeTime = 2;

    [SerializeField] bool isMainMenu = false;

    public enum FadeState
    {
        FADEIN,
        FADEOUT
    }

    protected void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartCoroutine(FadeTo(0f, fadeTime));

        if(!isMainMenu)
            GameManager.instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMainMenu)
        {
            TogglePause();
        }
    }

    /// <summary>
    /// A method to handle conditions of a GameState change (e.g. activate pause menu)
    /// </summary>
    /// <param name="currentState"></param>
    /// <param name="previousState"></param>
    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }

    /// <summary>
    /// From black to transparent. Alpha becomes 0
    /// </summary>
    public void FadeIn()
    {
        // Fade in from black.
        StartCoroutine(FadeTo(0f, fadeTime));
    }

    /// <summary>
    /// From normal to black. Alpha Becomes 1
    /// </summary>
    public void FadeOut()
    {
        // Fade out to black.
        StartCoroutine(FadeTo(1f, fadeTime));
    }

    /// <summary>
    /// A coroutine that fades the alpha of the fadeImage to a value (0 or 1) over a specified time in seconds.
    /// </summary>
    /// <param name="aValue"></param>
    /// <param name="aTime"></param>
    /// <returns></returns>
    IEnumerator FadeTo(float aValue, float aTime)
    {
        // Could have done this with the Unity Animator but I thought it would be cleaner with code.
        float alpha = fadeImage.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            fadeImage.color = newColor;
            yield return null;
        }
    }

    public void StartGame()
    {
        // Still would like some fadeout logic here.  I don't really understand Corountines, so I don't know how to make one let fade out complete before loading a new level.

        LoadLevel("01_Phases");
    }

    public void TogglePause()
    {

        GameManager.instance.UpdateState(GameManager.instance.CurrentGameState == GameManager.GameState.RUNNING ? GameManager.GameState.PAUSED : GameManager.GameState.RUNNING);
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
