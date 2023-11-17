using TMPro;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    InfoManager infoManager => InfoManager.Instance;

    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI wisdomText;
    [SerializeField] private TextMeshProUGUI charmText;
    [SerializeField] private TextMeshProUGUI remainingPointsText;

    [SerializeField] private GameObject[] decreaseButtons;
    [SerializeField] private GameObject[] increaseButtons;

    private int strength;
    private int agility;
    private int health;
    private int wisdom;
    private int charm;
    private int remainingPoints;

    private void OnEnable()
    {
        // 초기화
        strength = SaveManager.Strength;
        agility = SaveManager.Agility;
        health = SaveManager.Health;
        wisdom = SaveManager.Wisdom;
        charm = SaveManager.Charm;
        remainingPoints = SaveManager.Remain;

        // UI 갱신
        UpdateUI();
    }

    public void DownStatus(int index)
    {
        if (GetStat(index) > GetMinStatus(index))
        {
            SetStat(index, GetStat(index) - 1);
            remainingPoints++;
            UpdateUI();
        }
    }

    public void UpStatus(int index)
    {
        if (remainingPoints > 0)
        {
            SetStat(index, GetStat(index) + 1);
            remainingPoints--;
            UpdateUI();
        }
    }

    public void OnClickFinish()
    {
        // 최종 스탯 적용
        SaveManager.Strength = strength;
        SaveManager.Agility = agility;
        SaveManager.Health = health;
        SaveManager.Wisdom = wisdom;
        SaveManager.Charm = charm;
        SaveManager.Remain = remainingPoints;

        infoManager.UpdateInfo();

        // 해당 스크립트 비활성화
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        strengthText.text = strength.ToString();
        agilityText.text = agility.ToString();
        healthText.text = health.ToString();
        wisdomText.text = wisdom.ToString();
        charmText.text = charm.ToString();
        remainingPointsText.text = remainingPoints.ToString();

        for (int i = 0; i < 5; i++)
        {
            decreaseButtons[i].SetActive(GetStat(i) > GetMinStatus(i));
            increaseButtons[i].SetActive(remainingPoints > 0);
        }
    }

    private int GetStat(int index)
    {
        switch (index)
        {
            case 0: return strength;
            case 1: return agility;
            case 2: return health;
            case 3: return wisdom;
            case 4: return charm;
            default: return 0;
        }
    }

    private void SetStat(int index, int value)
    {
        switch (index)
        {
            case 0: strength = value; break;
            case 1: agility = value; break;
            case 2: health = value; break;
            case 3: wisdom = value; break;
            case 4: charm = value; break;
        }
    }

    private int GetMinStatus(int index)
    {
        switch (index)
        {
            case 0: return SaveManager.Strength;
            case 1: return SaveManager.Agility;
            case 2: return SaveManager.Health;
            case 3: return SaveManager.Wisdom;
            case 4: return SaveManager.Charm;
            default: return 0;
        }
    }
}
