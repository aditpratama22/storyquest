using UnityEngine;

public class ColorShiftArtifact : InteractiveObject
{
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private Color targetColor = new Color(0.2f, 0.8f, 1f);
    [SerializeField] private float shiftSpeed = 3f;

    private void Awake()
    {
        if (objectRenderer == null) objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!isActivated) return;
        objectRenderer.material.color = Color.Lerp(objectRenderer.material.color, targetColor, Time.deltaTime * shiftSpeed);
    }

    public override void OnInteract()
    {
        ReportInteraction();
    }

    public override string GetDescription()
    {
        return base.GetDescription() + " Artefak ini berubah warna.";
    }
}