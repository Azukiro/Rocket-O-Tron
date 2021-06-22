using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using SDD.Events;

/// This enum is used to manage the different main states of the game
public enum GameState { starting, menu, play, pause, resume, goToNextLevel, victory, gameover }

/// Manages the game mechanics
public class GameManager : MonoBehaviour, IEventHandler
{
    #region Singleton

    /// The unique instance of GameManager
    private static GameManager _Instance;

    /// The unique instance property
    public static GameManager Instance
    {
        get { return _Instance; }
        private set { }
    }

    /// <summary>
    ///     Singleton pattern
    /// </summary>
    private void Awake()
    {
        if (!_Instance)
            _Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion Singleton

    #region States

    /// The current state of the game
    private GameState _State = GameState.starting;

    /// Return True if the player is currently playing
    private bool IsPlaying
    {
        get
        {
            return _State == GameState.play || _State == GameState.resume;
        }
    }

    /// <summary>
    ///     Change the state of the game
    /// </summary>
    /// <param name="targetState">
    ///     The state to set
    /// </param>
    private void ChangeState(GameState targetState)
    {
        _State = targetState;
        switch (_State)
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
                EventManager.Instance.Raise(new GameBestScoreEvent() { _EBestScore = BestScore() });
                break;

            case GameState.gameover:
                OnGameOverState();
                EventManager.Instance.Raise(new GameOverEvent());
                EventManager.Instance.Raise(new GameBestScoreEvent() { _EBestScore = BestScore() });
                break;

            default:
                break;
        }
    }

    /// <summary>
    ///     Function called after calling ChangeState(GameState.menu)
    /// </summary>
    private void OnMenuState()
    {
        DisableGameTime();
    }

    /// <summary>
    ///     Function called after calling ChangeState(GameState.play)
    /// </summary>
    private void OnPlayState()
    {
        InitGame();
        EnableGameTime();
    }

    /// <summary>
    ///     Function called after calling ChangeState(GameState.pause)
    /// </summary>
    private void OnPauseState()
    {
        DisableGameTime();
    }

    /// <summary>
    ///     Function called after calling ChangeState(GameState.resume)
    /// </summary>
    private void OnResumeState()
    {
        EnableGameTime();
    }

    /// <summary>
    ///     Function called after calling ChangeState(GameState.nextLevel)
    /// </summary>
    private void OnNextLevelState()
    {
    }

    /// <summary>
    ///     Function called after calling ChangeState(GameState.victory)
    /// </summary>
    private void OnVictoryState()
    {
        DisableGameTime();
        SaveProgression();
    }

    /// <summary>
    ///     Function called after calling ChangeState(GameState.gameover)
    /// </summary>
    private void OnGameOverState()
    {
        DisableGameTime();
        SaveProgression();
    }

    #endregion States

    #region Game Management

    /// Max time of a game
    [SerializeField] private int _TimeBeforeLoose;

    /// Time between two pauses
    private float _PauseCountDown = 0;

    /// The HUD time counter
    private float _CountDown;

    /// The HUD score
    private int _Score;

    private void Start()
    {
        /// Store the current scene index
        InitScene();

        /// Init the HUD statistics
        InitGame();

        /// If the game is starting, launch the game menu
        if (_State == GameState.starting)
            ChangeState(GameState.menu);
    }

    /// <summary>
    ///     Update the HUD statistics
    /// </summary>
    private void InitGame()
    {
        _Score = 0;
        _CountDown = 0;
        UpdateStatistics();
    }

    /// <summary>
    ///     Manage the victory & defeat conditions and update the HUD statistics
    /// </summary>
    private void Update()
    {
        if (IsPlaying)
        {
            /// Update statistics
            _CountDown += Time.deltaTime;
            UpdateStatistics();

            /// Loose condition
            if (_TimeBeforeLoose <= _CountDown)
                ChangeState(GameState.gameover);

            /// Cancel condition
            float pauseInput = Input.GetAxis("Cancel");
            if (pauseInput == 1 && _PauseCountDown == 0)
            {
                /// Pause
                EventManager.Instance.Raise(new MenuPauseButtonClickedEvent());
                _PauseCountDown = 1;
            }
            else
            {
                /// Decrement the pauseCountDown
                _PauseCountDown = Mathf.Max(0, _PauseCountDown - Time.deltaTime);
            }
        }
    }

    #endregion Game Management

    #region Events

    /// <summary>
    ///     Subscribe to all the events
    /// </summary>
    private void OnEnable()
    {
        SubscribeEvents();
    }

