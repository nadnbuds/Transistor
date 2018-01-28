using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.Events;
using UnityEngine.UI;

// decrement vitals
// generate
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Text gameOver;
    [SerializeField]
    private List<Vital> vitalList;
    public UnityEvent OnGameOver { get; private set; }

    // Float is cooldown time
    private Dictionary<Producer, float> producers;
    private Dictionary<Vital, float> vitals;

    private enum States
    {
        Init,
        Play,
        GameOver
    }
    private StateMachine<States> fsm;

    private void Awake()
    {
        // Initialize Producers
        producers = new Dictionary<Producer, float>();

        // Initialize Vitals
        vitals = new Dictionary<Vital, float>();
        foreach (Vital v in vitalList)
        {
            vitals[v] = v.DecrementInterval;
        }

        // Initialize FSM
        fsm = StateMachine<States>.Initialize(this);
        OnGameOver = new UnityEvent();
    }

    private void Start()
    {
        fsm.ChangeState(States.Init);
    }

    public void GameOver()
    {
        OnGameOver.Invoke();
        //When invoke is down, functions that relate to onGameOver will be called
        fsm.ChangeState(States.GameOver);
    }

    public void AddProducer(Producer prod)
    {
        producers.Add(prod, prod.SpawnInterval);
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

    // Updates producer generation intervals and vital decrement intervals
    private void Play_Update()
    {
        foreach (var p in producers)
        {
            Producer prod = p.Key;
            if (p.Value <= 0f)
            {
                producers[prod] = prod.SpawnInterval;
                prod.Generate();
            }
            else
            {
                producers[prod] -= Time.deltaTime;
            }
        }
        foreach (var v in vitals)
        {
            Vital vital = v.Key;
            if (v.Value <= 0f)
            {
                vitals[vital] = vital.DecrementInterval;
                vital.Health--;
            }
            else
            {
                vitals[vital] -= Time.deltaTime;
            }
        }
    }

    private void Play_Exit()
    {

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