using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenSwitch : MonoBehaviour
{
    [SerializeField] GameObject oven;
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
        oven.GetComponent<Oven>().OvenCook();
    }
}
