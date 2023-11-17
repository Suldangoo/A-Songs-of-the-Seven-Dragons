using TMPro;
using UnityEngine;

public class PotionShopInfo : MonoBehaviour
{
    InfoManager InfoManager => InfoManager.Instance;

    [SerializeField] private TextMeshProUGUI smallCostText;
    [SerializeField] private TextMeshProUGUI bigCostText;

    private int smallCost = 100;
    private int bigCost = 500;

    private void OnEnable()
    {
        // Charm에 따른 비용 조정
        int charmBonus = SaveManager.Charm; // 상수값으로 조정

        // 실제 비용에 Charm 반영
        smallCost -= charmBonus;
        bigCost -= charmBonus;

        // UI에 출력
        smallCostText.text = $"{smallCost}골드";
        bigCostText.text = $"{bigCost}골드";
    }

    public void OnClickBuyPotion(int size)
    {
        int cost = 0;
        int maxQuantity = 0;

        // 소형 포션
        if (size == 0)
        {
            cost = smallCost;
            maxQuantity = 3;
        }
        // 대형 포션
        else if (size == 1)
        {
            cost = bigCost;
            maxQuantity = 3;
        }

        // 포션이 구매 가능한지 확인
        if (CanBuyPotion(size, cost, maxQuantity))
        {
            BuyPotion(size, cost, maxQuantity);
        }
        else
        {
            // 구매 불가 메시지 또는 다른 처리를 여기에 추가
            Debug.Log("포션이 구매 불가능합니다.");
        }
    }

    private bool CanBuyPotion(int size, int cost, int maxQuantity)
    {
        // 플레이어의 골드와 포션 소지 개수 확인
        int playerGold = SaveManager.Gold;
        int currentQuantity = (size == 0) ? SaveManager.SmallHpPotion : SaveManager.LargeHpPotion;

        return playerGold >= cost && currentQuantity < maxQuantity;
    }

    private void BuyPotion(int size, int cost, int maxQuantity)
    {
        // 구매 비용 차감
        SaveManager.Gold -= cost;

        // 소형 포션 구매
        if (size == 0)
        {
            SaveManager.SmallHpPotion++;
        }
        // 대형 포션 구매
        else if (size == 1)
        {
            SaveManager.LargeHpPotion++;
        }

        // 값 갱신
        InfoManager.UpdateInfo();
    }
}
