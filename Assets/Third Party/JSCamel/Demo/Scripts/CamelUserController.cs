using UnityEngine;
using System.Collections;

public class CamelUserController : MonoBehaviour {
    CamelCharacter camelCharacter;

    void Start()
    {
        camelCharacter = GetComponent<CamelCharacter>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            camelCharacter.Attack();
        }
        if (Input.GetButtonDown("Jump"))
        {
            camelCharacter.Jump();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            camelCharacter.Hit();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            camelCharacter.Death();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            camelCharacter.Rebirth();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            camelCharacter.EatStart();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            camelCharacter.EatEnd();
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            camelCharacter.Gallop();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            camelCharacter.Canter();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            camelCharacter.Trot();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            camelCharacter.Walk();
        }

        camelCharacter.forwardSpeed = camelCharacter.maxWalkSpeed * Input.GetAxis("Vertical");
        camelCharacter.turnSpeed = Input.GetAxis("Horizontal");
    }
}
