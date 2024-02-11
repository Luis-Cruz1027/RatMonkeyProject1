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

    private Rigidbody2D rb2d;

    private float dashCooldown = 5f;

    private float usableDash = 0f;
    private float force = 300f;
    private bool isDashing = false;


    // Start is called before the first frame update
    void Start()
    {
        SELF_EXTENTS = GetComponent<SpriteRenderer>().bounds.extents;
        rb2d = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;
        if(!isDashing){
                if(Input.GetKey(KeyCode.D)){
                dir += Vector2.right;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if(Input.GetKey(KeyCode.A)){
                dir += -Vector2.right;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if(isGrounded() && Input.GetKey(KeyCode.W)){
                Debug.Log("Jumping");
                rb2d.AddForce(transform.up * force);
            }
        }
        if(rb2d.velocity.y >= 0){
            rb2d.gravityScale = 2f;
        }
        else{
            rb2d.gravityScale = 4f;
        }
        velocity = dir * Speed;
        // dash when shift in the direction you are headed
        if(Input.GetKey(KeyCode.LeftShift) && Time.time > usableDash && velocity != Vector3.zero){
            isDashing = true;
            Dash(velocity);
            isDashing = false;
            usableDash = Time.time + dashCooldown;
            
        }
        // if(Time.time > usableDash){
        //     Debug.Log("Ready!");
        // }
        // else{
        //     Debug.Log("Not Ready!");
        // }
        
        transform.position += velocity * Time.deltaTime;
    }

    public bool isGrounded(){
        Vector3 rayPosition = new Vector3(transform.position.x, transform.position.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.down, SELF_EXTENTS.y, groundlayer);
        if(hit.collider != null){
            return true;
        }

        return false;
    }

    public void Dash(Vector3 velocity){
        int dashDamage = 100;
        Debug.Log(velocity);
        Vector2 originalPos = transform.position;
        Vector2 endPos;
        RaycastHit2D wallHit = Physics2D.Raycast(originalPos, velocity.normalized, Speed * Time.deltaTime * 50f, LayerMask.GetMask("Ground"));
       
        RaycastHit2D[] slash;
        if(wallHit.collider != null){
            Debug.Log("there is a wall you nincompoop");
            if(velocity.x > 0){
                transform.position = new Vector3(wallHit.point.x - SELF_EXTENTS.x -.05f, wallHit.point.y, 0f);
            }
            else{
                transform.position = new Vector3(wallHit.point.x + SELF_EXTENTS.x + .05f, wallHit.point.y, 0f);
            }
            endPos = transform.position;
            
        }
        else{
            transform.position += velocity * Time.deltaTime * 50f;
            endPos = transform.position;
        }
        if(endPos.x > originalPos.x){
            slash = Physics2D.RaycastAll(originalPos, velocity, endPos.x - originalPos.x, LayerMask.GetMask("Enemy"));
            Debug.DrawLine(originalPos, endPos, Color.red, 1f);
        }
        else{
            slash = Physics2D.RaycastAll(originalPos, velocity, originalPos.x - endPos.x, LayerMask.GetMask("Enemy"));
            Debug.DrawLine(originalPos, endPos, Color.white, 4f);
        }
        
        for(int i = 0; i < slash.Length; i++){
            if(i == 0){
                slash[i].collider.GetComponent<enemy>().DealDamage(dashDamage);
            }
            else{
                slash[i].collider.GetComponent<enemy>().DealDamage(dashDamage/2);
            }
        }
    }

    
}
