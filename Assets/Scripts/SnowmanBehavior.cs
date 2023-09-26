using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBehavior : MonoBehaviour, IDamageable
{

    public GameObject _snowballPrefab;
    public GameObject _snowballSpawnL;
    public GameObject _snowballSpawnR;
    public GameObject deathVfxPrefab;

    public int startingHealth = 5;
    public float hitFxDuration = 0.2f;
    public float throwInterval = 2f;
    public int snowballDamage = 1;
    public float snowballSpeed = 3f;
    private int _health;
    private float flashDuration = 0f;
    private float throwTimer = 2f;
    private bool facingLeft = false;

    public int Health { get { return _health; } set { _health = value; } }

    private SpriteRenderer spriteRenderer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _health = startingHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        throwTimer -= Time.deltaTime;
        if (throwTimer <= 0)
        {
            // Throw snowball
            throwTimer = throwInterval;
            SpawnSnowball(snowballDamage, snowballSpeed);
        }
        if (flashDuration > 0)
        {
            flashDuration -= Time.deltaTime;
        } else
        {
            spriteRenderer.color = Color.white;
        }
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
        } else
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }
    public void SpawnSnowball(int damage, float snowballSpeed)
    {
        if (_snowballPrefab && _snowballSpawnL && _snowballSpawnR)
        {
            GameObject snowball = Instantiate(_snowballPrefab, facingLeft ? _snowballSpawnL.transform.position : _snowballSpawnR.transform.position, Quaternion.Euler(45, 0, 0));
            SnowballBehavior snowballBehavior = snowball.GetComponent<SnowballBehavior>();
            snowballBehavior.moveLeft = facingLeft;
            snowballBehavior.despawnTime = 3f;
            snowballBehavior.friendly = false;
            snowballBehavior.damage = damage;
            snowballBehavior.moveSpeed = snowballSpeed;
        }
    }
    public void Damage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            if (deathVfxPrefab) Instantiate(deathVfxPrefab, transform.position + (Vector3.up * 0.25f), Quaternion.Euler(45, 0, 0));
            Destroy(gameObject);
        }
        spriteRenderer.color = Color.red;
        flashDuration = hitFxDuration;
    }

}
