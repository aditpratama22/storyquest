using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARBridge : MonoBehaviour
{
    [Header("Izin")]
    [SerializeField] private PermissionHelper permissionHelper;
    [SerializeField] private GameObject permissionDeniedPanel;

    private bool _isOpening;

    public void OpenAR(string storyId)
    {
        if (_isOpening)
        {
            Debug.LogWarning("[ARBridge] Permintaan AR sedang diproses, diabaikan.");
            return;
        }

        if (permissionHelper == null)
        {
            Debug.LogError("[ARBridge] PermissionHelper belum diisi di Inspector!");
            return;
        }

        _isOpening = true;

        permissionHelper.RequestCamera(granted =>
        {
            _isOpening = false;

            if (!granted)
            {
                Debug.LogWarning("[ARBridge] Izin kamera ditolak.");
                if (permissionDeniedPanel != null)
                    permissionDeniedPanel.SetActive(true);
                return;
            }

            Debug.Log("Membuka AR untuk cerita: " + storyId);
            NavigationManager.Instance.Push(SceneNames.AR_STAGE);
        });
    }

    // private void Start()
    // {
    //     if (SceneManager.GetActiveScene().name == SceneNames.AR_STAGE)
    //     {
    //         StartCoroutine(SimulasiARSelesai());
    //     }
    // }

    private IEnumerator SimulasiARSelesai()
    {
        Debug.Log("[MockAR] Sesi dimulai, memindai marker...");
        
        yield return new WaitForSeconds(3f);

        if (GameManager.Instance != null && GameManager.Instance.CurrentSession != null)
        {
            GameManager.Instance.RecordObjective(25);
            Debug.Log("[ARBridge] Objektif selesai, +25 poin.");
        }

        NavigationManager.Instance.Push(SceneNames.RESULT); 
    }
}