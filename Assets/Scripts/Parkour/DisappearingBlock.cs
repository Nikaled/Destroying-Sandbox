using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    [SerializeField] MeshRenderer BlockMesh;
    [SerializeField] Material DisappearingMaterial;
    [SerializeField] BoxCollider BlockCollider;
    public float TimeToDisappear=10;
    private bool IsDisappearing;
    public float TimeToAppearAgain;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
       
            if(IsDisappearing == false)
            {
                IsDisappearing = true;
                StartCoroutine(Disappearing());
            }
        }
    }
    private IEnumerator Disappearing()
    {
        Material matInstance = Instantiate(DisappearingMaterial);
        BlockMesh.materials = new Material[2] { BlockMesh.materials[0], matInstance };
        StartCoroutine(TurnWhiteCor(matInstance));
        IEnumerator TurnWhiteCor(Material platformMaterial)
        {
            float CurrentAlpha = 0;
            float step = 0.05f;
            float stepCount = 1 / step;
            float stepTime = TimeToDisappear / stepCount; 
            while (CurrentAlpha < 1)
            {
                yield return new WaitForSeconds(stepTime);
                CurrentAlpha += step;
                Debug.Log("PlatformAlpha:" + CurrentAlpha);
                ChangeAlphaOnMaterial(platformMaterial, CurrentAlpha);
            }
        BlockCollider.enabled = false;
            BlockMesh.enabled = false;
            StartCoroutine(WaitForAppear());
        }
        yield return null;
    }
    private IEnumerator WaitForAppear()
    {
        yield return new WaitForSeconds(TimeToAppearAgain);
        ChangeAlphaOnMaterial(BlockMesh.materials[1],0);
        BlockCollider.enabled = true;
        BlockMesh.enabled = true;
        IsDisappearing = false;
    }
    private void ChangeAlphaOnMaterial(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);

    }
}
