// MapMaker.cs
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [Header("Default Map (fallback)")]
    [SerializeField] private string defaultMapName = "Square";

    [Header("Prefabs")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject startCubePrefab;

    [SerializeField] private NumberManager numberManager; 

    // 버튼 클릭으로 전달된 맵 이름을 여기에 보관
    private static string selectedMapName;

    // 외부(버튼)에서 호출해서 static 변수에 셋팅
    public static void SetSelectedMap(string mapName)
    {
        selectedMapName = mapName;
    }

    public static Vector3 StartCubePosition { get; private set; }
    public bool StartPositionReady { get; private set; }

    private void Awake()
    {
      //  numberManager = GetComponent<NumberManager>();
    }

    private void Start()
    {
        // 버튼에서 넘어온 이름이 있으면 그걸, 없으면 default
        string mapToLoad = !string.IsNullOrEmpty(selectedMapName)
            ? selectedMapName
            : defaultMapName;

        GenerateMap(mapToLoad);
    }

    private void GenerateMap(string mapName)
    {
        // (1) DB 로드
        var db = Resources.Load<MapDatabase>("Maps/MapDatabase");
        if (db == null)
        {
            Debug.LogError("MapDatabase not found in Resources/Maps.");
            return;
        }

        var mapData = db.GetMapByName(mapName);
        if (mapData == null)
        {
            Debug.LogError($"Map '{mapName}' not found in database.");
            return;
        }

        // (2) 포지션 리스트
        var allPositions = mapData.GetCubePositions();
        if (allPositions.Count == 0)
        {
            Debug.LogWarning("No cube positions found in the selected map.");
            return;
        }

        // (3) 시작 큐브 위치 랜덤 선택
        StartCubePosition = allPositions[Random.Range(0, allPositions.Count)];
        var cubePositions = new List<Vector3>(allPositions);
        cubePositions.Remove(StartCubePosition);

        // (4) 숫자 생성
        var values = numberManager.GenerateBalancedNumbers(cubePositions.Count);

        // (5) 일반 큐브
        for (int i = 0; i < cubePositions.Count; i++)
        {
            var cube = Instantiate(cubePrefab, cubePositions[i], Quaternion.identity);
            if (cube.TryGetComponent<CubeController>(out var ctrl))
                ctrl.Init(values[i]);
        }

        // (6) 시작 큐브
        Instantiate(startCubePrefab, StartCubePosition, Quaternion.identity);
        StartPositionReady = true;
    }
}
