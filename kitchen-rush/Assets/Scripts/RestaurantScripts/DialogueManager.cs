using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public GameObject leftBubble;
    public GameObject rightBubble;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    private int dialogueIndex = 0;
private readonly string[] dialogues = {  
    "Arey wah! Dwapar Yug mein makhan churaya, Rishi-muni ka prasad khaya... ab naye yug ka swad dekhna hai!",  
    "Naye yug ka swad? Matlab burger, pizza aur yeh naye zamane ke drinks?",  
    "Haan! Har yug ka apna swad hota hai. Jaise Treta Yug mein Ram ji kanda-mool khate the, aur Satyug mein Amrit milta tha!",  
    "Aur ab KalYug ka khaana! Par yeh aapko pasand aayega?",  
    "Kyun nahi? Swad ka asli maza prem aur bhakti se banta hai!"
};

    private const string StoryPlayedKey = "hasStoryPlayed";

    void Start()
    {
        if (PlayerPrefs.GetInt(StoryPlayedKey, 0) == 0) 
        {
            InitializeDialogue();
            PlayerPrefs.SetInt(StoryPlayedKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

        private bool IsInputDetected()
{
    // For desktop (mouse click)
    if (Input.GetMouseButtonDown(0)) return true;

    // For mobile (touch input)
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) return true;
    }

    return false;
}

    void Update()
    {
        if (IsInputDetected() && dialoguePanel.activeSelf)  
        {
            ShowNextDialogue();
        }
    }
    


    private void InitializeDialogue()
    {
        dialoguePanel.SetActive(true);
        leftBubble.SetActive(true);
        rightBubble.SetActive(false);
        leftText.text = dialogues[0];
    }

    private void ShowNextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex >= dialogues.Length)
        {
            dialoguePanel.SetActive(false);
            return;
        }

        bool isLeftSpeaking = dialogueIndex % 2 == 0;
        leftBubble.SetActive(isLeftSpeaking);
        rightBubble.SetActive(!isLeftSpeaking);

        if (isLeftSpeaking)
            leftText.text = dialogues[dialogueIndex];
        else
            rightText.text = dialogues[dialogueIndex];
    }
}
