using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGameManager : MonoBehaviour
{

    public const float timeDelayDiagoanl = 0.425f;
    public const float timeDelayStray = 0.375f;
    public Image bar;
    public Cinemachine.CinemachineVirtualCamera mainCamera;
    float barSizeOriginal;

    TilemapBehaviour map;
    float timeDelayLeft = 0f;
    int health = 0;

    // Start is called before the first frame update
    void Start()
    {
        barSizeOriginal = bar.rectTransform.rect.width;
        Time.timeScale = 1f;
        map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
        if (SaveData.GetSaveData().LoadMap()==null)
        {
            
            map.CreateMap();
            health = 100;
        }
        else
        {
            map.LoadMap(SaveData.GetSaveData().LoadMap());
            health = SaveData.GetSaveData().LoadHealth();
            Debug.Log(health);
        }

        mainCamera.Follow = GameObject.Find("Hero(Clone)").transform;

        bar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barSizeOriginal * (0.01f * health));
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDelayLeft > 0)
        {
            timeDelayLeft -= Time.deltaTime;
            map.playerMoved = false;
        }
    }

    public float GetTimeDelay()
    {
        return timeDelayLeft;
    }

    public void SetTimeDelayDiagoanl()
    {
        timeDelayLeft = timeDelayDiagoanl;
    }

    public void SetTimeDelayStray()
    {
        timeDelayLeft = timeDelayStray;
    }

    public void loadScene()
    {
        StartCoroutine(AsyncloadScene());
    }

    public IEnumerator AsyncloadScene()
    {
        SaveData.GetSaveData().SaveMap(map.GetTileMap());
        SaveData.GetSaveData().SaveHealth(health);
        Time.timeScale = 0f;
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public static void Restart()
    {
        SaveData.GetSaveData().ResetData();
        SceneManager.LoadScene(0);
    }

}
