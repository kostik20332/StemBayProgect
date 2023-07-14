using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Playerlogic : MonoBehaviourPunCallbacks
{
    public TMP_Text moneyAmountText;

    public Joystick joystick;

    Animator animator;

    private Rigidbody2D playerRB;
    private Rigidbody2D bulletRB;

    public GameObject bulletPrefab;
    public GameObject gameEndScreenPrefab;
    private GameObject gameEndScreen;
    private GameObject canvas;
    private GameObject bullet;
    private GameObject acessToUiElements;

    private AcessToPlayerUiElements scriptWithUiElements;

    private Vector2 bulletPos;

    public Image helthBar;

    private float moveInputHorizontal;
    private float moveInputVertical;
    public float horizontalScreenBorder;
    public float verticalScreenBorder;
    [HideInInspector]
    public float moneyAmount = 0f;
    private float speed = 300f;
    [HideInInspector]
    public float health = 100f;

    private bool face = true;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();

        acessToUiElements = GameObject.Find("AcessToUiElements");
        scriptWithUiElements = acessToUiElements.GetComponent<AcessToPlayerUiElements>();
        moneyAmountText = scriptWithUiElements.moneyAmountText;
        helthBar = scriptWithUiElements.helthBar;
        joystick = scriptWithUiElements.joystick;
        canvas = scriptWithUiElements.canvas;

        horizontalScreenBorder = Screen.width / 200 - 1;
        verticalScreenBorder = Screen.height / 200 - 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            moveInputHorizontal = joystick.Horizontal;
            moveInputVertical = joystick.Vertical;
            scriptWithUiElements.helthBar.fillAmount = health / 100f;
            moneyAmountText.text = "" + moneyAmount;
            if(this.transform.position.x > horizontalScreenBorder && moveInputHorizontal > 0
                || this.transform.position.x < -horizontalScreenBorder && moveInputHorizontal < 0)
            {
                moveInputHorizontal = 0f;
            }
            if (this.transform.position.y > verticalScreenBorder && moveInputVertical > 0
                || this.transform.position.y < -verticalScreenBorder && moveInputVertical < 0)
            {
                moveInputVertical = 0f;
            }
            playerRB.velocity = new Vector2(moveInputHorizontal * speed * Time.deltaTime, moveInputVertical * speed * Time.deltaTime);
            if(Mathf.Abs(moveInputHorizontal) > 0 || Mathf.Abs(moveInputVertical) > 0)
            {
                animator.SetFloat("Speed", 1);
            }
            else if (moveInputHorizontal == 0 && moveInputVertical == 0)
            {
                animator.SetFloat("Speed", 0);
            }
            if (moveInputHorizontal < 0 && face || moveInputHorizontal > 0 && !face)
            {
                Flip();
            }

            if (health <= 0f)
            {
                CheckOnGameEnd();
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    private void CheckOnGameEnd()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 3)
        {
            gameEndScreen = PhotonNetwork.Instantiate(gameEndScreenPrefab.name, new Vector2(gameEndScreenPrefab.transform.position.x, gameEndScreenPrefab.transform.position.y), Quaternion.identity);
        }
    }

    public void ShootFromGun()
    {
        if(face)
        {
            bulletPos = new Vector2(this.transform.position.x + 1f, this.transform.position.y + 0.1625f);
        }
        else
        {
            bulletPos = new Vector2(this.transform.position.x - 1f, this.transform.position.y + 0.1625f);
        }
        bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletPos, Quaternion.identity);
        bulletRB = bullet.GetComponent<Rigidbody2D>();
        if (face)
        {
            bulletRB.AddForce(new Vector2(1.5f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            bulletRB.AddForce(new Vector2(-1.5f, 0f), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Money" && view.IsMine)
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("RpcMoneyAdd", RpcTarget.All);
            //PhotonNetwork.Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Bullet" && view.IsMine)
        {
            health -= 10;
            PhotonNetwork.Destroy(col.gameObject);
        }
    }

    [PunRPC]
    private void RpcMoneyAdd()
    {
        moneyAmount++;
    }

    void Flip()
    {
        face = !face;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
