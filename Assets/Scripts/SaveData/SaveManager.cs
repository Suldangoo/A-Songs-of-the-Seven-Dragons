using UnityEngine;

public static class SaveManager
{
    static string progressKey = "progress"; // 진행 여부
    static string genderKey = "gender";
    static string nickNameKey = "nickName";
    static string levelKey = "level";
    static string experienceKey = "experience";
    static string hpKey = "hp";
    static string strengthKey = "strength";
    static string agilityKey = "agility";
    static string healthKey = "health";
    static string wisdomKey = "wisdom";
    static string charmKey = "charm";
    static string goldKey = "gold";
    static string weaponKey = "weapon";
    static string armorKey = "armor";
    static string smallHpPotionKey = "smallHpPotion";
    static string largeHpPotionKey = "largeHpPotion";

    static string sfxVolumeKey = "sfxVolume";
    static string bgmVolumeKey = "bgmVolume";

    public static bool Progress
    {
        get { return PlayerPrefs.GetInt(progressKey) == 1; }
        set { PlayerPrefs.SetInt(progressKey, value ? 1 : 0); }
    }

    public static bool Gender
    {
        get { return PlayerPrefs.GetInt(genderKey) == 1; }
        set { PlayerPrefs.SetInt(genderKey, value ? 1 : 0); }
    }

    public static string NickName
    {
        get { return PlayerPrefs.GetString(nickNameKey); }
        set { PlayerPrefs.SetString(nickNameKey, value); }
    }

    public static int Level
    {
        get
        {
            if (!PlayerPrefs.HasKey(levelKey))
            {
                PlayerPrefs.SetInt(levelKey, 1);
            }

            return PlayerPrefs.GetInt(levelKey);
        }
        set { PlayerPrefs.SetInt(levelKey, value); }
    }

    public static int Experience
    {
        get { return PlayerPrefs.GetInt(experienceKey); }
        set { PlayerPrefs.SetInt(experienceKey, value); }
    }

    public static float HP
    {
        get { return PlayerPrefs.GetFloat(hpKey); }
        set { PlayerPrefs.SetFloat(hpKey, value); }
    }

    public static int Strength
    {
        get { return PlayerPrefs.GetInt(strengthKey); }
        set { PlayerPrefs.SetInt(strengthKey, value); }
    }

    public static int Agility
    {
        get { return PlayerPrefs.GetInt(agilityKey); }
        set { PlayerPrefs.SetInt(agilityKey, value); }
    }

    public static int Health
    {
        get { return PlayerPrefs.GetInt(healthKey); }
        set { PlayerPrefs.SetInt(healthKey, value); }
    }

    public static int Wisdom
    {
        get { return PlayerPrefs.GetInt(wisdomKey); }
        set { PlayerPrefs.SetInt(wisdomKey, value); }
    }

    public static int Charm
    {
        get { return PlayerPrefs.GetInt(charmKey); }
        set { PlayerPrefs.SetInt(charmKey, value); }
    }

    public static int Gold
    {
        get { return PlayerPrefs.GetInt(goldKey); }
        set { PlayerPrefs.SetInt(goldKey, value); }
    }

    public static int Weapon
    {
        get { return PlayerPrefs.GetInt(weaponKey); }
        set { PlayerPrefs.SetInt(weaponKey, value); }
    }

    public static int Armor
    {
        get { return PlayerPrefs.GetInt(armorKey); }
        set { PlayerPrefs.SetInt(armorKey, value); }
    }

    public static int SmallHpPotion
    {
        get { return PlayerPrefs.GetInt(smallHpPotionKey); }
        set { PlayerPrefs.SetInt(smallHpPotionKey, value); }
    }

    public static int LargeHpPotion
    {
        get { return PlayerPrefs.GetInt(largeHpPotionKey); }
        set { PlayerPrefs.SetInt(largeHpPotionKey, value); }
    }

    public static float SfxVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey(sfxVolumeKey))
            {
                PlayerPrefs.SetFloat(sfxVolumeKey, 0.5f);
            }

            return PlayerPrefs.GetFloat(sfxVolumeKey);
        }
        set { PlayerPrefs.SetFloat(sfxVolumeKey, value); }
    }

    public static float BgmVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey(bgmVolumeKey))
            {
                PlayerPrefs.SetFloat(bgmVolumeKey, 0.5f);
            }

            return PlayerPrefs.GetFloat(bgmVolumeKey);
        }
        set { PlayerPrefs.SetFloat(bgmVolumeKey, value); }
    }
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
