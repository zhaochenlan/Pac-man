using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public static float gameTime = 0;
    public int pacman_life = 3;
    public int noOfDots = 120;
    public GameObject pacman_prototype;
    public static GameObject pacman;
    public GameObject pacmanLife;
    public static wayPoint StartWP;
    public wayPoint pacmanStartWP;
    protected float GameStartTime;

    public GameObject redMonster;
	public GameObject blueMonster;
	public GameObject pinkMonster;
	public GameObject greenMonster;
	public GameObject purpleMonster;
    public List<wayPoint> PatrolPath;
    public wayPoint[] boxWP;

    public static AudioSource eatDots;
    public static AudioSource death;
    public static AudioSource powerUp;
    public static AudioSource eatMonster;
    public static AudioSource winMusic;

    public Text ScoreLable;
    public Text TimeLable;

    enum GameState
    {
        inGame,
        gameOver
    }

    GameState myGameState;

    private void Awake()
    {
        StartWP = GameObject.Find("wayPoint").GetComponent<wayPoint>();
        setUpGame();
        ScoreLable = GameObject.Find("Score").GetComponent<Text>();
        TimeLable = GameObject.Find("Timer").GetComponent<Text>();
        setUpAudio();
    }

    // Start is called before the first frame update
    void Start()
    {
        pacman = GameObject.FindWithTag("pacman");
        myGameState = GameState.inGame;
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        inputManger();
        if (myGameState == GameState.inGame)
        {
            ScoreLable.text = score + "";

            gameTime = Time.time - GameStartTime;
            TimeLable.text = "time:" + (int)gameTime;

            if (score >= noOfDots)
                win();
        }
    }

    void setUpAudio() {
        eatDots = GameObject.Find("EatDots").GetComponent<AudioSource>();
        death = GameObject.Find("Death").GetComponent<AudioSource>();
        powerUp = GameObject.Find("powerUp").GetComponent<AudioSource>();
        eatMonster = GameObject.Find("EatMonster").GetComponent<AudioSource>();
        winMusic = GameObject.Find("Ready").GetComponent<AudioSource>();
    }

    void inputManger()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && myGameState == GameState.gameOver)
        {
            SceneManager.LoadScene("MenuScene");
        }

    }

    virtual protected void setUpGame() {
        GameStartTime = Time.time;
        setUpMonsters();
        setupPacmanLife();
    }

    virtual protected void setUpMonsters()
    {
        GameObject monster_new;
        monster_new = redMonster;
        monster_new.GetComponent<monster_movement>().weekUpTime = 1;
        monster_new.GetComponent<monster_movement>().nextWp = StartWP;
        Instantiate(monster_new, new Vector3(-1.71f,-2.23f,0), Quaternion.identity);

        monster_new = pinkMonster;
        monster_new.GetComponent<monster_movement>().weekUpTime = 2;
        monster_new.GetComponent<monster_movement>().nextWp = StartWP;
        Instantiate(monster_new, new Vector3(1.73f, -2.19f, 0), Quaternion.identity);

        monster_new = blueMonster;
        monster_new.GetComponent<monster_movement>().weekUpTime = 3;
        monster_new.GetComponent<monster_movement>().nextWp = StartWP;
        Instantiate(monster_new, new Vector3(-1.71f, -4.34f, 0), Quaternion.identity);

        monster_new = greenMonster;
        monster_new.GetComponent<monster_movement>().weekUpTime = 4;
        monster_new.GetComponent<monster_movement>().nextWp = StartWP;
        monster_new.GetComponent<monster_movement_green>().PatrolPath = PatrolPath;
        Instantiate(monster_new, new Vector3(1.73f, -4.34f, 0), Quaternion.identity);
    }

    protected void setupPacmanLife() {
        for (int i = 0; i < pacman_life; i++) {
            Object.Instantiate(pacmanLife, new Vector3(8+2*i, 14, 0), Quaternion.identity);
        }
    }

    void win()
    {
        GameObject.Find("GameOver").GetComponent<Text>().text = "  You Win";
        GameObject.Find("notice").GetComponent<Text>().text = "press 'Esc' back to menu";
        winMusic.Play();
        myGameState = GameState.gameOver;
        Time.timeScale = 0;
    }

    void gameOver()
    {
        GameObject.Find("GameOver").GetComponent<Text>().text = "Game Over";
        GameObject.Find("notice").GetComponent<Text>().text = "press 'Esc' back to menu";
        myGameState = GameState.gameOver;
        Time.timeScale = 0;
    }

    public void pacmanDeth() {
        Destroy(GameObject.FindWithTag("pacmanLife"));
        pacman_life--;
        death.Play();
        if (pacman_life >= 0)
        {
            Object.Instantiate(pacman_prototype, pacmanStartWP.transform.position, Quaternion.identity);
            StartCoroutine(FindNewPacman());

        } else
        {
            gameOver();
        }
        
    }

    IEnumerator FindNewPacman()
    {
        yield return new WaitForSeconds(0.1f);
        pacman = GameObject.FindWithTag("pacman");
    }
}
