using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour // 큐브 파괴 전담 관리자
{
    public static DestroyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// 큐브 파괴 실행 (리스트 내 오브젝트 제거)
    public void DestroyCubes(List<CubeController> cubes)
    {
        if (cubes == null || cubes.Count == 0) return;

        // 점수 계산 로직 추가
        int scoreToAdd = cubes.Count * 100;
        UIManager.Instance?.AddScore(scoreToAdd);

        foreach (var cube in cubes)
        {
            if (cube != null)
                Destroy(cube.gameObject);
        }

        SumCheckManager.Instance.ClearAfterDestruction();
    }

}