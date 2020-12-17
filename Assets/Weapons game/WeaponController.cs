using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //weapon attributes
    public int balasTotales;
    public int balasCargador;
    public int balasCargadorArma;
    public float range = 10f;
    public float daño = 25f;
    public GameObject effect;
    public GameObject bloodEffect;
    public AudioSource fire, reload;

    int balasRecarga;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Aim") && Input.GetButtonDown("Fire") && balasCargador > 0)
        {
            fire.Play();
            Shot();
        }

        if (Input.GetButtonDown("Reload") && balasTotales > 0)
        {
            reload.Play();
            Reload();
        }
        if (balasCargador <= 0 && balasTotales > 0)
        {
            reload.Play();
            Reload();
        }
    }
    void Shot()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, range))
        {
            Debug.Log(hit.collider.name);
            
            if(hit.transform.CompareTag("Enemy"))
            {

                GameObject _effect = Instantiate(bloodEffect, hit.point, Quaternion.identity);
                Destroy(_effect, 0.3f);

                Vida vida = hit.transform.GetComponent<Vida>();
                if (vida == null)
                {
                    throw new System.Exception("No se encontro el componente Vida del Enemigo");
                }
                else
                {
                    vida.RecibirDaño(daño);
                }
            }
            else
            {
                GameObject _effect = Instantiate(effect, hit.point, Quaternion.identity);
                Destroy(_effect, 0.5f);
            }
        }

        balasCargador--;

    }

    void Reload()
    {
        // count balasCargador reloaded
        balasRecarga = balasCargadorArma - balasCargador;
        if(balasTotales< balasCargadorArma)
        {
            balasCargador += balasTotales;
                balasTotales = 0;
        }
        else
        {
            balasCargador = balasCargadorArma;
        }
        //control for negative balastotales
        if (balasTotales > 0)
        {
            balasTotales -= balasRecarga;
        }
        else
        {
        balasTotales = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }
}
