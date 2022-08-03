using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    private void Awake()
    {
        versionText.text = $"version {Application.version}";
    }
}
