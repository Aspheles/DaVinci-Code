using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public string number;
    public Room room;
    public Material mat;

    private void Update()
    {
        mat.color = Color.red;
    }
    public void GetData(JSONNode data)
    {
        number = data[0].AsObject["number"];
    }
}
