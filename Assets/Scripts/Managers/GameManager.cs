using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public enum GameState { menu, play, pause, resume, goToNextLevel, victory, gameover }

public class GameManager : MonoBehaviour, IEventHandler
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
        private set { }
    }

    private void Awake()
    {
        if (!_instance) _instance = this;
        else Destroy(gameObject);
    }

    #endregion Singleton

    #region States

    // The current state of the game
    private GameState m_State;

    // Return True if the player is currently playing
    public bool IsPlaying
    {
        get
        {
            return m_State == GameState.play || m_State == GameState.resume;
        }
    }

    private void Start()
    {
        InitGame();
        ChangeState(GameState.menu);
    }

    private void ChangeState(GameState targetState)
    {
        m_State = targetState;
        switch (m_State)
        {
            case GameState.menu:
                EventManager.Instance.Raise(new GameMenuEvent());
                break;

            case GameState.play:
                InitGame();
                EventManager.Instance.Raise(new GamePlayEvent());
                break;

            case GameState.pause:
                EventManager.Instance.Raise(new GamePauseEvent());
                break;

            case GameState.resume:
                EventManager.Instance.Raise(new GameResumeEvent());
                break;

            case GameState.goToNextLevel:

                break;

            case GameState.victory:
                EventManager.Instance.Raise(new GameVictoryEvent());
                break;

            case GameState.gameover:
                EventManager.Instance.Raise(new GameOverEvent());
                break;

            default:
                break;
        }
    }

    #endregion States

    #region Game Management

    // Max time of a game
    [SerializeField] private int TimeBeforeLoose;

    // The Timer of the game
    private float CountDown;

    private int Score;

    private int UserLife;

    private void InitGame()
    {
        Score = 0;
        CountDown = 0;
        UserLife = 0;
    }

    private void Update()
    {
        if (IsPlaying)
        {
            // Update statistics
            CountDown += Time.deltaTime;
            UpdateStatistics();

            // Loose condition
            if (TimeBeforeLoose <= CountDown)
                ChangeState(GameState.gameover);

            // Cancel condition
            float pauseInput = Input.GetAxis("Cancel");
            if (pauseInput == 1)
                EventManager.Instance.Raise(new MenuPauseButtonClickedEvent());
        }
    }

    #endregion Game Management

    #region Events

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void SubscribeEvents()
    {
        // Menu buttons
        EventManager.Instance.AddListener<MenuButtonClickedEvent>(MenuButtonClickedEvent);
        EventManager.Instance.AddListener<MenuPlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<MenuPauseButtonClickedEvent>(MenuPauseButtonClicked);
        EventManager.Instance.AddListener<MenuNextLevelButtonClickedEvent>(MenuNextLevelButtonClicked);
        EventManager.Instance.AddListener<MenuResumeButtonClickedEvent>(MenuResumeButtonClicked);

        // Game events
        EventManager.Instance.AddListener<GamePlayerLooseLifeEvent>(GamePlayerLooseLife);
        EventManager.Instance.AddListener<GamePlayerKillEnnemyEvent>(GamePlayerKillEnnemy);
        EventManager.Instance.AddListener<GamePlayerInExitDoorEvent>(GamePlayerInExitDoor);
    }

    public void UnsubscribeEvents()
    {
        // Menu buttons
        EventManager.Instance.RemoveListener<MenuButtonClickedEvent>(MenuButtonClickedEvent);
        EventManager.Instance.RemoveListener<MenuPlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<MenuPauseButtonClickedEvent>(MenuPauseButtonClicked);
        EventManager.Instance.RemoveListener<MenuNextLevelButtonClickedEvent>(MenuNextLevelButtonClicked);
        EventManager.Instance.RemoveListener<MenuResumeButtonClickedEvent>(MenuResumeButtonClicked);

        // Game events
        EventManager.Instance.RemoveListener<GamePlayerLooseLifeEvent>(GamePlayerLooseLife);
        EventManager.Instance.RemoveListener<GamePlayerKillEnnemyEvent>(GamePlayerKillEnnemy);
        EventManager.Instance.RemoveListener<GamePlayerInExitDoorEvent>(GamePlayerInExitDoor);
    }

    private void MenuButtonClickedEvent(MenuButtonClickedEvent e)
    {
        ChangeState(GameState.menu);
    }

    private void PlayButtonClicked(MenuPlayButtonClickedEvent e)
    {
        ChangeState(GameState.play);
    }

    private void MenuPauseButtonClicked(MenuPauseButtonClickedEvent e)
    {
        ChangeState(GameState.pause);
    }

    private void MenuResumeButtonClicked(MenuResumeButtonClickedEvent e)
    {
        ChangeState(GameState.resume);
    }

    private void MenuNextLevelButtonClicked(MenuNextLevelButtonClickedEvent e)
    {
    }

    private void GamePlayerLooseLife(GamePlayerLooseLifeEvent e)
    {
        UserLife = e.eLife;
        UpdateStatistics();

        if (e.eLife <= 0)
            ChangeState(GameState.gameover);
    }

    private void GamePlayerKillEnnemy(GamePlayerKillEnnemyEvent e)
    {
        Score += 50;
        UpdateStatistics();
    }

    private void GamePlayerInExitDoor(GamePlayerInExitDoorEvent e)
    {
        ChangeState(GameState.victory);
    }

    private void UpdateStatistics()
    {
        EventManager.Instance.Raise(new GameStatisticsChangedEvent()
        {
            eScore = Score,
            eLife = UserLife,
            eTime = CountDown
        });
    }

    #endregion Events
}