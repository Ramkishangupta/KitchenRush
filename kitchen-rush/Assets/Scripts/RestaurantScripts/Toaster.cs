using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    [SerializeField] GameObject toast;
    [SerializeField] Transform trans;
    [SerializeField] SpriteRenderer spRender;
    [SerializeField] Sprite toasterOn;
    [SerializeField] Sprite toasterOff;
    [SerializeField] Sprite toasterCollect;
    private GameObject table;
    private bool on = false;
    private bool waitSwap = false;
    // Start is called before the first frame update
    void Start()
    {
        table = GameObject.Find("Table");
    }

    // Update is called once per frame
    void Update()
    {
        if (trans.childCount < 1 && waitSwap)
        {
            spRender.sprite = toasterOff;
            waitSwap = false;
        }
    }

    public void Toast(GameObject g)
    {
        SetOn(true);
        spRender.sprite = toasterOn;
        Destroy(g);
        StartCoroutine(Toast());
    }

    IEnumerator Toast()
    {
        yield return new WaitForSeconds(1);
        SetOn(false);
        spRender.sprite = toasterCollect;

        Instantiate(toast);
        waitSwap = true;
    }

    public void SetOn(bool b)
    {
        on = b;
    }

    public bool IsOn()
    {
        return on;
    }
}
