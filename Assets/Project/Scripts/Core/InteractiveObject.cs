using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    [Header("Pengaturan Umum")]
    [SerializeField] protected string displayName = "Objek";
    [SerializeField] protected int pointValue = 10;

    public int PointValue => pointValue;
    public string DisplayName => displayName;

    protected bool isActivated;

    public abstract void OnInteract();

    public virtual string GetDescription()
    {
        return $"{displayName} bernilai {pointValue} poin.";
    }

    protected void ReportInteraction()
    {
        isActivated = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddPoints(pointValue);
        }
        Debug.Log($"[Interaksi] {GetDescription()}");
    }
}