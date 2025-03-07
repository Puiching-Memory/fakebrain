using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBox : MonoBehaviour, IClick
{
    public GameObject postCard;


    public void ClickEvent()
    {
        postCard.SetActive(true);
        gameObject.SetActive(false);
    }
}
