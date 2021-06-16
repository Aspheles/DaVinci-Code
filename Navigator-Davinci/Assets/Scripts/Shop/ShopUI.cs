using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject upgradeItem;
    public Transform location;

    private void Start()
    {
        LoadUpgradeItems();
    }

    public void LoadUpgradeItems()
    {
        foreach(Upgrade upgrade in UpgradeManager.instance.upgrades)
        {

            upgradeItem.GetComponent<upgradeItem>().id = upgrade.id;
            upgradeItem.GetComponent<upgradeItem>().name = upgrade.name;
            upgradeItem.GetComponent<upgradeItem>().description = upgrade.description;
            upgradeItem.GetComponent<upgradeItem>().level = upgrade.level;
            upgradeItem.GetComponent<upgradeItem>().price = upgrade.price;

            GameObject item = Instantiate(upgradeItem, location.transform.position, Quaternion.identity);

            item.transform.SetParent(location);
            
        }
    }

}
