using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour
{
    public Vector2Int positionInGrid;
    [SerializeField] private bool isLocked;
    [SerializeField, HideInInspector] private Image image;

    public bool IsLocked
    {
        get => isLocked;
        set
        {
            isLocked = value;
            Redraw();
        }
    }

    private void OnValidate()
    {
        AssignReference();
        Redraw();
    }


    private void AssignReference()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
    private void Redraw()
    {
        image.color = isLocked ? Color.black : Color.white;
    }
}