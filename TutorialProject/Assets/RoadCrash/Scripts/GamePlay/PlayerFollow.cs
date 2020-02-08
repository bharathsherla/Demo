using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;

    private Vector3 cameraOffset;

    [Range(0f, 1f)]
    public float smoothfactor = 0.5f;

    public bool lookAtPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset =  transform.position - player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = player.position + cameraOffset;
		//transform.position = pos;//Vector3.Slerp(transform.position, pos, smoothfactor);
		//Vector3 test = Vector3.Lerp(transform.position, pos, 1f * Time.deltaTime);
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, pos.x ,50f * Time.fixedDeltaTime), pos.y, pos.z);
        //if (lookAtPlayer)
        //    transform.LookAt(player);
		
    }
}
