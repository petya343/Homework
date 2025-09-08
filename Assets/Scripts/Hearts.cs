using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour, IHearts
{
    [SerializeField]
    private Sprite full_heart;
    [SerializeField]
    private Sprite lost_heart;
    private Image[] children;
    private int lives = 3;

    private void Start()
    {
        children = GetComponentsInChildren<Image>();
    }
    public void SetLives(int lives)
    {
        this.lives = lives;
        UpdateLives();
    }

    private void UpdateLives()
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].sprite = i < lives ? full_heart : lost_heart;
        }
    }
}
