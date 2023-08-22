using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private Robot robot;
    void Start()
    {
        robot = Robot.Instance;
    }

    public void Move()
    {
        robot.Move();
    }
    
    public void RotateRight()
    {
        robot.RotateRight();
    }


    public void RotateLeft()
    {
        robot.RotateLeft();
    }
}
