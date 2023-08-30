using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum RobotAction
{
    Move,
    RotateRight,
    RotateLeft,
    Cycle
}

public class RobotManager : MonoBehaviour
{
    public static RobotManager Instance { get; private set; }
    
    [SerializeField] private RobotBlock robotBlockPrefab;
    [SerializeField] private RectTransform gameOverWindow;

    private List<Robot> allRobots;
    private List<RobotBlock> allRobotBlocks;
    private RobotBlock selectedRobotBlock;
    private bool gameActive;

    public Action<RobotBlock> OnSelectRobot;

    public Robot SelectedRobot => selectedRobotBlock.GetRobot;
    public bool GameActive => gameActive;

    private void Awake()
    {
        Instance = this;
        allRobots = FindObjectsOfType<Robot>().ToList();
        allRobotBlocks = new List<RobotBlock>();
        foreach (var robot in allRobots)
        {
            var robotBlock = Instantiate(robotBlockPrefab, transform);
            robotBlock.Init(robot);
            allRobotBlocks.Add(robotBlock);
        }

        SelectRobot(allRobotBlocks.FirstOrDefault());
        OnSelectRobot += SelectRobot;
    }

    private void Update()
    {
        var allRobotsFinishedWalking = allRobots.All(robot => robot.IsFinishedWalking);

        if (gameActive && (allRobotsFinishedWalking || CheckRobotCollision()) )
        {
            gameActive = false;
            foreach (var robot in allRobots)
                robot.StopGame();
            StartCoroutine(OnGameEnd());
        }
    }

    public void StartGame()
    {
        foreach (var robot in allRobots)
            robot.StartGame();
        gameActive = true;
    }
    
    public void ExecuteEvent(Robot robot, RobotAction action)
    {
        switch (action)
        {
            case RobotAction.Move:
                robot.Move();
                break;
            case RobotAction.RotateRight:
                robot.RotateRight();
                break;
            case RobotAction.RotateLeft:
                robot.RotateLeft();
                break;
            case RobotAction.Cycle:
                robot.RotateLeft();
                break;
        }
    }

    public void RestartGame()
    {
        gameOverWindow.gameObject.SetActive(false);
        foreach (var robot in allRobots)
            robot.RestoreDefaults();
    }


    private IEnumerator OnGameEnd()
    {
        yield return new WaitForSeconds(0.5f);
        var isWon = allRobots.All(robot => robot.OnRightFinishingCell);
        gameOverWindow.gameObject.SetActive(true);
        gameOverWindow.GetComponent<GameOverWindowDrawer>().ReDraw(isWon,isWon ? 100 : 0);
    }

    private void SelectRobot(RobotBlock robotBlockToSelect)
    {
        if (robotBlockToSelect == null) return;
        if (selectedRobotBlock == null)
        {
            selectedRobotBlock = robotBlockToSelect;
            selectedRobotBlock.GetComponent<Button>().interactable = false;
        }
        else
        {
            selectedRobotBlock.GetComponent<Button>().interactable = true;
            selectedRobotBlock = robotBlockToSelect;
            selectedRobotBlock.GetComponent<Button>().interactable = false;
        }
    }

    private bool CheckRobotCollision()
    {
        var robotPositions = allRobots.Select(robot => robot.PositionInGrid);
        return robotPositions.Count() != robotPositions.Distinct().Count();
    }
}
