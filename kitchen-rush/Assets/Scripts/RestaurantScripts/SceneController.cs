using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("Recipes");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLevel()
    {
        return level;
    }

    public void LevelFinished()
    {
        SceneManager.LoadScene("Level Menu");
    }
}
