using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour
{
    public Vector2Int positionInGrid;
    [SerializeField] private CellType cellType;
    [SerializeField, HideInInspector] private Image image;

    public bool IsLocked => cellType == CellType.Obstacle;
    public bool IsWin => cellType == CellType.Finish;


    private void OnValidate()
    {
        AssignReference();
    }


    private void AssignReference()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}

public enum CellType
{
    Normal,
    Obstacle,
    Finish
}