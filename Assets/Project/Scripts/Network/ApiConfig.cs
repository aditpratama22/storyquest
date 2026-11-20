using UnityEngine;

[CreateAssetMenu(fileName = "ApiConfig", menuName = "StoryQuest/API Config")]
public class ApiConfig : ScriptableObject
{
    [Header("Server")]
    public string baseUrl = "https://jsonplaceholder.typicode.com";

    [Header("Endpoint")]
    public string storiesPath = "/posts";
    public string submitScorePath = "/posts";

    [Header("Perilaku")]
    [Tooltip("Batas waktu dalam detik. 0 berarti tanpa batas — jangan dipakai.")]
    public int timeoutSeconds = 10;

    public string StoriesUrl => baseUrl.TrimEnd('/') + storiesPath;
    public string SubmitScoreUrl => baseUrl.TrimEnd('/') + submitScorePath;
}