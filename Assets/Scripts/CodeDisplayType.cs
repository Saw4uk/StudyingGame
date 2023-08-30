using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CodeType
{
    NotACode,
    Pseudocode,
    Code
}

public class CodeDisplayType : MonoBehaviour
{
    public static CodeDisplayType Instance { get; private set; }

    [SerializeField] private Button openCloseButton;
    [SerializeField] private GameObject buttonContainer;

    [SerializeField] private Button notACodeButton;
    [SerializeField] private Button pseudocodeButton;
    [SerializeField] private Button codeButton;

    private Button selectedButton;
    private CodeType currentCodeType;
    private bool isOpened;
    private bool isReady;
    private float buttonContainerOffset;

    public Action<CodeType> OnChangeCodeType;

    public CodeType GetCurrentCodeType => currentCodeType;

    private void Awake()
    {
        Instance = this;
        notACodeButton.onClick.AddListener(() => ChangeCodeType(notACodeButton, CodeType.NotACode));
        pseudocodeButton.onClick.AddListener(() => ChangeCodeType(pseudocodeButton, CodeType.Pseudocode));
        codeButton.onClick.AddListener(() => ChangeCodeType(codeButton, CodeType.Code));

        openCloseButton.onClick.AddListener(ChangeWindowState);
        isOpened = false;
        isReady = true;
        var gridLayoutGroup = buttonContainer.GetComponent<GridLayoutGroup>();
        buttonContainerOffset = (gridLayoutGroup.cellSize.x * 3) + (gridLayoutGroup.spacing.x * 3);
    }

    private void Start()
    {
        ChangeCodeType(codeButton, CodeType.NotACode);
    }

    private void ChangeCodeType(Button button, CodeType codeType)
    {
        if (selectedButton != null)
            selectedButton.interactable = true;

        currentCodeType = codeType;
        selectedButton = button;
        selectedButton.interactable = false;

        OnChangeCodeType.Invoke(currentCodeType);
        
        if (isOpened)
            ChangeWindowState();
    }

    private void ChangeWindowState()
    {
        if (!isReady) return;
        var buttonContainerPosition = buttonContainer.transform.localPosition.x;
        var speed = 3f;

        if (isOpened)
            StartCoroutine(RepositionButtonContainer(buttonContainerPosition,
                buttonContainerPosition - buttonContainerOffset, speed));
        else
            StartCoroutine(RepositionButtonContainer(buttonContainerPosition,
                buttonContainerPosition + buttonContainerOffset, speed));

        var buttonTransform = openCloseButton.transform;
        buttonTransform.localScale = buttonTransform.GetChild(0).transform.localScale =
            isOpened ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

        isOpened = !isOpened;
    }

    private IEnumerator RepositionButtonContainer(float startPosition, float endPosition, float speed)
    {
        isReady = false;
        var progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * speed;
            var x = Mathf.Lerp(startPosition, endPosition, progress);
            buttonContainer.transform.localPosition = new Vector3(x, buttonContainer.transform.localPosition.y);
            yield return null;
        }

        buttonContainer.transform.localPosition = new Vector3(endPosition, buttonContainer.transform.localPosition.y);
        isReady = true;
    }
}