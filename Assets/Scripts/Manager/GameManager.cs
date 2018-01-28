using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.Events;
using System.Linq;
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

    [Space(10)]

    [SerializeField]
    private AudioInfo backgroundAudio = null;

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
        //Play background music
        AudioManager.Instance.PlayLoopingAudioSource(backgroundAudio);

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
        foreach (var p in producers.Keys.ToList())
        {
            if (producers[p] <= 0f)
            {
                producers[p] = p.SpawnInterval;
                p.Generate();
            }
            else
            {
                producers[p] -= Time.deltaTime;
            }
        }
        foreach (var v in vitals.Keys.ToList())
        {
            if (vitals[v] <= 0f)
            {
                vitals[v] = v.DecrementInterval;
                v.Health--;
            }
            else
            {
                vitals[v] -= Time.deltaTime;
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