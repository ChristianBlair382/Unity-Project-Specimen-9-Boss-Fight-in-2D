using System.Collections;
using System.Collections.Generic;
using UnityEngine;
us

public class SceneManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }
}
