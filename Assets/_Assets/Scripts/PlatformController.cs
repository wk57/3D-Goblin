using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] Rigidbody platformRb;
    [SerializeField] float platformSpeed;
    [SerializeField] Transform[] platformPositions;
    [SerializeField] bool moveNext = true;
    [SerializeField] float waitTime;


    private int actualPosition = 0;
    private int nextPosition = 1;


    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (moveNext)
        {
            StopCoroutine(WaitForMovement(0));
            platformRb.MovePosition(Vector3.MoveTowards(platformRb.position, platformPositions[nextPosition].position, platformSpeed * Time.deltaTime));
        }


        if (Vector3.Distance(platformRb.position, platformPositions[nextPosition].position) <= 0)
        {
            StartCoroutine(WaitForMovement(waitTime));
            actualPosition = nextPosition;
            nextPosition++;
            if (nextPosition > platformPositions.Length - 1)
            {
                nextPosition = 0;
            }
        }
    }

    //corrutina
    IEnumerator WaitForMovement(float time)
    {
        moveNext = false;
        yield return new WaitForSeconds(time); //toda corrutina tiene que devolver con un yield return
        moveNext = true;

    }
}
