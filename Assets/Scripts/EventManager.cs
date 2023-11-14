using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class Event
{
    public string title; // 이벤트 제목 (인스펙터에 표시용)
    public Sprite illustration; // 일러스트 이미지
    [TextArea(1, 50)] public string text; // 텍스트 내용
    public string[] buttonTexts; // 버튼 텍스트 배열
    public UnityEvent[] buttonEvents; // 버튼 클릭 시 발생할 이벤트 배열
}

public class EventManager : MonoBehaviour
{
    #region 싱글톤
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventManager>();
            }
            return instance;
        }
    }
    private static EventManager instance;
    #endregion

    // 현재 발생하는 이벤트
    public int nowEvent = 0; // 가장 기본으로 0번 이벤트 (마을 이벤트) 가 실행된다.

    // 게임에 등장하는 이벤트들
    public Event[] events;

    // 화면의 UI에 표시되는 오브젝트들
    [SerializeField] private Image illustrationImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text button1;
    [SerializeField] private TMP_Text button2;
    [SerializeField] private TMP_Text button3;
    [SerializeField] private TMP_Text button4;

    private void Start()
    {
        // 게임 시작 시 첫 번째 이벤트 실행
        ShowEvent(nowEvent);
    }

    public void ShowEvent(int eventIndex)
    {
        if (eventIndex < events.Length)
        {
            Event currentEvent = events[eventIndex];

            // illustrationImage, text 등의 UI 요소에 currentEvent의 내용을 갱신
            illustrationImage.sprite = currentEvent.illustration;
            illustrationImage.gameObject.SetActive(currentEvent.illustration != null);

            text.text = currentEvent.text;

            // 각 버튼 텍스트 및 이벤트 설정
            SetButton(button1, currentEvent.buttonTexts.Length > 0 ? currentEvent.buttonTexts[0] : "", currentEvent.buttonEvents.Length > 0 ? currentEvent.buttonEvents[0] : null);
            SetButton(button2, currentEvent.buttonTexts.Length > 1 ? currentEvent.buttonTexts[1] : "", currentEvent.buttonEvents.Length > 1 ? currentEvent.buttonEvents[1] : null);
            SetButton(button3, currentEvent.buttonTexts.Length > 2 ? currentEvent.buttonTexts[2] : "", currentEvent.buttonEvents.Length > 2 ? currentEvent.buttonEvents[2] : null);
            SetButton(button4, currentEvent.buttonTexts.Length > 3 ? currentEvent.buttonTexts[3] : "", currentEvent.buttonEvents.Length > 3 ? currentEvent.buttonEvents[3] : null);
        }
    }

    private void SetButton(TMP_Text buttonText, string text, UnityEvent onClickEvent)
    {
        // 시작 부분에 '▶ ' 추가
        string formattedText = $"▶ {text}";
        buttonText.text = formattedText;

        // UITextInteraction 스크립트를 추가하고 onClickEvent를 할당
        UITextInteraction textInteraction = buttonText.gameObject.GetComponent<UITextInteraction>();
        if (textInteraction == null)
        {
            textInteraction = buttonText.gameObject.AddComponent<UITextInteraction>();
        }
        textInteraction.onClickEvent = onClickEvent;

        // SetActive를 텍스트가 비어있지 않은 경우에만 True로 설정
        buttonText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }

    public void ChangeEvent(int eventIndex)
    {
        nowEvent = eventIndex;
        ShowEvent(nowEvent);
    }
}
