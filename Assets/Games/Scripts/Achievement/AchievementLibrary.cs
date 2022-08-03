using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Create Data/Achievement Library Data", fileName = "Achievement Library Data")]
public class AchievementLibrary : ScriptableObject
{
    [SerializeField] private string libraryName;
    [SerializeField] private List<AchievementData> achievements = new List<AchievementData>();

    public List<AchievementData> GetAchievements()
    {
        return achievements;
    }
}
