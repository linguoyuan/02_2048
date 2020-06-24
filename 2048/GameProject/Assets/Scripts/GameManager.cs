using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    public UiManager _uiManager;
    private static GameCore core; //全局静态变量
    private void Awake()
    {
        _gameManager = this;
    }
    void Start()
    {
        core = new GameCore();
        core.GenerateNumber();
        core.GenerateNumber();
    }

    void Update()
    {
        //先模拟输入
        if (Input.GetKeyDown(KeyCode.A))
        {
            //左
            Debug.Log("左");
            keydown("a");
            Debug.Log("core.IsChange =" + core.IsChange);
            //如果地图改变
            if (core.IsChange)
            {
                core.GenerateNumber();
                //Console.Write(core.Map[r, c] + "\t");                
                if (core.IsOver())
                {
                    Debug.Log("游戏结束");
                }
                UpdateAllCards();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //左
            Debug.Log("上");
            keydown("w");
            Debug.Log("core.IsChange =" + core.IsChange);
            //如果地图改变
            if (core.IsChange)
            {
                core.GenerateNumber();
                //Console.Write(core.Map[r, c] + "\t");                
                if (core.IsOver())
                {
                    Debug.Log("游戏结束");
                }
                UpdateAllCards();
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //左
            Debug.Log("右");
            keydown("d");
            Debug.Log("core.IsChange =" + core.IsChange);
            //如果地图改变
            if (core.IsChange)
            {
                core.GenerateNumber();
                //Console.Write(core.Map[r, c] + "\t");                
                if (core.IsOver())
                {
                    Debug.Log("游戏结束");
                }
                UpdateAllCards();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //左
            Debug.Log("下 :" + core.IsChange);
            keydown("s");
            Debug.Log("core.IsChange =" + core.IsChange);
            //如果地图改变
            if (core.IsChange)
            {
                core.GenerateNumber();
                //Console.Write(core.Map[r, c] + "\t");               
                if (core.IsOver())
                {
                    Debug.Log("游戏结束");
                }
                UpdateAllCards();
            }
        }
        
    }

    private void UpdateAllCards()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                _uiManager.UpdateCard(i, j, core.Map[i, j]);
            }
        }
    }


    private static void keydown(string str)
    {
        switch (str)
        {
            case "w":
                core.Move(MoveDirection.Up); break;
            case "s":
                core.Move(MoveDirection.Down); break;
            case "a":
                core.Move(MoveDirection.Left); break;
            case "d":
                core.Move(MoveDirection.Right); break;
            //default:
            //    break;
        }
    }
}
