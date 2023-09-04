using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlock : MonoBehaviour
{
    [SerializeField] private Image actionImage;
    [SerializeField] private Image robotImage;
    [SerializeField] private TMP_Text blockName;
    
    protected RobotAction action;
    protected Robot robot;
    
    private Dictionary<CodeType, string> actionNames;

    public Action<CodeBlock> WhenRemovedFromCycle;

    public virtual void Init(CodeBlockGenerator codeBlockGenerator)
    {
        robot = RobotManager.Instance.SelectedRobot;
        action = codeBlockGenerator.OnExecuteAction;
        actionNames = codeBlockGenerator.ActionNames;
        actionImage.sprite = codeBlockGenerator.Image;
        robotImage.sprite = robot.RobotImage;
        OnChangingCodeType(CodeDisplayType.Instance.GetCurrentCodeType);
        CodeDisplayType.Instance.OnChangeCodeType += OnChangingCodeType;
    }

    public virtual void ExecuteEvent()
    {
        RobotManager.Instance.ExecuteEvent(robot, action);
    }

    public void DestroyMe()
    {
        CodeDisplayType.Instance.OnChangeCodeType -= OnChangingCodeType;
        WhenRemovedFromCycle?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnChangingCodeType(CodeType codeType)
    {
        switch (codeType)
        {
            case CodeType.Code:
                actionImage.gameObject.SetActive(false);
                robotImage.gameObject.SetActive(false);
                blockName.text =
                    $"<color=#{ColorUtility.ToHtmlStringRGB(robot.RobotNameColor)}>{robot.RobotName}</color>.{actionNames[CodeType.Code]}";
                blockName.fontSize = 30;
                break;
            case CodeType.Pseudocode:
                actionImage.gameObject.SetActive(false);
                robotImage.gameObject.SetActive(false);
                blockName.text =
                    $"<color=#{ColorUtility.ToHtmlStringRGB(robot.RobotNameColor)}>{robot.RobotName}</color> {actionNames[CodeType.Pseudocode]}";
                blockName.fontSize = 24;
                break;
            case CodeType.NotACode:
                actionImage.gameObject.SetActive(true);
                robotImage.gameObject.SetActive(true);
                blockName.text = "";
                break;
        }
    }
}