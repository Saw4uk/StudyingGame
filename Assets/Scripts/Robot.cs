using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public static Robot Instance { get; private set; }
    [SerializeField] private Cell startCell;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private GridMap gridMapReference;
    
    [Header("DEBUG")]
    [SerializeField, ReadOnlyInspector] private Vector2Int direction;
    [SerializeField, ReadOnlyInspector] private Vector2Int positionInGrid;
    
    private Vector2Int up = Vector2Int.up;
    private Vector2Int right = Vector2Int.right;
    private Vector2Int down = Vector2Int.down;
    private Vector2Int left = Vector2Int.left;
    
    private Queue<Action> actions;

    private bool isReady;

    private void Awake()
    {
        Instance = this;
        isReady = true;
    }

    private void Start()
    {
        gridMapReference = GridMap.Instance;
        actions = new Queue<Action>();
        
        direction = new Vector2Int(0, 1);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI);
        
        positionInGrid = startCell ? startCell.positionInGrid : Vector2Int.zero;
        transform.position = gridMapReference.GetWorldPosition(positionInGrid);
    }

    private void Update()
    {
        if (isReady && actions.Count > 0)
            actions.Dequeue().Invoke();
    }

    public void Move()
    {
        actions.Enqueue(_Move);
    }

    private void _Move()
    {
        if (gridMapReference.CheckPosition(positionInGrid + direction))
        {
            positionInGrid += direction;
            StartCoroutine(MoveCoroutineTo(gridMapReference.GetWorldPosition(positionInGrid)));
        }
    }

    public void RotateRight()
    {
        actions.Enqueue(() => _Rotate(false));
    }


    public void RotateLeft()
    {
        actions.Enqueue(() => _Rotate(true));
    }

    private void _Rotate(bool isLeft)
    {
        // var angle = Mathf.Atan2(direction.y, direction.x);
        // angle += isLeft ? Mathf.PI / 2 : -Mathf.PI / 2;
        // angle %= Mathf.PI * 2;
        //
        // if (Mathf.Abs(angle) < 0.000001f)
        //     direction = new Vector2Int(1, 0);
        // else if (Mathf.Abs(angle - Mathf.PI/2) < 0.000001f)
        //     direction = new Vector2Int(0, 1);
        // else if (Mathf.Abs(angle + Mathf.PI/2) < 0.000001f)
        //     direction = new Vector2Int(0, -1);
        // else
        //     direction = new Vector2Int(-1, 0);

        if (direction == up && isLeft)
            direction = left;
        else if (direction == up && !isLeft)
            direction = right;
        else if (direction == right && isLeft)
            direction = up;
        else if (direction == right && !isLeft)
            direction = down;
        else if (direction == down && isLeft)
            direction = right;
        else if (direction == down && !isLeft)
            direction = left;
        else if (direction == left && isLeft)
            direction = down;
        else 
            direction = up;
        

        StartCoroutine(RotateCoroutine(isLeft));
    }

    private IEnumerator MoveCoroutineTo(Vector2 worldPos)
    {
        isReady = false;
        var progress = 0f;
        Vector2 startPosition = transform.position;
        while (progress < 1f)
        {
            transform.position = Vector3.Slerp(startPosition, worldPos, progress);
            progress += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = worldPos;
        isReady = true;
    }

    private IEnumerator RotateCoroutine(bool isLeft)
    {
        isReady = false;
        var progress = 0f;
        while (progress < 1f)
        {
            var delta = Time.deltaTime * rotationSpeed;
            transform.Rotate(Vector3.forward, delta * 90f * (isLeft ? 1 : -1));
            progress += delta;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI);
        
        isReady = true;
    }
}