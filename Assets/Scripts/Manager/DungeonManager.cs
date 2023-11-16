using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Collections;

[System.Serializable]
public class Monster
{
    public string Name; // 몬스터 이름
    public int Code; // 몬스터 코드
    public int AttackPower; // 몬스터의 공격력
    public int HP; // 몬스터의 체력
    public int Speed; // 몬스터의 스피드
    public int RewardGold; // 처치 시 보상 골드
    public int RewardExp; // 처치 시 보상 경험치
}

[System.Serializable]
public class Dungeon
{
    public string title; // 던전 이름
    public Sprite illustration; // 던전 일러스트
    public string dedescription; // 던전 첫 입장 시 설명 텍스트
    public int treasure; // 던전 최대 보물 보상
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
    InfoManager infoManager => InfoManager.Instance;
    ItemManager itemManager => ItemManager.Instance;

    public Dungeon currentDungeon; // 현재 플레이어가 위치한 던전
    public int progress = 0; // 던전 진행률

    public Dungeon[] dungeons; // 던전

    // 화면의 UI에 표시되는 오브젝트들
    [SerializeField] private Image illustrationImage;
    [SerializeField] private Image monsterImage;
    [SerializeField] private Animator monsterAnim;
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
        illustrationImage.sprite = currentDungeon.illustration; // 던전 일러스트 출력

