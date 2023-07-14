using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersSpawn : MonoBehaviour
{
    public GameObject[] playerPrefabs = new GameObject[3];
    private GameObject player;

    [HideInInspector]
    public Playerlogic playerScript;

    public float minX, minY, maxX, maxY;

    public TMP_InputField nameInput;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));
        player = PhotonNetwork.Instantiate(playerPrefabs[Random.Range(0, 3)].name, randomPosition, Quaternion.identity);
        playerScript = player.GetComponent<Playerlogic>();
    }

    public void NameEnter()
    {
        player.name = nameInput.text;
    }

    public void ShootFromPlayerGun()
    {
        playerScript.ShootFromGun();
    }
}
