using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseSetup : MonoBehaviour
{
    private static FirebaseDatabase fetchedDatabase;
    public static FirebaseDatabase Database
    {
        get
        {
            if (fetchedDatabase == null) fetchedDatabase = FirebaseDatabase.DefaultInstance;
            return fetchedDatabase;
        }
    }
}
