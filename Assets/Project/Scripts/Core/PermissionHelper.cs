using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class PermissionHelper : MonoBehaviour
{
    /// <summary>
    /// Meminta izin kamera. Callback menerima true bila diizinkan.
    /// Aman dipanggil di platform selain Android (langsung true).
    /// </summary>
    public void RequestCamera(Action<bool> onResult)
    {
        StartCoroutine(RequestCameraRoutine(onResult));
    }

    private IEnumerator RequestCameraRoutine(Action<bool> onResult)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            onResult?.Invoke(true);
            yield break;
        }

        bool answered = false;
        bool granted = false;

        var callbacks = new PermissionCallbacks();
        callbacks.PermissionGranted += _ => { granted = true; answered = true; };
        callbacks.PermissionDenied += _ => { granted = false; answered = true; };
        callbacks.PermissionDeniedAndDontAskAgain += _ => { granted = false; answered = true; };

        Permission.RequestUserPermission(Permission.Camera, callbacks);

        // Batas tunggu 30 detik agar aplikasi tidak menggantung jika dialog gagal muncul.
        float timeout = Time.realtimeSinceStartup + 30f;
        while (!answered && Time.realtimeSinceStartup < timeout)
            yield return null;

        Debug.Log($"[Permission] Kamera: {(granted ? "diizinkan" : "ditolak")}");
        onResult?.Invoke(granted);
#else
        yield return null;
        onResult?.Invoke(true);
#endif
    }

    /// <summary>Membuka halaman pengaturan aplikasi di perangkat Android.</summary>
    public void OpenAppSettings()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var uriClass = new AndroidJavaClass("android.net.Uri"))
        {
            string package = Application.identifier;
            using (var uri = uriClass.CallStatic<AndroidJavaObject>("parse", "package:" + package))
            using (var intent = new AndroidJavaObject(
                "android.content.Intent",
                "android.settings.APPLICATION_DETAILS_SETTINGS", uri))
            {
                currentActivity.Call("startActivity", intent);
            }
        }
#endif
    }
}