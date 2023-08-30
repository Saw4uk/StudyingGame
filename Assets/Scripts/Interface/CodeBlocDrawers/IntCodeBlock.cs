using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntCodeBlock : CodeBlock
{
    
    [SerializeField] private TMP_InputField inputField;
    protected int Amount => Mathf.Abs(int.Parse(inputField.text != "" ? inputField.text : "0"));

    public override void ExecuteEvent()
    {
        var amount = Amount;
        for (int i = 0; i < amount; i++)
        {
            RobotManager.Instance.ExecuteEvent(robot, action);
        }
    }
}
