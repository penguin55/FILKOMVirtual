using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Create Data/Rangkaian Data", fileName = "New Rangkaian Data")]
public class RangkaianData : ScriptableObject 
{
    public string rangkaianID;
    public string rangkaianName;
    [TextArea(10, 20)] public string rangkaianDescription;

    public List<QuestData> penugasan = new List<QuestData>();
}
