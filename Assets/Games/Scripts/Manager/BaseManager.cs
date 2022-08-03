using System.Threading.Tasks;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected bool isInitialized;
    public bool IsInitialized { get { return isInitialized; } }

    protected virtual void OnInitialize() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
}
