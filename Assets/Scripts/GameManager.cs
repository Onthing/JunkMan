using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    public int level = 1;
    public int foodCount = 110;
    public List<Enemy> enemyList = new List<Enemy>();
    bool restStep = true;
    public Text foodText;
    public Text failText;
    private Player player;
    private MapManager mapManager;
    [HideInInspector] public bool isEnd = false;
    void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        InitGame();
    }
	void Start () {
        failText.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mapManager = GetComponent<MapManager>();
    }
    void InitGame()
    {
        // foodText = GameObject.Find("foodText").GetComponent<Text>();
        //mapManager.InitMap();
        UpdateFoodText(0);
       
    }
    void UpdateFoodText(int change)
    {
        if (change == 0)
            foodText.text = "food" + foodCount;
        else
        {
            string str = "";
            if (change < 0)
            {
                str = change.ToString();
            }
            else
            {
                str = "+" + change;
            }
            foodText.text = str + "food" + foodCount;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
    public void AddFood(int count)
    {
        foodCount += count;
        UpdateFoodText(count);
    }
    public void ReduceFood(int count)
    {
        foodCount -= count;
        UpdateFoodText(-count);
        if (foodCount <= 0)
            failText.enabled = true;
    }

    public void OnPlayerMove()
    {
        if (restStep)
        {
            restStep = false;
        }
        else
        {
            foreach (var enemy in enemyList)
            {
                enemy.Move();
            }
            restStep = true;
        }
        if (player.targetPos.x >=8 && player.targetPos.y >= 8)
        {
            isEnd = true;
            Debug.Log("arrive");
           // Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene("Game");
        }
    }
    void OnLevelWasLoaded(int sceneLevel)//系统函数
    {
        level++;
        //InitGame();//初始化游戏
    }
}
