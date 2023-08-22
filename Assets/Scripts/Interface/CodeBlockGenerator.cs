using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CodeBlockGenerator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform objectToGenerateCopy;
    
    [SerializeField] private Sprite image;
    [SerializeField] private string blockName;
    [SerializeField] private UnityEvent onExecuteAction;
    [SerializeField] private CodeBlock codeBlockPrefab;
    
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas mainCanvas;

    public Sprite Image => image;

    public string BlockName => blockName;

    public CodeBlock CodeBlockPrefab => codeBlockPrefab;

    public UnityEvent OnExecuteAction => onExecuteAction;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        mainCanvas = GetComponentInParent<Canvas>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Instantiate(this, objectToGenerateCopy.transform);
        transform.SetParent(mainCanvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        Destroy(gameObject);
    }
}
