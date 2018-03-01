using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	public GameObject projectile;
	public AudioClip fireSound;
	public float padding = 1f;
	public float projectileSpeed = 10;
	public float firingRate;
	public float health = 300f;
    bool moveLeft = false;
    bool moveRight = false;
    [SerializeField]
    GameObject livesParent;
    [SerializeField]
    GameObject heart;
    public List<GameObject> lifeList;

    float xmin;
	float xmax;
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			Debug.Log ("Player Collided with missile");
			health -= missile.GetDamage();
			missile.Hit();

            int index = lifeList.Count - 1;
            Destroy(lifeList[index]);
            lifeList.RemoveAt(index);

            if (health <= 0) {
				Die();
			}
		}
	}
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Loss Screen");
        for(int i = 0; i < 3; i++)
        {
            Destroy(ScoreKeeper.scoreObjects[i]);
        }
		Destroy(gameObject);
	}
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;

        if(lifeList != null && lifeList.Count > 0)
        {
            foreach (GameObject g in lifeList)
            {
                Destroy(g);
            }
        }

        lifeList = new List<GameObject>();
        int lives = (int)health / 100;
        for (int i = 0; i < lives; i++)
        {
            Vector3 position = livesParent.transform.position;
            position.x += i * (Screen.width)/18f;
            GameObject life = Instantiate(heart, position, new Quaternion(), livesParent.transform);
            lifeList.Add(life);
        }
	}
	
	void Fire(){
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position, MusicPlayer.soundSliderValue);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
            fire();
		}
		if(Input.GetKeyUp(KeyCode.Space)){
            cancelFire();
        }
		if(Input.GetKey(KeyCode.LeftArrow) || moveLeft)
        {
            goLeft();
		}
        else if (Input.GetKey(KeyCode.RightArrow) || moveRight)
        {
            goRight();
		}
		
		// restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

    public void fire()
    {
        InvokeRepeating("Fire", 0.0001f, 0.5f);
    }

    public void cancelFire()
    {
        CancelInvoke("Fire");
    }

    public void goLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public void goRight()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void left(bool b)
    {
        if(b)
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
    }

    public void right(bool b)
    {
        if (b)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
    }
}
