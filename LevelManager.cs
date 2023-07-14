using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int playersAmount = 0;
    public int amount;
    private bool allowedToPlay;

    // Start is called before the first frame update
    public void ObjectDisactivation(GameObject obj)
    {
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        amount = playersAmount;
        if (playersAmount > 1)
        {
            allowedToPlay = true;
        }

        if(allowedToPlay && playersAmount == 1)
        {
            allowedToPlay = false;
        }
    }
}
