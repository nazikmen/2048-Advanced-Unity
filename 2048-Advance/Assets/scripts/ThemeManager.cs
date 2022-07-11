using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public List<Color> colorLists1;
    public List<Color> colorLists2;
    public List<Color> colorLists3;
    public List<Color> colorLists4;
    public List<Color> colorLists5;

    [HideInInspector]
    public List<List<Color>> colorLists;


    //[HideInInspector]
    //public List<Color> selectedList;

    public int curTheme;

    //private GameObject[] uiObjects;

    private void Awake()
    {
        colorLists = new List<List<Color>>();
        colorLists.Add(colorLists1);
        colorLists.Add(colorLists2);
        colorLists.Add(colorLists3);
        colorLists.Add(colorLists4);
        colorLists.Add(colorLists5);

        //curTheme = 1;
        //selectedList = (List<Color>)this.GetType().GetField("colorLists"+curTheme.ToString()).GetValue(this);

        //_mainCamera.backgroundColor = selectedList[12];

        //if (uiObjects == null)
        //    uiObjects = GameObject.FindGameObjectsWithTag("ui");

        //foreach (GameObject uiObject in uiObjects)
        //{
        //    var image = uiObject.GetComponent<Image>();
        //    if(image != null)
        //    {
        //        image.color = selectedList[13];
        //    }
        //}

    }

    public Color GetColor(int num)
    {
        Color col = Color.black;
        int n = 2;
        int nl = 0;
        while (n<2048)
        {
            if (n <= num)
            {
                col = colorLists[curTheme][nl];
                nl++;
                n *= 2;
                continue;
            }
            
            return col;
        }
        return col;
    }





}
