using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    //IGameManager gameManager;

    List<TouchDetail> touchDetails = new List<TouchDetail>();
    delegate void test(TouchDetail touch);
    event test began, moved, stationary;

    void Start()
    {
        //gameManager = GameObject.Find("Game Manager").GetComponent<IGameManager>();
        /*began += gameManager.TouchBegan;
        moved += gameManager.TouchBegan;
        stationary += gameManager.TouchBegan;*/
    }

    void Update()
    {
        if (touchDetails.Count > 0)
        {
            if (touchDetails.Count != Input.touchCount)
            {
                foreach (Touch touch in Input.touches)
                {

                    if (touch.phase == TouchPhase.Began)
                    {
                        touchDetails.Add(new TouchDetail(touch.fingerId, touch, TouchObject(touch)));
                    }
                }
            }
            for (int i = 0; i < touchDetails.Count; i++)
            {
                TouchDetail touchDetail = touchDetails[i];
                int index = touchDetails.IndexOf(touchDetail);
                Debug.Log(touchDetail.getGameObject());
                switch (touchDetail.getTouch().phase)
                {
                    case TouchPhase.Began:
                        //began(touchDetail);
                        Touchable touched = touchDetail.GetTouchable();
                        if (touched != null)
                        {
                            Debug.Log("a");
                            touched.Touched();
                        }
                        break;
                    case TouchPhase.Moved:
                        //moved(touchDetail);
                        break;
                    case TouchPhase.Stationary:
                        //stationary(touchDetail);
                        break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        touchDetails.RemoveAt(index);
                        i--;
                        break;

                    default:
                        break;
                }
                foreach (Touch touch in Input.touches)
                    if (touchDetail.getId() == touch.fingerId)
                    {
                        //Debug.Log(TouchObject(touch));
                        touchDetail.updateGameObject(TouchObject(touch));
                        touchDetail.updateTouch(touch);
                        touchDetails[index] = touchDetail;
                    }
            }
        }
        else
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
                touchDetails.Add(new TouchDetail(touch.fingerId, touch, TouchObject(touch)));
        }
    }

    private GameObject TouchObject(Touch touch)
    {
        GameObject touched = null;
        RaycastHit2D hit;

        if (hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero))
        {
            touched = hit.collider.gameObject;
            //Debug.Log(touched.name);
        }
        //Debug.Log(touched);
        return touched;

    }

}
