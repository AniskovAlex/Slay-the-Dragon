using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGameManager : MonoBehaviour
{

    TilemapBehaviour map;
    float timeDelayLeft = 0f;
    public const float timeDelayDiagoanl = 0.425f;
    public const float timeDelayStray = 0.375f;
    /*GameObject saveObject;
    SaveState saveState;*/

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
        /*saveObject = GameObject.Find("Save State");
        Debug.Log(saveObject);*/
        if (SaveData.GetSaveData().LoadMap()==null)
        {
            /*saveObject = Resources.Load("Save State", typeof(GameObject)) as GameObject;
            Debug.Log(saveObject.name);
            saveObject = GameObject.Instantiate(saveObject);
            saveState = saveObject.GetComponent<SaveState>();
            saveState.SaveMap(map.GetTileMap());*/
            map.createMap();
        }
        else
        {
            map.loadMap(SaveData.GetSaveData().LoadMap());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDelayLeft > 0) timeDelayLeft -= Time.deltaTime;

        /*if (Input.anyKeyDown)
        {
            loadScene();
        }*/
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
        Time.timeScale = 0f;
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //SceneManager.MoveGameObjectToScene(saveObject, SceneManager.GetSceneAt(1));
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
