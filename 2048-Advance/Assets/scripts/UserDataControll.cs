using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class JsonData
{
    public List<UserData> lsData;
}

[SerializeField]
public class UserData{
    public int width;
    public int height;
    //public List<List<int>> nums;

    

}

public class UserDataControll : MonoBehaviour
{
    JsonData ud;
    private void Awake()
    {
        ud = new JsonData();
        ud.lsData = new List<UserData>();
        ud.lsData.Add(new UserData());
    }



    void Start()
    {
        
        print(JsonPath());
        //ReadJson();
        SaveJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string JsonPath()
    {
        return Application.streamingAssetsPath + "/ud.json";
    }


    void ReadJson()
    {
        if (!File.Exists(JsonPath()))
        {
            Debug.LogError("Чтение файла не существует!");
            JsonUtility.ToJson(ud);
            return;
        }
        string json = File.ReadAllText(JsonPath());
        
        ud = JsonUtility.FromJson<JsonData>(json);
        //print(ud.width.ToString());
        //print(ud.height.ToString());
        //print(ud.nums.Count.ToString());
        //for (int i = 0; i < ud.lsPlayerMessage.Count; i++)
        //{
        //    Debug.Log(ud.lsPlayerMessage[i].PlayerId);
        //    Debug.Log(ud.lsPlayerMessage[i].PlayerName);
        //    for (int j = 0; j < jsonTemp.lsPlayerMessage[i].PlayerEquip.Count; j++)
        //    {
        //        Debug.Log(ud.lsPlayerMessage[i].PlayerEquip[j].EquipId);
        //        Debug.Log(ud.lsPlayerMessage[i].PlayerEquip[j].EquipName);
        //    }
        //}
    }


    void SaveJson()
    {
        // Если локально нет соответствующего файла json, воссоздаем
        if (!File.Exists(JsonPath()))
        {
            File.Create(JsonPath());
            JsonInit();
        }
        string json = JsonUtility.ToJson(ud, true);
        print(json + " baballan");
        json = "{ \"jepa\": 0 }";
        File.WriteAllText(JsonPath(), json);
        Debug.Log("Сохранить успешно");
        
    }

    private void JsonInit()
    {

        ud.lsData[0].width = Camera.main.GetComponent<GameController>()._width;
        ud.lsData[0].height = Camera.main.GetComponent<GameController>()._height;
        
    }
}


