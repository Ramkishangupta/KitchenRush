using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelScript : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] string id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Moves the panel left and right when clicked
    /// <remarks>
    /// <para>When panel in the right, movement just to the left</para>
    /// <para>When panel in the left, movement just to the right</para>
    /// </remarks>
    /// </summary>
    private void MovePanel() {


        if (id == "right")
        {
            panel.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            panel.transform.position = new Vector3(-25f, 0, 0);
        }
    }

    private void OnMouseDown(){
        if (Input.GetMouseButtonDown(0) ||Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) MovePanel();
    }
}
