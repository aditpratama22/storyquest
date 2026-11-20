using UnityEngine;

public class RotatingArtifact : InteractiveObject
{
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float normalSpeed = 30f;
    [SerializeField] private float activatedSpeed = 240f;

    private void Update()
    {
        float speed = isActivated ? activatedSpeed : normalSpeed;
        transform.Rotate(rotationAxis * (speed * Time.deltaTime));
    }

    public override void OnInteract()
    {
        ReportInteraction();
    }

    public override string GetDescription()
    {
        return base.GetDescription() + " Artefak ini berputar cepat saat aktif.";
    }
}