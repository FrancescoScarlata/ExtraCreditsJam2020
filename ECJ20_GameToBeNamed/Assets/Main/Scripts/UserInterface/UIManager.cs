using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class to manage consistent UI throughout the game.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] MainMenu mainMenu;
    [SerializeField] PauseMenu pauseMenu;

    [SerializeField] Image fadeImage;
    [SerializeField] float fadeTime = 2;

    public enum FadeState
    {
        FADEIN,
        FADEOUT
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void FadeIn()
    {
        // Fade in from black.
        StartCoroutine(FadeTo(0f, fadeTime));
    }

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
}
