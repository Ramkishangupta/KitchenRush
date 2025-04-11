using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSteps : MonoBehaviour
{
    [SerializeField] GameObject[] steps;
    [SerializeField] Sprite check;
    private int order;
    // Start is called before the first frame update
    void Start()
    {
        order = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Correct();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Incorrect();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRecipe();
        }
    }

    public void Correct()
    {
        if (order > -1 && order < 6)
        {
            steps[order].GetComponent<Image>().sprite = check;
            steps[order].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            order += 1;
        }
        
    }

    public void Incorrect()
    {
        if (order > 0 && order < 7)
        {
            order -= 1;
            steps[order].GetComponent<Image>().sprite = null;
            steps[order].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }

    public void ResetRecipe()
    {
        foreach(GameObject g in steps) {
            g.GetComponent<Image>().sprite = null;
            g.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        order = 0;
    }


}
