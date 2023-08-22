using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interface;
using UnityEngine;

public class CodeSpaceLogic : MonoBehaviour, ICodeSpace
{
    private List<CodeBlock> codeBlockDrawers;

    private void Awake()
    {
        codeBlockDrawers = new List<CodeBlock>();
    }

    public void AddBlock(CodeBlock block)
    {
        codeBlockDrawers.Add(block);
    }

    public void ReadAllBlocks()
    {
        codeBlockDrawers = codeBlockDrawers.Where(x => x != null).ToList();
        Robot.Instance.StartGame();
        foreach (var codeBlockDrawer in codeBlockDrawers)
        {
            codeBlockDrawer.ExecuteEvent();
        }
    }
}
