using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour
{
    public void FetchCurrencyFromDB(string accountid)
    {
        Debug.Log("Fetching money from db");

        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("accountid", accountid),
        };

        ApiHandler.instance.CallApiRequest("post", form, Request.FETCHINGMONEY);
    }
}
