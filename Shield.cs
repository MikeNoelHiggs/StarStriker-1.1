using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Sprite[] shieldSprites = new Sprite[3];
    [SerializeField] private int shieldHP = 100;
    [SerializeField] private int str = 1;
    private bool active = false;
    private Color defaultColor = new Color(0, 257, 255, 253);

    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (shieldHP <= 100)
        {
            ChangeSprite(0);
        }
        else if (shieldHP <= 200)
        {
            ChangeSprite(1);
        }
        else if (shieldHP > 200)
        {
            ChangeSprite(2);
        }

        if (shieldHP < 0)
        {
            shieldHP = 0;
        }
    }

    public void GenerateShield()
    {
        if (active == false)
        {
            if (str >= 3)
            {
                Generate(3);
            }
            else if (str == 2)
            {
                Generate(2);
            }
            else
            {
                Generate(1);
            }

        }
        void Generate(int str)
        {
            shieldHP = str * 100;
            active = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = shieldSprites[str - 1];
            ResetColor();  
        }
    }

    public void Deactivate()
    {
        active = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetShieldStr(int i)
    {
        if (i <= 1)
        {
            str = 1;
        }

        else if (i == 2)
        {
            str = 2;
        }

        else
        {
            str = 3;
        }
    }

    public bool GetActive()
    {
        return active;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active)
        {
            return;
        }
        if (collision.tag == "Enemy Laser" || collision.tag == "Enemy" || collision.tag == "Astroid" || collision.tag == "E-Missile")
        {
            shieldHP -= 10 * ActivePlayerProfile.selectedStage;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Invoke("ResetColor", .2f);

            if (collision.tag == "Enemy Laser")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.tag == "E-Missile")
                {
                    shieldHP -= 25;
                }

            if (shieldHP <= 0)
            {
                Deactivate();
            }

        }
    }

    
    private void ChangeSprite(int i)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = shieldSprites[i];
    }

    private void ResetColor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
    }

    public int GetShieldHP()
    {
        return this.shieldHP;
    }

}
