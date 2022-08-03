using Sirenix.OdinInspector;
using System.Threading.Tasks;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class RangkaianManager : Singleton<RangkaianManager>
{
    public static string ActiveRangkaian { get; set; }

    [SerializeField, ReadOnly] private string activeRangkaian;

    private void Awake()
    {
        OnInitialize();
    }

    public static async Task<bool> Sync()
    {
        ActiveRangkaian = await TGAuth.GetRangkaianAsync();
        Instance.activeRangkaian = ActiveRangkaian;
        return true;
    }
}
