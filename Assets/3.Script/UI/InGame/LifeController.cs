using UnityEngine;

public class LifeController : MonoBehaviour
{
    public Animator animator;
    private Vector3 respawnPosition;
    
    private int deathCount = 0;
    private void Start()
    {
        MapMaker mapMaker = FindObjectOfType<MapMaker>();
        if (mapMaker != null)
        {
            respawnPosition = MapMaker.startCubePosition + Vector3.up * 10f;
        }
        else
        {
            Debug.LogError("mapMaker not found in scene!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            deathCount++;

            if (deathCount >= 3)
            {
                animator.SetTrigger("DieTrigger");
                Debug.Log("💀 플레이어 사망 횟수 초과! Game Over 처리");
                // GameManager.Instance.GameOver(); 등
            }
            else
            {
                RespawnPlayer();
            }
        }
    }
    private void RespawnPlayer()
    {
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            transform.position = respawnPosition;
            cc.enabled = true;
        }
        else
        {
            transform.position = respawnPosition;
        }
        animator.SetTrigger("IntroTrigger");

        Debug.Log("[LifeController] 플레이어 리스폰 완료!");
    }
}