using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    public int WIDTH;
    public int HEIGHT;
    private bool win;
    private GameObject[,] tiles;
    float frames = 0;
	// Use this for initialization
	void Start () {
        tiles = new GameObject[WIDTH, HEIGHT];
		for(int w = ( - WIDTH / 2); w < (WIDTH / 2); w++)
        {
            for (int h = (-HEIGHT / 2); h < (HEIGHT / 2) + 1; h++)
            {
                tiles[(w + WIDTH / 2), h + HEIGHT / 2] = Instantiate(Resources.Load("Prefabs/Base") as GameObject);
                tiles[(w + WIDTH / 2), h + HEIGHT / 2].transform.position = new Vector3(w, 0, h);
                tiles[(w + WIDTH / 2), h + HEIGHT / 2].GetComponentInChildren<TopBehaviour>().ColorTop((h + HEIGHT / 2) / 9f, 1f, 1f);
            }
        }
	}

    public void UpdateBoard(float[,] map, Vector2 pos)
    {
        if (!win) { 
            for (int w = 0; w < WIDTH; w++)
            {
                for (int h = 0; h < HEIGHT; h++)
                {
                    if (map != null)
                    {
                        if (w == WIDTH / 2 && h == pos.y)
                        {
                            tiles[w, h].GetComponentInChildren<TopBehaviour>().ColorTop(0, 0f, 1f);
                        }
                        else if (pos.x + w < map.GetLength(0))
                        {
                            tiles[w, h].GetComponentInChildren<TopBehaviour>().ColorTop(map[(int)pos.x + w, h], 1f, 1f);
                        }
                        else
                        {
                            tiles[w, h].GetComponentInChildren<TopBehaviour>().ColorTop(0, 1f, 0f);
                        }
                    }
                    else
                    {
                        float hue = ((h + w / 9f + 16f) + Time.time) % 1;

                        tiles[w,h].GetComponentInChildren<TopBehaviour>().ColorTop(hue, 1f, 1f);
                    }
                }
            }
        }
        else
        {
            for (int w = 0; w < WIDTH; w++)
            {
                for (int h = 0; h < HEIGHT; h++)
                {
                    tiles[w,h].GetComponentInChildren<TopBehaviour>().ColorTop(0, 0f, 1f);
                }
            }
        }
    }

    public void ClearTiles()
    {
        for(int w = 0; w < WIDTH; w++)
        {
            for(int h = 0; h < HEIGHT; h++)
            {
                tiles[w, h].SetActive(false);
            }
        }
    }

    public void Win() {
        win = true;
    }
}
