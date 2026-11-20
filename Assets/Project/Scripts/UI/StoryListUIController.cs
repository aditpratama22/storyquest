using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryListUIController : MonoBehaviour, IBackHandler
{
    [System.Serializable]
    public class StoryEntry
    {
        public string id;
        public string title;
        [TextArea] public string synopsis;
    }

    [Header("Data sementara")]
    [SerializeField] private StoryEntry[] stories;

    [Header("Referensi UI")]
    [SerializeField] private Transform listContainer;
    [SerializeField] private GameObject itemPrefab;

    [Header("Integrasi")]
    [SerializeField] private ARBridge arBridge;

    private void Start()
    {
        BuildList();
    }

    private void BuildList()
    {
        // Bersihkan isi lama sebelum membangun ulang.
        foreach (Transform child in listContainer)
            Destroy(child.gameObject);

        foreach (StoryEntry story in stories)
        {
            GameObject item = Instantiate(itemPrefab, listContainer);
            item.transform.Find("Txt_Title").GetComponent<TMP_Text>().text = story.title;
            item.transform.Find("Txt_Synopsis").GetComponent<TMP_Text>().text = story.synopsis;

            // Variabel lokal WAJIB untuk menghindari jebakan closure.
            StoryEntry captured = story;
            item.GetComponent<Button>().onClick.AddListener(() => SelectStory(captured));
        }
        Debug.Log($"[StoryList] {stories.Length} cerita ditampilkan.");
    }

    private void SelectStory(StoryEntry story)
    {
        Debug.Log($"[StoryList] Cerita dipilih: {story.title}");
        GameManager.Instance.BeginSession(story.id, story.title);
        arBridge.OpenAR(story.id);
    }

    public bool OnBackPressed() => false; // Biarkan navigasi yang menangani.
}