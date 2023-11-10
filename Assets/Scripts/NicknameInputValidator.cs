using TMPro;
using UnityEngine;

public class NicknameInputValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField;
    private const int maxCharacterCount = 6;

    private void Start()
    {
        if (nicknameInputField != null)
        {
            // 입력필드에 이벤트 리스너 등록
            nicknameInputField.onValueChanged.AddListener(OnNicknameValueChanged);
        }
    }

    private void OnNicknameValueChanged(string input)
    {
        if (input.Length > maxCharacterCount)
        {
            // 입력이 6글자를 초과하면 입력 필드의 값을 자르기
            nicknameInputField.text = input.Substring(0, maxCharacterCount);
        }
    }
}
