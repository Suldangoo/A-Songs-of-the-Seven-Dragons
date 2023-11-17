using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneManager : MonoBehaviour
{
    SceneChanger sceneChanger => SceneChanger.Instance;

    public Image illustrationImage; // 프롤로그 일러스트 이미지
    public TMP_Text prologueText; // 프롤로그 텍스트
    public Image startButtonImage; // 게임 시작 버튼 이미지
    public TMP_Text startButtonText; // 게임 시작 버튼 텍스트

    private void Start()
    {
        StartCoroutine(StartAfterDelay());
    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(1f); // 1초 대기

        StartCoroutine(PrologueSequence());
    }

    public void OnClickTitle()
    {
        sceneChanger.SceneChange("Title");
    }

    private IEnumerator PrologueSequence()
    {
        // 일러스트 나타나기
        yield return FadeInImage(illustrationImage, 1f);

        // 프롤로그 텍스트 나타나기
        yield return ShowPrologueText();

        // 게임 시작 버튼 나타나기
        yield return FadeInImage(startButtonImage, 1f);
        yield return FadeInText(startButtonText, 1f);

        // 코루틴이 모두 종료된 후에 버튼 Interactable 활성화
        startButtonImage.GetComponent<Button>().interactable = true;
    }

    private IEnumerator FadeInImage(Image image, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            image.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = targetColor;
    }

    private IEnumerator FadeInText(TMP_Text text, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            text.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.color = targetColor;
    }

    private IEnumerator ShowPrologueText()
    {
        string prologueContent = "위대한 일곱 용들 중 흑룡, 바하무트는\n" + SaveManager.NickName + "의 손에 토벌당했다.\n\n" +
            "바하무트는 비록 다른 용들에 비해\n선한 용은 아니었지만, 그는 필요악으로서\n" +
            "강한 힘으로 세계에 혼돈을 가져오는 악들을\n물리치는, 세계의 균형을 지키는 용이었다.\n\n" +
            "그랬던 바하무트가 어째서 폭주했는지,\n" +
            "이루티아 대륙의 북쪽을 어째서 파괴했는지는\n그 누구도 알지 못한다.\n\n" +
            SaveManager.NickName + "이(가) 바하무트를 쓰러트렸을 때,\n바하무트는 눈물을 흘리고 있었다.\n" +
            SaveManager.NickName + "은(는) 바하무트를 토벌한 공으로\n엘버라 기사단의 기사단장으로 작위를 임명받았지만,\n" +
            "어째서 바하무트가 눈물을 흘렸는지는 알지 못한다.";

        prologueText.text = "";

        foreach (char letter in prologueContent)
        {
            prologueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 한 글자씩 나타나는 딜레이
        }
    }
}
