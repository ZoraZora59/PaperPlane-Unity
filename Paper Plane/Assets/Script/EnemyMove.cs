using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    public static EnemyMove instance;

    private Vector2 enemySpeed;

    public float speed = 5f;//敌人飞行速度

    private bool check = true;//确保方向不变

    private Rigidbody2D rb;

    Vector3 direction;//飞行方向

    public float tumble = 10.0f;//旋转速度

    // Use this for initialization
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = tumble;//定义角速度旋转
        setNew();

    }
    //void OnTriggerEnter(Collider e)
    //{
    //    if (e.gameObject.tag.CompareTo("Player") == 0)
    //    {
    //        Destroy(this.gameObject);
    //        GameManager.instance.playerHealth--;
    //    }
    //    else if (e.gameObject.tag.CompareTo("FireB") == 0)
    //    {
    //        Destroy(this.gameObject);
    //        Destroy(e.gameObject);
    //    }

    //}
    void setNew()//初始化速度方向
    {
        if (check == true)
        {
            //Vector3 temp = Input.mousePosition;
            //Vector3 playerPosition = Camera.main.WorldToScreenPoint(rb.position);


            //Vector3 direction = temp - playerPosition;
            Vector3 direction;
            direction.x = Random.Range(-1, 1);
            if (direction.x == 0)
                direction.x = 0.0035f;
            direction.y = Random.Range(-1, 1);
            if (direction.y == 0)
                direction.y = 0.0035f;
            direction.z = 0;//将Z轴置0,保持在2D平面内  
            direction = direction.normalized;//转化为单位向量
            enemySpeed = direction;
            enemySpeed.x *= speed;
            enemySpeed.y *= speed;
            check = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D e)
    {
        if (e.gameObject.tag.CompareTo("Player") == 0)
        {
            GameManager.instance.playerHealth-=1;
            GameManager.instance.destroy4.Play();
            Destroy(this.gameObject);
        }
        else if (e.gameObject.tag.CompareTo("FireB") == 0)
        {
            if (GameManager.instance.destroySound % 4 == 0)
                GameManager.instance.destroy4.Play();
            else if (GameManager.instance.destroySound % 3 == 0)
                GameManager.instance.destroy3.Play();
            else if (GameManager.instance.destroySound % 2 == 0)
                GameManager.instance.destroy2.Play();
            else GameManager.instance.destroy1.Play();
            GameManager.instance.Point+=GameManager.instance.hardValue;
            GameManager.instance.destroySound++;
            Destroy(this.gameObject);
            Destroy(e.gameObject);
        }
    }
    void back()
    {
        if (rb.position.x > 6.5f || rb.position.x < -6.5f)
            enemySpeed.x = -enemySpeed.x;
        if (rb.position.y > 5f || rb.position.y < -5f)
            enemySpeed.y = -enemySpeed.y;
    }//飞出界反弹
    void push()
    {
        rb.velocity = enemySpeed;
    }
    // Update is called once per frame
    void Update()
    {
        back();
        push();
    }
}
