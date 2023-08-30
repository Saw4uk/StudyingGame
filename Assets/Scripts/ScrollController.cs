using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Interface
{
    public class ScrollController : MonoBehaviour
    {
        public UnityEvent onBlockAdd;
        
        private ScrollRect scrollRect;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            onBlockAdd.AddListener(ForceScrollDown);
        }

        private void ForceScrollDown()
        {
            StartCoroutine(_ForceScrollDown());
        }

        private IEnumerator _ForceScrollDown () {
            yield return new WaitForEndOfFrame ();
            Canvas.ForceUpdateCanvases ();
            scrollRect.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases ();
        }
    }
}
