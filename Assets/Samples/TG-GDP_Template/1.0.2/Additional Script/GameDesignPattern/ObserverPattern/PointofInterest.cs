using UnityEngine;

namespace TomGustin.GameDesignPattern
{
    public class PointofInterest : Subject
    {
        [SerializeField] private string poiName;

        public override void OnNotify<T>(string sender, T param)
        {
            foreach (IObserver observer in observers) observer.OnNotify(sender, param);
        }

        public void OnNotifyMessage(string message)
        {
            foreach (IObserver observer in observers) observer.OnNotify(poiName, message);
        }

#if UNITY_EDITOR 
        private void OnValidate()
        {
            gameObject.name = $"POI-{poiName.Replace(' ', '_')}";
        }
#endif
    }
}