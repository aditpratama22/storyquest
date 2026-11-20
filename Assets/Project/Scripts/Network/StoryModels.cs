using System;
using UnityEngine;

/// <summary>Satu entri cerita sebagaimana dikirim server.</summary>
[Serializable]
public class StoryDto
{
    public string id;
    public string title;
    public string synopsis;
    public string markerCode;
    public int maxPoints;
}

/// <summary>Pembungkus respons daftar cerita.</summary>
[Serializable]
public class StoryListResponse
{
    public string status;
    public StoryDto[] data;
    public string message; // hanya terisi saat status = error
}

/// <summary>
/// Pembungkus untuk JSON yang akarnya berupa array murni,
/// misalnya: [ {...}, {...} ]. JsonUtility tidak dapat
/// mengurainya secara langsung.
/// </summary>
[Serializable]
public class ArrayWrapper<T>
{
    public T[] items;

    public static T[] Parse(string rawJson)
    {
        string wrapped = "{\"items\":" + rawJson + "}";
        return JsonUtility.FromJson<ArrayWrapper<T>>(wrapped).items;
    }
}

/// <summary>Muatan yang dikirim ke server saat pemain menyelesaikan sesi.</summary>
[Serializable]
public class ScoreSubmission
{
    public string sessionId;
    public string playerName;
    public string storyId;
    public int points;
    public int objectivesCompleted;
    public int durationSeconds;
}

/// <summary>Balasan server atas pengiriman skor.</summary>
[Serializable]
public class ScoreResponse
{
    public string status;
    public string message;
    public int totalPoints;
    public int rank;
}