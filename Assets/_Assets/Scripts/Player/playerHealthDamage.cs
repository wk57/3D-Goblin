using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealthDamage : MonoBehaviour
{
    [SerializeField] int playerHealth = 100;
    [SerializeField] bool invencible = false;
    public float invulnerabilityTime = 1;
    public float stopSpeed = 0.2f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void DamageTaken(int damage)
    {
        if (!invencible && playerHealth > 0) 
        {
            playerHealth -= damage;
            anim.Play("Damage");
            StartCoroutine(Invulnerability());
            StartCoroutine(StopPlayerVelocity());

            if (playerHealth == 0)
            {
                GameOver();
            }
        }
                
    }

    void GameOver()
    {
        Debug.Log("caca");
        Time.timeScale = 0;
    }

    IEnumerator Invulnerability()
    {
        invencible = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        invencible = false;
    }


    IEnumerator StopPlayerVelocity()
    {
        var actuaPlayerSpeed = GetComponent<PlayerController>().speed;
        GetComponent<PlayerController>().speed = 0;

        yield return new WaitForSeconds(stopSpeed);
        GetComponent<PlayerController>().speed = actuaPlayerSpeed;


    }




}
