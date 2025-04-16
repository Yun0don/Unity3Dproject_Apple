using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour
{
    [Header("References")]
    public GameObject cubePrefab; // 숫자가 표시되는 큐브 프리팹
    public GameObject startCubePrefab; // 플레이어가 시작하는 큐브 프리팹
    public DesignManager designManager; // 큐브 위치 데이터를 제공하는 매니저

    private Vector3 playerStartPos; // 플레이어 시작 위치로 사용할 큐브의 위치

    private const int MinValue = 1; // 생성할 숫자의 최소값
    private const int MaxValue = 9; // 생성할 숫자의 최대값

    private void Start()
    {
        List<Vector3> cubePositions = designManager.GetCubePositions(); // 전체 큐브 위치 목록 가져오기

        playerStartPos = cubePositions[Random.Range(0, cubePositions.Count)]; // 시작 위치 큐브 무작위 선택
        List<Vector3> spawnPositions = new List<Vector3>(cubePositions); // 위치 리스트 복사
        spawnPositions.Remove(playerStartPos); // 시작 위치 큐브 제외한 나머지 위치만 추출

        List<int> values = GenerateBalancedNumbers(spawnPositions.Count); // 조건에 맞는 숫자 리스트 생성
        SpawnCubes(spawnPositions, values); // 숫자 큐브 생성

        Instantiate(startCubePrefab, playerStartPos, Quaternion.identity); // 시작 큐브 따로 생성
    }

    private void SpawnCubes(List<Vector3> positions, List<int> values)
    {
        if (positions.Count != values.Count) // 위치 수와 값 수가 맞지 않으면 에러
        {
            Debug.LogError("Cube position/value count mismatch!");
            return;
        }

        for (int i = 0; i < positions.Count; i++) // 각 위치에 큐브 생성 후 값 할당
        {
            GameObject cube = Instantiate(cubePrefab, positions[i], Quaternion.identity); // 큐브 생성
            CubeController controller = cube.GetComponent<CubeController>(); // 컨트롤러 가져오기
            if (controller != null)
                controller.Init(values[i]); // 값 설정
        }
    }

    private List<int> GenerateBalancedNumbers(int total)
    {
        List<int> result = new List<int>(); // 결과 리스트
        int maxAttempts = 1000; // 최대 시도 횟수 제한

        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            result = GenerateNumberList(total);
            if (IsBalanced(result))
            {
                Shuffle(result);
                return result;
            }
        }
    
        Debug.LogWarning("GenerateBalancedNumbers reached max attempts.");
        return new List<int>();
    }

    private List<int> GenerateNumberList(int total)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < total; i++)
        {
            int num = Random.Range(MinValue, MaxValue + 1);
            list.Add(num);
        }
        return list;
    }

    private bool IsBalanced(List<int> list)
    {
        Dictionary<int, int> count = new Dictionary<int, int>();
        for (int i = MinValue; i <= MaxValue; i++)
        {
            count[i] = 0;
        }

        int totalSum = 0;
        foreach (int n in list)
        {
            totalSum += n;
            count[n]++;
        }

        bool validPairs =
            count[1] > count[9] &&
            count[2] > count[8] &&
            count[3] > count[7];

        return (totalSum % 10 == 0) && validPairs;
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++) // 피셔-예이츠 셔플 알고리즘
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public Vector3 GetStartCubePosition()
    {
        return playerStartPos; // 외부에서 시작 위치를 가져올 수 있도록 제공
    }
}
