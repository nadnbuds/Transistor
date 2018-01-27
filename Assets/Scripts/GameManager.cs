using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class GameManager : Singleton<GameManager>
{
    // TODO replace
    private List<Vital> vitals;

    private enum States
    {
        Init,
        Play,
        GameOver
    }
    private StateMachine<States> fsm;

    private void Awake()
    {
        fsm = StateMachine<States>.Initialize(this);
    }

    private void Start()
    {
        fsm.ChangeState(States.Init);
    }

    public void GameOver()
    {
        fsm.ChangeState(States.GameOver);
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
        // TODO
    }
    #endregion States
}