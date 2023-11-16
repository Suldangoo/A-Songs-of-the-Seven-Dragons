using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // --- 싱글톤 패턴
    public static GameManager Instance{
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    private static GameManager instance;

    // --- 외부 오브젝트
    SceneChanger sceneChanger => SceneChanger.Instance; // 씬 체인저

    private void Awake()
    {
        // 씬 전환 후에 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    // 타이틀 화면 터치 시 실행될 메서드
    public void OnTouchTitle()
    {
        // 만약 진행상황이 있다면 'Game' 씬으로 이동
        if (SaveManager.Progress)
        {
            sceneChanger.SceneChange("Game");
        }
        // 진행상황이 없다면 'CreateCharacter' 씬으로 이동
        else
        {
            sceneChanger.SceneChange("CreateCharacter");
        }
    }

    // 타이틀로
    public void ExitTitle()
    {
        sceneChanger.SceneChange("Title");
    }

    // 게임 종료
    public void ExitGame()
    {
        Application.Quit();
    }
}