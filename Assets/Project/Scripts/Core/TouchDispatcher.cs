using UnityEngine;

public class TouchDispatcher : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddPoints(10);
                    Debug.Log($"Artefak {hit.collider.gameObject.name} tersentuh! Poin bertambah.");
                }

                hit.transform.localScale *= 1.1f;
            }
        }
    }
}