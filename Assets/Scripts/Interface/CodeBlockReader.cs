using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlockReader : MonoBehaviour, IDropHandler
{
    private LayoutGroup layoutGroup;
    [SerializeField] private Transform codeSpaceObject;
    private ICodeSpace codeSpace;
    private void Awake()
    {
        layoutGroup = GetComponentInParent<LayoutGroup>();
        if(codeSpaceObject == null || !codeSpaceObject.TryGetComponent<ICodeSpace>(out codeSpace))
            codeSpace = GetComponentInParent<ICodeSpace>();
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        var codeBlock = eventData.pointerDrag.GetComponent<CodeBlockGenerator>();
        if(codeBlock == null) return;
        var codeBlockDrawer = Instantiate(codeBlock.CodeBlockPrefab, layoutGroup.transform);
        codeBlockDrawer.transform.SetAsLastSibling();
        codeSpace.AddBlock(codeBlockDrawer);
        transform.SetAsLastSibling();
        codeBlockDrawer.Init(codeBlock);
    }
}
