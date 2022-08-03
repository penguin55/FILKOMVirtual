using System.Collections;
using System.Collections.Generic;
using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoGedung : BasePanel
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI gedungName;
    [SerializeField] private Image gedungImage;
    [SerializeField] private TextMeshProUGUI gedungDescription;
    [SerializeField] private TextMeshProUGUI gedungURLVR;
    [SerializeField] private Button urlVR;
    [SerializeField] private Button cancelButton;

    private PlayerController player;
    private InfoGedungData lastGedungData;

    private void Awake()
    {
        player = ServiceLocator.Resolve<PlayerController>();
    }

    public void ShowInfoGedung(InfoGedungData infoGedungData)
    {
        player.Pause(true);
        panel.SetActive(true);
        cancelButton.onClick.AddListener(() => ClosePanel());
        lastGedungData = infoGedungData;

        RenderInfo();
    }

    private void RenderInfo()
    {
        urlVR.onClick.RemoveAllListeners();
        gedungName.text = lastGedungData.namaGedung;
        gedungImage.sprite = lastGedungData.gambarGedung;
        gedungDescription.text = lastGedungData.deskripsiGedung;

        if (!lastGedungData.urlVR.Equals(string.Empty))
        {
            EnableVRMode();
            urlVR.onClick.AddListener(()=> RedirectToBrowser(lastGedungData.urlVR));
        } else
        {
            DisableVRMode();
        }
    }

    private void EnableVRMode()
    {
        urlVR.interactable = true;
        gedungURLVR.text = $"<color=\"green\"> VR Tersedia";
    }

    private void DisableVRMode()
    {
        urlVR.interactable = false;
        gedungURLVR.text = $"<color=\"red\"> VR Tidak Tersedia";
    }

    private void RedirectToBrowser(string url)
    {
        Application.OpenURL(url);
        print($"Redirect to {url}");
    }

    public void ClosePanel()
    {
        cancelButton.onClick.RemoveAllListeners();
        panel.SetActive(false);
        lastGedungData = null;
        player.Pause(false);
    }
}
