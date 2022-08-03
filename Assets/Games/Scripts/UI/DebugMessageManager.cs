using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMessageManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;

    private void Awake()
    {
       messagePanel.GetComponent<Button>().onClick.AddListener(()=> TapAnywhere());
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        ActivateMessagePanel(true);
    }

    public void TapAnywhere()
    {
        messageText.text = "NULL!";
        ActivateMessagePanel(false);
    }

    private void ActivateMessagePanel(bool active)
    {
        messagePanel.SetActive(active);
    }
}
