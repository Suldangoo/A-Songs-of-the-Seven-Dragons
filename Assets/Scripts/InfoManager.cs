using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoManager : MonoBehaviour
{
    #region 싱글톤
    public static InfoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InfoManager>();
            }
            return instance;
        }
    }
    private static InfoManager instance;
    #endregion

    [SerializeField] private Image[] characterImages; // 남자, 여자 캐릭터 이미지 배열 (0: 남자, 1: 여자)
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image experienceFillImage;
    [SerializeField] private Image hpFillImage;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text strengthText;
    [SerializeField] private TMP_Text agilityText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text wisdomText;
    [SerializeField] private TMP_Text charmText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private Image[] weaponImages; // 무기 이미지 배열
    [SerializeField] private Image[] armorImages; // 방어구 이미지 배열
    [SerializeField] private TMP_Text smallHpPotionText;
    [SerializeField] private TMP_Text largeHpPotionText;

    private void Start()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        // 1. 캐릭터 외형
        bool isMale = SaveManager.Gender;
        characterImages[0].gameObject.SetActive(isMale); // 남자 캐릭터 이미지
        characterImages[1].gameObject.SetActive(!isMale); // 여자 캐릭터 이미지

        // 2. 캐릭터 닉네임
        nickNameText.text = SaveManager.NickName;

        // 3. 캐릭터 레벨
        levelText.text = $"Level: {SaveManager.Level}";

        // 4. 캐릭터 경험치
        int maxExperience = SaveManager.Level * 5;
        float fillAmount = (float)SaveManager.Experience / maxExperience;
        experienceFillImage.fillAmount = fillAmount;

        // 5. 캐릭터 HP
        int maxHp = SaveManager.Health * 3;
        fillAmount = SaveManager.Hp / (float)maxHp;
        hpFillImage.fillAmount = fillAmount;
        hpText.text = $"{SaveManager.Hp}/{maxHp}";

        // 6. 캐릭터 스테이터스
        strengthText.text = SaveManager.Strength.ToString();
        agilityText.text = SaveManager.Agility.ToString();
        healthText.text = SaveManager.Health.ToString();
        wisdomText.text = SaveManager.Wisdom.ToString();
        charmText.text = SaveManager.Charm.ToString();

        // 7. 골드
        goldText.text = SaveManager.Gold.ToString();

        // 8. 무기
        int weaponIndex = SaveManager.Weapon;
        for (int i = 0; i < weaponImages.Length; i++)
        {
            if (i == weaponIndex)
            {
                weaponImages[i].gameObject.SetActive(true);
            }
            else
            {
                weaponImages[i].gameObject.SetActive(false);
            }
        }

        // 9. 방어구
        int armorIndex = SaveManager.Armor;
        for (int i = 0; i < armorImages.Length; i++)
        {
            if (i == armorIndex)
            {
                armorImages[i].gameObject.SetActive(true);
            }
            else
            {
                armorImages[i].gameObject.SetActive(false);
            }
        }


        // 10. 포션
        smallHpPotionText.text = SaveManager.SmallHpPotion.ToString();
        largeHpPotionText.text = SaveManager.LargeHpPotion.ToString();
    }
}
