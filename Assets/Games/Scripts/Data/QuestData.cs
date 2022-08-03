using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Create Data/Quest Data", fileName = "New Quest Data")]
public class QuestData : ScriptableObject
{
    public string questID;
    public string questName;
    [TextArea(5,20)] public string questDescription;
    public QuestData requirementQuest;
}
