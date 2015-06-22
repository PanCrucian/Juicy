using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {
    [System.Serializable]
    public class CustomData
    {
        public Text coins;
        public Text score;
        public Text timer;
        public Button replay;
    }
    public CustomData data;
    public enum GameStates
    {
        Pause,
        Game
    }
    public GameStates state;

    public int score;
    public int coins;
    public int timer = 3;
    private int timerCounter;

    public static Game Instance
    {
        get
        {
            return _instance;
        }
    }
    private static Game _instance;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        timerCounter = timer;
        StartCoroutine(TimerNumerator());
        if (PlayerPrefs.HasKey("Coins"))
            coins = PlayerPrefs.GetInt("Coins");
    }

    void Update()
    {
        data.score.text = score.ToString();
        data.coins.text = coins.ToString();

        data.timer.text = timerCounter.ToString();
        if (timerCounter == 0)
        {
            if (data.timer.gameObject.activeSelf)
            {
                state = GameStates.Game;
                data.timer.gameObject.SetActive(false);
                data.score.gameObject.SetActive(true);
            }
        }
    }

    public void IncScore()
    {
        score++;
    }
    public void IncScore(int value)
    {
        score += value;
    }

    public void IncCoins()
    {
        coins++;
        PlayerPrefs.SetInt("Coins", coins);
    }
    public void IncCoins(int value)
    {
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);
    }

    IEnumerator TimerNumerator()
    {
        yield return new WaitForSeconds(0.5f);
        timerCounter--;
        if (timer > 0)
            StartCoroutine(TimerNumerator());
    }

    public void GameOver()
    {
        StartCoroutine(GameOverNumerator());  
    }

    IEnumerator GameOverNumerator()
    {
        yield return new WaitForSeconds(0.75f);
        data.replay.gameObject.SetActive(true);
    }

    public void Replay()
    {
        Application.LoadLevel("Game");
    }
}
