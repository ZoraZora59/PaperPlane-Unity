using UnityEngine;
using System.Collections;
[System.Serializable]
public class Boundary//移动边界
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerControl : MonoBehaviour {
    //GameObject x=instantiate(,,)asGameObject 预制体实例化
    public AudioSource shotMusic;
    public Boundary boundary;
    public float moveSpeed=5f;
    private float fireTime=0;//射击时间
    public float fireRate = 0.35f;//射击速率
    public GameObject bullet;//子弹
    public GameObject bulletPosition;//子弹出现位置
    public float bulletTime = 6f;//子弹销毁时间
    public static PlayerControl instance;
    void Start () {
        instance = this;
    }
    void FixedUpdate()//类似Update，不过是固定时间而不是每一帧
    {
        playerFire();
        playerMove();   
    }
    
    void playerMove()//角色移动
    {
        float moveH = Input.GetAxis("Horizontal");//水平
        float moveV = Input.GetAxis("Vertical");//垂直
        Vector3 moveSet = new Vector3(moveH, moveV, 0f);//用于赋值的移动向量
        Rigidbody2D rb = GetComponent<Rigidbody2D>();//获取碰撞体组件
        if (rb != null)
        {
            rb.velocity = moveSet * moveSpeed;//速度设置
            rb.position = new Vector3//移动区域限制
                (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
                0f
                );
        }
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(rb.position);
        Vector3 direction = mousePosition - playerPosition;
        direction.z = 0;//将Z轴置0,保持在2D平面内  
        direction = direction.normalized;//转化为单位向量
        rb.transform.up = direction;
    }
    void playerFire()//角色射击
    {
        if(Input.GetButton("Fire1")&&Time.time>fireTime)
        {
            fireTime = Time.time + fireRate;
            GameObject playerBullet = Instantiate(bullet, bulletPosition.transform.position, bulletPosition.transform.rotation) as GameObject;
            shotMusic.Play();
            Destroy(playerBullet, bulletTime);//固定时间后自动销毁
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
