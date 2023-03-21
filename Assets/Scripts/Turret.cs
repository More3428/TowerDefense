using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy; 
    
    [Header("General")]
    public float range = 15f;
    
    [Header("Use Bullets (default)")]
     public GameObject bulletPrefab;
     public float fireRate = 1f;
     private float fireCountdown = 0f;

     [Header("Use Laser")] 
     public bool useLaser = false;
     public LineRenderer LineRenderer;
     public ParticleSystem impactEffect;
     public Light impactLight;
     public int damageOverTime = 30;
     public float slowpct = .5f;
     
     
     
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

   
    public Transform firePoint; 
    public Transform partToRotate;
    public float turnSpeed = 10f; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy; 
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (LineRenderer.enabled)
                {
                    LineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false; 
                }
                    
            }
            return;
        }
            
        //moving turret
        LockOnTarget();
        if (useLaser)
        {
            Laser();
        }
        else
        {
             if (fireCountdown <= 0f)
             {
                 Shoot();
                 fireCountdown = 1f / fireRate; 
             }
            
             fireCountdown -= Time.deltaTime; 
        }

       
    }

    void Laser()
    {
        
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowpct);
        
        if (!LineRenderer.enabled)
        {
            LineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true; 
        }
        LineRenderer.SetPosition(0, firePoint.position);
        LineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        
        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir); 
        
        
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if(bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
