using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDeviceScript : MonoBehaviour
{
    bool open;
    public GameObject content;
    public BoxCollider2D doorOpenCollider;
    public BoxCollider2D doorClosedCollider;
    [SerializeField] Sprite openDoor;
    [SerializeField] Sprite closeDoor;
    // Start is called before the first frame update
    void Start()
    {
        CloseDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Opens the device which is clicked
    /// <remarks>
    /// <para>Enables the content of the device</para>
    /// <para>Enables collider to close the device</para>
    /// <para>Disables collider to open the device</para>
    /// <para>Shows open icon</para>
    /// </remarks>
    /// </summary>
    private void OpenDoor() {
        open = true;
        content.SetActive(true);
        doorClosedCollider.enabled = false;
        doorOpenCollider.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = openDoor;
    }

    /// <summary>
    /// Closes the device which is clicked
    /// <remarks>
    /// <para>Disables the content of the device</para>
    /// <para>Disables collider to close the device</para>
    /// <para>Enables collider to open the device</para>
    /// <para>Shows closed icon</para>
    /// </remarks>
    /// </summary>
    private void CloseDoor() {
        open = false;
        content.SetActive(false);
        doorClosedCollider.enabled = true;
        doorOpenCollider.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = closeDoor;
    }

    private void OnMouseUp()
    {
        if (open)
        {

            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
}
