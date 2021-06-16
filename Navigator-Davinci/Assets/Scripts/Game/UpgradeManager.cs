using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public List<Upgrade> upgrades;

    private void Start()
    {
        instance = this;
    }


}
