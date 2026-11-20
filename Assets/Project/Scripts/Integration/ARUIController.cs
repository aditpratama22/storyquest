using UnityEngine;
using TMPro;

public class ARUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text txtInfo;

    private void Start()
    {
        // Ambil data sesi yang sedang berjalan dari GameManager
        SessionData session = GameManager.Instance.CurrentSession;
        
        if (session != null)
        {
            // Tampilkan ID dan Judul ke layar
            txtInfo.text = $"AR Memindai...\nID: {session.storyId}\nJudul: {session.storyTitle}";
        }
        else
        {
            txtInfo.text = "Sesi AR tidak ditemukan.";
        }
    }
}