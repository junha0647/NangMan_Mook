using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�̵��ӵ�")][SerializeField] private float Speed;
    [Header("������")][SerializeField] private float JumpForce;
    [Header("ü��")][SerializeField] private int HealthPoint;

    private Rigidbody2D rigid;
    private Animator anim;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!UIManager.isDraw)
        {
            Move();
            Jump();
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // === �̵� �ִϸ��̼� === //
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }

        // === �̵� �� �¿� ���� === //
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // === �̵��ӵ� ���� === //
        if (rigid.velocity.x > Speed)
        {
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < Speed * (-1))
        {
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
        }

        // === �̵� �� �̲��� ���� === //
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.01f, rigid.velocity.y);
        }

        // === ���� �̲��� ���� === //
        if (h == 0)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
            
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }

        if (rigid.velocity.y <= 0)
        {
            Vector2 frontVec = new Vector2(rigid.position.x + rigid.velocity.x * 0.03f, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2.5f, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                {
                    anim.SetBool("isJump", false);
                }
            }
            // ���⼭ ������ ���� rayHit�� 2f�� distance�� ����(1.2f)�� �� �����ؾ� ��.
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Spike")
    //    {
    //        if (this.gameObject.layer == 8)
    //        {
    //            OnDamaged();
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            if (gameObject.layer == 8)
            {
                OnDamaged();
                int dirc = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
                rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);
            }
        }
    }

    void OnDamaged()
    {
        Debug.Log("������ ����");
        --HealthPoint;
        gameObject.layer = 9;

        Invoke("OffDamaged", 1f);
    }

    void OffDamaged()
    {
        Debug.Log("���� Ǯ��");
        gameObject.layer = 8;
    }
}