using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonLongPressListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [Tooltip("Hold duration in seconds")]
    [Range(0.3f, 5f)] public float holdDuration = 3f;
    public UnityEvent onLongPress;
    public UnityEvent OutLongPress;

    private bool isPointerDown = false;
    private bool isLongPressed = false;
    private bool isactive = true;
    private float elapsedTime = 0f;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
    }

    private void Update()
    {
        if (isPointerDown && !isLongPressed)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= holdDuration)
            {
                isLongPressed = true;
                elapsedTime = 0f;
                if (button.interactable && !object.ReferenceEquals(onLongPress, null))
                {
                    onLongPress.Invoke();
                    LeanTween.scale(this.transform.gameObject, new Vector3(1.3f, 1.3f, 1.3f), .3f).setDelay(0.2f).setEase(LeanTweenType.easeInQuad);
                    isactive = true;
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        isLongPressed = false;
        elapsedTime = 0f;
        LeanTween.scale(this.transform.gameObject, new Vector3(1f, 1f, 1f), .2f).setDelay(0.2f).setEase(LeanTweenType.easeInQuad);           
        isactive = false;

        OutLongPress.Invoke();
    }
}