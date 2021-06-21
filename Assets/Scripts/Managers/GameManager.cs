using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using SDD.Events;

public enum GameState { starting, menu, play, pause, resume, goToNextLevel, victory, gameover }

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
        if (!_instance)
            _instance = this;
        else
            Destroy(gameObject);
    }

    #endregion Singleton

    #region States

    // The current state of the game
    private GameState m_State = GameState.starting;

    // Return True if the player is currently playing
    private bool IsPlaying
    {
        get
        {
            return m_State == GameState.play || m_State == GameState.resume;
        }
    }

    // Change the state of the Game
    private void ChangeState(GameState targetState)
    {
        // Debug.Log(targetState);
        m_State = targetState;

        switch (m_State)
        {
            case GameState.menu:
                OnMenuState();
                EventManager.Instance.Raise(new GameMenuEvent());
                break;

            case GameState.play:
                OnPlayState();
                EventManager.Instance.Raise(new GamePlayEvent());
                break;

            case GameState.pause:
                OnPauseState();
                EventManager.Instance.Raise(new GamePauseEvent());
                break;

            case GameState.resume:
                OnResumeState();
                EventManager.Instance.Raise(new GameResumeEvent());
                break;

            case GameState.goToNextLevel:
                OnNextLevelState();
                break;

            case GameState.victory:
                OnVictoryState();
                EventManager.Instance.Raise(new GameVictoryEvent());
                break;

            case GameState.gameover:
                OnGameOverState();
                EventManager.Instance.Raise(new GameOverEvent());
                break;

            default:
                break;
        }
    }

    private void OnMenuState()
    {
        DisableGameTime();
    }

    private void OnPlayState()
    {
        InitGame();
        EnableGameTime();
    }

    private void OnPauseState()
    {
        DisableGameTime();
    }

    private void OnResumeState()
    {
        EnableGameTime();
    }

    private void OnNextLevelState()
    {
    }

    private void OnVictoryState()
    {
        DisableGameTime();
    }

    private void OnGameOverState()
    {
        DisableGameTime();
    }

    #endregion States

    #region Game Management

    // Max time of a game
    [SerializeField] private int TimeBeforeLoose;

    // The HUD time
    private float CountDown;

    // The HUD score
    private int Score;

    // The HUD user life
    private int UserLife;

    private void Start()
    {
        // Store the current scene index
        InitScene();

        // Init the HUD statistics
        InitGame();

        // If not redo, print the game menu
        if (m_State == GameState.starting)
            ChangeState(GameState.menu);
    }

    // Update the HUD statistics
    private void InitGame()
    {
        Score = 0;
        CountDown = 0;
        UserLife = 0;
        UpdateStatistics();
    }

    // Time between two pauses
    private float pauseCountDown = 0;

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
            if (pauseInput == 1 && pauseCountDown == 0)
            {
                // Pause
                EventManager.Instance.Raise(new MenuPauseButtonClickedEvent());
                pauseCountDown = 1;
            }
            else
            {
                pauseCountDown = Mathf.Max(0, pauseCountDown - Time.deltaTime);
            }
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
        EventManager.Instance.AddListener<MenuRePlayButtonClickedEvent>(MenuRePlayButtonClicked);
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
        EventManager.Instance.RemoveListener<MenuRePlayButtonClickedEvent>(MenuRePlayButtonClicked);
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
        ReloadScene();
        ChangeState(GameState.play);
    }

    private void MenuRePlayButtonClicked(MenuRePlayButtonClickedEvent e)
    {
        ReloadScene();
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
        NextScene();
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

    #region Level Management

    [SerializeField] private List<string> sceneNames;
    private int sceneIndex;

    private void InitScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        sceneIndex = sceneNames.IndexOf(currentSceneName);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextScene()
    {
        sceneIndex = (sceneIndex + 1) % (sceneNames.Count);
        SceneManager.LoadScene(sceneNames[sceneIndex]);
    }

    #endregion Level Management

    #region Game time

    private void DisableGameTime()
    {
        Time.timeScale = 0;
    }

    private void EnableGameTime()
    {
        Time.timeScale = 1;
    }

    #endregion Game time
}