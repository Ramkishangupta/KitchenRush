using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Game logic
    public static bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    private Waiter waiter; //updated


    //Inventory logic
    // [SerializeField] GameObject inventory;
    // [SerializeField] GameObject invOpen;
    // [SerializeField] GameObject invClose;

    //Recipe Book Logic
    [SerializeField] GameObject recipePanel;

    void Start()
{
    waiter = FindObjectOfType<Waiter>(); // updated
}
    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMenu()
    {
        if (gameIsPaused)
        {
            Resume();
        }

        else
        {
            Pause();
        }
    }

    // public void Resume()
    // {
    //     pauseMenuUI.SetActive(false);
    //     Time.timeScale = 1f;
    //     gameIsPaused = false;
    // }

public void Resume() //updated
{
    pauseMenuUI.SetActive(false);
    
    if (waiter != null && waiter.scoreMenu != null && waiter.scoreMenu.activeSelf)
    {
        waiter.scoreMenu.SetActive(false); // Hide score menu if it's open
    }

    Time.timeScale = 1f;
    gameIsPaused = false;
}




    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Level Menu");
        Time.timeScale = 1f;
    }

    // public void MainMenu()
    // {
    //     SceneManager.LoadScene("Main Menu");
    //     Time.timeScale = 1f;
    // }



    // public void ShowInventory()
    // {
    //     if (!inventory.activeSelf)
    //     {
    //         inventory.SetActive(true);
    //         invOpen.SetActive(false);
    //         invClose.SetActive(true);
    //     }
    // }

    // public void CloseInventory()
    // {
    //     if (inventory.activeSelf)
    //     {
    //         inventory.SetActive(false);
    //         invOpen.SetActive(true);
    //         invClose.SetActive(false);
    //     }
    // }

    public void CloseRecipeBook()
    {
        recipePanel.SetActive(false);
    }

    public void Quit()
    {
        PlayerPrefs.DeleteKey("hasStoryPlayed"); // Reset the flag
        PlayerPrefs.Save();
        Application.Quit();
    }
}
