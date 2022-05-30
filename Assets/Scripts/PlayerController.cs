using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    [SerializeField] 
    private float speed;
    [SerializeField]
    private float jumpForce;
    private float input;
    public Vector3 gravity;
    public bool isOnGround = true;
    public GameManager gameManager;
    public TextMeshProUGUI scoreText;
    public bool canDieFromFall = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity = gravity;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * input *  speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (!gameManager.isGameStarted)
        {
            gameManager.StartGame();
        }
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Good"))
        {
            // Set the momentum to zero before apply more force
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
            Jump();
        }
        else if (other.CompareTag("Bad"))
        {
            Destroy(gameObject);
            gameManager.GameOver();
            
        }
        else if (other.CompareTag("HeightLimit"))
        {
            canDieFromFall = true;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        isOnGround = true;

        if (canDieFromFall)
        {
            gameManager.GameOver();
        }
    }
}
