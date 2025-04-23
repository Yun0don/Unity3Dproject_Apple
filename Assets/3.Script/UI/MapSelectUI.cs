using UnityEngine;
using UnityEngine.UI;

public class MapSelectUI : MonoBehaviour
{
    public MapMaker mapMaker;

    public void OnClickMapButton(string mapName)
    {
        if (mapMaker != null)
        {
            mapMaker.GenerateMap(mapName);
        }
    }
    
}