        StartDungeon(currentDungeon);
    }

    public void StartDungeon(Dungeon dungeon)
    {
        UpdateProgressUI();

        text.text = currentDungeon.dedescription; // 던전 설명 출력

        monsterImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

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
        infoManager.UpdateInfo();

        illustrationImage.gameObject.SetActive(currentDungeon.illustration != null);
        dungeonProgress.text = $"던전 진행률: {progress}%";
        progressBar.fillAmount = progress / 100f;
    }

    // 던전을 탐색하는 메소드
    public void ExploreDungeon()
    {
        int randomValue = Random.Range(1, 101); // 1부터 100까지의 랜덤한 값

        // 던전 진행률 1~4% 상승
        progress += Random.Range(1, 5);

        // 업데이트된 던전 정보를 UI에 반영
        UpdateProgressUI();

        if (randomValue <= 40) // 40% 확률: 던전 탐색 텍스트
        {
            // Dungeon 클래스의 text 중 랜덤하게 선택
            string dungeonText = currentDungeon.text[Random.Range(0, currentDungeon.text.Length)];
            ShowDungeonText(dungeonText);
        }
        else if (randomValue <= 70) // 30% 확률: 몬스터 조우
        {
            EncounterMonster();
        }
        else if (randomValue <= 90) // 20% 확률: 휴식처 이벤트 (미구현)
        {
            RestEvent();
        }
        else // 10% 확률: 보물 이벤트 (미구현)
        {
            TreasureEvent();
        }
    }

    // 던전 탐색 텍스트를 출력하는 메소드
    private void ShowDungeonText(string text)
    {
        this.text.text = text;

        // 업데이트된 던전 정보를 UI에 반영
        UpdateProgressUI();
    }

    // 몬스터 조우 이벤트를 처리하는 메소드
    private void EncounterMonster()
    {
        monsterImage.gameObject.SetActive(true); // 몬스터 활성화
        dungeonProgress.gameObject.SetActive(false); // 잠깐 비활성화

        Monster randomMonster = currentDungeon.monsters[Random.Range(0, currentDungeon.monsters.Length)];

        monsterAnim.SetInteger("monster", randomMonster.Code); // 몬스터 애니메이터 재생

        this.text.text = $"{randomMonster.Name}이(가) 등장했다!\n체력: {randomMonster.HP}\n공격력: {randomMonster.AttackPower}\n스피드: {randomMonster.Speed}";

        // 버튼1 텍스트와 이벤트 설정
        SetButton(button1, "전투 시작!", () => StartBattle(randomMonster));

        // 버튼2 텍스트와 이벤트 설정
        SetButton(button2, "도망치다.", RunAway);

        // 비활성화된 버튼3와 버튼4
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    // 몬스터로부터 도망치는 메소드
    public void RunAway()
    {
        monsterImage.gameObject.SetActive(false); // 몬스터 비활성화
        dungeonProgress.gameObject.SetActive(true);

        StartDungeon(currentDungeon);
    }

    // 전투 시작 메소드
    private void StartBattle(Monster monster)
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        StartCoroutine(AutoBattle(monster));
    }

    // 자동 전투 코루틴
    private IEnumerator AutoBattle(Monster monster)
    {
        // 전투 로그 초기화
        text.text = "";

        // 몬스터 초기 체력 저장
        int monsterHP = monster.HP;

        // 전투 중인 경우
        while (SaveManager.Hp > 0 && monsterHP > 0)
        {
            // 플레이어와 몬스터의 데미지 계산
            int playerDamage = CalculatePlayerDamage();
            int monsterDamage = CalculateMonsterDamage(monster);

            // 몬스터에게 데미지 입히기
            monsterHP -= playerDamage;

            // 전투 로그에 플레이어의 공격 메시지 추가
            text.text += $"당신의 공격! {playerDamage}의 데미지를 주었다!\n";

            // 몬스터 사망 체크
            if (monsterHP <= 0)
            {
                // 몬스터 사망 처리
                monsterAnim.SetTrigger("death");

                // 전투 로그에 승리 메시지 추가
                text.text += $"{monster.Name}와의 전투에서 승리했다!\n";
                text.text += $"{monster.RewardGold}골드를 획득했다.\n";
                text.text += $"{monster.RewardExp}만큼의 경험치를 획득했다.";

                // SaveManager에 골드와 경험치 반영
                SaveManager.Gold += monster.RewardGold;
                SaveManager.Experience += monster.RewardExp;

                // 값 갱신
                infoManager.UpdateInfo();

                // 버튼1 텍스트와 이벤트 설정
                button1.gameObject.SetActive(true);
                SetButton(button1, "다시 탐색을 시작한다.", () => StartDungeon(currentDungeon));

                yield break; // 코루틴 종료
            }

            // 0.7초 대기
            yield return new WaitForSeconds(0.7f);

            // 플레이어에게 데미지 입히기
            SaveManager.Hp -= monsterDamage;

            // 전투 로그에 몬스터의 공격 메시지 추가
            text.text += $"{monster.Name}의 공격! {monsterDamage}의 피해를 입었다!\n";

            // 플레이어 사망 체크
            if (SaveManager.Hp <= 0)
            {
                // 플레이어 사망 처리 (후속 처리는 PlayerDead() 메소드에서)
                PlayerDead();

                yield break; // 코루틴 종료
            }

            // 0.7초 대기
            yield return new WaitForSeconds(0.7f);
        }

        // 값 갱신
        infoManager.UpdateInfo();
    }

    // 플레이어의 데미지 계산 메소드
    private int CalculatePlayerDamage()
    {
        // 플레이어의 공격력 계산
        int playerStrength = SaveManager.Strength;
        int weaponIndex = SaveManager.Weapon;
        int damage = 0;

        if (weaponIndex >= 0 && weaponIndex < itemManager.weaponDatas.Length)
        {
            WeaponData weaponData = itemManager.weaponDatas[weaponIndex];
            int fixedIncrease = weaponData.fixedIncrease;
            float percentIncrease = weaponData.percentIncrease;

            damage = Mathf.RoundToInt((playerStrength * 2 + fixedIncrease) * percentIncrease);
        }

        // 랜덤 변동 추가 (+- 10%)
        float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
        damage = Mathf.Max(1, Mathf.RoundToInt(damage * randomFactor));

        return damage;
    }

    // 몬스터의 데미지 계산 메소드
    private int CalculateMonsterDamage(Monster monster)
    {
        // 몬스터의 공격력
        int monsterAttackPower = monster.AttackPower;

        // 플레이어의 방어력 계산
        int playerAgility = SaveManager.Agility;
        int armorIndex = SaveManager.Armor;
        int totalDefense = 0;

        if (armorIndex >= 0 && armorIndex < itemManager.armorDatas.Length)
        {
            ArmorData armorData = itemManager.armorDatas[armorIndex];
            int fixedIncrease = armorData.fixedIncrease;
            float percentIncrease = armorData.percentIncrease;

            totalDefense = Mathf.RoundToInt((playerAgility * 2 + fixedIncrease) * percentIncrease);
        }

        // 최종 데미지 계산
        int damage = Mathf.Max(1, monsterAttackPower - totalDefense);

        // 랜덤 변동 추가 (+- 10%)
        float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
        damage = Mathf.Max(1, Mathf.RoundToInt(damage * randomFactor));

        return damage;
    }

    // 플레이어가 죽었을 때 호출되는 메소드
    private void PlayerDead()
    {
        // 플레이어 사망 처리
        // (추가적인 게임 오버 화면 표시 등의 로직을 여기에 추가할 수 있습니다.)
        // 예: GameManager.Instance.GameOver();
    }


    // 휴식처 이벤트를 처리하는 메소드
    private void RestEvent()
    {
        // "쉴만한 장소를 발견했다. 잠깐 휴식하면 체력을 보충할 수 있을 것 같다." 라는 문구 출력
        this.text.text = "쉴만한 장소를 발견했다. 잠깐 휴식하면 체력을 보충할 수 있을 것 같다.";

        // 버튼1 텍스트와 이벤트 설정
        SetButton(button1, "휴식한다.", () => Rest());

        // 버튼2 텍스트와 이벤트 설정
        SetButton(button2, "지나친다.", () => StartDungeon(currentDungeon));

        // 비활성화된 버튼3와 버튼4
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    // 휴식을 하는 메소드
    private void Rest()
    {
        // SaveManager를 이용하여 체력 회복
        int maxHp = SaveManager.Health * 3;
        SaveManager.Hp += 10;

        // 최대 체력을 넘지 않도록 조절
        SaveManager.Hp = Mathf.Min(SaveManager.Hp, maxHp);

        // 휴식 후 ExploreDungeon 실행
        ExploreDungeon();
    }

    // 보물 이벤트를 처리하는 메소드
    private void TreasureEvent()
    {
        text.text = "보물 상자를 발견했다! 열어볼까?";

        // 버튼1 텍스트와 이벤트 설정
        SetButton(button1, "열어본다.", OpenTreasure);

        // 버튼2 텍스트와 이벤트 설정
        SetButton(button2, "지나친다.", () => StartDungeon(currentDungeon));
    }

    // 보물 상자를 열 때 호출되는 메소드
    private void OpenTreasure()
    {
        int goldAmount = Random.Range(10, currentDungeon.treasure); // 10 ~ 최대보상 사이의 랜덤한 골드 획득

        // 골드를 얻은 텍스트 출력
        text.text = $"보물 상자를 열어 {goldAmount}골드를 획득했다!";

        // SaveManager를 이용하여 골드 획득
        SaveManager.Gold += goldAmount;

        // 값 갱신
        infoManager.UpdateInfo();

        // 버튼1 텍스트와 이벤트 설정
        SetButton(button1, "지나친다.", () => StartDungeon(currentDungeon));
        button2.gameObject.SetActive(false);
    }

    // 마을로 귀환하는 메소드
    public void BackToVillage()
    {
        progress = 0; // 던전 진행도 초기화
        dungeonProgress.gameObject.SetActive(false); // 던전 진행률을 나타내는 GUI 오브젝트 끄기

        eventManager.ChangeEvent(0); // 마을로 돌아가기
    }
}
