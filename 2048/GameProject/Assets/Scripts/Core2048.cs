using System;
using System.Collections.Generic;
using UnityEngine;

///游戏类核心算法，与平台无关
class GameCore
{
    private int[,] map;    //地图的字段
    private int[] mergeArray;       //合并数组
    private int[] removeZeroArray;  //用来进行移动的辅助数组
    private int[,] originalMap;     //原地图，用于比较移动后地图是否变化
    public bool IsChange { get; set; }   // 用于判断地图是否发生改变
    private List<Location> emptyLOC;//用于统计空位置的List（长度可变数组）
    private System.Random random;          //用于生成随机位置和随机数
    private Dictionary<int, string> test;

    public int[,] Map      //地图的属性
    {
        get { return map; }
    }

    public GameCore()      //构造函数
    {
        map = new int[4, 4];
        mergeArray = new int[map.GetLength(0)];
        removeZeroArray = new int[4];
        emptyLOC = new List<Location>(16);
        random = new System.Random();
        originalMap = new int[4, 4];
    }

    private void RemovwZero()  //把零调整到后边，非零数按顺序调整到左边
    {
        Array.Clear(removeZeroArray, 0, 4);

        int index = 0;
        for (int i = 0; i < mergeArray.Length; i++)
        {
            if (mergeArray[i] != 0)
                removeZeroArray[index++] = mergeArray[i];
        }
        removeZeroArray.CopyTo(mergeArray, 0);
    }

    private void Merge()  //合并，即相同的数字相加
    {
        RemovwZero();
        for (int i = 0; i < mergeArray.Length - 1; i++)
        {
            if (mergeArray[i] != 0 && mergeArray[i + 1] == mergeArray[i])
            {
                mergeArray[i] += mergeArray[i + 1];
                mergeArray[i + 1] = 0;
                //加分
            }
        }
        RemovwZero();//把0全部移到右边
    }

    //上移，下移，左移，右移
    private void MoveUp()
    {
        for (int c = 0; c < map.GetLength(1); c++)
        {
            for (int r = 0; r < map.GetLength(0); r++)
                mergeArray[r] = map[r, c];

            Merge();
            for (int r = 0; r < map.GetLength(0); r++)
                map[r, c] = mergeArray[r];
        }
    }

    private void MoveDown()
    {
        for (int c = 0; c < map.GetLength(1); c++)
        {
            for (int r = map.GetLength(0) - 1; r >= 0; r--)
                mergeArray[3 - r] = map[r, c];

            Merge();
            for (int r = map.GetLength(0) - 1; r >= 0; r--)
                map[r, c] = mergeArray[3 - r];
        }
    }

    private void MoveLeft()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
                mergeArray[c] = map[r, c];

            Merge();
            for (int c = 0; c < 4; c++)
                map[r, c] = mergeArray[c];
        }
    }

    private void MoveRight()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 3; c >= 0; c--)
                mergeArray[3 - c] = map[r, c];
            Merge();
            for (int c = 3; c >= 0; c--)
            {
                map[r, c] = mergeArray[3 - c];
            }
        }
    }

    //将移动的调用简化的函数
    public void Move(MoveDirection direction)
    {
        Array.Copy(map, originalMap, map.Length);
        IsChange = false;

        switch (direction)
        {
            case MoveDirection.Up: MoveUp(); break;
            case MoveDirection.Down: MoveDown(); break;
            case MoveDirection.Left: MoveLeft(); break;
            case MoveDirection.Right: MoveRight(); break;
        }
        //移动后对比
        CheckMapChange();
    }


    private void CheckMapChange()
    {
        for (int r = 0; r < map.GetLength(0); r++)
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                if (map[r, c] != originalMap[r, c])
                {
                    IsChange = true;
                    return;
                }
            }
        }
    }

    // 找出空位置
    public void GetEmpty()
    {
        emptyLOC.Clear();

        for (int r = 0; r < map.GetLength(0); r++)
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                if (map[r, c] == 0)
                    emptyLOC.Add(new Location(r, c));
            }

        }
    }

    // 在空白位置上， 随机生成数字(2 (90%)     4(10%))
    public void GenerateNumber()
    {
       
        GetEmpty();
        //list下表从0开始，不是从1开始，最后一个空余地方的时候，会出现数组越界出错。
        //int emptyLocIndex = random.Next(1, emptyLOC.Count);
        int emptyLocIndex = random.Next(1, emptyLOC.Count) - 1;
        //Debug.Log("emptyLOC.Count = " + emptyLOC.Count);
        //Debug.Log("emptyLocIndex = " + emptyLocIndex);
        
        Location loc = emptyLOC[emptyLocIndex];
        map[loc.RIndex, loc.CIndex] = random.Next(1, 11) == 1 ? 4 : 2;
        emptyLOC.RemoveAt(emptyLocIndex);
    }

    // 判断游戏是否结束    
    public bool IsOver()
    {//既没有空位置，又不能合并
        if (emptyLOC.Count > 0) return false;
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 3; c++)
            {
                if (map[r, c] == map[r, c + 1] || map[c, r] == map[c + 1, r])
                    return false;
            }
        return true;
    }
}


//类2 
//用于计算空白位置时的辅助类
class Location
{
    public int RIndex { get; set; }

    public int CIndex { get; set; }

    public Location(int r, int c)
    {
        RIndex = r;
        CIndex = c;
    }

}


//枚举，用于表示方向
enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}