using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [Serializable]
    public class Ship : MonoBehaviour
    {
        public Sprite shipSprite;
        public string shipName;
        public int shipMaxHealth;
        public int shipFirePower;
        public float shipSpeed;


        public Sprite GetShipSprite()
        {
            return this.shipSprite;
        }

        public string GetShipName()
        {
            return this.shipName;
        }

        public int GetShipMaxHealth()
        {
            return this.shipMaxHealth;
        }

        public int GetShipFirePower()
        {
            return this.shipFirePower;
        }

        public float GetShipSpeed()
        {
            return this.shipSpeed;
        }

        public Ship Clone()
        {
            return (Ship)this.MemberwiseClone();
        }

    }
}
