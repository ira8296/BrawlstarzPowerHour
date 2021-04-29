using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager man;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!man.isDead && man.canMove)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                Vector2 temp = this.transform.position;
                temp.x -= 3f* Time.deltaTime;
                this.transform.position = temp;
                if (isFacingRight())
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
                anim.SetBool("isWalking", true);
                //Debug.Log("Pirate is Walking");
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                Vector2 temp = this.transform.position;
                temp.x += 3f * Time.deltaTime;
                this.transform.position = temp;
                if (!isFacingRight())
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
                anim.SetBool("isWalking", true);
                //Debug.Log("Pirate is Walking");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Note, this is relative, should be changed
                if (this.transform.position.y < -2.9f)
                {
                    Vector2 temp = this.transform.position;
                    temp.y += 4f;
                    this.transform.position = temp;
                }
            }
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)
                && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isWalking",false);
                //Debug.Log("Pirate is Idle");
            }
        }
    }

    bool isFacingRight()
    {
        if(transform.localScale.x > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
