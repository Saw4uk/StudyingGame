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
    public bool IsBusy { get; private set; }

    public void ChangeState(bool state) => IsBusy = state;

    private void OnValidate()
    {
        AssignReference();
    }


    private void AssignReference()
    {
        if (image == null)
            image = GetComponent<Image>();
    }

    private void Awake()
    {
        IsBusy = false;
    }
}

public enum CellType
{
    Normal,
    Obstacle,
    Finish
}