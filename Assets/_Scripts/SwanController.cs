using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanController : MonoBehaviour {
    public Rigidbody2D rigidBody;
    public float maxTimeBetweenSpanws =8;
    public float minTimeBetweenSpanws = 3;
    public float rightSpawnPosX;
    public float leftSpawnPosX;
    public float maxSpawnPoxY;
    public float minSpawnPoxY;
    public float minSpeed;
    public float maxSpeed;
	// Use this for initialization
	void Start () {
        Random.InitState(System.DateTime.Today.Millisecond);
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        float waitTime = Random.Range(minTimeBetweenSpanws, maxTimeBetweenSpanws);
        yield return new WaitForSeconds(waitTime);
        float temp = Random.Range(0f, 2f);
        bool facingLeft = (int)temp == 0;
        float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;
        float posY = Random.Range(minSpawnPoxY, maxSpawnPoxY);
        Vector3 spawnPos = new Vector3(posX, posY,transform.position.z);
        Rigidbody2D porpInstance = Instantiate<Rigidbody2D>(rigidBody, spawnPos, Quaternion.identity);
        if (!facingLeft)
        {
            Vector3 scale = porpInstance.transform.localScale;
            scale.x *= -1f;
            porpInstance.transform.localScale = scale;
        }
        float speed = Random.Range(minSpeed, maxSpeed);
        speed *= facingLeft ? -1f : 1f;
        porpInstance.velocity = new Vector2(speed, 0);
        StartCoroutine(spawn());
        while (porpInstance!=null)
        {
            if (facingLeft)
            {
                if(porpInstance.transform.position.x< leftSpawnPosX - 0.5f)
                {
                    Destroy(porpInstance.gameObject);
                }
            }
            else
            {
                if (porpInstance.transform.position.x > rightSpawnPosX + 0.5f)
                {
                    Destroy(porpInstance.gameObject);
                }
            }
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
