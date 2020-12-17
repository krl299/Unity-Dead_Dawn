using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScreen : MonoBehaviour
{
    public Text bulletText;
    public WeaponController weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = weapon.balasTotales + "\n" + weapon.balasCargador + "/" + weapon.balasCargadorArma;
    }
}
