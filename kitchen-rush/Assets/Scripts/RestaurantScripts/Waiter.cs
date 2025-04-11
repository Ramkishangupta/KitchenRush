using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waiter : MonoBehaviour
{
    [SerializeField] GameObject SceneController;
    public List<string> levelRecipe = new List<string>();
    private int recipe;
    private int randomize;
    private int score;
    private bool lastRecipe;
    private int plate;
    private bool doneRecipe;

    public AudioSource winSound; //win Sound

    //Order Logic
    [SerializeField] GameObject waiterOrder;
    [SerializeField] Sprite water;
    [SerializeField] GameObject waterRecipe;
    [SerializeField] Sprite coke;
    [SerializeField] GameObject cokeRecipe;
    [SerializeField] Sprite juiceLemon;
    [SerializeField] GameObject lemonRecipe;
    [SerializeField] Sprite milk;
    [SerializeField] Sprite shake;
    [SerializeField] GameObject shakeRecipe;
    [SerializeField] Sprite toast;
    [SerializeField] GameObject toastRecipe;
    [SerializeField] Sprite hamburger;
    [SerializeField] GameObject hamburguerRecipe;
    [SerializeField] Sprite hamburgerBacon;
    [SerializeField] GameObject hamBaconRecipe;
    [SerializeField] public GameObject scoreMenu; //updated
    [SerializeField] public GameObject interactionPanel;
    [SerializeField] Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        recipe = PlayerPrefs.GetInt("Recipes");
        score = 0;
        lastRecipe = false;
        plate = 0;
        doneRecipe = true;

        if (recipe == 5)
        {
            ChangeOrder(1);
        }

        else if (recipe == 6)
        {
            ChangeOrder(5);
        }

        else
        {
            ChangeOrder(recipe);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeOrder(int order)
    {
        if (doneRecipe)
        {
            if (order == 1)
            {
                randomize = GetRandom(1, 5);
            }

            plate += 1;

            waterRecipe.SetActive(false);
            cokeRecipe.SetActive(false);
            lemonRecipe.SetActive(false);
            shakeRecipe.SetActive(false);
            toastRecipe.SetActive(false);
            hamburguerRecipe.SetActive(false);
            hamBaconRecipe.SetActive(false);

            if (order == 1 && randomize == 1)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = water;
                waterRecipe.SetActive(true);
            }

            else if (order == 1 && randomize == 2)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = coke;
                cokeRecipe.SetActive(true);
            }

            else if (order == 1 && randomize == 3)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = juiceLemon;
                lemonRecipe.SetActive(true);
            }

            else if (order == 1 && randomize == 4)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = milk;
                lemonRecipe.SetActive(true);
            }

            else if (order == 2)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = shake;
                shakeRecipe.SetActive(true);
            }

            else if (order == 3)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = toast;
                toastRecipe.SetActive(true);
            }

            else if (order == 4)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = hamburger;
                hamburguerRecipe.SetActive(true);
            }

            else if (order == 5)
            {
                waiterOrder.GetComponent<SpriteRenderer>().sprite = hamburgerBacon;
                hamBaconRecipe.SetActive(true);
            }

            doneRecipe = false;
        }
    }

    public void CheckRecipe(List<string> l)
    {
        if (recipe == 1)
        {
            lastRecipe = true;
            Recipe1(l);
        }

        else if (recipe == 2)
        {
            lastRecipe = true;
            Recipe2(l);
        }

        else if (recipe == 3)
        {
            lastRecipe = true;
            Recipe3(l);
        }

        else if (recipe == 4)
        {
            lastRecipe = true;
            Recipe4(l);
        }

        else if (recipe == 5)
        {
            if (plate == 3)
            {
                lastRecipe = true;
                Recipe1(l);
            }

            else if (plate == 2)
            {
                Recipe1(l);
                ChangeOrder(1);
            }

            if (plate == 1)
            {
                Recipe1(l);
                ChangeOrder(1);
            }

        }

        else if (recipe == 6)
        {
            if (plate == 4)
            {
                lastRecipe = true;
                Recipe3(l);
            }

            else if (plate == 3)
            {
                Recipe4(l);
                ChangeOrder(3);
            }

            else if (plate == 2)
            {
                Recipe2(l);
                ChangeOrder(4);
            }

            if (plate == 1)
            {
                Recipe5(l);
                ChangeOrder(2);
            }
        }

        else if (recipe == 7)
        {
            Recipe2(l);
            lastRecipe = true;
            Recipe3(l);
        }

        else if (recipe == 8)
        {
            Recipe3(l);
            lastRecipe = true;
            Recipe4(l);
        }

        else if (recipe == 9)
        {
            Recipe4(l);
            lastRecipe = true;
            Recipe5(l);
        }

        else if (recipe == 10)
        {
            Recipe1(l);
            lastRecipe = true;
            Recipe5(l);
        }
    }


    public void CheckPizza(List<string> l, bool c)
    {
        if (l.Count > 0)
        {
            if (c)
            {
                //do sth
            }

            else
            {
                Wrong();
            }
        }
        else
        {
            Wrong();
        }
    }

    // private void NextRecipe()
    // {
    //     if (lastRecipe)
    //     {
            
    //         score += 10;
    //         PlayerPrefs.SetInt("PuntsTotals", PlayerPrefs.GetInt("PuntsTotals") + score);
    //         scoreText.text = "SCORE: " + score;
    //         ScoreMenu();
    //         lastRecipe = false;
    //     }

    //     else
    //     {
    //         score += 10;
    //         doneRecipe = true;
    //     }
    // }

    // private void OnInteractionPanelClick()
    // {
    //     interactionPanel.SetActive(false); // Hide Interaction Panel
    //     scoreMenu.SetActive(true); // Show Score Menu
    // }

