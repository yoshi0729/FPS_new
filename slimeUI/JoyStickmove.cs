using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform handle; // ジョイスティックのハンドル
    public float maxRadius = 100f; // ハンドルが動ける最大距離

    private Vector2 inputVector;

    public Vector2 InputVector => inputVector; // 公開プロパティで方向を取得可能

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out position
        );

        // ハンドルの移動を制限
        position = Vector2.ClampMagnitude(position, maxRadius);
        handle.anchoredPosition = position;

        // 入力ベクトルの正規化
        inputVector = position / maxRadius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ハンドルをリセット
        handle.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
    }
}