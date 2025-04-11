using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   
    [SerializeField] private GameObject[] inventoryPositions;
    private bool fullPos0 = false;
    private bool fullPos1 = false;
    private bool fullPos2 = false;
    private bool fullPos3 = false;
    private bool fullPos4 = false;
    private bool fullPos5 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Moves an item dragged on the inventory to several predefined positions
    /// <remarks>
    /// <para>Sets inventory position used</para>
    /// <para>Sets position attribute of the item GameObject</para>
    /// </remarks>
    /// </summary>
    /// <param name="item">GameObject</param>
    /// <returns>
    /// Error message if inventory is full
    /// </returns>
    public void MoveToPos(GameObject item){
        if (!fullPos0){
            item.transform.position = inventoryPositions[0].transform.position;
            fullPos0 = true;
            if (item.GetComponent<Ingredient>()) item.GetComponent<Ingredient>().SetInvPosition(0);
            if (item.GetComponent<Dish>()) item.GetComponent<Dish>().SetInvPosition(0);
        }

        else if (!fullPos1){
            item.transform.position = inventoryPositions[1].transform.position;
            fullPos1 = true;
            if (item.GetComponent<Ingredient>()) item.GetComponent<Ingredient>().SetInvPosition(1);
            if (item.GetComponent<Dish>()) item.GetComponent<Dish>().SetInvPosition(1);
        }

        else if (!fullPos2){
            item.transform.position = inventoryPositions[2].transform.position;
            fullPos2 = true;
            if (item.GetComponent<Ingredient>()) item.GetComponent<Ingredient>().SetInvPosition(2);
            if (item.GetComponent<Dish>()) item.GetComponent<Dish>().SetInvPosition(2);
        }

        else if (!fullPos3){
            item.transform.position = inventoryPositions[3].transform.position;
            fullPos3 = true;
            if (item.GetComponent<Ingredient>()) item.GetComponent<Ingredient>().SetInvPosition(3);
            if (item.GetComponent<Dish>()) item.GetComponent<Dish>().SetInvPosition(3);
        }

        else if (!fullPos4){
            item.transform.position = inventoryPositions[4].transform.position;
            fullPos4 = true;
            if (item.GetComponent<Ingredient>()) item.GetComponent<Ingredient>().SetInvPosition(4);
            if (item.GetComponent<Dish>()) item.GetComponent<Dish>().SetInvPosition(4);
        }

        else if (!fullPos5){
            item.transform.position = inventoryPositions[5].transform.position;
            fullPos5 = true;
            if (item.GetComponent<Ingredient>()) item.GetComponent<Ingredient>().SetInvPosition(5);
            if (item.GetComponent<Dish>()) item.GetComponent<Dish>().SetInvPosition(5);
        }

        else{
            print("not enough space");
        }
    }


    /// <summary>
    /// repositions inventory when a item is picked up
    /// <remarks>
    /// <para>Resets available positions of the inventory</para>
    /// <para>Resets position attribute of the item GameObject</para>
    /// </remarks>
    /// </summary>
    /// <param name="item">GameObject</param>
    public void Redistribute(GameObject item){
        if (item.GetComponent<Ingredient>())
        {
            if (item.GetComponent<Ingredient>().GetInvPosition() == 0)
            {
                fullPos0 = false;
            }
            if (item.GetComponent<Ingredient>().GetInvPosition() == 1)
            {
                fullPos1 = false;
            }
            if (item.GetComponent<Ingredient>().GetInvPosition() == 2)
            {
                fullPos2 = false;
            }
            if (item.GetComponent<Ingredient>().GetInvPosition() == 3)
            {
                fullPos3 = false;
            }
            if (item.GetComponent<Ingredient>().GetInvPosition() == 4)
            {
                fullPos4 = false;
            }
            if (item.GetComponent<Ingredient>().GetInvPosition() == 5)
            {
                fullPos5 = false;
            }
        }

        if (item.GetComponent<Dish>())
        {
            if (item.GetComponent<Dish>().GetInvPosition() == 0)
            {
                fullPos0 = false;
            }
            if (item.GetComponent<Dish>().GetInvPosition() == 1)
            {
                fullPos1 = false;
            }
            if (item.GetComponent<Dish>().GetInvPosition() == 2)
            {
                fullPos2 = false;
            }
            if (item.GetComponent<Dish>().GetInvPosition() == 3)
            {
                fullPos3 = false;
            }
            if (item.GetComponent<Dish>().GetInvPosition() == 4)
            {
                fullPos4 = false;
            }
            if (item.GetComponent<Dish>().GetInvPosition() == 5)
            {
                fullPos5 = false;
            }
        }

        
    }
}
