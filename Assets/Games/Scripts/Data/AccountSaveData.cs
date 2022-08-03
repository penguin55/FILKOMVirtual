using System;
using System.Collections.Generic;

[System.Serializable]
public class AccountSaveData
{
    public string user_id;
    public string user_name;
    public string user_code;
    public SavedData saved_data;
    
    public AccountSaveData()
    {
        saved_data = new SavedData();
    }

    public void UpdateLoginTime(DateTime date)
    {
        saved_data.time.day = date.Day;
        saved_data.time.month = date.Month;
        saved_data.time.year = date.Year;
        saved_data.time.hour = date.Hour;
        saved_data.time.minute = date.Minute;
    }

    [System.Serializable]
    public class SavedData
    {
        public SavedTime time;
        public List<GenericSaveData> list_penugasan;
        public List<GenericSaveData> list_achievement;

        public SavedData()
        {
            time = new SavedTime(DateTime.Now);
            list_penugasan = new List<GenericSaveData>();
            list_achievement = new List<GenericSaveData>();
        }

        [System.Serializable]
        public class SavedTime
        {
            public int day;
            public int month;
            public int year;
            public int hour;
            public int minute;

            public SavedTime(DateTime date)
            {
                day = date.Day;
                month = date.Month;
                year = date.Year;
                hour = date.Hour;
                minute = date.Minute;
            }
        }
    }

    [System.Serializable]
    public class GenericSaveData
    {
        public string id;
        public string dateCleared;

        public GenericSaveData(string id, string dateCleared)
        {
            this.id = id;
            this.dateCleared = dateCleared;
        }
    }
}