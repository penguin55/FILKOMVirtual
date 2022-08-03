using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player Info References")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerNIM;

    [Header("Player Info Watermark")]
    [SerializeField] private TextMeshProUGUI playerNameInfo;
    [SerializeField] private TextMeshProUGUI playerNIMInfo;

    public void UpdatePlayerInfo(string name, string nim)
    {
        int lengthString = name.Length > 24 ? 24 : name.Length;
        string truncateName = name.Substring(0, lengthString);
        playerName.text = truncateName;
        playerNIM.text = nim;

        playerNameInfo.text = name;
        playerNIMInfo.text = nim;
    }
}
