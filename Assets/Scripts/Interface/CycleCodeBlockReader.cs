using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CycleCodeBlockReader : CodeBlockReader
{
    public override void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<CodeBlockGenerator>().CodeBlockPrefab is ICodeSpace) return;
        base.OnDrop(eventData);
    }
}
