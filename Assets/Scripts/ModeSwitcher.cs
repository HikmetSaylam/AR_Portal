using System;
using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ModeSwitcher : MonoSingleton<ModeSwitcher>
{
    [SerializeField] private ARTrackedImageManager _arTrackedImageManager;
    [SerializeField] private Camera portalCamera, skanCamera;
    [SerializeField] private Button skipSkanModeButton;
    [SerializeField] private Image skipAnimationImage;
    [SerializeField] private GameObject portalCanvas;
    [SerializeField] private float skipAnimationSpeed;

    private void Start()
    {
        portalCamera.enabled = false;
        skanCamera.enabled = true;
        portalCanvas.SetActive(false);
        skipSkanModeButton.onClick.AddListener(SkipToSkan);
    }

    private void SkipToPortal()
    {
        SkipToOtherMode(SkipToPortalMode);
    }

    private void SkipToSkan()
    {
        SkipToOtherMode(SkipToSkanMode);
    }

    private void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        SkipToPortal();
    }

    private void SkipToOtherMode(Action otherModeAction)
    {
        StartCoroutine(otherModeAction == SkipToPortalMode
            ? PlaySkipAnimation(0, 1, SkipToPortalMode)
            : PlaySkipAnimation(1, 0, SkipToSkanMode));
    }
    
    private void SkipToPortalMode()
    {
        _arTrackedImageManager.enabled = false;
        portalCamera.enabled = true;
        skanCamera.enabled = false;
        portalCanvas.SetActive(true);
    }
    
    private void SkipToSkanMode()
    {
        GameManager.Instance.ResetGame();
        portalCamera.enabled = false;
        skanCamera.enabled = true;
        portalCanvas.SetActive(false);
        _arTrackedImageManager.enabled = true;
    }

    private IEnumerator PlaySkipAnimation(float current, float target,Action modeFunction)
    {
        skipAnimationImage.gameObject.SetActive(true);
        float lerp = 0;
        while (lerp<1)
        {
            skipAnimationImage.fillAmount = Mathf.Lerp(current, target, lerp += skipAnimationSpeed * Time.deltaTime);
            yield return null;
        }
        modeFunction.Invoke();
        skipAnimationImage.gameObject.SetActive(false);
    }
    
}
