using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string name; // 무기 이름
    public int fixedIncrease; // 고정 상승 수치
    public float percentIncrease; // 퍼센트 상승 수치
    [TextArea(2, 5)] public string description; // 무기 설명 (2줄 이상, 5줄 이하)
}

[System.Serializable]
public class ArmorData
{
    public string name; // 방어구 이름
    public int fixedIncrease; // 고정 상승 수치
    public float percentIncrease; // 퍼센트 상승 수치
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

    [SerializeField] private WeaponData[] weaponDatas; // 무기 데이터 배열
    [SerializeField] private ArmorData[] armorDatas; // 방어구 데이터 배열
}