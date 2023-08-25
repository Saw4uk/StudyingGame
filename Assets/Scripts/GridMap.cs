using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GridMap : MonoBehaviour
{
    public static GridMap Instance { get; private set; }

    private GridLayoutGroup gridLayout;
    private Cell[,] map;
    public int Width => map.GetLength(0);
    public int Height => map.GetLength(1);

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        map = new Cell[10, 11];
        var x = 0;
        var y = 0;
        foreach (var cell in gridLayout.transform.OfType<Transform>())
        {
            map[x, y] = cell.GetComponent<Cell>();
            map[x, y].positionInGrid = new Vector2Int(x, y);
            x++;
            if (x >= 10)
            {
                x = 0;
                y++;
            }
        }
    }

    public bool CheckPosition(Vector2Int p) => ObstacleCheck(p);
    private bool BoundaryCheck(Vector2Int p) => p.x >= 0 && p.x < Width && p.y >= 0 && p.y < Height;
    private bool ObstacleCheck(Vector2Int p) => BoundaryCheck(p) && !map[p.x, p.y].IsLocked;

    public bool IsPositionWin(Vector2Int p)
    {
        return BoundaryCheck(p) && map[p.x, p.y].IsWin;
    }

    public Vector2 GetWorldPosition(Vector2Int p)
    {
        p = CutPosition(p);
        return map[p.x, p.y].transform.position;
    }

    public Vector2Int CutPosition(Vector2Int p) => 
        new Vector2Int(Mathf.Clamp(p.x, 0, Width-1), Mathf.Clamp(p.y, 0, Height - 1));
}