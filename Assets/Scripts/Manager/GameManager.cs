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
    private List<VitalBehavior> vitalList;
    public UnityEvent OnGameOver { get; private set; }

    // Float is cooldown time
    private Dictionary<Producer, float> producers = new Dictionary<Producer, float>();
    private Dictionary<VitalBehavior, float> vitals = new Dictionary<VitalBehavior, float>();

    private enum States
    {
        Init,
        Play,
        GameOver
    }
    private StateMachine<States> fsm;

    private void Awake()
    {
        // Initialize Vitals
        foreach (VitalBehavior v in vitalList)
        {
            vitals[v] = v.DecrementInterval;
        }

        // Initialize FSM
        fsm = StateMachine<States>.Initialize(this);
        OnGameOver = new UnityEvent();
    }

    private void Start()
    {
        fsm.ChangeState(States.Play);
    }

    public void GameOver()
    {
        //OnGameOver.Invoke();//
        //When invoke is down, functions that relate to onGameOver will be called
        print(Time.frameCount);
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

        List<VitalBehavior> vb = new List<VitalBehavior>();
        foreach (var v in vitals)
        {
            vb.Add(v.Key);
        }

        for (int i = 0; i < vb.Count; ++i)
        {
            VitalBehavior vital = vb[i];
            if (vitals[vital] <= 0f)
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