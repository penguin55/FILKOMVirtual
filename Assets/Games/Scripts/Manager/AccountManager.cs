using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class AccountManager : Singleton<AccountManager>
{
    [SerializeField, ReadOnly] private AccountSaveData activeAccount;

    private void Awake()
    {
        OnInitialize();
    }

    public void SetActiveAccount(AccountSaveData activeAccount)
    {
        if (activeAccount != null)
        {
            this.activeAccount = activeAccount;
            this.activeAccount.UpdateLoginTime(DateTime.Now);
            SyncData();
        }
    }

    public AccountSaveData GetActiveAccount()
    {
        if (IsAccountNotLoggedIn())
        {
            print("Not login");
            AccountSaveData guestAccount = new AccountSaveData();
            guestAccount.user_name = "Guest Player";
            guestAccount.user_id = "000000000000000";
            guestAccount.user_code = "ADMIN";

            return guestAccount;
        }
        else return activeAccount;
    }

    public AccountSaveData.SavedData GetSavedData()
    {
        if (IsAccountNotLoggedIn())
        {
            return null;
        }
        else return activeAccount.saved_data;
    }

    public void Logout()
    {
        activeAccount = null;
    }

    public static async void SyncData()
    {
        if (!Instance || Instance.IsAccountNotLoggedIn()) return;
        var reference = FirebaseSetup.Database.GetReference("account");

        string data = JsonUtility.ToJson(Instance.activeAccount.saved_data);

        await reference.Child(Instance.activeAccount.user_id).Child("saved_data").SetRawJsonValueAsync(data);
    }

    public bool IsAccountNotLoggedIn()
    {
        return activeAccount == null || string.IsNullOrEmpty(activeAccount.user_id);
    }
}
