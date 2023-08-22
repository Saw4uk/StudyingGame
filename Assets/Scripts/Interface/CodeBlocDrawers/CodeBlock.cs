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
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text blockName;

    protected UnityEvent action;

    public virtual void Init(CodeBlockGenerator codeBlockGenerator)
    {
        image.sprite = codeBlockGenerator.Image;
        blockName.text = codeBlockGenerator.BlockName;
        action = codeBlockGenerator.OnExecuteAction;
    }

    public virtual void ExecuteEvent()
    {
        action.Invoke();
    }

    public void DestroyMe()
    {
       Destroy(gameObject); 
    }
}
