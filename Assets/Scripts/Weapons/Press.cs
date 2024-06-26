using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Press : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer[] pressMeshes;
    [SerializeField] BoxCollider pressCollider;
    bool StartPosFounded;
    Vector3 StartTransformPosition;
    float pressFindSpeed = 100;
    float pressActivatedSpeed = 2;
    bool DestroyingBlocksState;

    private void Update()
    {
        if (StartPosFounded == false)
        {
            FindStartPosition();
        }
        else
        {
            rb.velocity = Vector3.down * pressActivatedSpeed;
            if (transform.position.y <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (StartPosFounded == false)
        {
            StartCoroutine(ActivateColliderWithDelay());
            pressCollider.size = new Vector3(1, 1, 1);
            StartPosFounded = true;
            StartTransformPosition = transform.position;
            Debug.Log("Start position:" + StartTransformPosition);
            Debug.Log("Start position");
            rb.velocity = Vector3.zero;
            gameObject.transform.position = StartTransformPosition + new Vector3(0, 10, 0);
            DestroyingBlocksState = true;
            for (int i = 0; i < pressMeshes.Length; i++)
            {
                pressMeshes[i].enabled = true;
            }
        }
    }
    IEnumerator ActivateColliderWithDelay()
    {
        yield return new WaitForSeconds(0.4f);
        pressCollider.isTrigger = false;
    }
    private void OnCollisionEnter(Collision collision)
    {

        var DestrCol = collision.gameObject.GetComponent<DestroyCollision>();
        if (DestrCol != null)
        {
            DestrCol.TakeDamage(DestrCol.transform.position + new Vector3(2, 2, 2));
        }
        if (collision.gameObject.CompareTag("Undestructable"))
        {
            Destroy(gameObject);
        }
    }
    private void FindStartPosition()
    {
        rb.velocity = Vector3.down * pressFindSpeed;
    }
}
