using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPathing : MonoBehaviour
{
    List<Transform> Waypoints;//try this with GameObject variable insted of Transform
    float moveSpeed;
    WaveConfig waveConfig;

    int waypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = waveConfig.GetMoveSpeed();

        Waypoints = waveConfig.GetWavePoints();
        transform.position = Waypoints[waypointIndex].transform.position;
  
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= Waypoints.Count - 1)
        {
            var targetPosition = Waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
