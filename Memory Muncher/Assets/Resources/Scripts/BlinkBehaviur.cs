using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBehaviur : MonoBehaviour {

    // Use this for initialization
    public int rate;
    private int delay;
    private bool blink = true;
	void Start () {
        delay = Time.frameCount;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Renderer>().enabled = blink;
        if (Time.frameCount - delay > rate)
        {
            blink = !blink;
            delay = Time.frameCount;
        }
    }
}
