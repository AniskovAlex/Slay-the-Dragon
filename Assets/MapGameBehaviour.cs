using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGameBehaviour : MonoBehaviour, IGameManager
{

    TilemapBehaviour map;
    float timeDelayLeft = 0f;
    const float timeDelayDiagoanl = 0.425f;
    const float timeDelayStray = 0.375f;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDelayLeft > 0) timeDelayLeft -= Time.deltaTime;
    }

    public void TouchBegan(TouchDetail touch)
    {
        Move(touch);

    }
    public void TouchMoved(TouchDetail touch)
    {
        Move(touch);
    }

    public void TouchStationary(TouchDetail touch)
    {
        Move(touch);
    }

    private void Move(TouchDetail touch)
    {
        GameObject touchedObject = touch.getGameObject();
        if (touchedObject != null)
            switch (touch.getGameObject().name)
            {
                case "up button":
                    if(timeDelayLeft<=0)
                    if (map.movePlayer(new Vector2(1, 1), Vector2.up * 0.5f))
                        timeDelayLeft = timeDelayDiagoanl;
                    break;
                case "right button":
                    if (timeDelayLeft <= 0)
                        if (map.movePlayer(new Vector2(1, -1), Vector2.right))
                        timeDelayLeft = timeDelayDiagoanl;
                    break;
                case "down button":
                    if (timeDelayLeft <= 0)
                        if (map.movePlayer(new Vector2(-1, -1), Vector2.down * 0.5f))
                        timeDelayLeft = timeDelayDiagoanl;
                    break;
                case "left button":
                    if (timeDelayLeft <= 0)
                        if (map.movePlayer(new Vector2(-1, 1), Vector2.left))
                        timeDelayLeft = timeDelayDiagoanl;
                    break;
                case "up right button":
                    if (timeDelayLeft <= 0)
                        if (map.movePlayer(new Vector2(1, 0), Vector2.up * 0.25f + Vector2.right * 0.5f))
                        timeDelayLeft = timeDelayStray;
                    break;
                case "up left button":
                    if (timeDelayLeft <= 0)
                        if (map.movePlayer(new Vector2(0, 1), Vector2.up * 0.25f + Vector2.left * 0.5f))
                        timeDelayLeft = timeDelayStray;
                    break;
                case "down right button":
                    if (timeDelayLeft <= 0)
                        if (map.movePlayer(new Vector2(0, -1), Vector2.down * 0.25f + Vector2.right * 0.5f))
                        timeDelayLeft = timeDelayStray;
                    break;
                case "down left button":
                    if (timeDelayLeft <= 0)
                        if (map.movePlayer(new Vector2(-1, 0), Vector2.down * 0.25f + Vector2.left * 0.5f))
                        timeDelayLeft = timeDelayStray;
                    break;
                default:
                    break;
            }
    }
}
