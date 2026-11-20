using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClient : MonoBehaviour
{
    public static ApiClient Instance { get; private set; }

    [SerializeField] private ApiConfig config;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (config == null)
            Debug.LogError("[API] ApiConfig belum diisi di Inspector.");
    }

    /// <summary>
    /// Mengambil daftar cerita. onSuccess menerima array cerita,
    /// onError menerima pesan yang layak ditampilkan ke pengguna.
    /// </summary>
    public void GetStories(Action<StoryDto[]> onSuccess, Action<string> onError)
    {
        StartCoroutine(GetStoriesRoutine(onSuccess, onError));
    }

    private IEnumerator GetStoriesRoutine(Action<StoryDto[]> onSuccess, Action<string> onError)
    {
        string url = config.StoriesUrl;
        Debug.Log($"[API] GET {url}");

        // Blok using memastikan resource dilepas walau terjadi exception.
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.timeout = config.timeoutSeconds;
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                string message = DescribeError(request);
                Debug.LogWarning($"[API] Gagal: {message} (kode {request.responseCode})");
                onError?.Invoke(message);
                yield break;
            }

            string json = request.downloadHandler.text;
            Debug.Log($"[API] Respons diterima, {json.Length} karakter.");

            StoryDto[] stories;
            try
            {
                stories = ParseStories(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"[API] Parsing gagal: {e.Message}");
                onError?.Invoke("Format data dari server tidak dikenali.");
                yield break;
            }

            if (stories == null || stories.Length == 0)
            {
                onError?.Invoke("Belum ada cerita yang tersedia.");
                yield break;
            }

            onSuccess?.Invoke(stories);
        }
    }

    /// <summary>
    /// Mengurai respons. Mendukung dua bentuk: berpembungkus
    /// {"status":..,"data":[..]} dan array murni [..].
    /// </summary>
    private StoryDto[] ParseStories(string json)
    {
        string trimmed = json.TrimStart();
        if (trimmed.StartsWith("["))
        {
            // Bentuk array murni perlu class wrapper.
            return ArrayWrapper<StoryDto>.Parse(json);
        }

        StoryListResponse parsed = JsonUtility.FromJson<StoryListResponse>(json);
        return parsed?.data;
    }

    /// <summary>
    /// Menerjemahkan kegagalan teknis menjadi pesan yang dapat dibaca pengguna.
    /// </summary>
    public static string DescribeError(UnityWebRequest request)
    {
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                return "Tidak dapat terhubung ke server. Periksa koneksi internet Anda.";
            case UnityWebRequest.Result.ProtocolError:
                return $"Server menolak permintaan (kode {request.responseCode}).";
            case UnityWebRequest.Result.DataProcessingError:
                return "Data dari server tidak dapat diproses.";
            default:
                return request.error ?? "Terjadi kesalahan yang tidak diketahui.";
        }
    }
}