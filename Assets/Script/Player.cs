using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float jumpForce = 300f;

    private bool isJump = false;

    [SerializeField]
    private GameObject bulletPos = null;

    [SerializeField]
    private GameObject bulletobj = null;

    void Update()
    {
        PlayerMove();

        if(Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    private void PlayerMove()
        {
            float h = Input.GetAxis("Horizontal");
            float playerSpeed = h * moveSpeed * Time.deltaTime;
            Vector3 vector3 = new Vector3();
            vector3.x = playerSpeed;
            transform.Translate(vector3);

            if (h < 0)
            {
                GetComponent<Animator>().SetBool("Walk", true);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (h == 0)
            {
                GetComponent<Animator>().SetBool("Walk", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("Walk", true);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    private void PlayerJump()
    {
        if(isJump == false)
        {
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Jump", true);

            Vector2 vector2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
            isJump = true;
        }
    }

    private void OncollsionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            isJump = false;
        }
    }
    private void Fire()
    {
        GetComponent<AudioSource>().Play();
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bulletobj, bulletPos.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);
    }
}