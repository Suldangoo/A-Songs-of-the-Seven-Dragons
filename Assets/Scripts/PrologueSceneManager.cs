using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrologueSceneManager : MonoBehaviour
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

    public void GameStart()
    {
        sceneChanger.SceneChange("Game");
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
        string prologueContent = "검과 마법이 교차하는 대륙 '이루티아'.\n이곳엔 신으로 섬겨지는\n일곱 마리의 용이 존재한다.\n\n" +
            "그러나 여신년 120년,\n일곱 용 중 하나인 흑룡 바하무트가\n" +
            "원인 불명으로 갑작스럽게 날뛰기 시작했고,\n" +
            "이루티아 대륙의 북쪽은 바하무트에게\n무자비한 파멸을 맞이했다.\n\n" +
            "이로 인해 엘버라 마을의 '엘버라 기사단'의\n" +
            "모든 기사에게, 대륙의 남쪽으로 떠나\n바하무트를 토벌하라는 임무가 주어진다.\n\n" +
            "그렇게 엘버라 기사단의 엘리트 기사,\n" +
            SaveManager.NickName + " 역시 바하무트를 토벌하기 위해\n" +
            "모험을 시작하게 된다.";

        prologueText.text = "";

        foreach (char letter in prologueContent)
        {
            prologueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 한 글자씩 나타나는 딜레이
        }
    }
}
