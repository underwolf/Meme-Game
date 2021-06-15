using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class lifeManager : MonoBehaviour
{

    public Image[] coracao;

    public void destroyHeart(int vida)
    {
        Destroy(coracao[vida].gameObject);
    }
}
