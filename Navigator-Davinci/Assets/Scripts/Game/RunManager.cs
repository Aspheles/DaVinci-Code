using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public Run run;
    public Room room;
    public bool roomCompleted;
    public float timer = 0f;
    
    private void Update()
    {
        if(room == null)
        {
            room = GameObject.Find("Room").GetComponent<Room>();
        }

        if(run == null)
        {
            Run.instance.CreateRun(run);
            run = Run.instance.GetRun();
        }

        if(run != null && run.isCompleted == false)
        {
            timer += Time.deltaTime;
        }
    }


}
