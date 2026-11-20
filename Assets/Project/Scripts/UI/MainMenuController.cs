using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour, IBackHandler
{
    [Header("Tombol")]
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnAbout;
    [SerializeField] private Button btnCloseAbout;

    [Header("Teks")]
    [SerializeField] private TMP_Text txtPoints;

    [Header("Panel")]
    [SerializeField] private GameObject aboutPanel;

    private void OnEnable()
    {
        btnStart.onClick.AddListener(HandleStart);
        btnContinue.onClick.AddListener(HandleContinue);
        btnAbout.onClick.AddListener(HandleAbout);
        btnCloseAbout.onClick.AddListener(CloseAbout);

        if (GameManager.Instance != null)
        {
            RefreshPoints();
        }
    }

    private void OnDisable()
    {
        btnStart.onClick.RemoveListener(HandleStart);
        btnContinue.onClick.RemoveListener(HandleContinue);
        btnAbout.onClick.RemoveListener(HandleAbout);
        btnCloseAbout.onClick.RemoveListener(CloseAbout);
    }

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("[Menu] GameManager tidak ditemukan. Jalankan aplikasi dari scene Splash.");
            txtPoints.text = "Poin: -";
            btnContinue.interactable = false;
            aboutPanel.SetActive(false);
            return;
        }

        RefreshPoints();
        bool hasSession = !string.IsNullOrEmpty(GameManager.Instance.SelectedStoryId);
        btnContinue.interactable = hasSession;
        aboutPanel.SetActive(false);
    }

    private void RefreshPoints()
    {
        if (txtPoints != null)
        {
            txtPoints.text = $"Poin: {GameManager.Instance.TotalPoints}";
        }
    }

    private void HandleStart()
    {
        Debug.Log("[Menu] Tombol Mulai ditekan.");
        NavigationManager.Instance.Push(SceneNames.STORY_LIST);
    }

    private void HandleContinue()
    {
        Debug.Log($"[Menu] Melanjutkan cerita {GameManager.Instance.SelectedStoryId}.");
        NavigationManager.Instance.Push(SceneNames.STORY_LIST);
    }

    private void HandleAbout()
    {
        aboutPanel.SetActive(true);
        Debug.Log("[Menu] Panel Tentang dibuka.");
    }

    private void CloseAbout()
    {
        aboutPanel.SetActive(false);
        Debug.Log("[Menu] Panel Tentang ditutup.");
    }

    public bool OnBackPressed()
    {
        if (aboutPanel != null && aboutPanel.activeSelf)
        {
            aboutPanel.SetActive(false);
            Debug.Log("[Menu] Tombol kembali menutup panel Tentang.");
            return true;
        }
        return false;
    }
}