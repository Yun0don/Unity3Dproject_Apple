using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "GrappleGame/MapData", order = 1)]
public class MapDataSO : ScriptableObject
{
    public string mapName;

    [TextArea(15, 15)]
    public string[] layout;

    public float spacing = 7f; // 간격 설정

    public List<Vector3> GetCubePositions()
    {
        List<Vector3> positions = new List<Vector3>();

        for (int z = 0; z < layout.Length; z++)
        {
            string row = layout[z];
            for (int x = 0; x < row.Length; x++)
            {
                if (row[x] == '*')
                {
                    Vector3 pos = new Vector3(x * spacing, 0f, z * spacing);
                    positions.Add(pos);
                }
            }
        }

        return positions;
    }
}






