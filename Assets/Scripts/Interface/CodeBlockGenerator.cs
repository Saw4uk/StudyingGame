using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CodeBlockGenerator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform objectToGenerateCopy;

    [SerializeField] private Sprite image;
    [SerializeField] private RobotAction onExecuteAction;
    [SerializeField] private CodeBlock codeBlockPrefab;
    [SerializeField] private string pseudocodeActionName;
    [SerializeField] private string codeActionName;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas mainCanvas;
    private TextMeshProUGUI actionNameText;
    private Image actionImage;
    private Dictionary<CodeType, string> actionNames;

    public Sprite Image => image;
    public CodeBlock CodeBlockPrefab => codeBlockPrefab;
    public RobotAction OnExecuteAction => onExecuteAction;
    public Dictionary<CodeType, string> ActionNames => actionNames;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        mainCanvas = GetComponentInParent<Canvas>();
        actionNameText = GetComponentsInChildren<TextMeshProUGUI>(true).First(child => child.name == "ActionName");
        actionImage = GetComponentsInChildren<Image>(true).First(child => child.name == "ActionImage");
        actionImage.sprite = image;
        actionNames = new Dictionary<CodeType, string>
        {
            { CodeType.Code, codeActionName }, 
            { CodeType.Pseudocode, pseudocodeActionName }
        };
        OnChangingCodeType(CodeDisplayType.Instance.GetCurrentCodeType);
        CodeDisplayType.Instance.OnChangeCodeType += OnChangingCodeType;
    }

    private void OnDestroy()
    {
        CodeDisplayType.Instance.OnChangeCodeType -= OnChangingCodeType;
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

    private void OnChangingCodeType(CodeType codeType)
    {
        switch (codeType)
        {
            case CodeType.Code:
                actionImage.gameObject.SetActive(false);
                actionNameText.gameObject.SetActive(true);
                actionNameText.fontSize = 30;
                actionNameText.text = codeActionName;
                break;
            case CodeType.Pseudocode:
                actionImage.gameObject.SetActive(false);
                actionNameText.gameObject.SetActive(true);
                actionNameText.fontSize = 22;
                actionNameText.text = pseudocodeActionName;
                break;
            case CodeType.NotACode:
                actionImage.gameObject.SetActive(true);
                actionNameText.gameObject.SetActive(false);
                break;
        }
    }
}