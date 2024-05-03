using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public UpdateToHP HPBar;
    public GameObject HP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Throwable"))
        {
            HPBar.health -= 10f;
            HP.transform.localScale = new Vector3(HPBar.health/HPBar.maxhealth,HP.transform.localScale.y,1f);
            HP.transform.localPosition = new Vector3((HPBar.health/HPBar.maxhealth/2f-0.5f),HP.transform.localPosition.y,0f);
            if(HPBar.health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
