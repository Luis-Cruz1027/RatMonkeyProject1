using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform myplayer;
    public Vector3 offset;
    [SerializeField] private float damping;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.Translate(myplayer.transform.position.x, myplayer.transform.position.y, -10);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 targetPosition = myplayer.transform.position + offset;
        targetPosition.z = transform.position.z;
        Vector3 vel = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }
}
