using UnityEngine;

// PlayerPrefs를 통한 정보 저장 & 불러오기
public static class SaveManager
{
    static string isSaveKey = "isSave";
    static string genderKey = "gender";
    static string nickNameKey = "nickName";
    static string bestScoreKey = "bestScore";
    static string sfxVolumeKey = "sfxVolume";
    static string bgmVolumeKey = "bgmVolume";

    // 진행중인 데이터가 존재하는가
    public static bool IsSave
    {
        get
        {
            int i = PlayerPrefs.GetInt(isSaveKey);
            return i == 1 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt(isSaveKey, value ? 1 : 0);
        }
    }

    // 캐릭터 성별
    // true : 남성 / false : 여성
    public static bool Gender
    {
        get
        {
            int i = PlayerPrefs.GetInt(genderKey);
            return i == 1 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt(genderKey, value ? 1 : 0);
        }
    }

    // 캐릭터 닉네임
    public static string NickName
    {
        get
        {
            return PlayerPrefs.GetString(nickNameKey);
        }
        set
        {
            PlayerPrefs.SetString(nickNameKey, value);
        }
    }

    // play Scene에서 최고 점수
    public static int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt(bestScoreKey);
        }
        set
        {
            PlayerPrefs.SetInt(bestScoreKey, value);
        }
    }    

    // BGM 볼륨
    static float SfxVolume_default = 0.5f;
    public static float SfxVolume
    {
        get 
        {
            // 이전 저장 데이터 없는 경우 기본 값 할당
            if (!PlayerPrefs.HasKey(sfxVolumeKey))
            {
                PlayerPrefs.SetFloat(sfxVolumeKey, SfxVolume_default);
            }

            return PlayerPrefs.GetFloat(sfxVolumeKey);
        }
        set
        {
            PlayerPrefs.SetFloat(sfxVolumeKey, value);
        }
    }

    // SFX 볼륨
    static float BgmVolume_default = 0.5f;
    public static float BgmVolume
    {
        get
        {
            // 이전 저장 데이터 없는 경우 기본 값 할당
            if (!PlayerPrefs.HasKey(bgmVolumeKey))
            {
                PlayerPrefs.SetFloat(bgmVolumeKey, BgmVolume_default);
            }

            return PlayerPrefs.GetFloat(bgmVolumeKey);

        }
        set
        {
            PlayerPrefs.SetFloat(bgmVolumeKey, value);
        }
    }

    // 모든 저장 데이터 삭제 메소드
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
