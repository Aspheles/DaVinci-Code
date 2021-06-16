using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour
{
    public List<Upgrade> upgrades;

    public void FetchCurrencyFromDB(string accountid)
    {
        Debug.Log("Fetching money from db");

        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("accountid", accountid),
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.FETCHINGMONEY);
    }

    public void SendPurchaseRequest(string accountid, string upgradeid, string level, string price)
    {
        Debug.Log("Sending purchase request");

        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("accountid", accountid),
            new MultipartFormDataSection("upgradeid", upgradeid),
            new MultipartFormDataSection("level", level),
            new MultipartFormDataSection("price", price)
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.PURCHASEREQUEST);
    }

    public void CheckUpgrdes(string accountid)
    {
        Debug.Log("Checking if user has upgrades");

        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("accountid", accountid),
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.FETCHUPGRADES);
    }

    public void LoadUpgrades()
    {
        ApiHandler.instance.CallApiRequest("get", null, Request.LOADUPGRADES);
    }

    public void ApplyUpgrades()
    {
        foreach(Upgrade upgrade in upgrades)
        {
            Debug.Log("Found Upgrade " + upgrade.name);
            Debug.Log("Upgrade Level: " + upgrade.level);

            if(upgrade.power == Upgrade.Powers.HEALTH)
            {
                switch (upgrade.level)
                {
                    case 1:
                        RunManager.instance.IncreaseMaxHealth(1);
                        break;
                    case 2:
                        RunManager.instance.IncreaseMaxHealth(2);
                        break;

                }
            }else if(upgrade.power == Upgrade.Powers.RETRY)
            {
                switch (upgrade.level)
                {
                    case 1:
                        RunManager.instance.retries += 1;
                        break;
                    case 2:
                        RunManager.instance.retries += 2;
                        break;
                }
            }
        }
    }

    public void RemoveUpgrade(string accountid, string upgradeid)
    {
        Debug.Log("Removing used upgrade");

        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("accountid", accountid),
            new MultipartFormDataSection("upgradeid", upgradeid)
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.REMOVEUPGRADE);
    }
}
