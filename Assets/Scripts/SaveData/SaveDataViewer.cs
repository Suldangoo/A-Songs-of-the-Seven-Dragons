using UnityEngine;

// 디버그 용 클래스
// save data를 에디터에서 볼 수 있도록 함 
public class SaveDataViewer : MonoBehaviour
{
    #region 싱글톤
    public static SaveDataViewer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveDataViewer>();
            }
            return instance;
        }
    }
    private static SaveDataViewer instance;
    #endregion

    // public 변수들로 값을 표시
    public bool progress;
    public bool gender;
    public string nickName;
    public int level;
    public int experience;
    public float hp;
    public int strength;
    public int agility;
    public int health;
    public int wisdom;
    public int charm;
    public int gold;
    public int weapon;
    public int armor;
    public int smallHpPotion;
    public int largeHpPotion;
    public float sfxVolume;
    public float bgmVolume;

    private void Awake()
    {
        // 모든 씬에서 하나만 유지
        if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // 저장된 데이터 표시
    public void GetSave()
    {
        progress = SaveManager.Progress;
        gender = SaveManager.Gender;
        nickName = SaveManager.NickName;
        level = SaveManager.Level;
        experience = SaveManager.Experience;
        hp = SaveManager.HP;
        strength = SaveManager.Strength;
        agility = SaveManager.Agility;
        health = SaveManager.Health;
        wisdom = SaveManager.Wisdom;
        charm = SaveManager.Charm;
        gold = SaveManager.Gold;
        weapon = SaveManager.Weapon;
        armor = SaveManager.Armor;
        smallHpPotion = SaveManager.SmallHpPotion;
        largeHpPotion = SaveManager.LargeHpPotion;
        sfxVolume = SaveManager.SfxVolume;
        bgmVolume = SaveManager.BgmVolume;
    }

    // 세이브 데이터 수정
    public void SetSave()
    {
        SaveManager.Progress = progress;
        SaveManager.Gender = gender;
        SaveManager.NickName = nickName;
        SaveManager.Level = level;
        SaveManager.Experience = experience;
        SaveManager.HP = hp;
        SaveManager.Strength = strength;
        SaveManager.Agility = agility;
        SaveManager.Health = health;
        SaveManager.Wisdom = wisdom;
        SaveManager.Charm = charm;
        SaveManager.Gold = gold;
        SaveManager.Weapon = weapon;
        SaveManager.Armor = armor;
        SaveManager.SmallHpPotion = smallHpPotion;
        SaveManager.LargeHpPotion = largeHpPotion;
        SaveManager.SfxVolume = sfxVolume;
        SaveManager.BgmVolume = bgmVolume;
    }

    // 세이브 데이터 초기화
    public void ClearSave()
    {
        SaveManager.ClearData();
    }
}
