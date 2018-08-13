using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour{

    int IntroDelay = 5;
    float startTime = 0;
    int scene;
    void Start()
    {
        CrossLevel.Scene++;
        scene = CrossLevel.Scene;
        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time - startTime > IntroDelay || Input.GetKey("return") || Input.touchCount > 0)
        {
            SceneManager.LoadScene(scene);
        }

    }
}
