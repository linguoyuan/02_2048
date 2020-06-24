using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //存放数字图片
    public Sprite[] sprites;
    //存放格子列表
    public List<GameObject> gridList = new List<GameObject>(16);

    public void UpdateCard(int row, int cloumn, int value)
    {
        //将二维数组的值转化为具体的位置值
        int pos = row*4 + cloumn;
        //Debug.Log("pos = " + pos);
        Debug.Log("value = " + value);

        //1.对应的格子按值显示对应sprite
        GameObject go = gridList[pos].transform.Find("Text").gameObject;
        if(go != null)
        {
            if (value != 0)
            {
                go.GetComponent<Text>().text = value.ToString();
            }
            else
            {
                go.GetComponent<Text>().text = "";
            }
        }
    }
}
