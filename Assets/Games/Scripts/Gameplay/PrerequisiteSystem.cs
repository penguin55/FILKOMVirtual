using System.Collections;
using System.Collections.Generic;
using TomGustin.Interface;
using UnityEngine;
using UnityEngine.Events;

public class PrerequisiteSystem : BaseManager, ISystemLoad
{
    [SerializeField] private NavigationDynamic navDynamic;
    [SerializeField] private List<PrerequisiteData> prerequisiteRangkaian;
    [SerializeField] private List<PrerequisiteData> prerequisitePenugasan;

    void ISystemLoad.Initialized()
    {
        CheckRangkaian();
        CheckPenugasan();
        isInitialized = true;
    }

    bool ISystemLoad.IsInitialized()
    {
        return isInitialized;
    }

    private void CheckRangkaian()
    {
        string rangkaian_id = RangkaianManager.ActiveRangkaian;

        PrerequisiteData fetchedEvent = prerequisiteRangkaian.Find(x => x.id.Equals(rangkaian_id));
        if (fetchedEvent != null)
        {
            fetchedEvent.events?.Invoke();
            if (fetchedEvent.reBakeNavMesh)
            {
                navDynamic.Bake();
            }
        }
    }

    private void CheckPenugasan()
    {
        AccountSaveData.SavedData savedData = AccountManager.Instance.GetSavedData();
        if (savedData != null)
        {
            foreach (AccountSaveData.GenericSaveData data in savedData.list_penugasan)
            {
                PrerequisiteData fetchedEvent = prerequisitePenugasan.Find(x => x.id.Equals(data.id));
                if (fetchedEvent != null) fetchedEvent.events?.Invoke(); 
            }
        }

        print("Init Prerequisite");
    }

    [System.Serializable]
    public class PrerequisiteData
    {
        public string id;
        public UnityEvent events;
        public bool reBakeNavMesh;
    }
}
