﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene("AsteroidGame");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Button Test");
    }
}
