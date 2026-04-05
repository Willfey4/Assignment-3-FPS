using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour 
{
	public float speed = 10.0f;
	public int damage = 1;

    void Start()
    {
        StartCoroutine("SelfDestruct");
        //GM = GameObject.Find("GameManager");
    }

	void OnTriggerEnter(Collider other) 
    {
		

        if (other.gameObject.tag == "enemy") //&& 
            //collisionInfo.impactForceSum.magnitude > targetThresh)
		{
            GameObject enemy = other.gameObject;
            WanderingAI ai = enemy.GetComponent<WanderingAI>();
            if (ai != null && ai.IsAlive())
            {
                ai.SetAlive(false);
                //if (GM != null) GM.SendMessage("EnemyHit");
                enemy.SendMessage("ReactToHit");
            }
            
            Destroy(gameObject);
        }

        else
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player != null) 
            {
                player.Hurt(damage);
            }
            Destroy(this.gameObject);
        }


	}
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
