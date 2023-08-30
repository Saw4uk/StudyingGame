using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    [SerializeField] private Cell startCell;
    [SerializeField] private Cell finishCell;
    [SerializeField] private Vector2Int startDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private string pseudocodeRobotName;
    [SerializeField] private string codeRobotName;
    [SerializeField] private Color robotNameColor;
    private GridMap gridMapReference;

    [Header("DEBUG")] [SerializeField, ReadOnlyInspector]
    private Vector2Int direction;

    [SerializeField, ReadOnlyInspector] private Vector2Int positionInGrid;

    private Queue<Action> actions;
    private bool isReady;
    private bool gameEnd;
    private string robotName;
    private Sprite robotImage;
    
    public Vector2Int PositionInGrid => positionInGrid;
    public string RobotName => robotName;
    public Sprite RobotImage => robotImage;
    public Color RobotNameColor => robotNameColor;
    public bool IsFinishedWalking => isReady && actions.Count == 0;

    public bool OnRightFinishingCell =>
        GridMap.Instance.IsPositionWin(positionInGrid) && finishCell.positionInGrid == positionInGrid;

    private void Awake()
    {
        CodeDisplayType.Instance.OnChangeCodeType += OnChangingCodeType;
        robotImage = GetComponentsInChildren<Image>().First(child => child.gameObject.name == "Body").sprite;
    }

    private void Start()
    {
        StartCoroutine(InitializeCoroutine());
    }

    private void Update()
    {
        if (isReady && actions.Count > 0 && !gameEnd)
            actions.Dequeue().Invoke();
    }

    public void Move()
    {
        actions.Enqueue(_Move);
    }

    public void RotateRight()
    {
        actions.Enqueue(() => _Rotate(false));
    }


    public void RotateLeft()
    {
        actions.Enqueue(() => _Rotate(true));
    }

    public void StartGame()
    {
        isReady = true;
        gameEnd = false;
    }
    
    public void StopGame()
    {
        gameEnd = true;
    }

    private void _Move()
    {
        if (gridMapReference.CheckPosition(positionInGrid + direction))
        {
            positionInGrid += direction;
            StartCoroutine(MoveCoroutineTo(gridMapReference.GetWorldPosition(positionInGrid)));
        }
    }

    private void _Rotate(bool isLeft)
    {
        var angle = Mathf.Atan2(direction.y, direction.x);
        angle += isLeft ? Mathf.PI / 2 : -Mathf.PI / 2;
        direction = new Vector2Int((int)Mathf.Cos(angle), (int)Mathf.Sin(angle));
        StartCoroutine(RotateCoroutine(isLeft));
    }

    private IEnumerator MoveCoroutineTo(Vector2 worldPos)
    {
        isReady = false;
        var progress = 0f;
        Vector2 startPosition = transform.position;
        while (progress < 1f)
        {
            transform.position = Vector2.Lerp(startPosition, worldPos, progress);
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

    private IEnumerator InitializeCoroutine()
    {
        yield return null;
        gridMapReference = GridMap.Instance;
        actions = new Queue<Action>();

        RestoreDefaults();
    }

    public void RestoreDefaults()
    {
        direction = startDirection;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI);

        positionInGrid = startCell != null ? startCell.positionInGrid : Vector2Int.zero;
        transform.position = gridMapReference.GetWorldPosition(positionInGrid);
    }
    
    private void OnChangingCodeType(CodeType codeType)
    {
        switch (codeType)
        {
            case CodeType.Code:
                robotName = codeRobotName;
                break;
            case CodeType.Pseudocode:
                robotName = pseudocodeRobotName;
                break;
        }
    }
}