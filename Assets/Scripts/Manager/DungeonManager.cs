using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class Monster
{
    public string Name; // 몬스터 이름
    public Sprite illustration; // 몬스터 일러스트
    public int AttackPower; // 몬스터의 공격력
    public int HP; // 몬스터의 체력
    public int RewardGold; // 처치 시 보상 골드
}

[System.Serializable]
public class Dungeon
{
    public string title; // 던전 이름
    public Sprite illustration; // 던전 일러스트
    public string dedescription; // 던전 첫 입장 시 설명 텍스트
    [TextArea(1, 50)] public string[] text; // 던전 탐색 시 출력할 텍스트들
    public Monster[] monsters; // 해당 던전에서 나올 수 있는 몬스터
    public Monster boss; // 해당 던전의 보스
}

public class DungeonManager : MonoBehaviour
{
    #region 싱글톤
    public static DungeonManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DungeonManager>();
            }
            return instance;
        }
    }
    private static DungeonManager instance;
    #endregion

    EventManager eventManager => EventManager.Instance;

    public Dungeon currentDungeon; // 현재 플레이어가 위치한 던전
    public int progress = 0; // 던전 진행률

    public Dungeon[] dungeons; // 던전

    // 화면의 UI에 표시되는 오브젝트들
    [SerializeField] private Image illustrationImage;
    [SerializeField] private TMP_Text dungeonProgress;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text button1;
    [SerializeField] private TMP_Text button2;
    [SerializeField] private TMP_Text button3;
    [SerializeField] private TMP_Text button4;

    // 던전에 들어가는 메소드
    public void EnterDungeon(int dungeon)
    {
        currentDungeon = dungeons[dungeon]; // 위치한 던전 갱신
        dungeonProgress.gameObject.SetActive(true); // 던전 진행률을 나타내는 GUI 오브젝트 켜기

        UpdateProgressUI();

        illustrationImage.sprite = currentDungeon.illustration; // 던전 일러스트 출력
        text.text = currentDungeon.dedescription; // 던전 설명 출력

        // 버튼1 텍스트와 이벤트 설정
        SetButton(button1, "탐색한다.", ExploreDungeon);

        // 버튼2 텍스트와 이벤트 설정
        SetButton(button2, "돌아간다.", BackToVillage);

        // 비활성화된 버튼3와 버튼4
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    // 버튼 텍스트와 이벤트를 설정하는 메소드
    private void SetButton(TMP_Text buttonText, string text, UnityAction onClickEvent)
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

        // UnityAction을 UnityEvent로 변환하여 할당
        UnityEvent clickAction = new UnityEvent();
        clickAction.AddListener(onClickEvent);
        textInteraction.onClickEvent = clickAction;

        // SetActive를 텍스트가 비어있지 않은 경우에만 True로 설정
        buttonText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }

    private void UpdateProgressUI()
    {
        dungeonProgress.text = $"던전 진행률: {progress}%";
        progressBar.fillAmount = progress / 100f;
    }

    // 던전을 탐색하는 메소드
    public void ExploreDungeon()
    {
        // 여기에 던전 탐색 시 발생할 이벤트 및 진행도 상승 등을 추가
        // 이후에는 버튼 텍스트와 이벤트를 다시 업데이트할 수 있습니다.
    }

    // 마을로 귀환하는 메소드
    public void BackToVillage()
    {
        progress = 0; // 던전 진행도 초기화
        dungeonProgress.gameObject.SetActive(false); // 던전 진행률을 나타내는 GUI 오브젝트 끄기

        eventManager.ChangeEvent(0); // 마을로 돌아가기
    }
}
