using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetButtonSkill : MonoBehaviour
{
    public Image img;

    public Sprite[] sprt;

    public SetGifts gift;

    public PaiAtributosEdit meuNumero;

    void Update()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                img.sprite = sprt[0];
                PlayerPrefs.SetInt("P1ButtonA", gift.skill);
                StartCoroutine("GO");
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                img.sprite = sprt[1];
                PlayerPrefs.SetInt("P1ButtonB", gift.skill);
                StartCoroutine("GO");
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                img.sprite = sprt[2];
                PlayerPrefs.SetInt("P1ButtonX", gift.skill);
                StartCoroutine("GO");
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                img.sprite = sprt[3];
                PlayerPrefs.SetInt("P1ButtonY", gift.skill);
                StartCoroutine("GO");
            }
        }
        else if (SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                img.sprite = sprt[0];
                PlayerPrefs.SetInt("P2ButtonA", gift.skill);
                StartCoroutine("GO");
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
            {
                img.sprite = sprt[1];
                PlayerPrefs.SetInt("P2ButtonB", gift.skill);
                StartCoroutine("GO");
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button2))
            {
                img.sprite = sprt[2];
                PlayerPrefs.SetInt("P2ButtonX", gift.skill);
                StartCoroutine("GO");
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
            {
                img.sprite = sprt[3];
                PlayerPrefs.SetInt("P2ButtonY", gift.skill);
                StartCoroutine("GO");
            }
        }
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(1);
        gift.atributo.enabled = true;
        gift.select.SetActive(false);

    }
}
