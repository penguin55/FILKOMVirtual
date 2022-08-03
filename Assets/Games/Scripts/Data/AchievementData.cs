using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Create Data/Achievement Data", fileName = "Achievement Data")]
public class AchievementData : ScriptableObject
{
    public string achievementID;
    public string achievementName;
    [TextArea(5, 20)] public string achievementDescription;
}
