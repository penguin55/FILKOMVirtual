using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class LoginPanel : BasePanel
{
    [Header("References")]
    [SerializeField] private TMP_InputField nimField;
    [SerializeField] private TMP_InputField passField;

    private MainMenuManager mainMenuManager;
    private DebugMessageManager message;

    private void Awake()
    {
        mainMenuManager = ServiceLocator.Resolve<MainMenuManager>();
        message = ServiceLocator.Resolve<DebugMessageManager>();
    }

    public async void LoginAccount()
    {
        string nim = nimField.text;
        string pass = passField.text;

        int code_pass = await TGAuth.Login(nim, pass);
        ResponseCode(code_pass);
    }

    public void BackToMain()
    {
        mainMenuManager.ChangePanelTo(MainMenuManager.PanelType.MainPanel);
    }

    public void StartTheGame()
    {
        mainMenuManager.StartTheGame();
    }

    private void ResponseCode(int code)
    {
        switch (code)
        {
            case 200:
                StartTheGame();
                break;
            case 101:
                message.ShowMessage($"{code}: Password Salah!");
                break;
            case 404:
                message.ShowMessage($"{code}: Akun tidak ditemukan! Mohon hubungi panitia PK2Maba.");
                break;
        }
    }
}
