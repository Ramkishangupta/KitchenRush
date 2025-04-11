using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    //Movement
    private bool isBeingHeld = false;
    private Vector3 mousePos = new Vector3();

    //Position
    private bool inInventory = false;
    private bool inBin = false;
    private bool inWaiter = false;
    private bool inTable = false;
    private bool inOven = false;
    private Vector3 lastPosition;
    private GameObject lastParent;

    //Inventory logic
    private Inventory inventoryScript;
    private int invPos;
    [SerializeField] private GameObject inventory;

    //Table logic
    private GameObject table;

    //Recipe logic
    private List<string> ingredients = new List<string>();
    private GameObject waiter;

    //Oven logic
    private GameObject oven;

    //Delivery logic
    private bool cooked;
    void Start()
    {
        table = GameObject.Find("Table");
        waiter = GameObject.Find("Waiter"); 
        oven = GameObject.Find("Oven");
        inventoryScript = inventory.GetComponent<Inventory>();
        lastPosition = gameObject.transform.position;
        lastParent = null;
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
        //creatorTray.GetComponent<CreatorTray>().SetReset();
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

    /// <summary>
    /// Logic when object is dropped on waiter
    /// </summary>
    private void DropOnWaiter()
    {
        waiter.GetComponent<Waiter>().CheckPizza(GetIngredients(), IsCooked());
        Destroy(gameObject);
    }

    private void DropOnOven()
    {
        if (oven.GetComponent<Oven>().IsOpen())
        {
            oven.GetComponent<Oven>().OvenPrepare(this.gameObject);
        }

        else
        {
            DropOutLimits();
        }
    }

    /// <summary>
    /// Logic when object is dropped on other places (where it cant be placed)
    /// </summary>
    private void DropOutLimits()
    {
        gameObject.transform.position = lastPosition;
        gameObject.transform.SetParent(table.transform);
    }

    public bool IsInTable()
    {
        return inTable;
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

        else if (inTable)
        {
            DropOnTable();
        }

        else if (inWaiter)
        {
            DropOnWaiter();
        }

        else if (inOven)
        {
            DropOnOven();
        }

        else
        {
            DropOutLimits();
        }
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
    /// <returns>Current position in inventory</returns>
    public int GetInvPosition()
    {
        return invPos;
    }

    /// <summary>
    /// Add ingredient to ingredient array
    /// </summary>
    /// <param name="s">String</param>
    public void AddIngredient(string s)
    {
        ingredients.Add(s);
    }

    public List<string> GetIngredients()
    {
        return ingredients;
    }

    public void SetCooked()
    {
        cooked = true;
    }

    public bool IsCooked()
    {
        return cooked;
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

        if (collision.gameObject.tag == "Waiter")
        {
            inWaiter = true;
        }

        if (collision.gameObject.tag == "Oven")
        {
            inOven = true;
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

        if (collision.gameObject.tag == "Waiter")
        {
            inWaiter = false;
        }

        if (collision.gameObject.tag == "Oven")
        {
            inOven = true;
        }

    }
}
