using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RobotBlock : MonoBehaviour
{
    private Robot robot;
    private TextMeshProUGUI robotName;
    private Image robotImage;

    public Robot GetRobot => robot;

    private void Awake()
    {
        robotImage = GetComponentsInChildren<Image>().First(child => child.name == "RobotImage");
        robotName = GetComponentsInChildren<TextMeshProUGUI>().First(child => child.name == "RobotName");
    }

    public void Init(Robot robot)
    {
        this.robot = robot;
        robotName.color = robot.RobotNameColor;
        robotImage.sprite = robot.RobotImage;
        GetComponent<Button>().onClick.AddListener(SelectRobot);
        CodeDisplayType.Instance.OnChangeCodeType += OnChangingCodeType;
    }

    private void SelectRobot()
    {
        RobotManager.Instance.OnSelectRobot.Invoke(this);
    }
    
    private void OnChangingCodeType(CodeType codeType)
    {
        switch (codeType)
        {
            case CodeType.Code:
                robotImage.gameObject.SetActive(false);
                robotName.gameObject.SetActive(true);
                robotName.fontSize = 30;
                break;
            case CodeType.Pseudocode:
                robotImage.gameObject.SetActive(false);
                robotName.gameObject.SetActive(true);
                robotName.fontSize = 24;
                break;
            case CodeType.NotACode:
                robotImage.gameObject.SetActive(true);
                robotName.gameObject.SetActive(false);
                break;
        }
        
        robotName.text = robot.RobotName;
    }
}
