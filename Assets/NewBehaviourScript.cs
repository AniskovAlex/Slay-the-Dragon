using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMesh output;

    void Update()
    {
        Debug.Log("I am alive");
        
        if (Input.GetMouseButton(0))
        {
            output.text = "pressed!";
        }
    }
}
