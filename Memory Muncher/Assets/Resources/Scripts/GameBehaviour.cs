using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour {

    // Use this for initialization
    private float[,] map;
    public BoardBehaviour board;
    const int HEIGHT = 9;
    private Vector2 playerPos;
    private int maxWidth;
    private bool releasedH = true;
    private bool releasedV = true;
    private int pastCountH = 0;
    private int pastCountV = 0;
    private int evilTick = 0;
    private int evilX = 0;
    private int evilRate = 0;
    private int playerMoveRate = 8;
    private int normRate = 8;
    private int evilNormRate = 24;
    private float startTime;
    bool begin = false;
    private bool win = false;
    private bool lost = false;
    public PlayerAnimationBehaviour player;
    private GameObject coutDown;
    private bool loaded = false;

    void Start () {
        startTime = Time.time;
        CrossLevel.Level+=1;
        evilNormRate = evilNormRate - (CrossLevel.Level * 2);
        evilRate = evilNormRate;
        normRate = normRate - CrossLevel.Level;
        playerMoveRate = normRate;
        Text level = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>();
        level.text = "Level " + CrossLevel.Level;
        playerPos = new Vector2(0, 5);
        maxWidth = Random.Range(100, 200);
        map = new float[maxWidth, HEIGHT];
        for(int w = 0; w < maxWidth; w++)
        {
            for(int h = 0; h < HEIGHT; h++)
            {
                map[w, h] = Random.Range(0f, 1f);
            }
        }
        GenerateMap();
        evilTick = Time.frameCount;
	}

    private void GenerateMap()
    {

        /*
        ***oooooo************************
        ***o****oooo*********************
        oooo*******o*********************
        ***********o*********************
        ***********ooooo*****************
        */
        Vector2 currTile = new Vector2(0,5);
        bool xTurn = true;
        Vector2 currPathEnd = new Vector2(10, 5);
        bool end = false;
        while(!end)
        {
            while (currTile != currPathEnd && !end)
            {
                if (xTurn)
                {
                    currTile.x++;
                    if (currTile.x >= maxWidth) end = true;
                    else map[(int)currTile.x, (int)currTile.y] = 2;
                }
                else
                {
                    if (currTile.y - currPathEnd.y < 0) currTile.y++;
                    else if (currTile.y - currPathEnd.y > 0) currTile.y--;
                    map[(int)currTile.x, (int)currTile.y] = 2;
                }
            }
            xTurn = !xTurn;
            if (xTurn)
            {
                currPathEnd.x += Random.Range(2, 5);
            }
            else
            {
                int newY = 0;
                if (currPathEnd.y == 0)newY = Random.Range(1, 3);
                else if(currPathEnd.y == 8) newY = Random.Range(-1, -3);
                else newY = Random.Range(-2, 3);
                while (newY == 0)
                {
                    newY = Random.Range(-2, 3);
                }
                currPathEnd.y += newY;
                if (currPathEnd.y > 8) currPathEnd.y = 8;
                if (currPathEnd.y < 0) currPathEnd.y = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (begin && !win && !lost) {
            if (Time.frameCount - evilTick > evilRate)
            {
                for (int h = 0; h < HEIGHT; h++)
                {
                    map[evilX, h] = 0;
                }
                evilX++;
                evilTick = Time.frameCount;
            }

            int hr = (int)Input.GetAxisRaw("Horizontal");
            int vr = (int)Input.GetAxisRaw("Vertical");

            for(int i = 0; i < Input.touchCount; i++)
            {
                if((float)Input.GetTouch(i).position.x / (float)Screen.width < .2) hr = 1;
                if ((float)Input.GetTouch(i).position.x / (float)Screen.width > .8)
                {
                    if ((float)Input.GetTouch(i).position.y / (float)Screen.height > .5) vr = 1;
                    else vr = -1;
                }

            }

            if ((releasedH || Time.frameCount - pastCountH > playerMoveRate) && hr != 0)
            {
                playerPos.x += hr;
                releasedH = false;
                pastCountH = Time.frameCount;
            }
            if ((releasedV || Time.frameCount - pastCountV > playerMoveRate) && vr != 0)
            {
                player.moveY(vr);
                playerPos.y += vr;
                releasedV = false;
                pastCountV = Time.frameCount;
            }

            if (hr == 0 && !releasedH) releasedH = true;
            if (vr == 0 && !releasedV) releasedV = true;

            if (playerPos.x < 0) playerPos.x = 0;
            if (playerPos.y < 0) playerPos.y = 0;
            if (playerPos.y > 8) playerPos.y = 8;

            board.UpdateBoard(map, playerPos);
            if (playerPos.x + 8 >= maxWidth)
            {
                win = true;
                startTime = Time.time;
            }
            if (playerPos.x + 8 < maxWidth && map[(int)playerPos.x + 8, (int)playerPos.y] != 0 && map[(int)playerPos.x + 8, (int)playerPos.y] != 2)
            {
                player.Ouch();
                evilRate = evilNormRate - 15;
                playerMoveRate = normRate + 5;
            }
            else
            {
                player.resetRate();
                evilRate = evilNormRate;
                playerMoveRate = normRate;
            }
            if(playerPos.x + 8 < evilX)
            {
                begin = false;
                win = false;
                lost = true;
                StartCoroutine(resetScene());
                GameObject go = Instantiate(Resources.Load("Prefabs/gameover") as GameObject);
            }
        }
        else if (!begin && !win && !lost)
        {
            board.UpdateBoard(null, new Vector2(0, 0));
            Destroy(coutDown);
            if (Time.time - startTime < 1)
            {
                coutDown = Instantiate(Resources.Load("Prefabs/3") as GameObject);
                coutDown.transform.position = new Vector3(-.5f, 4, .25f);
            }
            else if (Time.time - startTime > 1 && Time.time - startTime < 2)
            {
                coutDown = Instantiate(Resources.Load("Prefabs/2") as GameObject);
                coutDown.transform.position = new Vector3(-.5f, 5, .25f);
            }
            else if (Time.time - startTime > 2 && Time.time - startTime < 3)
            {
                coutDown = Instantiate(Resources.Load("Prefabs/1") as GameObject);
                coutDown.transform.position = new Vector3(-.5f, 6, .25f);
            }
            else if (Time.time - startTime > 3 && Time.time - startTime < 4)
            {
                coutDown = Instantiate(Resources.Load("Prefabs/start") as GameObject);
                coutDown.transform.position = new Vector3(-.5f, 7, .25f);
            }
            else
            {
                begin = true;
                player.canStart();
            }
            
        }
        if (win)
        {
            if(Time.time - startTime < 2)
            {
                board.Win();
                board.UpdateBoard(map,playerPos);
            }
            else if (!loaded)
            {
                loaded = true;
                StartCoroutine(loadScene());
                board.ClearTiles();
                GameObject loading = Instantiate(Resources.Load("Models/load") as GameObject);
                loading.transform.localScale = new Vector3(1f / 16f, 1f / 16f, 1f / 16f);
                loading.transform.position = new Vector3(0, 0, 0);
                loading.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
	}

    IEnumerator loadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/Main");
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator resetScene()
    {
        float wait = Time.time;
        while( Time.time - wait < 3)
        {
            yield return null;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/GameOver");
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
