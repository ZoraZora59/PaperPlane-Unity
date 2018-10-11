using UnityEngine;
using System.Collections;
[System.Serializable]
public class fireBoundary//子弹边界
{
    public float x, y;
}
public class Bullet : MonoBehaviour {
    public static Bullet instance;

    public fireBoundary boundary;

    private bool check=true;//确保方向不变

    private Rigidbody2D rb;

    public float speed;//子弹飞行速度
    private Vector2 fire;//速度

    void Start () {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        setNew();
    }
	void setNew()//初始化速度方向
    {
        if (check == true)
        {
            Vector3 temp = Input.mousePosition;
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(rb.position);


            Vector3 direction = temp - playerPosition;
            direction.z = 0;//将Z轴置0,保持在2D平面内  
            direction = direction.normalized;//转化为单位向量
            fire = direction;
            fire.x *= speed;
            fire.y *= speed;
            check = false;
        }
    }
    void push()//子弹加速
    {
        rb.velocity = fire;
    }
    void checkOut()//出界则销毁
    {
        if (rb != null)//子弹飞出边界则销毁
        {
            if (rb.position.x > boundary.x || rb.position.x < -boundary.x || rb.position.y > boundary.y || rb.position.y < -boundary.y)
                Destroy(gameObject);
        }
    }
	void Update () {
        checkOut();
        push();
	}
}
