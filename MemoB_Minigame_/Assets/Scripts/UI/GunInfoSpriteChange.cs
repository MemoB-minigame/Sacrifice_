using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunInfoSpriteChange : MonoBehaviour
{
    public Image gunInfoImage;

    private Dictionary<string, Sprite> gunInfoImageDic;

    private WeaponManager weaponManager;

    void Awake()
    {
        weaponManager = GameObject.Find("Player/Weapons").GetComponent<WeaponManager>();

        gunInfoImageDic = new Dictionary<string, Sprite>();

        gunInfoImageDic = GetGunInfoImage();
    }

    void Update()
    {
        if (gunInfoImage.sprite.name != weaponManager.weapons[weaponManager.weaponNum].name)
        {
            Sprite temp = null;
            gunInfoImageDic.TryGetValue(weaponManager.weapons[weaponManager.weaponNum].name, out temp);
            gunInfoImage.sprite = temp;
        }
    }

    private Dictionary<string, Sprite> GetGunInfoImage()
    {
        Dictionary<string, Sprite> gunInfoImageDic = new Dictionary<string, Sprite>();

        Sprite[] spriteArr = Resources.LoadAll<Sprite>("GunInfoGunImage");

        for (int i = 0; i < spriteArr.Length; i++)
        {
            gunInfoImageDic.Add(spriteArr[i].name, spriteArr[i]);
        }

        return gunInfoImageDic;
    }
}
