using UnityEngine;

public class SimpleSpinner : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 240f;

    private void Update()
    {
        transform.Rotate(0f, 0f, degreesPerSecond * Time.unscaledDeltaTime);
    }
}