using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour {

    // Use this for initialization
    public GameObject up;
    public GameObject right;
    public GameObject down;
    public GameObject left;
    public GameObject stand;
    public GameObject ouch;

    GameObject currentFace;
    private bool releasedH = true;
    private bool releasedV = true;
    private int pastCountH = 0;
    private int pastCountV = 0;
    private int playerMoveRate = 8;
    private bool animFlip = false;
    private bool begin = false;
    private int normRate = 8;
    AudioSource sound;
    public AudioClip upSound;
    public AudioClip downSound;
    public AudioClip ouchSound;
    Vector3 playerPos;
    void Start () {
        sound = GetComponent<AudioSource>();
        normRate = normRate - CrossLevel.Level;
        playerMoveRate = normRate;
        currentFace = Instantiate(stand);
        currentFace.transform.SetParent(gameObject.transform);
        transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
        transform.Rotate(new Vector3(0, 180, 0));
        transform.position = new Vector3(0, 0, 1);
        playerPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (begin)
        {
            int vr = (int)Input.GetAxisRaw("Vertical");
            int hr = (int)Input.GetAxisRaw("Horizontal");

            for (int i = 0; i < Input.touchCount; i++)
            {
                if ((float)Input.GetTouch(i).position.x / (float)Screen.width < .2) hr = 1;
                if ((float)Input.GetTouch(i).position.x / (float)Screen.width > .8)
                {
                    if ((float)Input.GetTouch(i).position.y / (float)Screen.height > .5) vr = 1;
                    else vr = -1;
                }

            }

            if ((releasedV || Time.frameCount - pastCountV > playerMoveRate) && vr != 0)
            {
                releasedV = false;
                pastCountV = Time.frameCount;
                Destroy(currentFace);
                transform.localScale = new Vector3(1, 1, 1);
                transform.Rotate(new Vector3(0, 0, 0));
                transform.position = new Vector3(0, 0, 0);
                if (vr > 0)
                {
                    currentFace = Instantiate(up);
                    sound.clip = upSound;
                }
                if (vr < 0)
                {
                    currentFace = Instantiate(down);
                    sound.clip = downSound;
                }
                currentFace.transform.SetParent(gameObject.transform);
                transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
                transform.Rotate(new Vector3(0, 180, 0));
                currentFace.transform.position = new Vector3(0, 0, 0);
                transform.position = playerPos;
                sound.Play();
            }
            if ((releasedH || Time.frameCount - pastCountH > playerMoveRate) && hr != 0)
            {
                releasedH = false;
                pastCountH = Time.frameCount;
                Destroy(currentFace);
                transform.localScale = new Vector3(1, 1, 1);
                transform.Rotate(new Vector3(0, 0, 0));
                transform.position = new Vector3(0, 0, 0);
                if (hr > 0) currentFace = Instantiate(right);
                if (hr < 0) currentFace = Instantiate(left);
                currentFace.transform.SetParent(gameObject.transform);
                transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
                transform.Rotate(new Vector3(0, 180, 0));
                currentFace.transform.position = new Vector3(0, 0, 0);
                transform.position = playerPos;
            }
            if (vr == 0 && !releasedV)
            {
                releasedV = true;
                Destroy(currentFace);
                transform.position = new Vector3(0, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                transform.Rotate(new Vector3(0, 0, 0));
                currentFace = Instantiate(stand);
                currentFace.transform.SetParent(gameObject.transform);
                transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
                transform.Rotate(new Vector3(0, 180, 0));
                currentFace.transform.position = new Vector3(0, 0, 0);
                transform.position = playerPos;
            }
            if (hr == 0 && !releasedH)
            {
                releasedH = true;
                Destroy(currentFace);
                transform.position = new Vector3(0, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                transform.Rotate(new Vector3(0, 0, 0));
                currentFace = Instantiate(stand);
                currentFace.transform.SetParent(gameObject.transform);
                transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
                transform.Rotate(new Vector3(0, 180, 0));
                currentFace.transform.position = new Vector3(0, 0, 0);
                transform.position = playerPos;
            }
        }
    }

    public void Ouch()
    {
        Vector3 playerPos = transform.position;
        Destroy(currentFace);
        transform.position = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        transform.Rotate(new Vector3(0, 0, 0));
        currentFace = Instantiate(ouch);
        currentFace.transform.SetParent(gameObject.transform);
        transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
        transform.Rotate(new Vector3(0, 180, 0));
        currentFace.transform.position = new Vector3(0, 0, 0);
        transform.position = playerPos;
        playerMoveRate = normRate + 5;
        if(sound.clip != ouchSound)
        {
            sound.clip = ouchSound;
            sound.Play();
        }
    }

    public void resetRate()
    {
        playerMoveRate = normRate;
    }

    public void canStart()
    {
        begin = true;
    }

    public void moveY(int dy)
    {
        playerPos = transform.position;
        if (dy != 0)
        {
            playerPos.z += dy;
        }
        if (playerPos.z < -4) playerPos.z = -4;
        if (playerPos.z > 4) playerPos.z = 4;
        transform.position = playerPos;
    }

}
