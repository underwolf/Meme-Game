using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerUI : MonoBehaviour
{
    public Sprite[] characters;
    public Image characterActive, nextCharacter;


    public void SetCharacter(int CharacterSelected,int NextCharacter)
    {
        characterActive.sprite = characters[CharacterSelected];
        nextCharacter.sprite = characters[NextCharacter];
    }
}
