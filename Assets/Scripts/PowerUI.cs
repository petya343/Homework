using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }
    public void GetPower()
    {
       image.color = new Color(1f, 1f, 1f, 1f);
    }
    public void LosePower()
    {
        image.color = new Color(1f, 1f, 1f, 0f);
    }
}
