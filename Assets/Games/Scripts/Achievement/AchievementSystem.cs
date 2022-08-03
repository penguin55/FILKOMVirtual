using Sirenix.OdinInspector;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using TomGustin.Interface;
using UnityEngine;
using static AccountSaveData;

public class AchievementSystem : BaseManager, IObserver, ISystemLoad
{
    [SerializeField] private AchievementLibrary achievementLibrary;
    [SerializeField] private List<PointofInterest> pointOfInterests;

    [SerializeField, ReadOnly] protected List<Achievement> achievements = new List<Achievement>();

    private UIAchievementPopUp aui;

    private void Initialization()
    {
        aui = ServiceLocator.Resolve<UIAchievementPopUp>();
        InitPOI();
        InitAchievements();
        SyncAchievementToFirebase();

        isInitialized = true;
    }

    private void InitAchievements()
    {
        foreach (AchievementData achievementData in achievementLibrary.GetAchievements())
        {
            achievements.Add(new Achievement(achievementData));
        }
    }

    private void InitPOI()
    {
        foreach (PointofInterest poi in pointOfInterests)
        {
            poi.Subscribe(this);
        }
    }

    private void DeInitPOI()
    {
        foreach (PointofInterest poi in pointOfInterests)
        {
            poi.Unsubscribe(this);
        }
    }

    public List< Achievement> GetAchievements()
    {
        return achievements;
    }

    private void LocalSaveAchievement(Achievement fetchedAchievement)
    {
        fetchedAchievement.isAchieve = true;
        fetchedAchievement.clearedTime = System.DateTime.Now;
    }

    private void OnlineSaveAchievement(string achievementID)
    {
        AccountSaveData activeAccount = AccountManager.Instance.GetActiveAccount();
        if (activeAccount.saved_data.list_achievement.Exists((x) => x.id.Equals(achievementID)))
        {
            print($"Achievement System : {achievementID} is already exist!");
            return;
        }

        activeAccount.saved_data.list_achievement.Add(new AccountSaveData.GenericSaveData(achievementID, System.DateTime.Now.ToString()));
        AccountManager.SyncData();
    }

    private bool TryGetAchievement(string achievementID, out Achievement achievement)
    {
        if (achievements.Exists(x => x.achievementData.achievementID.Equals(achievementID)))
        {
            achievement = achievements.Find(x => x.achievementData.achievementID.Equals(achievementID));
            return true;
        }

        UnityEngine.Debug.Log("Achievement : " + achievementID + " didn't exist on Firebase");

        achievement = null;
        return false;
    }

    private void SyncAchievementToFirebase()
    {
        SavedData savedData = AccountManager.Instance.GetSavedData();
        if (savedData != null)
        {
            foreach (GenericSaveData data in savedData.list_achievement)
            {
                if (TryGetAchievement(data.id, out Achievement fetchedAchievement)) fetchedAchievement.isAchieve = true;
            }
        }
    }

    void IObserver.OnNotify<T>(T param)
    {
        
    }

    void IObserver.OnNotify<T>(string sender, T param)
    {
        print($"Observer Pattern : {sender} was Send Notification! with id {param as string}");

        string[] values = (param as string).Split(':');
        if (values.Length < 2) return;
        if (!InSameRangkaian(values[0])) return;

        string achievementID = values[1];

        if (TryGetAchievement(achievementID, out Achievement achievement))
        {
            if (!achievement.isAchieve)
            {
                LocalSaveAchievement(achievement);
                OnlineSaveAchievement(achievementID);
                print($"<color='green'>Achievement System : {achievementID} is saved!</color>");
                aui.QueuePopUpAchievement(achievement.achievementData.achievementName);
            }
        } else
        {
            print($"<color='red'>Achievement System : {achievementID} is not valid ID!</color>");
        }
    }

    bool ISystemLoad.IsInitialized()
    {
        return isInitialized;
    }

    void ISystemLoad.Initialized()
    {
        Initialization();
    }

    private bool InSameRangkaian(string rangkaian_info)
    {
        string rangkaian = string.Empty;
        switch (rangkaian_info)
        {
            case "rangkaian1":
                rangkaian = "rangkaian_1";
                break;
            case "rangkaian2":
                rangkaian = "rangkaian_2";
                break;
            case "rangkaian3":
                rangkaian = "rangkaian_3";
                break;
            case "general":
                return true;
        }

        return rangkaian.Equals(RangkaianManager.ActiveRangkaian);
    }

    [System.Serializable]
    public class Achievement
    {
        public AchievementData achievementData;
        public System.DateTime clearedTime;
        public bool isAchieve;

        public Achievement(AchievementData achievementData)
        {
            this.achievementData = achievementData;
            clearedTime = System.DateTime.Parse("9-17-1998 12:45");
            isAchieve = false;
        }
    }
}
