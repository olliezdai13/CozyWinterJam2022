using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballBehavior : MonoBehaviour
{
    public GameObject hitVfxPrefab;
    public float moveSpeed = 1f;
    public bool moveLeft = false;
    public float despawnTime = 5f;
    public int damage = 1;
    public bool friendly = true;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, despawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        } else
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((friendly && !other.CompareTag("Player")) || (!friendly && other.CompareTag("Player")))
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.Damage(damage);
                if (hitVfxPrefab) Instantiate(hitVfxPrefab, transform.position, Quaternion.Euler(45, 0, 0));
                Destroy(gameObject);
            }

        }  
    }
}
