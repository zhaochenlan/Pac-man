using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float gameTime = 0;
    public int pacman_life = 3;
    public int noOfDots = 120;
    public GameObject pacman;
    public GameObject pacmanLife;
    public wayPoint StartWP;
    public wayPoint pacmanStartWP;
    protected float GameStartTime;

	public GameObject redMonster;
	public GameObject blueMonster;
	public GameObject pinkMonster;
	public GameObject greenMonster;
	public GameObject purpleMonster;
    public List<wayPoint> PatrolPath;
    public wayPoint[] boxWP;

    public AudioSource eatDots;
    public AudioSource death;
    public AudioSource powerUp;
    public AudioSource eatMonster;
    public AudioSource winMusic;

    enum gameState
    {
        ready,
        inGame,
        gameOver
    }

    gameState myGameState = gameState.inGame;

    // Start is called before the first frame update
    void Start()
    {
        setUpGame();
        myGameState = gameState.inGame;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Score").GetComponent<Text>().text = score+"";

        gameTime = Time.time - GameStartTime;
        GameObject.Find("Timer").GetComponent<Text>().text = "time:"+(int)gameTime;
        inputManger();

        if (score>=noOfDots) {
            score = 0;
            win();
        }

    }

    void inputManger()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && myGameState==gameState.gameOver)
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
        GameObject monster_new = null;
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
        myGameState = gameState.gameOver;
        Time.timeScale = 0;
    }

    void gameOver()
    {
        GameObject.Find("GameOver").GetComponent<Text>().text = "Game Over";
        GameObject.Find("notice").GetComponent<Text>().text = "press 'Esc' back to menu";
        myGameState = gameState.gameOver;
        Time.timeScale = 0;
    }

    public void pacmanDeth() {
        Destroy(GameObject.FindWithTag("pacmanLife"));
        pacman_life--;
        death.Play();
        if (pacman_life >= 0)
        {
            Object.Instantiate(pacman, pacmanStartWP.transform.position, Quaternion.identity);
        } else
        {
            gameOver();
        }
        
    }
}
