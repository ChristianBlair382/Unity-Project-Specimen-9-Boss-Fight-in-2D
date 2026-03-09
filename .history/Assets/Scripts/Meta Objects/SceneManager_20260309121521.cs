using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator animator
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneWithDelay(SceneManager.GetActiveScene().buildIndex + 1, 0.5f));
    }

    private IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        //Play transition animation
        //Wait for the animation to finish
        yield return new WaitForSeconds(delay);
        //Load the next scene
        SceneManager.LoadScene(sceneIndex);
    }
}
