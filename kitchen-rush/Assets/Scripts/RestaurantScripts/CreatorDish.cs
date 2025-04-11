using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorDish: MonoBehaviour
{
    public GameObject item;
    public bool spawned;
    public Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetReset()
    {
        spawned = false;
    }

    private void OnMouseDown()
    {
        if (!spawned)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                Instantiate(item, trans);
                spawned = true;
            }
        }
    }
}
