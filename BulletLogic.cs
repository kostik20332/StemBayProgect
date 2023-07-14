using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private void Update()
    {
        if(this.transform.position.x > 20 || this.transform.position.x < -20)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
