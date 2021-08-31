using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    GameBehaviour gameManager;
    GameObject text;
    TextMesh textField;
    double holdTime = 0;

    private GameObject TouchObject(Touch touch)
    {
        GameObject touched = null;
        RaycastHit2D hit;
        
        if (hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.down))
        {
            touched = hit.collider.gameObject;
        }

        return touched;

    }
    private void Start()
    {
        text = GameObject.Find("Text");
        textField = text.GetComponent<TextMesh>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (gameManager.win)
                        gameManager.Restart();
                    else
                    {
                        GameObject f = TouchObject(touch);

                        if (f != null)
                        {
                            if (f.name == "Area of Attack")
                            {
                                Attack(f);
                            }
                            if (f.name == "Area of Defence")
                            {
                                Defence(f);
                            }
                            textField.text = "Da!";

                        }
                        textField.text = name;
                    }
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

    private void Attack(GameObject area)
    {
        gameManager.DamageEnemy();
    }

    private void Defence(GameObject area)
    {
        gameManager.Defence();
    }
}
