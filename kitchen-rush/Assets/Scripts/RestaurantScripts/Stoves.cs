using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoves : MonoBehaviour
{
    [SerializeField] private GameObject[] stovesPositions;
    private bool fullPos0 = false;
    private bool fullPos1 = false;
    private bool fullPos2 = false;
    private bool fullPos3 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Moves an item dragged on the stoves to several predefined positions
    /// <remarks>
    /// <para>Sets stoves position used</para>
    /// <para>Sets position attribute of the item GameObject</para>
    /// </remarks>
    /// </summary>
    /// <param name="item">GameObject</param>
    /// <returns>
    /// Error message if stoves are full
    /// </returns>
    public void MoveToPos(GameObject item)
    {
        if (!fullPos0)
        {
            item.transform.position = stovesPositions[0].transform.position;
            fullPos0 = true;
            if (item.GetComponent<Tool>()) item.GetComponent<Tool>().SetStovesPosition(0);
        }

        else if (!fullPos1)
        {
            item.transform.position = stovesPositions[1].transform.position;
            fullPos1 = true;
            if (item.GetComponent<Tool>()) item.GetComponent<Tool>().SetStovesPosition(1);
        }

        else if (!fullPos2)
        {
            item.transform.position = stovesPositions[2].transform.position;
            fullPos2 = true;
            if (item.GetComponent<Tool>()) item.GetComponent<Tool>().SetStovesPosition(2);
        }

        else if (!fullPos3)
        {
            item.transform.position = stovesPositions[3].transform.position;
            fullPos3 = true;
            if (item.GetComponent<Tool>()) item.GetComponent<Tool>().SetStovesPosition(3);
        }

        else
        {
            print("not enough space");
        }
    }


    /// <summary>
    /// Repositions stoves when a item is picked up
    /// <remarks>
    /// <para>Resets available positions of the stoves</para>
    /// <para>Resets position attribute of the item GameObject</para>
    /// </remarks>
    /// </summary>
    /// <param name="item">GameObject</param>
    public void Redistribute(GameObject item)
    {
        if (item.GetComponent<Tool>())
        {
            if (item.GetComponent<Tool>().GetStovesPosition() == 0)
            {
                fullPos0 = false;
            }
            if (item.GetComponent<Tool>().GetStovesPosition() == 1)
            {
                fullPos1 = false;
            }
            if (item.GetComponent<Tool>().GetStovesPosition() == 2)
            {
                fullPos2 = false;
            }
            if (item.GetComponent<Tool>().GetStovesPosition() == 3)
            {
                fullPos3 = false;
            }

        }
    }
}
