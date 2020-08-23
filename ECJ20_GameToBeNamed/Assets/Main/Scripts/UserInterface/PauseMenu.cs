using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        RestartButton.onClick.AddListener(HandleRestartClicked);
        QuitButton.onClick.AddListener(HandleQuitClicked);
    }

    void HandleResumeClicked()
    {
        GameManager.instance.TogglePause();
    }

    void HandleRestartClicked()
    {
        GameManager.instance.RestartGame();
    }

    void HandleQuitClicked()
    {
        GameManager.instance.QuitGame();
    }
}
