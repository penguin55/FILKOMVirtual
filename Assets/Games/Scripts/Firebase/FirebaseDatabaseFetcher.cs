using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseDatabaseFetcher : MonoBehaviour
{
    private const string REFERENCES_KEY = "version";
    private FirebaseDatabase database;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        database = FirebaseDatabase.DefaultInstance;
    }

    [ContextMenu("Test")]
    private void Test()
    {
        StartCoroutine(TestIE());
    }

    private IEnumerator TestIE()
    {
        var task = LoadData();
        yield return new WaitUntil(() => task.IsCompleted);

        print(task.Result);
    }

    private async Task<bool> SaveExists()
    {
        var dataSnapshot = await database.GetReference(REFERENCES_KEY).GetValueAsync();
        return dataSnapshot.Exists;
    }

    private async Task<string> LoadData()
    {
        var reference = database.GetReference(REFERENCES_KEY);
        var dataSnapshot = await reference.GetValueAsync();

        if (!dataSnapshot.Exists) return null;
        return dataSnapshot.GetRawJsonValue();
        /*var childSnapshot = await reference.Child("version").GetValueAsync();

        if (!childSnapshot.Exists) return null;*/

        //return JsonUtility.FromJson<string>(childSnapshot.GetRawJsonValue());
    }
}
