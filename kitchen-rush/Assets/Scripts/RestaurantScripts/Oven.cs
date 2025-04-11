using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField] GameObject ovenOpen;
    [SerializeField] GameObject ovenOn;
    [SerializeField] Transform trans;
    private bool cooking;
    private bool open;
    private bool prepared;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        cooking = false;
        ovenOpen.SetActive(false);
        ovenOn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OvenPrepare(GameObject food)
    {
        if (ovenOpen.activeSelf)
        {
            food.transform.position = trans.transform.position;
            food.transform.SetParent(trans);
            food.SetActive(false);
            ovenOpen.SetActive(false);
            prepared = true;
        }
    }

    public void OvenCook()
    {
        if (prepared)
        {
            cooking = true;
            ovenOn.SetActive(true);
            StartCoroutine(Cook1());
        }

        else
        {
            print("close oven dumboo");
        }
    }

    private void CloseDoor()
    {
        ovenOpen.SetActive(false);
        open = false;

        if (trans.childCount > 0)
        {
            Destroy(trans.GetChild(0).gameObject);
        }
    }

    private void OpenDoor()
    {
        if (!cooking)
        {
            ovenOpen.SetActive(true);
            open = true;
            if (trans.transform.childCount > 0)
            {
                trans.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public bool IsOpen()
    {
        return open;
    }
    IEnumerator Cook1() 
    {
        print("start cooking");
        yield return new WaitForSeconds(1);
        print("cooked");

        trans.GetChild(0).GetComponent<Tray>().SetCooked();

        cooking = false;
        ovenOn.SetActive(false);
    }

    private void OnMouseUp()
    {
        if (ovenOpen.activeSelf)
        {
            CloseDoor();
            print("close");
        }
        else
        {
            OpenDoor();
            print("open");
        }
    }


}
