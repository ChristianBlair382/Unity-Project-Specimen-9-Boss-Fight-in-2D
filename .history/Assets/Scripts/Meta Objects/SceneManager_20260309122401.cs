using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    public bool transtionTrigger = false;
    // Update is called once per frame
    void Update()
    {
        if(transitionTrigger)
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneWithDelay(SceneManager.GetActiveScene().buildIndex + 1, transitionTime));
    }

    private IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        //Play transition animation
        transitionAnimator.SetTrigger("startTransition");
        //Wait for the animation to finish
        yield return new WaitForSeconds(delay);
        //Load the next scene
        SceneManager.LoadScene(sceneIndex);
    }
}
