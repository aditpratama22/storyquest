using UnityEngine;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider loadingSlider;

    [Header("Settings")]
    [SerializeField] private float displayDuration = 2f;

    private float timer = 0f;
    private bool isFinished = false;

    private void Start()
    {
        if (loadingSlider != null)
        {
            // Paksa rentang slider dari 0 sampai 1
            loadingSlider.minValue = 0f;
            loadingSlider.maxValue = 1f;
            loadingSlider.value = 0f;
        }
        else
        {
            Debug.LogError("[SplashController] Objek 'LoadingSlider' belum ditarik ke Inspector!");
        }
    }

    private void Update()
    {
        if (isFinished) return;

        if (timer < displayDuration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / displayDuration);

            if (loadingSlider != null)
            {
                loadingSlider.value = progress;
            }
        }
        else
        {
            isFinished = true;
            if (NavigationManager.Instance != null)
            {
                NavigationManager.Instance.Replace(SceneNames.MAIN_MENU);
            }
        }
    }
}