using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour {

    [SerializeField]
    private Player _player;

    public GameObject[] Guns;

    public void BuyWeapon(int gun)
    {
        _player.Guns.Add(Guns[gun]);
    }
}
