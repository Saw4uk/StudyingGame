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
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform codeSpaceObject;
    private ICodeSpace codeSpace;
    private void Awake()
    {
        layoutGroup = GetComponentInParent<LayoutGroup>();
        if(codeSpaceObject == null || !codeSpaceObject.TryGetComponent<ICodeSpace>(out codeSpace))
            codeSpace = GetComponentInParent<ICodeSpace>();
        
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        var generator = eventData.pointerDrag.GetComponent<CodeBlockGenerator>();
        if(generator == null) return;
        var codeBlock = Instantiate(generator.CodeBlockPrefab, layoutGroup.transform);
        codeBlock.transform.SetAsLastSibling();
        codeSpace.AddBlock(codeBlock);
        transform.SetAsLastSibling();
        codeBlock.Init(generator);
        scrollRect.GetComponent<ScrollController>().onBlockAdd?.Invoke();
    }
}
