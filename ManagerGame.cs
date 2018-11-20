using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour {
    public int numberOfAsteroids;
    public int levelNumber = 1;
    public GameObject asteroid;
    

    public void UpdateNumberOfAsteroids(int change)
    {
        numberOfAsteroids += change;

        //testing asteroids left
        if(numberOfAsteroids <= 0)
        {
            Invoke("StartNewLevel", 3f);

        }
    }

    void StartNewLevel()
    {
        levelNumber++;
        //spawn new asteroids
        for (int i = 0; i < levelNumber*2; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-10.50f, 10.50f), 8f);
            Instantiate(asteroid,spawnPosition,Quaternion.identity);
            numberOfAsteroids++;
        }
    }

    public bool CheckForHighScore(int score)
    {
        int HighScore = PlayerPrefs.GetInt("highscore");
        return score > HighScore;
        
    }
}
