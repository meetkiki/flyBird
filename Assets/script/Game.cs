using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        panelGameReady.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }


    public void startGame()
    {
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
}
