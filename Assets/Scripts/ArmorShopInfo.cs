using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorShopInfo : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance;
    private InfoManager infoManager => InfoManager.Instance;

    [Header("현재 장착중인 방패 정보")]
    [SerializeField] private Image currentArmorImage;
    [SerializeField] private TMP_Text currentArmorNameText;
    [SerializeField] private TMP_Text currentArmorStatsText;
    [SerializeField] private TMP_Text currentArmorDescriptionText;

    [Header("구매하려는 방패 정보")]
    [SerializeField] private Image purchaseArmorImage;
    [SerializeField] private TMP_Text purchaseArmorNameText;
    [SerializeField] private TMP_Text purchaseArmorStatsText;
    [SerializeField] private TMP_Text purchaseArmorDescriptionText;
    [SerializeField] private TMP_Text purchaseArmorCostText;

    private int itemCode;

    public void UpdateShopInfo(int itemIndex)
    {
        itemCode = itemIndex;

        // 현재 장착중인 방어구 정보 갱신
        UpdateArmorInfo(SaveManager.Armor, currentArmorImage, currentArmorNameText, currentArmorStatsText, currentArmorDescriptionText);

        // 구매하려는 방어구 정보 갱신
        UpdateArmorInfo(itemCode, purchaseArmorImage, purchaseArmorNameText, purchaseArmorStatsText, purchaseArmorDescriptionText);
        purchaseArmorCostText.text = $"비용: {itemManager.armorDatas[itemCode].cost}";
    }

    private void UpdateArmorInfo(int ArmorIndex, Image image, TMP_Text nameText, TMP_Text statsText, TMP_Text descriptionText)
    {
        if (ArmorIndex >= 0 && ArmorIndex < infoManager.armorImages.Length)
        {
            image.sprite = infoManager.armorImages[ArmorIndex];
        }

        string ArmorName = itemManager.armorDatas[ArmorIndex].name;
        nameText.text = $"{ArmorName}";

        int ArmorFixedIncrease = itemManager.armorDatas[ArmorIndex].fixedIncrease;
        float ArmorPercentIncrease = itemManager.armorDatas[ArmorIndex].percentIncrease;

        string ArmorStats = $"방어력: {ArmorFixedIncrease}\n추가방어력: +{((ArmorPercentIncrease - 1) * 100).ToString("F0")}%";
        statsText.text = ArmorStats;

        string ArmorDescription = itemManager.armorDatas[ArmorIndex].description;
        descriptionText.text = ArmorDescription;
    }

    public void OnClickPurchase()
    {
        // 현재 돈 확인
        int currentGold = SaveManager.Gold;
        int purchaseCost = itemManager.armorDatas[itemCode].cost;

        // 돈이 충분하면 무기를 구매
        if (currentGold >= purchaseCost)
        {
            // 돈 차감
            SaveManager.Gold -= purchaseCost;

            // 무기 교체
            SaveManager.Armor = itemCode;

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
