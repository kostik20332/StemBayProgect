using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoneySpawn : MonoBehaviour
{
    public GameObject moneyPrefab;
    private GameObject money;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            PhotonNetwork.Instantiate(moneyPrefab.name, new Vector2(Random.Range(-10f,10f), Random.Range(-4f, 4f)), Quaternion.identity);
        }
    }
}
