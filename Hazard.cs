using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hazard : MonoBehaviour
    {
        [SerializeField] public int hp;
        [SerializeField] public float speed;
        [SerializeField] public int scoreValue;
        [SerializeField] public int expValue;
        [SerializeField] public Rigidbody2D ridgidBody;
        [SerializeField] public GameController gameController;
        [SerializeField] public GameObject explosionAnimation;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = gameController.GetPlayer();

            if (other.tag == "Killbox")
            {
                return;
            }
            else if (other.tag == "Astroid")
            {
                hp--;
                return;
            }
            else if (other.tag == "Player")
            {
                //player.SubHealth(25);
                return;

            }
            else if (other.tag == "Enemy Laser")
            {
                return;
            }
            else if (other.tag == "Laser")
            {
                Destroy(other.gameObject);
                hp -= player.GetFirePower();
            }
            else
            {
                return;
            }
        }

        public void GetGameControllerFromTag()
        {
            GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
            gameController = (GameController) gameControllerObject.GetComponent(typeof(GameController));
        }
    }
}