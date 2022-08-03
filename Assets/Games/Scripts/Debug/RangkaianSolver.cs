using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class RangkaianSolver : MonoBehaviour
{
    [Button]
    public void SolveRangkaian(string rangkaianID)
    {
        ServiceLocator.Resolve<PenugasanSystem>().GetRangkaian().GetActiveQuest().Find(x => x.questData.questID.Equals(rangkaianID)).questClear = true;
    }

    [Button]
    public void UnsolveRangkaian(string rangkaianID)
    {
        ServiceLocator.Resolve<PenugasanSystem>().GetRangkaian().GetActiveQuest().Find(x => x.questData.questID.Equals(rangkaianID)).questClear = false;
    }
}
