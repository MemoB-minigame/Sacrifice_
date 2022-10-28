using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallHP : MonoBehaviour
{
    private RectTransform smallHP_Parent_RectTransform;

    private Image smallHPBackground;
    private Image smallHPSlider;

    private Transform player_Transform;

    [SerializeField] private float alphaFadeOutSpeed = 1;
    private float timer;
    private int player_HP;

    public int Player_HP
    {
        set
        {
            player_HP = value;
            //hpNum.text = string.Format("{0:D2}", hp);
            smallHPSlider.fillAmount = 1 / 24.0f * player_HP;

            smallHPShow();
        }
    }

    void Awake()
    {
        smallHP_Parent_RectTransform = gameObject.GetComponent<RectTransform>();

        smallHPBackground = smallHP_Parent_RectTransform.Find("HPBackground").GetComponent<Image>();
        smallHPSlider = smallHP_Parent_RectTransform.Find("HPBackground/HPSlider").GetComponent<Image>();

        player_Transform = GameObject.Find("Player").GetComponent<Transform>();


    }

    void Update()
    {
        smallHP_Parent_RectTransform.position = new Vector3(Camera.main.WorldToScreenPoint(player_Transform.position).x, Camera.main.WorldToScreenPoint(player_Transform.position).y + 105, Camera.main.WorldToScreenPoint(player_Transform.position).z);

        timer += Time.deltaTime;
        if (timer >= 2 && smallHPBackground.color.a > 0 && smallHPSlider.color.a > 0)
        {
            smallHPFadeOut();
        }
    }

    private void smallHPShow()
    {
        timer = 0;

        smallHPBackground.color = new Color(smallHPBackground.color.r, smallHPBackground.color.g, smallHPBackground.color.b, 1);
        smallHPSlider.color = new Color(smallHPSlider.color.r, smallHPSlider.color.g, smallHPSlider.color.b, 1);
    }

    private void smallHPFadeOut()
    {
        smallHPBackground.color = new Color(smallHPBackground.color.r, smallHPBackground.color.g, smallHPBackground.color.b, smallHPBackground.color.a - alphaFadeOutSpeed * Time.deltaTime);
        smallHPSlider.color = new Color(smallHPSlider.color.r, smallHPSlider.color.g, smallHPSlider.color.b, smallHPSlider.color.a - alphaFadeOutSpeed * Time.deltaTime);
    }
}
