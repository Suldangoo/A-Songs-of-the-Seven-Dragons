using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponShopInfo : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance;
    private InfoManager infoManager => InfoManager.Instance;

    [Header("현재 장착중인 무기 정보")]
    [SerializeField] private Image currentWeaponImage;
    [SerializeField] private TMP_Text currentWeaponNameText;
    [SerializeField] private TMP_Text currentWeaponStatsText;
    [SerializeField] private TMP_Text currentWeaponDescriptionText;

    [Header("구매하려는 무기 정보")]
    [SerializeField] private Image purchaseWeaponImage;
    [SerializeField] private TMP_Text purchaseWeaponNameText;
    [SerializeField] private TMP_Text purchaseWeaponStatsText;
    [SerializeField] private TMP_Text purchaseWeaponDescriptionText;
    [SerializeField] private TMP_Text purchaseWeaponCostText;

    private int itemCode;

    public void UpdateShopInfo(int itemIndex)
    {
        itemCode = itemIndex;

        // 현재 장착중인 무기 정보 갱신
        UpdateWeaponInfo(SaveManager.Weapon, currentWeaponImage, currentWeaponNameText, currentWeaponStatsText, currentWeaponDescriptionText);

        // 구매하려는 무기 정보 갱신
        UpdateWeaponInfo(itemCode, purchaseWeaponImage, purchaseWeaponNameText, purchaseWeaponStatsText, purchaseWeaponDescriptionText);
        purchaseWeaponCostText.text = $"비용: {itemManager.weaponDatas[itemCode].cost - SaveManager.Charm}";
    }

    private void UpdateWeaponInfo(int weaponIndex, Image image, TMP_Text nameText, TMP_Text statsText, TMP_Text descriptionText)
    {
        if (weaponIndex >= 0 && weaponIndex < infoManager.weaponImages.Length)
        {
            image.sprite = infoManager.weaponImages[weaponIndex];
        }

        string weaponName = itemManager.weaponDatas[weaponIndex].name;
        nameText.text = $"{weaponName}";

        int weaponFixedIncrease = itemManager.weaponDatas[weaponIndex].fixedIncrease;
        float weaponPercentIncrease = itemManager.weaponDatas[weaponIndex].percentIncrease;

        string weaponStats = $"공격력: {weaponFixedIncrease}\n추가공격력: +{((weaponPercentIncrease - 1) * 100).ToString("F0")}%";
        statsText.text = weaponStats;

        string weaponDescription = itemManager.weaponDatas[weaponIndex].description;
        descriptionText.text = weaponDescription;
    }

    public void OnClickPurchase()
    {
        // 현재 돈 확인
        int currentGold = SaveManager.Gold;
        int purchaseCost = itemManager.weaponDatas[itemCode].cost;

        // Charm에 따른 비용 조정
        int adjustedCost = Mathf.Max(0, purchaseCost - SaveManager.Charm); // 상수값으로 조정

        // 돈이 충분하면 무기를 구매
        if (currentGold >= adjustedCost)
        {
            // 돈 차감
            SaveManager.Gold -= adjustedCost;

            // 무기 교체
            SaveManager.Weapon = itemCode;

            // 상점 창 닫기
            gameObject.SetActive(false);

            // 유저 정보 갱신
            InfoManager.Instance.UpdateInfo();

            // 상점 구매 완료 이벤트 실행
            EventManager.Instance.ShowEvent(7);
        }
        else
        {
            // 돈이 부족하면 알림
            Debug.Log("돈이 부족합니다.");
        }
    }

}
