using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] OutWalls;
    public GameObject[] floors;
    public int rows = 10;
    public int cols = 10;
    private Transform mapManager;
    private List<Vector2> postionList=new List<Vector2>();
    public int minCountWalls=3;
    public int maxCountWalls=8;
    public GameObject[] Wall;
    public GameObject[] foods;
    public GameObject[] enemys;
    public GameObject exit;

    GameManager gameManager;
    // Use this for initialization
    void Awake()
    {
        mapManager = new GameObject("Map").transform;
        gameManager = GetComponent<GameManager>();
        InitMap();
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void InitMap()
    {
       
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)
                {
                    int index = Random.Range(0, OutWalls.Length);
                    GameObject go = GameObject.Instantiate(OutWalls[index], new Vector3(x, y, 0), Quaternion.identity)as GameObject;
                    go.transform.SetParent(mapManager);
                }
                else
                {
                    int index = Random.Range(0, floors.Length);
                    GameObject go = GameObject.Instantiate(floors[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapManager);

                }
            }
        }
        postionList.Clear();
        for (int x = 2; x < cols - 2; x++)
        {
            for (int y = 2; y < rows - 2; y++)
            {
                postionList.Add(new Vector2(x, y));
            }
        }
        int wallCount = Random.Range(minCountWalls, maxCountWalls);
        for (int i = 0; i < wallCount; i++)
        {
            Vector2 pos = RandomPosition();
            int wallIndex = Random.Range(0, Wall.Length);
            GameObject prefab = RandomPrefab(Wall);
            GameObject go= GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapManager);
        }

        int foodCount = Random.Range(1, gameManager.level * 2 + 1);
        for (int i = 0; i < foodCount; i++)
        {
            Vector2 pos = RandomPosition();
            GameObject food = RandomPrefab(foods);
            GameObject go = GameObject.Instantiate(food, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapManager);
        }

        int enemyCount = gameManager.level/2;
        for (int i = 0; i < enemyCount; i++)
        {
            Debug.Log("2"+enemyCount);
            Vector2 pos = RandomPosition();
            GameObject enemy = RandomPrefab(enemys);
            GameObject go = GameObject.Instantiate(enemy, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapManager);
        }
         GameObject goo = GameObject.Instantiate(exit, new Vector2(rows-2,cols-2), Quaternion.identity) as GameObject;
         goo.transform.SetParent(mapManager);

    }
    Vector2 RandomPosition()
    {
        int positionIndex = Random.Range(0, postionList.Count);
        Vector2 pos = postionList[positionIndex];
        postionList.RemoveAt(positionIndex);
        return pos;
    }
    GameObject RandomPrefab(GameObject[] prefab)
    {
        int index = Random.Range(0, prefab.Length);
        return prefab[index];
    }
}
