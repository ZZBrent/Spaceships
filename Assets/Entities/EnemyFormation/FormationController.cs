using UnityEngine;
using System.Collections;
using System;

public class FormationController : MonoBehaviour {
	public GameObject[] enemyPrefabs;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	public float padding = 1;
	
	private bool movingRight = true;
	private float xmax;
	private float xmin;
	
	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xmax = rightEdge.x;
		xmin = leftBoundary.x;
		SpawnUntilFull();
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
            GameObject enemyPrefab = new GameObject();
            float enemyChoice = UnityEngine.Random.value;
            for(int i = enemyPrefabs.Length - 1; i >= 0; i--)
            {
                if(enemyChoice <= 1/Mathf.Pow(3, i))
                {
                    enemyPrefab = enemyPrefabs[i];
                    break;
                }
            }
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}else{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

    	// Check if the formation is going outside the playspace...
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		if(leftEdgeOfFormation < xmin)
        {
			movingRight = true;
		}
        else if(rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
		
		if(AllMembersDead()){
			Debug.Log("Empty Formation");
			SpawnUntilFull();
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
	
}
