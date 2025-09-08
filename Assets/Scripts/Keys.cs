using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keys : MonoBehaviour
{
    [SerializeField]
    private Sprite full_key;
    private Image[] children;

    private int keys = 0;

    private void Start()
    {
        children = GetComponentsInChildren<Image>();
    }
    public void SetKeys(int keys)
    {
        this.keys = keys;
        UpdateKeys();
    }

    private void UpdateKeys()
    {
        for (int i = 0; i < children.Length; i++)
        {

                Color current = children[i].color;
                current.a = i < keys ? 1f : 0.3f;
                children[i].color = current;
        }
    }
}
