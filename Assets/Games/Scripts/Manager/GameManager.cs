using MonsterLove.StateMachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomGustin.GameDesignPattern;
using TomGustin.Interface;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseManager[] systemLoads;

    [SerializeField, ReadOnly] private GameState currentState;

    private UIManager uim;
    private PlayerController player;
    private StateMachine<GameState> state;

    public enum GameState { None, Initialization, Pause, Play}

    private bool initialized;

    private void Awake()
    {
        StartCoroutine(PreInit());
    }

    private IEnumerator PreInit()
    {
        Initialization();
        yield return new WaitUntil(()=> initialized);
        SceneManagement.ReadyToLoad();
        yield return new WaitUntil(SceneManagement.CompleteLoading);
        state.ChangeState(GameState.Initialization);
    }

    private async void Initialization()
    {
        initialized = await RangkaianManager.Sync();
        uim = ServiceLocator.Resolve<UIManager>();
        state = StateMachine<GameState>.Initialize(this);
        player = ServiceLocator.Resolve<PlayerController>();
        state.Changed += (currentState) => this.currentState = currentState;
    }

    private void InitData()
    {
        AccountSaveData activeAccount = AccountManager.Instance.GetActiveAccount();
        uim.UpdatePlayerInfo(activeAccount.user_name, activeAccount.user_id);
    }

    #region States
    private void Initialization_Enter()
    {
        InitData();

        foreach (BaseManager baseSystem in systemLoads)
        {
            (baseSystem as ISystemLoad).Initialized();
        }

        state.ChangeState(GameState.Play);
    }

    private void Play_Enter()
    {
        player.InitPlayer();
    }
    #endregion

    public void ExitToMenu()
    {
        AccountManager.Instance.Logout();
        SceneManagement.LoadScene("MENU");
    }
}
