using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float pressedScale = 0.94f;
    [SerializeField] private float speed = 14f;

    private Vector3 _baseScale;
    private Vector3 _targetScale;

    private void Awake()
    {
        _baseScale = transform.localScale;
        _targetScale = _baseScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.unscaledDeltaTime * speed);
    }

    public void OnPointerDown(PointerEventData eventData) => _targetScale = _baseScale * pressedScale;
    public void OnPointerUp(PointerEventData eventData) => _targetScale = _baseScale;
}