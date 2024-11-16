using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;

[ExecuteInEditMode]
public class MapGrid : MonoBehaviour
{
    public Vector2 gridSize;
    public float camHeight;
    float camWidth;
    Vector3 originPos;

    int gridX, gridY;

    private void Awake()
    {
        gridX = Mathf.FloorToInt(gridSize.x);
        gridY = Mathf.FloorToInt(gridSize.y);
        camWidth = camHeight * 2560 / 1440;
        originPos = new Vector3(gridX / 2 * camWidth - camWidth / 2, -gridY / 2 * camHeight - camHeight / 2, 0);
    }

    private void Update()
    {
        ShowDebugLine();
    }

    void ShowDebugLine()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, gridY), GetWorldPosition(gridX, gridY), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(gridX, 0), GetWorldPosition(gridX, gridY), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(-x * camWidth, y * camHeight)   + originPos;
    }

}
