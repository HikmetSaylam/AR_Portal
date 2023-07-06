using UnityEngine;

namespace Assets.Scripts
{
    public class PortalController : MonoSingleton<PortalController>
    {
        [SerializeField] private Transform device;
        private int _entryCounter;

        public void  ResetEntryCounter()
        {
            _entryCounter = 0;
        }

        private void OnTriggerEnter(Component other)
        {
            if (!other.transform.Equals(device)) return;
            var fullRender = ++_entryCounter % 2 != 0;
            GameManager.Instance.SetMaterial(fullRender);
        }
    }
}
    
