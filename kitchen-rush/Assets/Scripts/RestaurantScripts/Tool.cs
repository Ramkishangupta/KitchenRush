using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    //Movement
    private bool isBeingHeld = false;
    private Vector3 mousePos = new Vector3();

    //Position
    private bool inTable = false;
    private bool inStoves = false;
    private bool onFire = false;
    private bool inCupboard = false;
    private bool inIngredient = false;
    private bool inBin = false;
    private bool inKnife = false;
    private bool inPan = false;

    private Vector3 lastPosition;
    private GameObject lastParent;

    //Stoves logic
    private Stoves stovesScript;
    private int stovesPos;
    private GameObject stoves;
    private bool cooking = false;
    [SerializeField] Sprite panSprite;
    [SerializeField] Sprite panDoneSprite;
    private bool waitSwap;

    //Table logic
    private GameObject table;

    //Cupboard logic
    private GameObject cupboard;

    //Ingredient logic
    private GameObject ingredientObject;
    [SerializeField] Transform trans;
    private GameObject creatorKnife;

    [SerializeField] string id;

    void Start()
    {
        stoves = GameObject.Find("Stoves");
        table = GameObject.Find("Table");
        cupboard = GameObject.Find("Cupboard");
        creatorKnife = GameObject.Find("CreatorKnife");
        stovesScript = stoves.GetComponent<Stoves>();
        lastPosition = gameObject.transform.position;

        
        //gameObject.transform.SetParent(cupboard.transform);
        lastParent = table;

        //inCupboard= true;
    }
    void Update()
    {
        MoveItem();

        if (onFire && GetTrans().transform.childCount == 1 && !cooking)
        {
            cooking = true;
            StartCoroutine(Cook10());
        }

        if (trans.childCount < 1 && waitSwap)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = panSprite;
            cooking = false;
            waitSwap = false;
        }
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
            if (inStoves)
            {
                stovesScript.Redistribute(gameObject);
            }

            isBeingHeld = true;
            gameObject.transform.SetParent(null);
        }
    }

    public Transform GetTrans()
    {
        return trans;
    }

    /// <summary>
    /// Logic when object is dropped on inventory
    /// <para>Object is moved to predefined positions</para>
    /// </summary>
    private void DropOnStoves()
    {
        if(GetID() == "Pan")
        {
            gameObject.transform.SetParent(stoves.transform);
            stovesScript.MoveToPos(gameObject);
            onFire = true;

            lastPosition = gameObject.transform.position;
            lastParent = stoves;
        }

        else
        {
            DropOutLimits();
        }
    }

    /// <summary>
    /// Logic when object is dropped on bin
    /// <para>Object is destroyed</para>
    /// </summary>
    private void DropOnCupboard()
    {
        DropOutLimits();
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

    private void DropOnIngredient()
    {
        if (GetID() == "Knife")
        {
            if (ingredientObject.GetComponent<Ingredient>().GetID() == "Tomato" || ingredientObject.GetComponent<Ingredient>().GetID() == "Banana"
                || ingredientObject.GetComponent<Ingredient>().GetID() == "Orange" || ingredientObject.GetComponent<Ingredient>().GetID() == "Pineapple")
            {
                ingredientObject.GetComponent<Ingredient>().ChopImage();
                ingredientObject.GetComponent<Ingredient>().SetID(ingredientObject.GetComponent<Ingredient>().GetID() + " cut");
            }
        }

        else
        {
            DropOutLimits();
        }
    }

    private void DropOnBin()
    {
        if (GetID() == "Knife")
        {
            Destroy(gameObject);
            creatorKnife.GetComponent<CreatorGlass>().SetReset();
        }

        if (GetID() == "Pan")
        {
            Destroy(gameObject);
        }
    }

    private void DropOnKnife()
    {
        if (GetID() == "Knife")
        {
            Destroy(gameObject);
            creatorKnife.GetComponent<CreatorGlass>().SetReset();
        }

        else
        {
            DropOutLimits();
        }
    }

    private void DropOnPan()
    {
        if (GetID() == "Pan")
        {
            Destroy(gameObject);
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

    private void OnMouseDown()
    {
        GrabItem();
    }


    private void OnMouseUp()
    {
        isBeingHeld = false;

        if (inStoves)
        {
            DropOnStoves();
        }

        else if (inCupboard)
        {
            DropOnCupboard();
        }

        else if (inIngredient)
        {
            DropOnIngredient();
        }

        else if (inKnife)
        {
            DropOnKnife();
        }

        else if (inPan)
        {
            DropOnPan();
        }

        else if (inTable)
        {
            DropOnTable();
        }
        
        else if (inBin)
        {
            DropOnBin();
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
    public void SetStovesPosition(int i)
    {
        stovesPos = i;
    }

    /// <summary>
    /// Gets the position of the array of the inventory
    /// </summary>
    /// <returns>Current position in inventory</returns>
    public int GetStovesPosition()
    {
        return stovesPos;
    }

    public string GetID() {
        return id;
    }

    /// <summary>
    /// Add ingredient to ingredient array
    /// </summary>
    /// <param name="s">String</param>
    public void AddIngredient(string s)
    {
        ///ingredients.Add(s);
    }

    IEnumerator Cook10()
    {
        yield return new WaitForSeconds(2);
        trans.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        trans.transform.GetChild(0).GetComponent<Ingredient>().Cook();
        //trans.transform.GetChild(0).gameObject.transform.SetParent(stoves.transform);
        gameObject.GetComponent<SpriteRenderer>().sprite = panDoneSprite;
        waitSwap = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stoves")
        {
            inStoves = true;
        }

        if (collision.gameObject.tag == "Cupboard")
        {
            inCupboard = true;
        }

        if (collision.gameObject.tag == "Table")
        {
            inTable = true;
        }

        if (collision.gameObject.tag == "Ingredient")
        {
            inIngredient = true;
            ingredientObject = collision.gameObject;
        }

        if (collision.gameObject.tag == "Bin")
        {
            inBin = true;
        }

        if (collision.gameObject.tag == "Creator Knife")
        {
            inKnife = true;
        }

        if (collision.gameObject.tag == "Creator Pan")
        {
            inPan = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stoves")
        {
            inStoves = false;
            onFire = false;
        }

        if (collision.gameObject.tag == "Cupboard")
        {
            inCupboard = false;
        }

        if (collision.gameObject.tag == "Table")
        {
            inTable = false;
        }

        if (collision.gameObject.tag == "Ingredient")
        {
            inIngredient = false;
            ingredientObject = null;
        }

        if (collision.gameObject.tag == "Creator Knife")
        {
            inKnife = false;
        }

        if (collision.gameObject.tag == "Creator Pan")
        {
            inPan = false;
        }

        if (collision.gameObject.tag == "Bin")
        {
            inBin = false;
        }
    }
}
