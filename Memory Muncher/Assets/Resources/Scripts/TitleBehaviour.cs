using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBehaviour : MonoBehaviour {

    public GameObject title;
    public GameObject enter;

    private void Start()
    {
        CrossLevel.Scene = 1;
        CrossLevel.Level = 0;
    }
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown("return") || Input.touchCount != 0)
        {
            StartCoroutine(loadScene());
            title.SetActive(false);
            enter.SetActive(false);
            GameObject loading = Instantiate(Resources.Load("Models/load") as GameObject);
            loading.transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
            loading.transform.position = new Vector3(0, 0, 0);
            loading.transform.Rotate(new Vector3(0, 180, 0));
        }
        transform.Rotate(new Vector3(0, 0, Mathf.Sin(Time.time) / 2));
	}

    IEnumerator loadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/Intro");
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
