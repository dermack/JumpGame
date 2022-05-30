using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public int scorePoints = 10;
    private GameManager gameManager;
    public ParticleSystem objectParticles;
    private AudioSource audioSource;
    public AudioClip explodeSound;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make them fall
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {      
        switch (other.tag)
        {
            case "Ground":
                Destroy(gameObject);
                break;
            case "Player":
                Instantiate(objectParticles, gameObject.transform.position, gameObject.transform.rotation);
                audioSource.PlayOneShot(explodeSound, 5.0f);
                gameManager.UpdateScore(scorePoints);
                Destroy(gameObject);
                break;

        }
    }
}
