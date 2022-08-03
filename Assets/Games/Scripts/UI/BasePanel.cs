using UnityEngine;
using UnityEngine.Events;

public class BasePanel : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnOpenPanel;
    [SerializeField] protected UnityEvent OnClosePanel;
    [SerializeField] protected GameObject panel;
    protected bool active;

    public virtual void ActivatePanel(bool active)
    {
        this.active = active;
        panel.SetActive(this.active);

        if (this.active) OnOpenPanel?.Invoke();
        else OnClosePanel?.Invoke();
    }

    protected virtual void Init()
    {
        
    }

    protected virtual void DestroyInit()
    {

    }
}
