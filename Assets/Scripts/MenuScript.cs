using UnityEngine;
using TMPro;
using System;

public class MenuScript : MonoBehaviour
{
    public TMP_InputField mazeHeight;
    public TMP_InputField mazeWidth;
    public TMP_InputField gameHeight;
    public TMP_InputField gameWidth;
    public GameObject menu;
    public GameObject InGameMenu;
    public GameObject mazeGenerator;
    
    private int height;
    private int width;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mazeGenerator.GetComponent<MazeGenerator>().DeleteMaze();
            menu.SetActive(true);
            InGameMenu.SetActive(false);
        }
    }

    public void ReGenerateMaze()
    {
        mazeGenerator.GetComponent<MazeGenerator>().DeleteMaze();
        mazeGenerator.GetComponent<MazeGenerator>().GenerateFull();
    }

    public void StartMazeGeneration()
    {
        height = Convert.ToInt32(mazeHeight.text);
        width = Convert.ToInt32(mazeWidth.text);
        if (height > 250)
        {
            height = 250;
        }
        if (width > 250)
        {
            width = 250;
        }
        if (height < 10)
        {
            height = 10;
        }
        if (width < 10)
        {
            width = 10;
        }
        if (height % 2 != 0)
        {
            height++;
        }
        if (width % 2 != 0)
        {
            width++;
        }
        mazeGenerator.GetComponent<MazeGenerator>().mazeHeight = height;
        mazeGenerator.GetComponent<MazeGenerator>().mazeWidth = width;
        mazeGenerator.GetComponent<MazeGenerator>().GenerateFull();
        menu.SetActive(false);
        InGameMenu.SetActive(true);
        mazeHeight.text = null;
        mazeWidth.text = null;
    }

    public void StartGameGeneration()
    {
        height = Convert.ToInt32(gameHeight.text);
        width = Convert.ToInt32(gameWidth.text);
        if (height > 30)
        {
            height = 30;
        }
        if (width > 30)
        {
            width = 30;
        }
        if (height < 10)
        {
            height = 10;
        }
        if (width < 10)
        {
            width = 10;
        }
        if (height % 2 != 0)
        {
            height++;
        }
        if (width % 2 != 0)
        {
            width++;
        }
        mazeGenerator.GetComponent<MazeGenerator>().mazeHeight = height;
        mazeGenerator.GetComponent<MazeGenerator>().mazeWidth = width;
        mazeGenerator.GetComponent<MazeGenerator>().GenerateGame();
        menu.SetActive(false);
        mazeHeight.text = null;
        mazeWidth.text = null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
