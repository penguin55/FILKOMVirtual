using UnityEngine;

namespace TomGustin.Interface
{
    public interface ITriggerCollider
    {
        void OnEnter(GameObject obj);
        void OnStay(GameObject obj);
        void OnExit(GameObject obj);
    }
}