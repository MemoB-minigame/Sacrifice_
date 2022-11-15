using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //CurrentWeapon
    public GameObject[] weapons;
    public int weaponNum;
    private void Start()
    {
        WeaponInitialize();
    }
    void Update()
    {
        ChangeWeapon();
    }
    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weapons[weaponNum].SetActive(false);
            weaponNum = --weaponNum < 0 ? weapons.Length - 1 : weaponNum;
            weapons[weaponNum].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            weapons[weaponNum].SetActive(false);
            weaponNum = ++weaponNum > weapons.Length - 1 ? 0 : weaponNum;
            weapons[weaponNum].SetActive(true);
        }
    }
    private void ChangeWeapon(int num)
    {
        weapons[weaponNum].SetActive(false);
        weaponNum = num;
        weapons[weaponNum].SetActive(true);
    }
    private void WeaponInitialize()
    {
        for(int i = 0; i < weapons.Length; i++)
            if(i!=weaponNum) weapons[i].SetActive(false);
        weapons[weaponNum].SetActive(true);
    }

    public Gun GetActiveWeapon() { return weapons[weaponNum].GetComponent<Gun>(); }
}
