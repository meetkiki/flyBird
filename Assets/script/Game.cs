using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    
    // 游戏状态
    public enum GameStatus
    {
        ON_READY,
        IN_GAME,
        GAME_OVER
    }

    private GameStatus status;


    public GameObject panelGameReady;
    public GameObject panelGamming;
    public GameObject panelGameOver;

    public PipelineManger pipelineManger;

    public Player player;

    public List<Text> uiScores = new List<Text>();

    public Text bestScore;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        this.status = GameStatus.ON_READY;
        this.player.onDeath += Player_onDeath;
        this.player.onScore = Player_Score;
        this.bestScore.text = PlayerPrefs.GetInt("best",0).ToString();
    }

    private void Player_Score(int score)
    {
        this.Score += score;
    }

    private void Player_onDeath()
    {
        this.status = GameStatus.GAME_OVER;
        pipelineManger.stopRun();
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }


    public void startGame()
    {
        this.Score = 0;
        this.status = GameStatus.IN_GAME;
        Debug.Log(string.Format("start Game ... {0}", this.Status.ToString()));
        pipelineManger.startRun();
        player.updateStatus(Player.PlayerStatus.FLY);
    }

    public void updateUI()
    {
        this.panelGameReady.SetActive(status == GameStatus.ON_READY);
        this.panelGamming.SetActive(status == GameStatus.IN_GAME);
        this.panelGameOver.SetActive(status == GameStatus.GAME_OVER);
    }


    private GameStatus Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            foreach (Text scoreText in uiScores)
            {
                scoreText.text = score.ToString();
            } 
            if (Convert.ToInt32(bestScore.text) < score)
            {
                this.bestScore.text = score.ToString();
                PlayerPrefs.SetInt("best", score);
            }
        }
    }
}
