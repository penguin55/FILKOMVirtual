using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : BaseManager
{
    [Header("Panels")]
    [SerializeField] private BasePanel mainPanel;
    [SerializeField] private BasePanel loginPanel;

    private PanelType currentPanel;

    public enum PanelType { None, MainPanel, LoginPanel}

    private void Awake()
    {
        OnInitialize();
        SceneManagement.ReadyToLoad();
    }

    private void Start()
    {
        ChangePanelTo(PanelType.MainPanel);
        OnStart();
    }

    public void ChangePanelTo(PanelType panelType)
    {
        DeactivateCurrentPanel();
        currentPanel = panelType;
        ActivateCurrentPanel();
    }

    public void StartTheGame()
    {
        SceneManagement.LoadScene("GAME");
    }

    private void DeactivateCurrentPanel()
    {
        switch (currentPanel)
        {
            case PanelType.MainPanel:
                mainPanel.ActivatePanel(false);
                break;
            case PanelType.LoginPanel:
                loginPanel.ActivatePanel(false);
                break;
        }
    }

    private void ActivateCurrentPanel()
    {
        switch (currentPanel)
        {
            case PanelType.MainPanel:
                mainPanel.ActivatePanel(true);
                break;
            case PanelType.LoginPanel:
                loginPanel.ActivatePanel(true);
                break;
        }
    }
}
