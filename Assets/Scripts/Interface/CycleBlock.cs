using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using TMPro;
using UnityEngine;

public class CycleBlock : IntCodeBlock, ICodeSpace
{
   private List<CodeBlock> codeBlockDrawers;
   private RectTransform rectTransform;
   private void Awake()
   {
      codeBlockDrawers = new List<CodeBlock>();
      rectTransform = GetComponent<RectTransform>();
   }

   public override void ExecuteEvent()
   {
      var amount = Amount;
      for (int i = 0; i < amount; i++)
      {
         foreach (var codeBlockDrawer in codeBlockDrawers)
         {
            codeBlockDrawer.ExecuteEvent();
         }
      }
   }

   public void AddBlock(CodeBlock block)
   {
      codeBlockDrawers.Add(block);
      rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + 40);
   }
}