//     private void NextRecipe()
// {
//     if (lastRecipe)
//     {
//         interactionPanel.SetActive(true);
//         OnInteractionPanelClick();
//         score += 10;
//         PlayerPrefs.SetInt("PuntsTotals", PlayerPrefs.GetInt("PuntsTotals") + score);
//         scoreText.text = "SCORE: " + score;

//         if (winSound != null)
//         {
//             winSound.Play(); // Play win sound when completing the recipe
//         }

//         ScoreMenu();
//         lastRecipe = false;
//     }
//     else
//     {
//         score += 10;
//         doneRecipe = true;
//     }
// }

private void NextRecipe()
{
    if (lastRecipe)
    {
        interactionPanel.SetActive(true); // Show the interaction panel
        StartCoroutine(WaitAndContinue());
    }
    else
    {
        score += 10;
        doneRecipe = true;
    }
}

private IEnumerator WaitAndContinue()
{
    yield return new WaitForSeconds(7f); // Wait for 5 seconds

    interactionPanel.SetActive(false); // Hide panel after time
    score += 10;
    PlayerPrefs.SetInt("PuntsTotals", PlayerPrefs.GetInt("PuntsTotals") + score);
    scoreText.text = "SCORE: " + score;

    if (winSound != null)
    {
        winSound.Play(); // Play win sound when completing the recipe
    }

    ScoreMenu(); // Show score menu
    lastRecipe = false;
}

    private void Wrong()
    {
        
    }

    private int GetRandom(int x, int y)
    {
        return Random.Range(x, y);
    }

    private void Recipe1(List<string> l)
    {
        if (l.Count > 0)
        {
            if (randomize == 1)
            {
                if (l[0] == "Water")
                {
                    NextRecipe();
                }

                else
                {
                    Wrong();
                }
            }

            else if (randomize == 2)
            {
                if (l[0] == "Coke")
                {
                    NextRecipe();
                }

                else
                {
                    Wrong();
                }
            }

            else if (randomize == 3) 
            {
                if (l[0] == "Lime")
                {
                    NextRecipe();
                }

                else
                {
                    Wrong();
                }
            }

            else if (randomize == 4)
            {
                if (l[0] == "Milk")
                {
                    NextRecipe();
                }

                else
                {
                    Wrong();
                }
            }
        }

        else
        {
            Wrong();
        }
    }

    private void Recipe2(List <string> l)
    {
        if (l.Count == 4)
        {
            if (l[0] == "Orange cut" || l[0] == "Pineapple cut" || l[0] == "Banana cut" || l[0] == "Milk")
            {
                if (l[1] == "Orange cut" || l[1] == "Pineapple cut" || l[1] == "Banana cut" || l[1] == "Milk")
                {
                    if (l[2] == "Orange cut" || l[2] == "Pineapple cut" || l[2] == "Banana cut" || l[2] == "Milk")
                    {
                        if (l[3] == "Orange cut" || l[3] == "Pineapple cut" || l[3] == "Banana cut" || l[3] == "Milk")
                        {
                            NextRecipe();
                        }

                        else
                        {
                            Wrong();
                        }
                    }

                    else
                    {
                        Wrong();
                    }
                }

                else
                {
                    Wrong();
                }
            }

            else
            {
                Wrong();
            }
        }

        else
        {
            Wrong();
        }
    }

    private void Recipe3(List<string> l)
    {
        if (l.Count == 3)
        {

            if (l[0] == "Toast")
            {
                if (l[1] == "Butter" || l[1] == "Jam")
                {
                    if (l[2] == "Jam" || l[2] == "Butter")
                    {
                        NextRecipe();
                    }

                    else
                    {
                        Wrong();
                    }
                }

                else
                {
                    Wrong();
                }
            }

            else
            {
                Wrong();
            }

        }

        else{
            Wrong();
        }
    }

    private void Recipe4(List<string> l)
    {
        if (l.Count == 4)
        {
            if (l[0] == "Bread bot")
            {
                if (l[1] == "Meat cooked")
                {
                    if (l[2] == "Tomato cut")
                    {
                        if (l[3] == "Bread top")
                        {
                            NextRecipe();
                        }

                        else
                        {
                            Wrong();
                        }
                    }

                    else
                    {
                        Wrong();
                    }
                }

                else
                {
                    Wrong();
                }
            }

            else
            {
                Wrong();
            }
        }
    }

    private void Recipe5(List<string> l)
    {
        if (l[0] == "Bread bot")
        {
            if (l[1] == "Meat cooked")
            {
                if (l[2] == "Bacon cooked")
                {
                    if (l[3] == "Bread top")
                    {
                        NextRecipe();
                    }

                    else
                    {
                        Wrong();
                    }
                }

                else
                {
                    Wrong();
                }
            }

            else
            {
                Wrong();
            }
        }

        else
        {
            Wrong();
        }
    }

    // private void ScoreMenu()
    // {
    //     scoreMenu.SetActive(true);
    //     Time.timeScale = 0f;
    // }



    private void ScoreMenu() //updated
{
    if (!PauseMenu.gameIsPaused)// Prevent pausing again if the game is already paused
    {
        scoreMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}

}