    /// <summary>
    ///     Unsubscribe to all the events
    /// </summary>
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    /// <summary>
    ///     Subscribe to the following events :
    ///     - For the menu events : MenuButtonClickedEvent, MenuPlayButtonClickedEvent, MenuRePlayButtonClickedEvent, MenuPauseButtonClickedEvent, MenuNextLevelButtonClickedEvent, MenuResumeButtonClickedEvent
    ///     - For the game events : GamePlayerLooseLifeEvent, GamePlayerKillEnnemyEvent, GamePlayerInExitDoorEvent
    /// </summary>
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
        EventManager.Instance.AddListener<GamePlayerKillEnemyEvent>(GamePlayerKillEnnemy);
        EventManager.Instance.AddListener<GamePlayerInExitDoorEvent>(GamePlayerInExitDoor);
    }

    /// <summary>
    ///     Unsubscribe to the following events :
    ///     - For the menu events : MenuButtonClickedEvent, MenuPlayButtonClickedEvent, MenuRePlayButtonClickedEvent, MenuPauseButtonClickedEvent, MenuNextLevelButtonClickedEvent, MenuResumeButtonClickedEvent
    ///     - For the game events : GamePlayerLooseLifeEvent, GamePlayerKillEnnemyEvent, GamePlayerInExitDoorEvent
    /// </summary>
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
        EventManager.Instance.RemoveListener<GamePlayerKillEnemyEvent>(GamePlayerKillEnnemy);
        EventManager.Instance.RemoveListener<GamePlayerInExitDoorEvent>(GamePlayerInExitDoor);
    }

    /// <summary>
    ///     When a MenuButtonClickedEvent event is raised, change the state to open the menu
    /// </summary>
    /// <param name="e">The event</param>
    private void MenuButtonClickedEvent(MenuButtonClickedEvent e)
    {
        ChangeState(GameState.menu);
    }

    /// <summary>
    ///     When a MenuPlayButtonClickedEvent event is raised, change the state to play the game
    /// </summary>
    /// <param name="e">The event</param>
    private void PlayButtonClicked(MenuPlayButtonClickedEvent e)
    {
        ReloadScene();
        ChangeState(GameState.play);
    }

    /// <summary>
    ///     When a MenuRePlayButtonClickedEvent event is raised, change the state to replay the game
    /// </summary>
    /// <param name="e">The event</param>
    private void MenuRePlayButtonClicked(MenuRePlayButtonClickedEvent e)
    {
        ReloadScene();
        ChangeState(GameState.play);
    }

    /// <summary>
    ///     When a MenuPauseButtonClickedEvent event is raised, change the state to pause the game
    /// </summary>
    /// <param name="e">The event</param>
    private void MenuPauseButtonClicked(MenuPauseButtonClickedEvent e)
    {
        ChangeState(GameState.pause);
    }

    /// <summary>
    ///     When a MenuResumeButtonClickedEvent event is raised, change the state to resume the game
    /// </summary>
    /// <param name="e">The event</param>
    private void MenuResumeButtonClicked(MenuResumeButtonClickedEvent e)
    {
        ChangeState(GameState.resume);
    }

    /// <summary>
    ///     When a MenuNextLevelButtonClickedEvent event is raised, launch the next nevel
    /// </summary>
    /// <param name="e">The event</param>
    private void MenuNextLevelButtonClicked(MenuNextLevelButtonClickedEvent e)
    {
        NextScene();
    }

    /// <summary>
    ///     When a GamePlayerLooseLifeEvent event is raised, update the HUD statistics and exit the game if the Player is dead
    /// </summary>
    /// <param name="e">The event</param>
    private void GamePlayerLooseLife(GamePlayerLooseLifeEvent e)
    {
        UpdateStatistics();

        if (e._ELife <= 0)
            ChangeState(GameState.gameover);
    }

    /// <summary>
    ///     When a GamePlayerKillEnnemyEvent event is raised, update the score
    /// </summary>
    /// <param name="e">The event</param>
    private void GamePlayerKillEnnemy(GamePlayerKillEnemyEvent e)
    {
        _Score += 50;
        UpdateStatistics();
    }

    /// <summary>
    ///     When a GamePlayerInExitDoorEvent event is raised, the player has won
    /// </summary>
    /// <param name="e">The event</param>
    private void GamePlayerInExitDoor(GamePlayerInExitDoorEvent e)
    {
        ChangeState(GameState.victory);
    }

    /// <summary>
    ///     Update the HUD statistics using a GameStatisticsChangedEvent event
    /// </summary>
    private void UpdateStatistics()
    {
        EventManager.Instance.Raise(new GameStatisticsChangedEvent()
        {
            _EScore = _Score,
            _ETime = _CountDown
        });
    }

    #endregion Events

    #region Level Management

    /// All the scenes names that can be loaded to play a game level
    [SerializeField] private List<string> _SceneNames;

    /// The current index of the scene that is currently played
    private int _SceneIndex;

    /// <summary>
    ///     Initialize the _SceneIndex with the index of _SceneNames that corresponds to the current scene
    /// </summary>
    private void InitScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        _SceneIndex = _SceneNames.IndexOf(currentSceneName);
    }

    /// <summary>
    ///     Reload the current scene
    /// </summary>
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    ///     Lanch the next level scene and save the game progression
    /// </summary>
    private void NextScene()
    {
        _SceneIndex = (_SceneIndex + 1) % (_SceneNames.Count);
        SceneManager.LoadScene(_SceneNames[_SceneIndex]);
        SaveProgression();
    }

    #endregion Level Management

    #region Player Prefs

    /// <summary>
    ///     Get the best score of the Player in a specific level
    /// </summary>
    /// <returns>
    ///     The best score ever done that corresponds to the current game
    /// </returns>
    private int BestScore()
    {
        return PlayerPrefs.HasKey($"Score{_SceneIndex}") ? PlayerPrefs.GetInt($"Score{_SceneIndex}") : 0;
    }

    /// <summary>
    ///     Save the progression of the Player in the PlayerPrefs
    /// </summary>
    private void SaveProgression()
    {
        /// Get the best score ever done
        int bestScore = BestScore();

        /// If the current score is better
        if (bestScore < _Score)
        {
            /// Store the next best score
            PlayerPrefs.SetInt($"Score{_SceneIndex}", _Score);
            PlayerPrefs.Save();
        }
    }

    #endregion Player Prefs

    #region Game time

    /// <summary>
    ///     Freeze the game to simulate a pause
    /// </summary>
    private void DisableGameTime()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    ///     Unfreeze the game to stop the pause simulation
    /// </summary>
    private void EnableGameTime()
    {
        Time.timeScale = 1;
    }

    #endregion Game time
}