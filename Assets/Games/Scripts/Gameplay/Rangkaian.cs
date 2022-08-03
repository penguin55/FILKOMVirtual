using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Rangkaian
{
    [SerializeField, ReadOnly] private RangkaianData rangkaianData;
    [SerializeField, ReadOnly] protected List<Quest> quests = new List<Quest>();

    public string Title { get { return rangkaianData.rangkaianName; } }
    public string Description { get { return rangkaianData.rangkaianDescription; } }

    public Rangkaian(RangkaianData rangkaianData)
    {
        if (rangkaianData)
        {
            this.rangkaianData = rangkaianData;

            foreach (QuestData questData in this.rangkaianData.penugasan)
            {
                quests.Add(new Quest(questData));
            }
        }
    }

    public List<Quest> GetActiveQuest()
    {
        List<Quest> activeQuests = quests.FindAll(x => CheckActiveQuest(x));
        return activeQuests;
    }

    public bool CheckActiveQuest(Quest quest)
    {
        if (quest.questData.requirementQuest == null) return true;
        return QuestClearance(quest.questData.requirementQuest);
    }

    private bool QuestClearance(QuestData requirement)
    {
        Quest fetchedQuest = quests.FirstOrDefault(x => x.questData.questID.Equals(requirement.questID));

        if (fetchedQuest == null || !fetchedQuest.questClear) return false;

        return true;
    }

    public string GetCurrentRangkaian()
    {
        return rangkaianData.rangkaianID;
    }

    public bool TryGetQuest(string questID, out Quest fetchedQuest)
    {
        if (quests.Exists(x => x.questData.questID.Equals(questID)))
        {
            fetchedQuest = quests.Find(x => x.questData.questID.Equals(questID));
            return true;
        }

        UnityEngine.Debug.Log("Quest : " + questID + " didn't exist on Firebase");

        fetchedQuest = null;
        return false;
    }
}

[System.Serializable]
public class Quest
{
    [ReadOnly] public QuestData questData;
    [ReadOnly] public bool questClear;
    [ReadOnly] public System.DateTime clearedTime;

    public Quest(QuestData questData)
    {
        this.questData = questData;
        clearedTime = System.DateTime.Parse("9-17-1998 12:45");
        questClear = false;
    }
}
