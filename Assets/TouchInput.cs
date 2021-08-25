using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    GameObject text;
    TextMesh textField;
    double holdTime = 0;

    private void Start()
    {
        text = GameObject.Find("Text");
        textField = text.GetComponent<TextMesh>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    textField.text = "Touched";
                    break;
                case TouchPhase.Moved:
                    if (touch.deltaPosition.sqrMagnitude > 100)
                    {
                        textField.text = "Moved";
                        holdTime = 0;    
                    }
                    break;
                case TouchPhase.Stationary:
                    holdTime += Time.fixedDeltaTime;
                    if (holdTime > 1)
                        textField.text = "Holding";
                    break;
                case TouchPhase.Ended:
                    textField.text = "untouched";
                    holdTime = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
