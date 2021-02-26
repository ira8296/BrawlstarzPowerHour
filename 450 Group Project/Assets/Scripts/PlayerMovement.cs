using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 temp = this.transform.position;
            temp.x -= .01f;
            this.transform.position = temp;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 temp = this.transform.position;
            temp.x += .01f;
            this.transform.position = temp;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Note, this is relative, should be changed
            if (this.transform.position.y < -2.9f)
            {
                Vector2 temp = this.transform.position;
                temp.y += 1f;
                this.transform.position = temp;
            }
        }
    }
}
