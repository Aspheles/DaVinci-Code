using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopNpc;
    public TMP_Text message;
    public GameObject vendor;
    public bool shopOpened;
    public GameObject confirmPanel;
    public upgradeItem item;
    public static ShopManager instance;
    public GameObject alertPanel;
    public GameObject timeIcon;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if((shopNpc.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).sqrMagnitude < 3.0f && !shopOpened)
        {
            message.text = "Press E to open the shop";

            if (Input.GetKeyDown(KeyCode.E))
            {
                shopOpened = true;
                vendor.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameCamera.instance.canMove = false;
                Player.instance.canwalk = false;
            }
        }
        else
        {
            message.text = "";
        }

        
    }

    public void OpenAlertPanel(string message)
    {
        alertPanel.GetComponentInChildren<TMP_Text>().text = message;
        alertPanel.SetActive(true);
    }

    public void CloseAlertPanel()
    {
        if(alertPanel.GetComponentInChildren<TMP_Text>().text == "Upgrade has been purchased")
        {
            alertPanel.SetActive(false);
            CloseShop();
            StartCoroutine(PlayPurchaseAnim("sold", .4f));

        }
        else
        {
            alertPanel.SetActive(false);
            CloseShop();
            StartCoroutine(PlayPurchaseAnim("deny", 1f));
        }
       
    }


    IEnumerator PlayPurchaseAnim(string condition, float speed)
    {
        yield return new WaitForSeconds(.3f);
        Animator anim = shopNpc.GetComponent<Animator>();
        anim.speed = speed;
        anim.SetBool(condition, true);

        yield return new WaitForSeconds(.2f);
        anim.SetBool(condition, false);
        anim.speed = 1f;
    }

    public void BuyConfirmation()
    {
        item.BuyUpgrade();
    }

    public void CloseBuyConfirmation()
    {
        confirmPanel.SetActive(false);
    }

    public void CloseShop()
    {
        shopOpened = false;
        vendor.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        GameCamera.instance.canMove = true;
        Player.instance.canwalk = true;
    }
}
