using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardDoors : MonoBehaviour
{
    [SerializeField] GameObject cupboard;
    [SerializeField] BoxCollider2D openDoors;
    [SerializeField] GameObject openDoorsSprite;
    [SerializeField] BoxCollider2D closedDoors;
    [SerializeField] GameObject closedDoorsSprite;
    [SerializeField] GameObject cupboardContent;
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!open)
            {
                openDoors.enabled = true;
                openDoorsSprite.SetActive(true);
                closedDoors.enabled = false;
                closedDoorsSprite.SetActive(false);
                cupboardContent.SetActive(true);
                open = true;
            }

            else
            {
                openDoors.enabled = false;
                openDoorsSprite.SetActive(false);
                closedDoors.enabled = true;
                closedDoorsSprite.SetActive(true);
                cupboardContent.SetActive(false);
                open = false;
            }
        }
    }
}
