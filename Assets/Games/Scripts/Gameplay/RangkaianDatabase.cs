using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangkaianDatabase : MonoBehaviour
{
    [SerializeField] private List<RangkaianData> rangkaianDataList = new List<RangkaianData>();

    public RangkaianData GetRangkaian(string rangkaianID)
    {
        if (rangkaianDataList.Exists(x => x.rangkaianID.Equals(rangkaianID)))
        {
            return rangkaianDataList.Find(x => x.rangkaianID.Equals(rangkaianID));
        }

        return null;
    }
}
