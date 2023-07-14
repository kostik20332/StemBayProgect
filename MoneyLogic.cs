using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
