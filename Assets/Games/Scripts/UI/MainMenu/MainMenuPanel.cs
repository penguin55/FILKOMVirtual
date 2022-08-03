using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class MainMenuPanel : BasePanel
{
    private MainMenuManager mainMenuManager;

    private void Awake()
    {
        mainMenuManager = ServiceLocator.Resolve<MainMenuManager>();
    }

    private void Start()
    {
        AudioManager.PlayBGM("main-bgm");
    }

    public void PlayAsGuest()
    {
        mainMenuManager.StartTheGame();
    }

    public void PlayAsAccount()
    {
        mainMenuManager.ChangePanelTo(MainMenuManager.PanelType.LoginPanel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
