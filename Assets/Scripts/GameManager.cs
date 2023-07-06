using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private Material[] materials;
        [SerializeField] private Button resetButton;


        private void Start()
        {
            resetButton.onClick.AddListener(ResetGame);
            SetMaterial(false);
        }
        public void SetMaterial(bool fullRender)
        {
            StartCoroutine(setMaterialEnumerator(fullRender));
        }

        public void ResetGame()
        {
            SetMaterial(false);
            DeviceController.Instance.ResetDevice();
            PortalController.Instance.ResetEntryCounter();
        }

        private IEnumerator setMaterialEnumerator(bool fullRender)
        {
            var stencilFilter = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

            foreach (var mat in materials)
            {
                mat.SetInt("_StencilFilter", (int)stencilFilter);
            }
            yield return null;
        }
        
    }
}
