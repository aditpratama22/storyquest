using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultController : MonoBehaviour, IBackHandler
{
    [SerializeField] private TMP_Text txtStoryTitle;
    [SerializeField] private TMP_Text txtObjectives;
    [SerializeField] private TMP_Text txtPoints;
    [SerializeField] private Button btnMenu;

    private void OnEnable() => btnMenu.onClick.AddListener(HandleBackToMenu);
    private void OnDisable() => btnMenu.onClick.RemoveListener(HandleBackToMenu);

    private void Start()
    {
        SessionData session = GameManager.Instance.CurrentSession;
        if (session == null)
        {
            Debug.LogWarning("[Result] Tidak ada sesi aktif.");
            NavigationManager.Instance.Replace(SceneNames.MAIN_MENU);
            return;
        }

        txtStoryTitle.text = session.storyTitle;
        txtObjectives.text = $"Objektif selesai: {session.objectivesCompleted}";
        txtPoints.text = $"+{session.pointsEarned} poin";
    }

    public void HandleBackToMenu()
    {
        GameManager.Instance.EndSession();
        // Replace: layar hasil tidak boleh dikunjungi ulang.
        NavigationManager.Instance.Replace(SceneNames.MAIN_MENU);
    }

    public bool OnBackPressed()
    {
        HandleBackToMenu();
        return true;
    }
}