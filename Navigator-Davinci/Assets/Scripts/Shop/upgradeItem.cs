using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class upgradeItem : MonoBehaviour
{
    public int id;
    public string name;
    public string description;
    public Upgrade.Powers power;
    public int level;
    public int price;

    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text itemPrice;
    public TMP_Text itemLevel;

    private void Start()
    {
        itemName.text = name;
        itemDescription.text = description;
        itemLevel.text = "Level: " + level.ToString();
        itemPrice.text = price.ToString();

    }

    public void ConfirmPurchase()
    {
        ShopManager.instance.confirmPanel.SetActive(true);
        ShopManager.instance.item = this;
    }

    public void BuyUpgrade()
    {
        if(UserInfo.instance.currency >= price)
        {
            UserInfo.instance.playerManager.SendPurchaseRequest(UserInfo.instance.id, id.ToString(), level.ToString(), price.ToString());
            ShopManager.instance.OpenAlertPanel("Upgrade has been purchased");
        }
        else
        {
            ShopManager.instance.OpenAlertPanel("You don't have enough Electroids");
            
        }

        ShopManager.instance.confirmPanel.SetActive(false);
    }

}
