using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance;
    private CanvasGroup fadeCanvasGroup;
    private float fadeDuration = 0.6f;

    private void Awake()
    {
        // 싱글톤 패턴
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // 캔버스 그룹 및 이미지 생성
        CreateFadeImage();

        // 씬 전환 후에 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    private void CreateFadeImage()
    {
        GameObject canvasObject = new GameObject("FadeCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();

        // 캔버스 그룹 및 이미지 생성
        fadeCanvasGroup = canvasObject.AddComponent<CanvasGroup>();
        fadeCanvasGroup.alpha = 0;

        Image fadeImage = new GameObject("FadeImage").AddComponent<Image>();
        fadeImage.transform.SetParent(canvasObject.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector2.zero;
        fadeImage.rectTransform.sizeDelta = new Vector2(1920, 1080); // 적절한 해상도 설정

        fadeImage.color = Color.black; // 검은 색 이미지

        // Sorting Order 설정
        canvas.overrideSorting = true;
        canvas.sortingOrder = -1; // 초기에는 -1로 설정

        // 캔버스를 씬에 유지
        DontDestroyOnLoad(canvasObject);
    }

    public void SceneChange(string sceneName)
    {
        StartCoroutine(FadeOutAndIn(sceneName));
    }

    private IEnumerator FadeOutAndIn(string sceneName)
    {
        // Sorting Order를 10으로 변경
        fadeCanvasGroup.transform.GetComponent<Canvas>().sortingOrder = 10;

        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(sceneName);

        yield return StartCoroutine(FadeIn());

        // Sorting Order를 -1로 변경
        fadeCanvasGroup.transform.GetComponent<Canvas>().sortingOrder = -1;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        fadeCanvasGroup.alpha = 1; // 완전히 검은 화면으로 설정
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        fadeCanvasGroup.alpha = 0; // 완전히 투명화
    }
}
