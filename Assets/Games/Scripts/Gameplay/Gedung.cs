using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class Gedung : MonoBehaviour
{
    [SerializeField] private InfoGedungData infoGedung;

    private UIInfoGedung uiInfoGedung;

    private void Awake()
    {
        uiInfoGedung = ServiceLocator.Resolve<UIInfoGedung>();
    }

    public void ShowInfoGedung()
    {
        uiInfoGedung.ShowInfoGedung(infoGedung);
    }
}
