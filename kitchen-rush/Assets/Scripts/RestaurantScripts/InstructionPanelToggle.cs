using UnityEngine;

public class InstructionPanelToggle : MonoBehaviour
{
    public GameObject instructionPanel; // Assign the panel in Inspector

    void Start()
    {
        instructionPanel.SetActive(false); // Hide panel at start
    }

    // Function to show the panel
    public void ShowPanel()
    {
        instructionPanel.SetActive(true);
    }

    // Function to close the panel
    public void ClosePanel()
    {
        instructionPanel.SetActive(false);
    }
}
