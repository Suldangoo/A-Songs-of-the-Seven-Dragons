using UnityEngine;
using UnityEngine.UI;

public class ScrollIndicatorController : MonoBehaviour
{
    public float maxHeight = 1500f; // 높이 값 변수, Inspector에서 수정 가능

    [SerializeField]
    private Image indicatorImage; // UI Image, Inspector에서 할당

    private RectTransform selfRectTransform;

    private void Start()
    {
        // UI 오브젝트의 Rect Transform 가져오기
        selfRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // 현재 UI 오브젝트의 Height 값 가져오기
        float currentHeight = selfRectTransform.rect.height;

        // 높이 값이 지정된 최대 높이를 넘으면 이미지를 불투명하게, 그렇지 않으면 투명하게
        float alpha = currentHeight > maxHeight ? 1f : 0f;
        SetImageAlpha(alpha);
    }

    private void SetImageAlpha(float alpha)
    {
        Color imageColor = indicatorImage.color;
        imageColor.a = alpha;
        indicatorImage.color = imageColor;
    }
}
