using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class MenuManager : MonoBehaviour, IEventHandler
{
    #region Panels

    [SerializeField] private GameObject m_MainMenuPanel;
    [SerializeField] private GameObject m_PauseMenuPanel;
    [SerializeField] private GameObject m_VictoryPanel;
    [SerializeField] private GameObject m_GameOverPanel;

    private List<GameObject> AllPanels;

    private void Awake()
    {
        AllPanels = new List<GameObject>();
        AllPanels.AddRange(new GameObject[] { m_MainMenuPanel, m_PauseMenuPanel, m_VictoryPanel, m_GameOverPanel });
    }

    private void SetPanel(GameObject panel)
    {
        AllPanels.ForEach(item => item.SetActive(panel == item));
    }

    #endregion Panels

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
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePauseMenu);
        EventManager.Instance.AddListener<GameResumeEvent>(GameResume);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePauseMenu);
        EventManager.Instance.RemoveListener<GameResumeEvent>(GameResume);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
        EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
    }

    private void GameMenu(GameMenuEvent e)
    {
        SetPanel(m_MainMenuPanel);
    }

    private void GamePauseMenu(GamePauseEvent e)
    {
        SetPanel(m_PauseMenuPanel);
    }

    private void GameResume(GameResumeEvent e)
    {
        SetPanel(null);
    }

    private void GamePlay(GamePlayEvent e)
    {
        SetPanel(null);
    }

    private void GameOver(GameOverEvent e)
    {
        SetPanel(m_GameOverPanel);
    }

    private void GameVictory(GameVictoryEvent e)
    {
        SetPanel(m_VictoryPanel);
    }

    #endregion Events

    #region UI callbacks

    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuPlayButtonClickedEvent());
    }

    public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuButtonClickedEvent());
    }

    public void MenuPauseButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuPauseButtonClickedEvent());
    }

    public void NextLevelButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuNextLevelButtonClickedEvent());
    }

    public void ResumeButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MenuResumeButtonClickedEvent());
    }

    #endregion UI callbacks
}