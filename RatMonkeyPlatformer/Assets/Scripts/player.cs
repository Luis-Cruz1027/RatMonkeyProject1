using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Physics")]
    public float Speed;
    private Vector3 velocity;

    public float gravity;
    public LayerMask groundlayer;

    private Vector2 SELF_EXTENTS;

    public Vector2 jumpOffset;

    private Rigidbody2D rb2d;


    // Start is called before the first frame update
    void Start()
    {
        SELF_EXTENTS = transform.GetComponent<SpriteRenderer>().bounds.extents;
        rb2d = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    private void FixedUpdate()
    {
        jumpOffset = new Vector2(transform.position.x, transform.position.y + 1f);
        Vector2 dir = Vector2.zero;
        if(Input.GetKey(KeyCode.D)){
            dir += Vector2.right;
        }
        if(Input.GetKey(KeyCode.A)){
            dir += Vector2.left;
        }
        if(isGrounded() && Input.GetKey(KeyCode.W)){
            rb2d.AddForce(transform.up * 100f);
        }
        if(rb2d.velocity.y >= 0){
            rb2d.gravityScale = 1f;
        }
        else{
            rb2d.gravityScale = 4f;
        }
        

        velocity = (dir * Speed);



        transform.position += velocity * Time.deltaTime;
    }

    public bool isGrounded(){
        Vector3 rayPosition = new Vector3(transform.position.x, transform.position.y + SELF_EXTENTS.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.down, .5f, groundlayer);
        if(hit.collider != null){
            return true;
        }

        return false;
    }

    
}
