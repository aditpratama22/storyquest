using UnityEngine;
using UnityEngine.UI;

public class SimpleCameraAR : MonoBehaviour
{
    [Header("UI Kamera")]
    [SerializeField] private RawImage cameraDisplay;
    [SerializeField] private int pointsReward = 25;

    private WebCamTexture _camTexture;
    private bool _isCompleted;

    private void Start()
    {
        StartCamera();
    }

    private void StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.LogWarning("[SimpleCameraAR] Tidak ada kamera yang terdeteksi.");
            return;
        }

        // Pilih kamera belakang jika ada (untuk Android)
        string selectedCam = devices[0].name;
        foreach (var device in devices)
        {
            if (!device.isFrontFacing)
            {
                selectedCam = device.name;
                break;
            }
        }

        _camTexture = new WebCamTexture(selectedCam, 1280, 720, 30);
        if (cameraDisplay != null)
        {
            cameraDisplay.texture = _camTexture;
        }

        _camTexture.Play();
        Debug.Log($"[SimpleCameraAR] Kamera aktif: {selectedCam}");
    }

    private void Update()
    {
        // Menyesuaikan orientasi kamera di HP Android agar tidak miring
        if (_camTexture != null && _camTexture.isPlaying && cameraDisplay != null)
        {
            int orient = -_camTexture.videoRotationAngle;
            cameraDisplay.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
    }

    /// <summary>Dipanggil saat tombol simulasi deteksi marker ditekan.</summary>
    public void TriggerObjectiveComplete()
    {
        if (_isCompleted) return;
        _isCompleted = true;

        StopCamera();

        if (GameManager.Instance != null && GameManager.Instance.CurrentSession != null)
        {
            GameManager.Instance.RecordObjective(pointsReward);
            Debug.Log($"[SimpleCameraAR] Marker terdeteksi! +{pointsReward} poin.");
        }

        NavigationManager.Instance.Push(SceneNames.RESULT);
    }

    public void StopCamera()
    {
        if (_camTexture != null && _camTexture.isPlaying)
        {
            _camTexture.Stop();
            Debug.Log("[SimpleCameraAR] Kamera dimatikan dengan bersih.");
        }
    }

    private void OnDisable() => StopCamera();
    private void OnDestroy() => StopCamera();
}