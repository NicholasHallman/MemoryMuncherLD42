using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDemo : MonoBehaviour {

    // Use this for initialization
    TopBehaviour top;
	void Start () {
        top = gameObject.GetComponent<TopBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        top.ColorTop(Time.time % 1, 1f, 1f);
    }
}
