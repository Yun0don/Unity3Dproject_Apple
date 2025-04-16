using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public NumberManager numberManager;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => numberManager.GetStartCubePosition() != Vector3.zero);

        Vector3 startPos = numberManager.GetStartCubePosition();
        Vector3 spawnPosition = startPos + Vector3.up * 5f;

        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }
}