using UnityEngine;
using TMPro;

public class SetStatus : MonoBehaviour
{
    // 인스펙터에서 설정할 TMpro 텍스트와 버튼 오브젝트
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI wisdomText;
    [SerializeField] private TextMeshProUGUI charmText;
    [SerializeField] private TextMeshProUGUI remainingPointsText;

    [SerializeField] private GameObject[] decreaseButtons; // 낮추기 버튼들
    [SerializeField] private GameObject[] increaseButtons; // 높이기 버튼들

    [SerializeField] private int minStatus = 5;     // 최소 스탯
    [SerializeField] private int maxStatus = 15;    // 최대 스탯

    // 초기 스테이터스 값
    [SerializeField] private int[] stats = { 5, 5, 5, 5, 5 }; // 힘, 민첩, 건강, 지혜, 매력
    [SerializeField] private int remainingPoints = 15; // 초기 남은 포인트

    private void Start()
    {
        UpdateUI();
    }

    // 특정 스테이터스를 감소시키는 메서드
    public void DownStatus(int index)
    {
        if (stats[index] > minStatus)
        {
            stats[index]--;
            remainingPoints++;
            UpdateUI();
        }
    }

    // 특정 스테이터스를 증가시키는 메서드
    public void UpStatus(int index)
    {
        if (stats[index] < maxStatus && remainingPoints > 0)
        {
            stats[index]++;
            remainingPoints--;
            UpdateUI();
        }
    }

    // UI 갱신을 위한 메서드
    private void UpdateUI()
    {
        strengthText.text = stats[0].ToString();
        agilityText.text = stats[1].ToString();
        healthText.text = stats[2].ToString();
        wisdomText.text = stats[3].ToString();
        charmText.text = stats[4].ToString();
        remainingPointsText.text = remainingPoints.ToString();

        // 낮추기 / 높이기 버튼 상태 갱신
        for (int i = 0; i < 5; i++)
        {
            decreaseButtons[i].SetActive(stats[i] > minStatus);
            increaseButtons[i].SetActive(stats[i] < maxStatus && remainingPoints > 0);
        }
    }
}
