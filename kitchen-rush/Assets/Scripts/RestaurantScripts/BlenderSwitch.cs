using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderSwitch : MonoBehaviour
{
    [SerializeField] GameObject blender;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if (!blender.GetComponent<Blender>().IsActive())
        {
            if (!blender.GetComponent<Blender>().IsEmpty())
            {
                blender.GetComponent<Blender>().Blend();
            }
            else
            {
            }
        }
        else
        {
        }
    }
}
