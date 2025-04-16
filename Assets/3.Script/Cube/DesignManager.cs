using System.Collections.Generic;
using UnityEngine;

public class DesignManager : MonoBehaviour // 큐브 배치 관리
{
    [Header("Design Settings")]
    public int width = 3;
    public int height = 3;
    public float spacing = 7f;

    /// <summary>
    /// 큐브를 배치할 위치 목록을 반환
    /// </summary>
    public List<Vector3> GetCubePositions()
    {
        List<Vector3> positions = new List<Vector3>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new Vector3(x * spacing, 0f, y * spacing);
                positions.Add(pos);
            }
        }

        return positions;
    }
}