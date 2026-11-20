using UnityEngine;

public class PulsingArtifact : InteractiveObject
{
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseAmount = 0.25f;

    private Vector3 _baseScale;

    private void Awake()
    {
        _baseScale = transform.localScale;
    }

    private void Update()
    {
        if (!isActivated) return;
        float factor = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = _baseScale * factor;
    }

    public override void OnInteract()
    {
        ReportInteraction();
    }

    public override string GetDescription()
    {
        return base.GetDescription() + " Artefak ini berdenyut.";
    }
}