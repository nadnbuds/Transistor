using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

// decrement vitals
// generate
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Text gameOver;
    [SerializeField]
    private Transform randomPointsParent;
    [SerializeField]
    private List<VitalBehavior> vitalList;
    // public UnityEvent OnGameOver { get; private set; }

    [SerializeField]
    private List<Event> eventList;
    //private List<EventData> eventList;
    public Event CurrentEvent { get; private set; }
    public UnityEvent OnEventChange { get; private set; }

    [Space(10)]

    [SerializeField]
    private AudioInfo backgroundAudio = null;

    private float TimeT;

    // Float is cooldown time
    private Dictionary<Producer, float> producers = new Dictionary<Producer, float>();
    //private Dictionary<Producer, double> producers = new Dictionary<Producer, double>();
    private Dictionary<VitalBehavior, float> vitals = new Dictionary<VitalBehavior, float>();
    //private Dictionary<VitalBehavior, double> vitals = new Dictionary<VitalBehavior, double>();

    private enum States
    {
        Play,
        GameOver
    }
    private StateMachine<States> fsm;

    private void Awake()
    {
        OnEventChange = new UnityEvent();
        // Initialize Vitals
        foreach (VitalBehavior v in vitalList)
        {
            vitals[v] = v.DecrementInterval;
        }

        // Initialize FSM
        fsm = StateMachine<States>.Initialize(this);
        // OnGameOver = new UnityEvent();
    }

    private void Start()
    {
        TimeT = 0;
        StartRandomEvent();
        fsm.ChangeState(States.Play);
        //Play background music
        AudioManager.Instance.PlayLoopingAudioSource(backgroundAudio);
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
    private void Play_Enter()
    {
        // Setup points
        List<Transform> randomPoints = new List<Transform>(randomPointsParent.GetComponentsInChildren<Transform>());
        foreach (Producer p in producers.Keys)
        {
            int index = Random.Range(0, randomPoints.Count);
            p.transform.position = randomPoints[index].position;
            randomPoints.RemoveAt(index);
        }
    }

    private void StartRandomEvent()
    {
        int rand = Random.Range(0, eventList.Count - 1);
        Event selectedEvent = eventList.ElementAt(rand);
        CurrentEvent = selectedEvent;
        OnEventChange.Invoke();
        if (selectedEvent.eventDataList.Count <= 0)
        {
            Debug.Log("Empty eventDataList in selected event.");
            return;
        }
        //Debug.Log ("Selected event: " + selectedEvent.eventDataList.ElementAt(rand));
        Debug.Log("Selected event: " + selectedEvent.name);
        foreach (EventData event_data in selectedEvent.eventDataList)
        {
            if (event_data.vital.name == "Thought")
            {
                if (event_data.number < 0)
                {
                    vitalList.ElementAt(0).DecrementInterval = 1;
                }
                else if (event_data.number == 0)
                {
                    vitalList.ElementAt(0).DecrementInterval = 2;
                }
                else if (event_data.number > 0)
                {
                    vitalList.ElementAt(0).DecrementInterval = 3;
                }
            }
            else if (event_data.vital.name == "Digestion")
            {
                if (event_data.number < 0)
                {
                    vitalList.ElementAt(1).DecrementInterval = 1;
                }
                else if (event_data.number == 0)
                {
                    vitalList.ElementAt(1).DecrementInterval = 2;
                }
                else if (event_data.number > 0)
                {
                    vitalList.ElementAt(1).DecrementInterval = 3;
                }
            }
            else if (event_data.vital.name == "HeartBeat")
            {
                if (event_data.number < 0)
                {
                    vitalList.ElementAt(2).DecrementInterval = 1;
                }
                else if (event_data.number == 0)
                {
                    vitalList.ElementAt(2).DecrementInterval = 2;
                }
                else if (event_data.number > 0)
                {
                    vitalList.ElementAt(2).DecrementInterval = 3;
                }
            }
        }
    }

    // Updates producer generation intervals and vital decrement intervals
    private void Play_Update()
    {
        TimeT += Time.deltaTime;
        if (TimeT >= 10)
        {
            Debug.Log("TimeT: " + TimeT);
            StartRandomEvent();
            TimeT = 0;
        }
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