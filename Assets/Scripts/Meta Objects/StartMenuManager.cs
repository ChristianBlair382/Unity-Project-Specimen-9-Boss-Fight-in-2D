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
    public Image 
        Specimen9Portrait1,
        Specimen9Portrait2,
        Specimen9Eye1,
        Specimen9Eye2;
    public SceneController sceneController;
    public GameObject TTDOverlay;
    private Rigidbody2D TTDOverlayRigidbody;
    void Awake()
    {
        TTDOverlayRigidbody = TTDOverlay.GetComponent<Rigidbody2D>();
        startMenuCanvas.enabled = false;
        creditsCanvas.enabled = false;
        Specimen9Portrait1.enabled = true;
        Specimen9Portrait2.enabled = false;
        Specimen9Eye1.enabled = false;
        Specimen9Eye2.enabled = false;
        StartCoroutine(StartAnimation());
    }
    void Update()
    {
        if(TTDOverlay.transform.position.y < -6.9f)
        {
            TTDOverlay.transform.position = new Vector3(TTDOverlay.transform.position.x, 6.9f, TTDOverlay.transform.position.z);
        }
        TTDOverlayRigidbody.velocity = new Vector2(0f, -1f);
    }
    public void OnStartButton ()
    {
        startMenuCanvas.enabled = false;
        creditsCanvas.enabled = false;
        // Switch the portrait to Specimen 9's portrait before loading the next scene
        Specimen9Portrait1.enabled = false;
        Specimen9Portrait2.enabled = true;
        Specimen9Eye1.enabled = true;
        Specimen9Eye2.enabled = true;
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
