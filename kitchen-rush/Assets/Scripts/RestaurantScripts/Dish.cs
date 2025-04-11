using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    //Movement
    private bool isBeingHeld = false;
    private Vector3 mousePos = new Vector3();

    //Position
    private bool inInventory = false;
    private bool inBin = false;
    private bool inWaiter = false;
    private bool inTable = false;
    private bool inDish = false;
    private bool inGlass = false;
    private Vector3 lastPosition;
    private GameObject lastParent;

    //Inventory logic
    private Inventory inventoryScript;
    private int invPos;
    [SerializeField] private GameObject inventory;

    //Table logic
    private GameObject table;

    //Recipe logic
    public List<string> ingredients = new List<string>();
    private GameObject waiter;

    //Glass logic
    private bool glassFull = false;
    [SerializeField] Sprite waterSprite;
    [SerializeField] Sprite limeSprite;
    [SerializeField] Sprite cokeSprite;
    [SerializeField] Sprite milkSprite;

    //Delivery logic
    private GameObject creatorDish;
    private GameObject creatorGlass;
    void Start()
    {
        table = GameObject.Find("Table");
        creatorDish = GameObject.Find("CreatorDish");
        creatorGlass = GameObject.Find("CreatorGlass");
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
        if (gameObject.tag == "Dish")
        {
            creatorDish.GetComponent<CreatorDish>().SetReset();
        }

        else if (gameObject.tag == "Glass")
        {
            creatorGlass.GetComponent<CreatorGlass>().SetReset();
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

    /// <summary>
    /// Logic when object is dropped on waiter
    /// </summary>
    private void DropOnWaiter()
    {
        waiter.GetComponent<Waiter>().CheckRecipe(GetIngredients());

        if (gameObject.tag == "Dish")
        {
            creatorDish.GetComponent<CreatorDish>().SetReset();
        }

        else if (gameObject.tag == "Glass")
        {
            creatorGlass.GetComponent<CreatorGlass>().SetReset();
        }

        Destroy(gameObject);
    }

    private void DropOnDish()
    {
        if (gameObject.tag == "Dish")
        {
            creatorDish.GetComponent<CreatorDish>().SetReset();
            Destroy(gameObject);
        }
    }

    private void DropOnGlass()
    {
        if (gameObject.tag == "Glass")
        {
            creatorGlass.GetComponent<CreatorGlass>().SetReset();
            Destroy(gameObject);
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

    public void ChangeGlassSprite(string s)
    {
        if (s == "Water")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = waterSprite;
        }

        else if (s == "Lime")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = limeSprite;
        }

        else if (s == "Coke")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cokeSprite;
        }

        else if (s == "Milk")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = milkSprite;
        }
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

        else if (inDish)
        {
            DropOnDish();
        }

        else if (inGlass)
        {
            DropOnGlass();
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

    public void AddIngredient(List<string> l)
    {
        foreach (string element in l)
        {
            AddIngredient(element);
            print(element);
        }
        
    }

    public List<string> GetIngredients()
    {
        return ingredients;
    }

    public void setGlassFull()
    {
        glassFull = true;
    }

    public bool IsGlassFull()
    {
        return glassFull;
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
            waiter = collision.gameObject;
        }

        if (collision.gameObject.tag == "Creator Dish")
        {
            inDish = true;
        } 

        if (collision.gameObject.tag == "Creator Glass")
        {
            inGlass = true;
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
            waiter = null;
        }
        
        if (collision.gameObject.tag == "Creator Dish")
        {
            inDish = false;
        }

        if (collision.gameObject.tag == "Creator Glass")
        {
            inGlass = false;
        }
    }
}
