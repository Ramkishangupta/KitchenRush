using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject[] introPanels; // Assign four different intro panels in the Inspector
    private int selectedLevel;
    private bool isIntroActive = false;

    void Start()
    {
        // Ensure the introPanels array is assigned before using it
        if (introPanels == null || introPanels.Length == 0)
        {
            Debug.LogError("introPanels array is not assigned in the Inspector!");
            return;
        }

        // Hide all panels initially
        foreach (GameObject panel in introPanels)
        {
            if (panel != null)
                panel.SetActive(false);
            else
                Debug.LogWarning("A panel in introPanels array is null!");
        }
    }

    void Update()
    {
        if (isIntroActive && Input.anyKeyDown)
        {
            StartLevel();
        }
    }

    public void Level1()
    {
        ShowIntro(1, 1);
    }

    public void Level2()
    {
        ShowIntro(2, 2);
    }

    public void Level3()
    {
        ShowIntro(3, 4);
    }


    void ShowIntro(int panelIndex, int recipeValue)
    {
        selectedLevel = panelIndex;
        PlayerPrefs.SetInt("Recipes", recipeValue);

        // Adjust index to match zero-based array indexing
        int panelArrayIndex = panelIndex - 1;

        // Check if index is within bounds
        if (panelArrayIndex >= 0 && panelArrayIndex < introPanels.Length)
        {
            if (introPanels[panelArrayIndex] != null)
            {
                introPanels[panelArrayIndex].SetActive(true); // Show the respective intro panel
                isIntroActive = true;
            }
            else
            {
                Debug.LogError($"introPanels[{panelArrayIndex}] is null. Check assignments in the Inspector.");
            }
        }
        else
        {
            Debug.LogError($"Invalid panelIndex: {panelIndex}. Array Size: {introPanels.Length}");
        }
    }

    void StartLevel()
    {
        int panelArrayIndex = selectedLevel - 1;

        if (panelArrayIndex >= 0 && panelArrayIndex < introPanels.Length && introPanels[panelArrayIndex] != null)
        {
            introPanels[panelArrayIndex].SetActive(false); // Hide intro panel
        }
        else
        {
            Debug.LogWarning($"Skipping hiding panel. Invalid index {panelArrayIndex}.");
        }

        isIntroActive = false;
        SceneManager.LoadScene("Kitchen"); // Load the Kitchen scene after the intro
    }
}
