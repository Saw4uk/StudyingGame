using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private Levels levelName;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ChangeScene);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene((int) levelName);
    }
}
