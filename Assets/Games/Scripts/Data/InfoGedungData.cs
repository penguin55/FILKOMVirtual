using UnityEngine;

[CreateAssetMenu(menuName = "Create Data/Info Gedung Data", fileName = "Info Gedung Data")]
public class InfoGedungData : ScriptableObject
{
    public string idGedung;
    public string namaGedung;
    public Sprite gambarGedung;
    [TextArea(5, 10)] public string urlVR;
    [TextArea(10, 20)] public string deskripsiGedung;
}
