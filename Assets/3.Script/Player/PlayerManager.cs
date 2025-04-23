using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public MapMaker mapMaker;

    private IEnumerator Start()
    {
        // 1) Null 체크
        if (mapMaker == null) {
            Debug.LogError("❌ mapMaker 할당 안됨!");
            yield break;
        }

        // 2) 맵 생성 완료 대기
        yield return new WaitUntil(() => mapMaker.StartPositionReady);

        // 3) 시작 위치 꺼내오기
        // -> 인스턴스 게터 사용 버전
        // Vector3 startPos = mapMaker.GetStartPosition();
        // -> static 프로퍼티 사용 버전
        Vector3 startPos = MapMaker.StartCubePosition;

        // 4) 스폰 위치 보정
        Vector3 spawnPosition = startPos + Vector3.up * 5f;
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }
}