using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public MapMaker mapMaker; // MapMaker에서 시작 위치를 받을 거야

    
    private IEnumerator Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // MapMaker가 시작 위치를 설정할 때까지 기다림
        yield return new WaitUntil(() => mapMaker.StartPositionReady);

        // 준비된 시작 위치 받아오기
        Vector3 startPos = mapMaker.GetStartPosition();
        Vector3 spawnPosition = startPos + Vector3.up * 5f;

        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }
}