using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class TGAuth
{ 
    private static string user_key = "ADMIN";

    public static bool IsOnline { get; set; }

    public static async Task Connect(UnityAction onConnect, UnityAction onFailed = null)
    {
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                onFailed?.Invoke();
            }
            else
            {
                IsOnline = true;
                onConnect?.Invoke();
            }
        });
    }
    
    public static async Task<int> Login(string nim, string pass)
    {
        user_key = nim;
        var test = LoadAccountData();
        AccountSaveData account = await LoadAccountData();
        int code_success = -1;

        if (account != null)
        {
            if (account.user_code.Equals(pass))
            {
                AccountManager.Instance.SetActiveAccount(account);
                code_success = 200;
            }
            else
            {
                code_success = 101;
            }
        }
        else
        {
            code_success = 404;
        }

        return code_success;
    }

    private static async Task<AccountSaveData> LoadAccountData()
    {
        if (string.IsNullOrEmpty(user_key)) user_key = "NULL_VALUE";
        var reference = FirebaseSetup.Database.GetReference("account");

        var dataSnapshot = await reference.GetValueAsync();
        if (!dataSnapshot.Exists) return null;

        var accountSnapshot = dataSnapshot.Child(user_key);
        return JsonUtility.FromJson<AccountSaveData>(accountSnapshot.GetRawJsonValue());
    }

    public static async Task<string> GetRangkaianAsync()
    {
        FirebaseApp.Create();
        FirebaseSetup.Database.GetReference("rangkaian_active").KeepSynced(true);
        var reference = FirebaseSetup.Database.GetReference("rangkaian_active");
        var dataSnapshot = await reference.GetValueAsync();

        if (!dataSnapshot.Exists) return string.Empty;

        return (string)dataSnapshot.Value;
    }
}
