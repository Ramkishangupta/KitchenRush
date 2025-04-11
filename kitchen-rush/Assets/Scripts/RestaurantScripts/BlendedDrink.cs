using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedDrink : MonoBehaviour
{
    //Movement
    private bool isBeingHeld = false;
    private Vector3 mousePos = new Vector3();

    //Position
    private bool inInventory = false;
    private bool inBin = false;
    private bool inTable = false;
    private bool inGlass = false;
    private bool inWaiter = false;
    private Vector3 lastPosition;
    private GameObject lastParent;
    private GameObject glassObject;
    private GameObject panel;

    //Inventory logic
    private Inventory inventoryScript;
    private int invPos;
    private GameObject inventory;

    //Table logic
    private GameObject table;

    //Waiter logic
    public List<string> ingredients = new List<string>();
    private GameObject waiter;

    private bool hidden;

    void Start()
    {
        inventory = GameObject.Find("Inventory");
        table = GameObject.Find("Table");
        panel = GameObject.Find("ScenePanel");
        waiter = GameObject.Find("Waiter");
        //zinventoryScript = inventory.GetComponent<Inventory>();
        lastPosition = gameObject.transform.position;

        gameObject.transform.SetParent(panel.transform);
        lastParent = panel;


        //Initialization
        hidden = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.SetParent(GameObject.Find("Blender").transform.GetChild(0).transform);
    }
    void Update()
    {
        MoveItem();
    }

    /// <summary>
    /// Moves the selected item through holding
    /// </summary>
    private void MoveItem()
    {
        if (isBeingHeld == true)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);

        }

    }

    /// <summary>
    /// Grabs the selected item
    /// </summary>
    private void GrabItem()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (inInventory)
            {
                inventoryScript.Redistribute(gameObject);
            }

            if (hidden)
            {
                hidden = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }

            isBeingHeld = true;
            gameObject.transform.SetParent(null);
        }
    }

    /// <summary>
    /// Logic when object is dropped on inventory
    /// <para>Object is moved to predefined positions</para>
    /// </summary>
    private void DropOnInventory()
    {
        gameObject.transform.SetParent(inventory.transform);
        inventoryScript.MoveToPos(gameObject);

        lastPosition = gameObject.transform.position;
        lastParent = inventory;
    }

    /// <summary>
    /// Logic when object is dropped on bin
    /// <para>Object is destroyed</para>
    /// </summary>
    private void DropOnBin()
    {
        Destroy(gameObject);
    }

    private void DropOnGlass()
    {
        if (glassObject.GetComponent<Dish>().IsInTable())
            {
                glassObject.gameObject.GetComponent<Dish>().AddIngredient(GetIngredients());
                glassObject.GetComponent<Dish>().setGlassFull();
            }
    }

    /// <summary>
    /// Logic when object is dropped on table
    /// </summary>
    private void DropOnTable()
    {
        gameObject.transform.SetParent(table.transform);

        lastPosition = gameObject.transform.position;
        lastParent = table;
    }

    private void DropOnWaiter()
    {
        waiter.GetComponent<Waiter>().CheckRecipe(GetIngredients());
        Destroy(gameObject);
    }

    /// <summary>
    /// Logic when object is dropped on other places (where it cant be placed)
    /// </summary>
    private void DropOutLimits()
    {
        gameObject.transform.position = lastPosition;
        gameObject.transform.SetParent(lastParent.transform);
    }



    /// <summary>
    /// Sets the position of the array of the inventory
    /// </summary>
    /// <param name="i">Integer</param>
    public void SetInvPosition(int i)
    {
        invPos = i;
    }

    /// <summary>
    /// Gets the position of the array of the inventory
    /// </summary>
    /// <returns>Integer</returns>
    public int GetInvPosition()
    {
        return invPos;
    }

    public void AddIngredients(List<string> l)
    {
        foreach (string element in l)
        {
            ingredients.Add(element);
        }
    }

    public List<string> GetIngredients()
    {
        return ingredients;
    }

    private void OnMouseDown()
    {
        GrabItem();
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;

        if (inInventory)
        {
            DropOnInventory();
        }

        else if (inBin)
        {
            DropOnBin();
        }

        else if (inGlass)
        {
            DropOnGlass();
        }

        else if (inTable)
        {
            DropOnTable();
        }

        else if (inWaiter)
        {
            DropOnWaiter();
        }

        else
        {
            DropOutLimits();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inventory")
        {
            inInventory = true;
        }

        if (collision.gameObject.tag == "Bin")
        {
            inBin = true;
        }

        if (collision.gameObject.tag == "Table")
        {
            inTable = true;
        }

        if (collision.gameObject.tag == "Glass")
        {
            glassObject = collision.gameObject;
            inGlass = true;
        }
        
        if (collision.gameObject.tag == "Waiter")
        {
            inWaiter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inventory")
        {
            inInventory = false;
        }

        if (collision.gameObject.tag == "Bin")
        {
            inBin = false;
        }

        if (collision.gameObject.tag == "Table")
        {
            inTable = false;
        }

        if (collision.gameObject.tag == "Glass")
        {
            inGlass = false;
            glassObject = null;
        }

        if (collision.gameObject.tag == "Waiter")
        {
            inWaiter = false;
        }
    }
}
