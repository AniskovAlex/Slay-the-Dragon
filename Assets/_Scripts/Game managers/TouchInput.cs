using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    List<TouchDetail> touchDetails = new List<TouchDetail>();

    void Start()
    {
    }

    void Update()
    {
        if (touchDetails.Count > 0)
        {
            //add new touch
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
            //update all touches
            for (int i = 0; i < touchDetails.Count; i++)
            {
                TouchDetail touchDetail = touchDetails[i];
                int index = touchDetails.IndexOf(touchDetail);
                //Debug.Log(touchDetail.getGameObject());
                Touchable touched = touchDetail.GetTouchable();
                if (touched != null)
                    switch (touchDetail.getTouch().phase)
                    {
                        case TouchPhase.Began:
                            touched.Touched();
                            break;
                        case TouchPhase.Moved:
                            break;
                        case TouchPhase.Stationary:
                            touched.Touching();
                            break;
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            touchDetails.RemoveAt(index);
                            i--;
                            break;
                        default:
                            break;
                    }
                //update list of touches
                foreach (Touch touch in Input.touches)
                    if (touchDetail.getId() == touch.fingerId)
                    {
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
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
        if (hit.Length>0)
        {
            //Debug.Log(hit.Length);
            foreach (RaycastHit2D x in hit)
                //Debug.Log(x.collider.gameObject.name);
            //Physics2D.
            touched = hit[hit.Length-1].collider.gameObject;
        }
        return touched;
    }

}
