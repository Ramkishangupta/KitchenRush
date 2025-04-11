using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    private bool active;
    private bool empty;
    private bool waitSwap;
    private List<string> ingredients;
    [SerializeField] GameObject smoothie;
    [SerializeField] Transform trans;
    [SerializeField] Sprite fullSprite;
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite endSprite;
    [SerializeField] Sprite emptySprite;
    [SerializeField] SpriteRenderer spRenderer;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        empty = true;
        waitSwap = false;
        ingredients = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trans.childCount < 1 && GetSwap())
        {
            spRenderer.sprite = emptySprite;
            SetSwap(false);
            SetEmpty(true);
        }
    }

    public void AddIngredient(GameObject ing)
    {
        if (IsEmpty())
        {
            SetEmpty(false);
            spRenderer.sprite = fullSprite;
        }

        ingredients.Add(ing.GetComponent<Ingredient>().GetID());
        Destroy(ing.gameObject);
    }

    public void Blend()
    {
        if (!IsEmpty())
        {
            SetActive(true);
            StartCoroutine(Blending());
        }

        else if (IsEmpty())
        {
            Error();
        }
    }

    public void SetActive(bool t)
    {
        active = t;
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetEmpty(bool t)
    {
        empty = t;
    }

    public bool IsEmpty()
    {
        return empty;
    }

    private void SetSwap(bool b)
    {
        waitSwap = b;
    }

    private bool GetSwap()
    {
        return waitSwap;
    }

    public void Error()
    {

    }

    IEnumerator Blending()
    {
        spRenderer.sprite = onSprite;
        yield return new WaitForSeconds(2);

        spRenderer.sprite = endSprite;
        SetSwap(true);
        Instantiate(smoothie, trans);
        trans.GetChild(0).GetComponent<BlendedDrink>().AddIngredients(ingredients);
        SetActive(false);
    }

}
