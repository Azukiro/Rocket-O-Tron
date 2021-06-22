using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

/// This class manages the Menu Events and the showing of Panels
public class MenuManager : MonoBehaviour, IEventHandler
{
    #region Panels

    /// Panel of the main menu
    [SerializeField] private GameObject _MainMenuPanel;

    /// Panel of the pause menu
    [SerializeField] private GameObject _PauseMenuPanel;

    /// Panel of the victory menu
    [SerializeField] private GameObject _VictoryPanel;

    /// Panel of the defeat menu
    [SerializeField] private GameObject _GameOverPanel;

    /// List that contains all the previous panels
    private List<GameObject> _AllPanels;

    /// <summary>
    ///     Store all the panels in the _AllPanels list
    /// </summary>
    private void Awake()
    {
        _AllPanels = new List<GameObject>();
        _AllPanels.AddRange(new GameObject[] { _MainMenuPanel, _PauseMenuPanel, _VictoryPanel, _GameOverPanel });
    }

    /// <summary>
    ///     Remove all the panels except the one specified in parameters
    /// </summary>
    ///
    /// <remarks>
    ///     If null given, remove all the panels and directly show the game.
    /// </remarks>
    ///
    /// <param name="panel">
    ///     The panel to show, or null
    /// </param>
    private void SetPanel(GameObject panel)
    {
        _AllPanels.ForEach(item => item.SetActive(panel == item));
    }

    #endregion Panels

    #region Events

    /// <summary>
    ///     Subscribe to all events
    /// </summary>
    private void OnEnable()
    {
        SubscribeEvents();
    }

    /// <summary>
    ///     Unsuscribe to all events
    /// </summary>
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    /// <summary>
    ///     Subscribe to the following events : GameMenuEvent, GamePauseEvent, GameResumeEvent, GamePlayEvent, GameOverEvent, GameVictoryEvent
    /// </summary>
    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePauseMenu);
        EventManager.Instance.AddListener<GameResumeEvent>(GameResume);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
    }

    /// <summary>
    ///     Unsubscribe to the following events : GameMenuEvent, GamePauseEvent, GameResumeEvent, GamePlayEvent, GameOverEvent, GameVictoryEvent
    /// </summary>
    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePauseMenu);
        EventManager.Instance.RemoveListener<GameResumeEvent>(GameResume);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
        EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
    }

    /// <summary>
    ///     Show the game menu panel
    /// </summary>
    /// <param name="e">The event</param>
    private void GameMenu(GameMenuEvent e)
    {
        SetPanel(_MainMenuPanel);
    }

    /// <summary>
    ///     Show the pause menu panel
    /// </summary>
    /// <param name="e">The event</param>
    private void GamePauseMenu(GamePauseEvent e)
    {
        SetPanel(_PauseMenuPanel);
    }

    /// <summary>
    ///     Show the game, and remove all the panels
    /// </summary>
    /// <param name="e">The event</param>
    private void GameResume(GameResumeEvent e)
    {
        SetPanel(null);
    }

    /// <summary>
    ///     Show the game, and remove all the panels
    /// </summary>
    /// <param name="e">The game</param>
    private void GamePlay(GamePlayEvent e)
    {
        SetPanel(null);
    }

    /// <summary>
    ///     Show the game over panel
    /// </summary>
    /// <param name="e">The event</param>
    private void GameOver(GameOverEvent e)
    {
        SetPanel(_GameOverPanel);
    }

    /// <summary>
    ///     Show the game victory panel
    /// </summary>
    /// <param name="e">The event</param>
    private void GameVictory(GameVictoryEvent e)
    {
        SetPanel(_VictoryPanel);
    }

    #endregion Events

    #region UI callbacks

    /// <summary>
    ///     Raise an MenuPlayButtonClickedEvent event when the play button is clicked
    /// </summary>
    ///
    /// <remarks>
    ///     This function is automatically called by Unity because it's link to an UI onClick() callback event
    /// </remarks>
    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuPlayButtonClickedEvent());
    }

    /// <summary>
    ///     Raise an MenuButtonClickedEvent event when the menu button is clicked
    /// </summary>
    ///
    /// <remarks>
    ///     This function is automatically called by Unity because it's link to an UI onClick() callback event
    /// </remarks>
    public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuButtonClickedEvent());
    }

    /// <summary>
    ///     Raise an MenuRePlayButtonClickedEvent event when the replay button is clicked
    /// </summary>
    ///
    /// <remarks>
    ///     This function is automatically called by Unity because it's link to an UI onClick() callback event
    /// </remarks>
    public void MenuRePlayButtonClicked()
    {
        EventManager.Instance.Raise(new MenuRePlayButtonClickedEvent());
    }

    /// <summary>
    ///     Raise an MenuPauseButtonClickedEvent event when the pause button is clicked
    /// </summary>
    ///
    /// <remarks>
    ///     This function is automatically called by Unity because it's link to an UI onClick() callback event
    /// </remarks>
    public void MenuPauseButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuPauseButtonClickedEvent());
    }

    /// <summary>
    ///     Raise an MenuNextLevelButtonClickedEvent event when the next level button is clicked
    /// </summary>
    ///
    /// <remarks>
    ///     This function is automatically called by Unity because it's link to an UI onClick() callback event
    /// </remarks>
    public void NextLevelButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuNextLevelButtonClickedEvent());
    }

    /// <summary>
    ///     Raise an MenuResumeButtonClickedEvent event when the resume button is clicked
    /// </summary>
    ///
    /// <remarks>
    ///     This function is automatically called by Unity because it's link to an UI onClick() callback event
    /// </remarks>
    public void ResumeButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuResumeButtonClickedEvent());
    }

    #endregion UI callbacks
}