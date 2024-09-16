using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] GameObject PlanePrefab;
    GameObject currentPlane;
    public AudioSource FireAndExplodeSoundSource;
    public AudioSource FlySoundSource;
    public static PlaneManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Vector3 PlaneUp = new Vector3(0, 50, 0);
        currentPlane = Instantiate(PlanePrefab, PlaneUp, Quaternion.identity);
        Player.instance.gameObject.SetActive(false);
        currentPlane.GetComponent<PlaneWeapon>().planeManager = this;
        FireAndExplodeSoundSource = currentPlane.GetComponent<PlaneWeapon>().FireAndExplodeSoundSource;
        FlySoundSource = currentPlane.GetComponent<PlaneWeapon>().FlySoundSource;
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { currentPlane.GetComponent<PlaneWeapon>().Fire(); });
        }
    }

    public void DisableSounds(bool Is)
    {
        if (FireAndExplodeSoundSource != null)
        {
        FireAndExplodeSoundSource.gameObject.SetActive(Is);
        }
        if(FlySoundSource != null)
        {
        FlySoundSource.gameObject.SetActive(Is);
        }
    }
    private void OnDisable()
    {
        if(currentPlane != null)
        {
        Destroy(currentPlane);
        }
        Player.instance.gameObject.SetActive(true);
        //Player.instance.examplePlayer.LockCursor(true);
    }
    public void OnPlaneDestroyed()
    {
        Player.instance.SwitchWeapon(1);
    }
    void Update()
    {
        Player.instance.ChangeWeaponInput();
    }
}
