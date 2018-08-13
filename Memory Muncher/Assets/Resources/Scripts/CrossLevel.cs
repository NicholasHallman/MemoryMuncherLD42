using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossLevel {

    public static int Level { get; set; }
    public static int Scene { get; set; }
    public static bool newHighScore { get; set; }
    public static int[] Scores;
    public static string[] Names;

    public static void TestScore(int score)
    {
        if (Scores == null)
        {
            Scores = new int[10];
            Names = new string[10];
        }
        int i = 0;
        while (i < 10 && score < Scores[i])
        {
            i++;
        }
        // if we get all the way through then score is smaller then all existing score
        if (i == 10)
        {
            newHighScore = false;
            return;
        }
        else
        {
            newHighScore = true;
        }

    }

    public static void readScores()
    {
        Scores = new int[10];
        Names = new string[10];
        Names = System.IO.File.ReadAllLines("names.txt");
        string[] stringScores = System.IO.File.ReadAllLines("scores.txt");
        Debug.Log(stringScores[0]);
        for(int i = 0; i < 10; i++)
        {
            Scores[i] = int.Parse(stringScores[i]);
        }
    }

    public static void writeScores()
    {
        System.IO.File.WriteAllLines("names.txt", Names);
        string[] stringScores = new string[10];
        for (int i = 0; i < 10; i++)
        {
            stringScores[i] = Scores[i].ToString();
        }
        System.IO.File.WriteAllLines("scores.txt", stringScores);
    }

    public static void AddScore(int score, string name)
    {
        if (Scores == null)
        {
            Scores = new int[10];
            Names = new string[10];
        }
        int i = 0;
        while(i < 10 && score < Scores[i])
        {
            i++;
        }
        // if we get all the way through then score is smaller then all existing score
        if(i == 10)
        {
            newHighScore = false;
            return;
        }
        else
        {
            newHighScore = true;
            int tempScore;
            string tempName;
            int prevScore = score;
            string prevName = name;
            for(i = i; i < 10; i++)
            {
                
                tempScore = Scores[i];
                tempName = Names[i];
                Scores[i] = prevScore;
                Names[i] = prevName;
                prevScore = tempScore;
                prevName = tempName;
            }
        }
    }
}
