using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public Canvas
        startMenuCanvas,
        creditsCanvas;
    public Button
        startButton,
        quitButton,
        creditsButton,
        backButton;
    public SceneController sceneController;
    void Awake()
    {
        startMenuCanvas.enabled = false;
        creditsCanvas.enabled = false;
        StartCoroutine(StartAnimation());
    }
    public void OnStartButton ()
    {
        startMenuCanvas.enabled = false;
        creditsCanvas.enabled = false;
        sceneController.LoadNextScene();
    }
    public void OnCreditsButton ()
    {
        startMenuCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }
    public void OnBackButton ()
    {
        startMenuCanvas.enabled = true;
        creditsCanvas.enabled = false;
    }
    public void OnQuitButton ()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        startMenuCanvas.enabled = true;
        yield return null;
    }
}
