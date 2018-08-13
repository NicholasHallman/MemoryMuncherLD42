using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour {

    // Use this for initialization
    Text[] text;
	void Start () {
        //CrossLevel.readScores();
        CrossLevel.TestScore(CrossLevel.Level);
        text = GetComponentsInChildren<Text>();
        // text [0] is the title
        for (int i = 1; i <= 10; i++)
        {
            text[i].text = i + " " + CrossLevel.Names[i - 1] + "                 " + CrossLevel.Scores[i - 1];
        }
        if (!CrossLevel.newHighScore)
        {
            GameObject.FindGameObjectWithTag("Input").SetActive(false);
            StartCoroutine(loadTitle());
        }
	}

    public void finishedInput()
    {
        CrossLevel.AddScore(CrossLevel.Level, GameObject.FindGameObjectWithTag("NewName").GetComponent<Text>().text);
        GameObject.FindGameObjectWithTag("Input").SetActive(false);
        for(int i = 1; i <= 10; i++)
        {
            text[i].text = i + " " + CrossLevel.Names[i - 1] + "                 " + CrossLevel.Scores[i - 1];
        }
        StartCoroutine(loadTitle());
    }

    IEnumerator loadTitle()
    {
        //CrossLevel.writeScores();
        float wait = Time.time;
        while (Time.time - wait < 3)
        {
            yield return null;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/Title");
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
