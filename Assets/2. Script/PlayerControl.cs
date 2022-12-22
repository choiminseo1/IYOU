using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerControl : MonoBehaviour
{

    public GameObject[] Soul;

    float moveSpeed;
    int score;
    public TextMeshProUGUI tmp;
    void Start()
    {
        //StartCoroutine(ColorBack());
        moveSpeed = 3f;
        int score = 0;

    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, v, 0);
        //transform.position += dir;
        GetComponent<Rigidbody2D>().velocity
             = dir * 5;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("WALL"))
        {
            GetComponent<SpriteRenderer>().color
                = collision.collider.GetComponent<SpriteRenderer>().color;
            StopCoroutine("Colorback");
            StartCoroutine("Colorback");

        }
        if (collision.collider.CompareTag("SOUL"))
        {
            //나의 소울 색이 같다면 > 사라짐
           if( GetComponent<SpriteRenderer>().color
                = collision.collider.GetComponent<SpriteRenderer>().color)
            {
                moveSpeed += 0.5f;
                score += 2;
                Destroy(collision.gameObject);
            }
            else
            {
                //나의 소울 색이 다르다면 > 도망가게
                float x = Random.Range(-8f, 8f);
                float y = Random.Range(-3f, 3f);
                Vector3 pos = new Vector3(x, y, 0);
                collision.transform.position = pos;

                moveSpeed -= 0.5f;
            }

        }
    }
    IEnumerator ColorBack()
    {
        yield return new WaitForSeconds(3f);
        //GetComponent<SpriteRenderer>().color = color;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator SoulCreate()
    {
        yield return new WaitForSeconds(3f);
        // 생성코드

        // Instantiate(게임오브젝트, 위치, 회전);

        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-3f, 4f);
        Vector3 pos = new Vector3(x, y, 0);

        Instantiate(Soul[0], pos, Quaternion.identity);
        StartCoroutine("SoulCrate");
    }

    public void Left()
    {
        transform.position += new Vector3(-1, 0, 0) * moveSpeed;
    }
    public void Right()
    {
        transform.position += new Vector3(1, 0, 0) * moveSpeed;
    }
    public void Up()
    {
        transform.position += new Vector3(0, 1, 0) * moveSpeed;
    }
    public void Down()
    {
        transform.position += new Vector3(0, -1, 0) * moveSpeed;
    }
}
