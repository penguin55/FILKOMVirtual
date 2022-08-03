using Firebase;
using Firebase.Extensions;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomGustin.GameDesignPattern;
using TomGustin.Interface;
using UnityEngine;
using UnityEngine.Events;
using static AccountSaveData;

public class PenugasanSystem : BaseManager, ISystemLoad, IObserver
{
    [SerializeField] private List<PointofInterest> pointOfInterests;
    [SerializeField] private List<QuestCompleteEvent> onCompletePenugasan;

    [SerializeField, ReadOnly] private Rangkaian activeRangkaian;
    private RangkaianDatabase rangkaianDatabase;
    private UIRangkaian ui;

    private const string REFERENCE_KEY = "rangkaian_info";

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        rangkaianDatabase = ServiceLocator.Resolve<RangkaianDatabase>();
        ui = ServiceLocator.Resolve<UIRangkaian>();
        InitPOI();
        OnInitialize();

        isInitialized = true;
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

    private void LocalSavePenugasan(Quest penugasan)
    {
        penugasan.questClear = true;
        penugasan.clearedTime = System.DateTime.Now;
    }

    private void OnlineSavePenugasan(string penugasanID)
    {
        AccountSaveData activeAccount = AccountManager.Instance.GetActiveAccount();
        if (activeAccount.saved_data.list_penugasan.Exists((x) => x.id.Equals(penugasanID)))
        {
            print($"Penugasan System : {penugasanID} is already exist!");
            return;
        }

        activeAccount.saved_data.list_penugasan.Add(new AccountSaveData.GenericSaveData(penugasanID, System.DateTime.Now.ToString()));
        AccountManager.SyncData();
    }

    protected async override void OnInitialize()
    {
        string result = await TGAuth.GetRangkaianAsync();
        print(result);
        RangkaianData rangkaianFetched = rangkaianDatabase.GetRangkaian(result);

        activeRangkaian = new Rangkaian(rangkaianFetched);
        SyncPenugasan();
        ui.ChangeActiveRangkaian(activeRangkaian.Title);
        ui.ShowDevMessage(activeRangkaian.Title, activeRangkaian.Description);
    }

    public Rangkaian GetRangkaian()
    {
        return activeRangkaian;
    }

    private void SyncPenugasan()
    {
        SavedData savedData = AccountManager.Instance.GetSavedData();
        if (savedData != null && activeRangkaian != null)
        {
            foreach (GenericSaveData data in savedData.list_penugasan)
            {
                if (activeRangkaian.TryGetQuest(data.id, out Quest fetchedQuest))
                {
                    fetchedQuest.questClear = true;

                    QuestCompleteEvent fetchedEvent = onCompletePenugasan.Find(x => x.penugasanID.Equals(data.id));
                    if (fetchedEvent != null) fetchedEvent.events?.Invoke();
                }
            }
        }
    }

    void ISystemLoad.Initialized()
    {
        Initialization();
    }

    bool ISystemLoad.IsInitialized()
    {
        return isInitialized;
    }

    void IObserver.OnNotify<T>(T param)
    {
        
    }

    void IObserver.OnNotify<T>(string sender, T param)
    {
        print($"Observer Pattern : {sender} was send Notification!");

        string[] values = (param as string).Split(':');
        if (values.Length < 2) return;
        if (!InSameRangkaian(values[0])) return;

        string penugasanID = values[1];

        if (activeRangkaian.TryGetQuest(penugasanID, out Quest penugasan))
        {
            if (penugasan.questClear) return;

            if (activeRangkaian.CheckActiveQuest(penugasan))
            {
                LocalSavePenugasan(penugasan);
                OnlineSavePenugasan(penugasanID);
                print($"<color='green'>Penugasan System : {penugasanID} is saved!</color>");

                QuestCompleteEvent fetchedEvent = onCompletePenugasan.Find(x => x.penugasanID.Equals(penugasanID));

                if (fetchedEvent != null) fetchedEvent.events?.Invoke();
            }
        }
        else
        {
            print($"<color='red'>Penugasan System : {penugasanID} is not valid ID!</color>");
        }
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
        }

        return rangkaian.Equals(RangkaianManager.ActiveRangkaian);
    }

    [System.Serializable]
    public class QuestCompleteEvent
    {
        public string penugasanID;
        public UnityEvent events;
    }
}
