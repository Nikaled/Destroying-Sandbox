using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody bulletRigidbody;
    public float speed = 300;
  public  int bulletDamage = 1;
   public bool TankProjectile;
    [SerializeField] GameObject DestroyAnimation;

    void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed;
        StartCoroutine(DestroyObj());
    }
    private void OnTriggerEnter(Collider other)
    {
    //    Debug.Log(other.name);
        if (other.GetComponent<DestroyCollision>() != null)
        {
            other.GetComponent<DestroyCollision>().TakeDamage(transform.position);
        Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
     //   Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<DestroyCollision>() != null)
        {
            collision.gameObject.GetComponent<DestroyCollision>().TakeDamage(transform.position);
        }
        Destroy(gameObject);
    }
    private IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
