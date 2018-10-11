using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
//TODO 难度选择界面，难度设定函数
public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameObject player;
    public GameObject enemy;

    public AudioSource destroy1;
    public AudioSource destroy2;
    public AudioSource destroy3;
    public AudioSource destroy4;
    public int destroySound;
    public AudioSource gameover;
    public AudioSource highestSound;

    public int hardValue;//难度加分

    public float GameHp = 1f;//每局设定的血量
    public float playerHealth;//当前血量
    public float hpWidth, hpHeight;//血条宽度高度
    public RectTransform Length;//UI组件的大小之类的就用它了

    public Text score;//得分文本显示\
    public int Point = 0;

    public float startWait=1f;//起始等待
    public int enemyNum;//单轮敌人个数
    public int wave = 15;//敌人波数
    public float waveWait = 2f;//每波间隔时间
    public float everyWait = 0.4f;//每个敌人生成的间隔时间
    private int hardCheck;

    private Quaternion spawnRotation;//敌人生成位置和旋转
    public Vector3 spawnValues;
    private Vector3 spawnPosition = Vector3.zero;

    public GameObject GameOver;
    public GameObject Replay;
    public GameObject ShowWord;
    public GameObject ShowHigh;
    private int temp;
    private bool highest = false;

    //StringReader sr;//最高分的文件读写部分
    //StreamWriter sw;
    //int[] highPoint = new int[10];//最高分文件

    private bool playing ;//游戏状态判断

    void Awake()
    {
        instance = this;
        playerHealth = GameHp;//初始化玩家生命值
    }
    // Use this for initialization
    void showEnd()//显示结束画面
    {
        checkHighScore();
        if (highest == true)
        {
            ShowHigh.SetActive(true);
            highestSound.Play();
        }
        else
            gameover.Play();
        GameOver.SetActive(true);
        Replay.SetActive(true);
        ShowWord.SetActive(true);
    }
    void saveHighScore()//保存最高分
    {
        PlayerPrefs.SetInt("HighScore", Point);
    }
    void checkHighScore()//检查最高分
    {
        if (Point > temp)
        {
            highest = true;
            saveHighScore();
        }
    }
    void Start()
    {
        playing = true;
        StartCoroutine(spawnWaves());
        hpWidth = Length.localScale.x;
        hardCheck = PlayerPrefs.GetInt("HardCheck");
        hardValue = PlayerPrefs.GetInt("HardValue");
        temp = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        addScore();
        checkPlay();
        Length.localScale = new Vector2(playerHealth / GameHp * hpWidth, hpHeight);//血条长度改变
        if (playing == false)
        {
            showEnd();
            if(Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("Play");
            if (Input.GetKeyDown(KeyCode.E))
                Application.Quit();
        }
    }
    void checkPlay()
    {
        if (playerHealth <= 0)
        {
            playing = false;
            Destroy(player);
        }
    }
    public void addScore()//分数登记
    {
        score.text = Point.ToString();
    }
    IEnumerator spawnWaves()//协程 敌人生成
    {
        yield return new WaitForSeconds(startWait);
        while (wave != 0)//波数监测
        {
            enemyNum = wave * hardCheck + 1;
            for (int i = 0; i < enemyNum; i++)//敌人生成
            {
                spawnPosition.x = Random.Range(-spawnValues.x, spawnValues.x);
                spawnPosition.y = Random.Range(-spawnValues.y, spawnValues.y);
                spawnPosition.z = spawnValues.z;
                spawnRotation = Quaternion.identity;
                GameObject one = Instantiate(enemy, spawnPosition, spawnRotation) as GameObject;
                Destroy(one, 6f);
                yield return new WaitForSeconds(everyWait);
                if ((playing) == false)
                    break;
            }
            if ((playing) == false)
                break;
            yield return new WaitForSeconds(waveWait);
            wave--;
            if (wave == 0)
                playing = false;
        }
    }
}
