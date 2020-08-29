using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FMODBanksLoader : MonoBehaviour
{
    public string[] bankNames;

    public TextMeshProUGUI loadingText;

    protected WaitForSeconds wait = new WaitForSeconds(0.5f);

    private void Awake()
    {
        StartCoroutine(WaitToLoadForBanks());
        
    }


    IEnumerator WaitToLoadForBanks()
    {
        yield return null;
        float timePassed = 0;
        int timeBy3;
        yield return wait;

        while (!FMODUnity.RuntimeManager.HasBanksLoaded)
        {
            //Debug.Log($"IsInitialized: {FMODUnity.RuntimeManager.IsInitialized}");
            yield return wait;
            if (loadingText)
            {
                timePassed += 1f;
                timeBy3 = (int)timePassed % 4;
                loadingText.text = "Loading";

                for (int i = 0; i < timeBy3; i++)
                {
                    loadingText.text += ".";
                }    
            }
           
        }
        SceneManager.LoadScene("00_MainMenu");
    }

}
