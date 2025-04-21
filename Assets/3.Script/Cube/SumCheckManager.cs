using System.Collections.Generic;
using UnityEngine;

public class SumCheckManager : MonoBehaviour // 큐브 숫자합 및 조건 판단 전담
{
    public static SumCheckManager Instance { get; private set; }

    private readonly List<CubeController> selectedCubes = new List<CubeController>();
    private List<CubeController> toDestroyCubes = new List<CubeController>();
    public bool isReadyToDestroy { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// 큐브 선택 또는 해제될 때 호출
    public void ToggleSelection(CubeController cube)
    {
        if (cube == null) return;

        if (cube.isSelected)
        {
            if (!selectedCubes.Contains(cube))
            {
                selectedCubes.Add(cube);
                Debug.Log($"[선택됨] {cube.name}, 값: {cube.value}");
            }
        }
        else
        {
            selectedCubes.Remove(cube);
            Debug.Log($"[해제됨] {cube.name}");
        }

        CheckSumOnly(); // 파괴 조건만 판단
    }


    /// 파괴 조건만 판단 (합이 정확히 10일 때만 준비 상태로 변경)
    private void CheckSumOnly()
    {
        int total = 0;
        foreach (var cube in selectedCubes)
            total += cube.value;

        if (total == 10)
        {
            toDestroyCubes = new List<CubeController>(selectedCubes);
            isReadyToDestroy = true;
        }
        else if (total > 10)
        {
            foreach (var cube in selectedCubes)
            {
                cube.isSelected = false;
                cube.ResetColor();
            }
            selectedCubes.Clear();
            isReadyToDestroy = false;
        }
        else
        {
            isReadyToDestroy = false;
        }
    }

    /// 외부에서 호출하여 파괴 실행
    public List<CubeController> GetCubesToDestroy()
    {
        return new List<CubeController>(toDestroyCubes);
    }

    /// 파괴 완료 후 상태 초기화
    public void ClearAfterDestruction()
    {
        toDestroyCubes.Clear();
        selectedCubes.Clear();
        isReadyToDestroy = false;
    }
}