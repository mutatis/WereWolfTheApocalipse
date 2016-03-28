using UnityEngine;
using System.Collections;

public class SetButtonSkill : MonoBehaviour
{
    public SetGifts gift;

    public PaiAtributosEdit meuNumero;

    void Update()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                PlayerPrefs.SetInt("P1ButtonA", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                PlayerPrefs.SetInt("P1ButtonB", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                PlayerPrefs.SetInt("P1ButtonX", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                PlayerPrefs.SetInt("P1ButtonY", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
        }
        else if (SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                PlayerPrefs.SetInt("P2ButtonA", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
            {
                PlayerPrefs.SetInt("P2ButtonB", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button2))
            {
                PlayerPrefs.SetInt("P2ButtonX", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
            {
                PlayerPrefs.SetInt("P2ButtonY", gift.skill);
                gift.atributo.enabled = true;
                gift.select.SetActive(false);
            }
        }
    }
}
