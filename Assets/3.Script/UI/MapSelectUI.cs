// MapSelectUI.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectUI : MonoBehaviour
{
    [Tooltip("맵 생성이 이루어질 씬 이름")]
    [SerializeField] private string gameSceneName = "IngameScene";

    // UI 버튼의 OnClick() 에 문자열 파라미터로 mapName 을 넣어 호출
    public void OnClickMapButton(string mapName)
    {
        // 1) 선택된 맵 이름 전달
        MapMaker.SetSelectedMap(mapName);

        // 2) 다음 씬(맵 생성 씬) 로드
        SceneManager.LoadScene(gameSceneName);
    }
}