using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 30f, -79);
    public float smoothSpeed = 1f;
    public Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // If Player doesn't exists, return
        if (GameObject.Find("Player") == null)
        {
            return;
        }

        // Set Camera offset based on player's height
        if (player.transform.position.y < 18)
        {
            offset.y = 23;
            offset.y -= player.transform.position.y;
        }


        Vector3 currentPosition = new Vector3(0, transform.position.y, transform.position.z);
        Vector3 desiredPosition = new Vector3(0, player.transform.position.y, player.transform.position.z);

        transform.position = Vector3.Lerp(currentPosition, desiredPosition + offset, smoothSpeed * Time.deltaTime);
    }
}
