using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    GameObject text;
    TextMesh textField;
    double holdTime = 0;

    private GameObject TouchObject(Touch touch)
    {
        GameObject touched = null;
        RaycastHit2D hit;
        
        if (hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.down))
        {
            Debug.Log("sssss");
            touched = hit.collider.gameObject;
        }

        return touched;

    }
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
                    GameObject f = TouchObject(touch);
                    string name = "Touched";
                    if (f != null)
                        name = f.name;
                    textField.text = name;
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
                    //textField.text = "untouched";
                    holdTime = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
