using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBehaviour : MonoBehaviour {

    // Use this for initialization
    private Material m;
    private MeshRenderer mr;
    void Awake () {
        mr = gameObject.GetComponent<MeshRenderer>();
        m = new Material(Shader.Find("Standard"));
        mr.sharedMaterial = m;
    }

    public void ColorTop(float hue, float sat, float value)
    {
        Color c = Color.HSVToRGB(hue, sat, value);
        m.color = c;
        mr.sharedMaterial = m;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
