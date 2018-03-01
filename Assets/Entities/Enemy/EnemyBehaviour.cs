using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float health = 150f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
        GameObject score = GameObject.Find("Score");
        if (score != null)
		    scoreKeeper = score.GetComponent<ScoreKeeper>();
	}

	void Update(){
		float prob = shotsPerSecond * Time.deltaTime;
		if(Random.value < prob){
			Fire ();
		}
	}
	
	void Fire(){
		GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position, MusicPlayer.soundSliderValue);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die(){

		AudioSource.PlayClipAtPoint(deathSound, transform.position, MusicPlayer.soundSliderValue);
        scoreKeeper.Score(scoreValue);
        var exp = gameObject.transform.parent.gameObject.GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject);
    }
}
