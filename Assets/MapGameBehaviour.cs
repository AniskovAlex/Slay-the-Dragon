using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGameBehaviour : MonoBehaviour, IGameManager
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Square");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TouchBegun(TouchDetail touch)
    {
        GameObject touchedObject = touch.getGameObject();
        if (touchedObject != null)
            switch (touch.getGameObject().name)
            {
                case "up button":
                    player.transform.position += Vector3.up * 0.5f;
                    break;
                case "right button":
                    player.transform.position += Vector3.right * 0.5f;
                    break;
                case "down button":
                    player.transform.position += Vector3.down * 0.5f;
                    break;
                case "left button":
                    player.transform.position += Vector3.left * 0.5f;
                    break;
                default:
                    break;
            }
    }


}
