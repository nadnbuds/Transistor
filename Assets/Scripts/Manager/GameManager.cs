using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    // TODO replace
    private List<Vital> vitals;
    public UnityEvent onGameOver{ get; private set; }
    public Text gameOver;
    private List<Producer> producers;

    private enum States
    {
        Init,
        Play,
        GameOver
    }
    private StateMachine<States> fsm;

    private void Awake()
    {
        producers = new List<Producer>();
        fsm = StateMachine<States>.Initialize(this);
        onGameOver = new UnityEvent();
    }

    private void Start()
    {
        fsm.ChangeState(States.Init);
    }

    public void GameOver()
    {
        onGameOver.Invoke();
        //When invoke is down, functions that relate to onGameOver will be called
        fsm.ChangeState(States.GameOver);
    }

    public void AddProducer(Producer prod)
    {
        producers.Add(prod);
    }

    #region States
    private void Init_Enter()
    {
        // TODO
    }

    private void Play_Enter()
    {
        // TODO
    }

    private void Play_Update()
    {
        // TODO
    }

    private void GameOver_Enter()
    {
        Time.timeScale = 0;
        gameOver.text = "Game Over!";
        gameOver.gameObject.SetActive(true);
        // TODO
    }
    #endregion States
}