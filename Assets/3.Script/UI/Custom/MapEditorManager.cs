using System.Collections.Generic;
using UnityEngine;

public class MapEditorManager : MonoBehaviour
{
    public List<ButtonTile> allTiles = new List<ButtonTile>();

    public void ResetAllTiles()
    {
        foreach (var tile in allTiles)
        {
            tile.ResetTile();
        }
    }

    public void RegisterTile(ButtonTile tile)
    {
        if (!allTiles.Contains(tile))
        {
            allTiles.Add(tile);
        }
    }

    public void SaveLayout()
    {
        Debug.Log("==== Saved Layout ====");

        for (int layerIndex = 1; layerIndex <= 10; layerIndex++)
        {
            string layerName = "Layer" + layerIndex;
            Transform layer = GameObject.Find(layerName)?.transform;

            if (layer == null)
            {
                Debug.LogWarning($"Layer {layerName} not found");
                continue;
            }

            string line = "";

            for (int i = 0; i < layer.childCount; i++)
            {
                ButtonTile tile = layer.GetChild(i).GetComponent<ButtonTile>();
                if (tile != null)
                {
                    line += tile.IsFilled ? "*" : "@";
                }
                else
                {
                    line += "@";
                }
            }

            Debug.Log($"{layer.name}: {line}");
        }
    }
}