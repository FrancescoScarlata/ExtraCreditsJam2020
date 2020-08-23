using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartMenu();

        StartButton.onClick.AddListener(HandleStartClicked);
    }

    void HandleStartClicked()
    {
        GameManager.instance.StartGame();
    }
}
