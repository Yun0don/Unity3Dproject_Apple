using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [Header("References")]
    public string mapName = "Square"; // Inspector에서 입력
    public GameObject cubePrefab;
    public GameObject startCubePrefab;

    [Header("Managers")]
    public NumberManager numberManager;

    private Vector3 startCubePosition;
    public bool StartPositionReady { get; private set; } = false;

    public Vector3 GetStartPosition() => startCubePosition;

    private void Start()
    {
        GenerateMap(mapName);
    }

    public void GenerateMap(string selectedMapName)
    {
        // 1. 데이터 로드
        MapDatabase db = Resources.Load<MapDatabase>("Maps/MapDatabase");
        if (db == null)
        {
            Debug.LogError("MapDatabase not found in Resources/Maps.");
            return;
        }

        MapDataSO mapData = db.GetMapByName(selectedMapName);
        if (mapData == null)
        {
            Debug.LogError($"Map '{selectedMapName}' not found in database.");
            return;
        }

        // 2. 위치 정보 수집
        List<Vector3> allPositions = mapData.GetCubePositions();
        if (allPositions.Count == 0)
        {
            Debug.LogWarning("No cube positions found in the selected map.");
            return;
        }

        // 3. 시작 위치 설정
        startCubePosition = allPositions[Random.Range(0, allPositions.Count)];
        List<Vector3> cubePositions = new List<Vector3>(allPositions);
        cubePositions.Remove(startCubePosition);

        // 4. 숫자 배정
        List<int> values = numberManager.GenerateBalancedNumbers(cubePositions.Count);

        // 5. 일반 큐브 생성
        for (int i = 0; i < cubePositions.Count; i++)
        {
            GameObject cube = Instantiate(cubePrefab, cubePositions[i], Quaternion.identity);
            CubeController controller = cube.GetComponent<CubeController>();
            if (controller != null)
                controller.Init(values[i]);
        }

        // 6. 시작 큐브 생성
        Instantiate(startCubePrefab, startCubePosition, Quaternion.identity);
        StartPositionReady = true;
    }
}
