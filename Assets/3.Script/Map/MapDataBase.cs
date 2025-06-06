using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDatabase", menuName = "GrappleGame/MapDatabase")]
public class MapDatabase : ScriptableObject
{
    [Tooltip("등록된 모든 맵 데이터(ScriptableObject)를 여기에 추가하세요.")]
    public List<MapDataSO> maps;

    public MapDataSO GetMapByName(string mapName)
    {
        return maps.Find(map => map.mapName == mapName);
    }
    public List<string> GetMapNames()
    {
        List<string> names = new List<string>();
        foreach (var map in maps)
        {
            if (map != null)
                names.Add(map.mapName);
        }
        return names;
    }
}