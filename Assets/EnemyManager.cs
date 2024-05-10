using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject item;
    public Transform spawn;
    public float speedMultiplier;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Throw()
    {
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        Vector3 rot = new Vector2(transform.position.x-player.position.x,transform.position.y-player.position.y).normalized;
        GameObject r = Instantiate(item,spawn.position,Quaternion.Euler(0,0,0f));
        r.transform.position = spawn.position;
        r.GetComponent<Rigidbody2D>().velocity =new Vector2(-1f,-1f)*rot*item.GetComponent<Object>().speed;
    }
    // Update is called once per frame
    public void Update()
    {
    }
}
