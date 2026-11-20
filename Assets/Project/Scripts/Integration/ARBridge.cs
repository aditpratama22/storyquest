using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARBridge : MonoBehaviour
{
    public void OpenAR(string storyId)
    {
        Debug.Log("Membuka AR untuk cerita: " + storyId);
        NavigationManager.Instance.Push(SceneNames.AR_STAGE);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneNames.AR_STAGE)
        {
            StartCoroutine(SimulasiARSelesai());
        }
    }

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