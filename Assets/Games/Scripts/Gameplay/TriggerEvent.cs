using System.Collections;
using System.Collections.Generic;
using TomGustin.Interface;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private PenugasanDataValidator dataValidator;
    [SerializeField] private UnityEvent onTrigger;
    [SerializeField] private bool destroyOnComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (!dataValidator.Active || dataValidator.Cleared) return;

        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            onTrigger?.Invoke();
            dataValidator.Clear();
            if (destroyOnComplete) gameObject.SetActive(false); //Destroy(gameObject);
        }
    }
}
