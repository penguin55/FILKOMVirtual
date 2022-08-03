using UnityEngine;
using UnityEngine.Events;

public class PenugasanDataValidator : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] private bool cleared;

    [SerializeField] private UnityEvent onActive;
    [SerializeField] private UnityEvent onCleared;

    public bool Active { get { return active; } }
    public bool Cleared { get { return cleared; } }

    public void Activate()
    {
        active = true;
        if (!cleared) onActive?.Invoke();
    }

    public void Clear()
    {
        cleared = true;
        onCleared?.Invoke();
    }
}
