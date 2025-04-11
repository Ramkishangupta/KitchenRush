using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
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
    private bool inFridge = false;
    private bool inPan = false;
    private bool inGlass = false;
    private bool inBlender = false;
    private bool inToaster = false;
    private Vector3 lastPosition;
    private GameObject lastParent;
    private GameObject dishObject;
    private GameObject glassObject;
    private GameObject toolObject;
    private GameObject panel;

    //Inventory logic
    private Inventory inventoryScript;
    private int invPos;
    private GameObject inventory;

    //Table logic
    private GameObject table;

    //Fridge logic
    private GameObject fridge;

    //Blender logic
    private GameObject blender;

    //Toaster logic
    private GameObject toaster;

    //Cupboard logic
    private GameObject cupboard;

    //Images logic
    [SerializeField] private Sprite IngredientCooked;
    [SerializeField] private Sprite IngredientChopped;
    private bool hidden;

    public string id;

    void Start()
    {
        inventory = GameObject.Find("Inventory");
        table = GameObject.Find("Table");
        fridge = GameObject.Find("Fridge");
        panel = GameObject.Find("ScenePanel");
        blender = GameObject.Find("Blender");
        toaster = GameObject.Find("Toaster");
        cupboard = GameObject.Find("Cupboard");

        //inventoryScript = inventory.GetComponent<Inventory>();
        lastPosition = gameObject.transform.position;
        gameObject.transform.SetParent(fridge.transform);
        lastParent = fridge;




        //Toast initalization
        if (GetID() == "Toast")
        {
            hidden = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.SetParent(toaster.transform.GetChild(0).transform);
            lastPosition = toaster.transform.GetChild(0).transform.position;
            lastParent = table;
            gameObject.transform.position = toaster.transform.GetChild(0).transform.position;
        }

        if (GetID() == "Bread top" || GetID() == "Bread bot" || GetID() == "Bread")
        {
            gameObject.transform.SetParent(cupboard.transform);
            lastPosition = gameObject.transform.position;
            lastParent = cupboard;
        }
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
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                hidden = false;
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

    /// <summary>
    /// Logic when object is dropped on stoves
    /// <para>Just tools are enabled, ingredients are moved back</para>
    /// </summary>
    private void DropOnFridge()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Logic when object is dropped on dish
    /// <para>Just ingredients are enabled, tools are moved back</para>
    /// </summary>
    private void DropOnDish() {
        if (GetID() == "Meat" || GetID() == "Meat cooked" || GetID() == "Bread top" || 
            GetID() == "Bread bot" || GetID() == "Bread" || GetID() == "Toast" || 
            GetID() == "Butter" || GetID() == "Bacon" || GetID() == "Bacon cooked"|| 
            GetID() == "Jam" || GetID() == "Tomato cut")
        {
            if (dishObject.GetComponent<Dish>().IsInTable())
            {
                if (GetID() == "Butter" || GetID() == "Jam")
                {
                    dishObject.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = IngredientCooked;
                    dishObject.gameObject.GetComponent<Dish>().AddIngredient(id);
                    Destroy(gameObject);
                }

                else
                {
                    gameObject.transform.SetParent(dishObject.transform);
                    gameObject.transform.localPosition = new Vector3(0, 0.1f * dishObject.transform.childCount, -dishObject.transform.childCount);
                    dishObject.gameObject.GetComponent<Dish>().AddIngredient(id);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                } 
            }
        }
        else
        {
            DropOutLimits();
        }
    }


    private void DropOnGlass()
    {
        if (gameObject.tag == "Drink" && !glassObject.GetComponent<Dish>().IsGlassFull())
        {
            if (glassObject.GetComponent<Dish>().IsInTable())
            {
                glassObject.gameObject.GetComponent<Dish>().AddIngredient(id);
                glassObject.GetComponent<Dish>().setGlassFull();

                if (GetID() == "Water")
                {
                    glassObject.gameObject.GetComponent<Dish>().ChangeGlassSprite("Water");
                }

                else if (GetID() == "Lime")
                {
                    glassObject.gameObject.GetComponent<Dish>().ChangeGlassSprite("Lime");
                }

                else if (GetID() == "Coke")
                {
                    glassObject.gameObject.GetComponent<Dish>().ChangeGlassSprite("Coke");
                }

                else if (GetID() == "Milk")
                {
                    glassObject.gameObject.GetComponent<Dish>().ChangeGlassSprite("Milk");
                }

                Destroy(gameObject);
            }
        }

    }


    /// <summary>
    /// Logic when object is dropped on tool
    /// <para>Logic will depend on the ids of the object and the tool</para>
    /// </summary>
    private void DropOnPan() {
        if (gameObject.tag == "Ingredient")
        {
            if (id == "Meat" && toolObject.GetComponent<Tool>().GetTrans().transform.childCount == 0)
            {
                gameObject.transform.SetParent(toolObject.GetComponent<Tool>().GetTrans().transform);
                gameObject.transform.localPosition = new Vector3(0, 0, -1);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            if (id == "Bacon" && toolObject.GetComponent<Tool>().GetTrans().transform.childCount == 0)
            {
                gameObject.transform.SetParent(toolObject.GetComponent<Tool>().GetTrans().transform);
                gameObject.transform.localPosition = new Vector3(0, 0, -1);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        lastPosition = gameObject.transform.position;
        lastParent = panel;
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
        gameObject.transform.position = lastPosition;
        gameObject.transform.SetParent(lastParent.transform);
    }

    private void DropOnBlender()
    {
        if (GetID() == "Banana cut" || GetID() == "Orange cut" || GetID() == "Pineapple cut" || GetID() == "Milk")
        {
            blender.GetComponent<Blender>().AddIngredient(this.gameObject);
        }

        else
        {
            DropOutLimits();
        }
    }

    private void DropOnToaster()
    {
        if (GetID() == "Bread")
        {
            if (!toaster.GetComponent<Toaster>().IsOn())
            {
                toaster.GetComponent<Toaster>().Toast(gameObject);
            }

            else
            {
                print("toaster on dummie");
                DropOutLimits();
            }
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
        gameObject.transform.SetParent(lastParent.transform);
    }

    public void ChopImage()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = IngredientChopped;

        if (GetID() == "Orange")
        {
            SetID("Orange");
        }
    }

    public void Cook()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = IngredientCooked;

        if (GetID() == "Meat")
        {
            SetID("Meat cooked");
        }

        else if (GetID() == "Bacon")
        {
            SetID("Bacon cooked");
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
    /// <returns>Integer</returns>
    public int GetInvPosition()
    {
        return invPos;
    }

    /// <summary>
    /// Gets object id
    /// </summary>
    /// <returns>String</returns>
    public string GetID()
    {
        return id;
    }

    public void SetID(string s)
    {
        id = s;
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

        else if (inDish)
        {
            DropOnDish();
        }

        else if (inPan)
        {
            DropOnPan();
        }

        else if (inGlass)
        {
            DropOnGlass();
        }

        else if (inBlender)
        {
            DropOnBlender();
        }

        else if (inToaster)
        {
            DropOnToaster();
        }

        else if (inTable)
        {
            DropOnTable();
        }

        else if (inWaiter)
        {
            DropOnWaiter();
        }

        else if (inFridge)
        {
            DropOnFridge();
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

        if (collision.gameObject.tag == "Waiter")
        {
            inWaiter = true;
        }

        if (collision.gameObject.tag == "Dish")
        {
            dishObject = collision.gameObject;
            inDish = true;
        }

        if (collision.gameObject.tag == "Glass")
        {
            glassObject = collision.gameObject;
            inGlass = true;
        }

        if (collision.gameObject.tag == "Fridge") {
            inFridge = true;
        }

        if (collision.gameObject.tag == "Blender")
        {
            inBlender = true;
        }

        if (collision.gameObject.tag == "Toaster")
        {
            inToaster = true;
        }

        if (collision.gameObject.tag == "Table")
        {
            inTable = true;
        }

        if (collision.gameObject.tag == "Tool")
        {
            if (collision.gameObject.GetComponent<Tool>().GetID() == "Pan")
            {
                toolObject = collision.gameObject;
                inPan = true;
            }
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

        if (collision.gameObject.tag == "Waiter")
        {
            inWaiter = false;
        }

        if (collision.gameObject.tag == "Dish") 
        {
            inDish = false;
            dishObject = null;
        }

        if (collision.gameObject.tag == "Glass")
        {
            inGlass = false;
            glassObject = null;
        }

        if (collision.gameObject.tag == "Fridge")
        {
            inFridge = false;
        }

        if (collision.gameObject.tag == "Blender")
        {
            inBlender = false;
        }

        if (collision.gameObject.tag == "Toaster")
        {
            inToaster = false;
        }

        if (collision.gameObject.tag == "Table")
        {
            inTable = false;
        }

        if (collision.gameObject.tag == "Tool")
        {
            if (collision.gameObject.GetComponent<Tool>().GetID() == "Pan")
            {
                inPan = false;
            }
        }

    }
}
