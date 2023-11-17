using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string name; // 무기 이름
    public int fixedIncrease; // 고정 상승 수치
    public float percentIncrease; // 퍼센트 상승 수치
    public int cost; // 비용
    [TextArea(2, 5)] public string description; // 무기 설명 (2줄 이상, 5줄 이하)
}

[System.Serializable]
public class ArmorData
{
    public string name; // 방어구 이름
    public int fixedIncrease; // 고정 상승 수치
    public float percentIncrease; // 퍼센트 상승 수치
    public int cost; // 비용
    [TextArea(2, 5)] public string description; // 방어구 설명 (2줄 이상, 5줄 이하)
}

public class ItemManager : MonoBehaviour
{
    #region 싱글톤
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
            }
            return instance;
        }
    }
    private static ItemManager instance;
    #endregion

    public WeaponData[] weaponDatas; // 무기 데이터 배열
    public ArmorData[] armorDatas; // 방어구 데이터 배열

    InfoManager infoManager => InfoManager.Instance;

    // 물약을 사용하는 메소드
    public void OnClickPotion(int size)
    {
        int maxHp = SaveManager.Health * 3;

        // 작은 물약 사용
        if (size == 0 && SaveManager.SmallHpPotion > 0)
        {
            SaveManager.SmallHpPotion--;
            int healAmount = Mathf.RoundToInt(maxHp * 0.3f);
            SaveManager.Hp += healAmount;
            SaveManager.Hp = Mathf.Min(SaveManager.Hp, maxHp);
            infoManager.UpdateInfo(); // 정보 갱신
        }

        // 큰 물약 사용
        else if (size == 1 && SaveManager.LargeHpPotion > 0)
        {
            SaveManager.LargeHpPotion--;
            int healAmount = Mathf.RoundToInt(maxHp * 0.8f);
            SaveManager.Hp += healAmount;
            SaveManager.Hp = Mathf.Min(SaveManager.Hp, maxHp);
            infoManager.UpdateInfo(); // 정보 갱신
        }
    }
}