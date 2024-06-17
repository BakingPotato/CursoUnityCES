using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody rb;

	[Header("Speeds")]
	public float normal_speed = 8;
	public float extra_speed = 12;
	private float actual_speed;

	[Header("Impulse")]
	public float impulse_duration = 0.75f;

	private Vector3 direction;

	public MeshRenderer mr;
	bool blink = false;
	Coroutine blinking = null;


	Coroutine impulse = null;

    private void Start()
    {
		actual_speed = normal_speed;
    }

    void Update()
	{
		GetMovementInput();
	}

	public void GetMovementInput()
	{
		////Obtenemos los movimientos del jugador
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		direction = new Vector3(horizontal, 0, vertical).normalized;

		//Aplicamos el normalized para que los diagonales no sean m�s r�pido
		//Vector2 inputDirection = playerActions.Player.Move.ReadValue<Vector2>();
		//Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

		MovePlayer(direction);
	}

	private void MovePlayer(Vector3 direction)
	{
		rb.angularVelocity = Vector3.zero;
		rb.velocity = direction * actual_speed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
			if (impulse != null)
				StopCoroutine(impulse);
			impulse = StartCoroutine(impulseCouroutine());
		}
	}

	IEnumerator  impulseCouroutine()
    {
		actual_speed = extra_speed;
		yield return new WaitForSeconds(impulse_duration);
		actual_speed = normal_speed;
		impulse = null;
	}

	//private void StopMovement()
	//{
	//	rb.velocity = Vector3.zero;

	//	//Evitamos que se mueva si ha chocado con otro objeto con colliders y rigidiBody
	//	rb.angularVelocity = Vector3.zero;
	//}

	IEnumerator blinkRoutine()
	{
		while (blink)
		{
            mr.enabled = true;
			yield return new WaitForSeconds(0.2f);
            mr.enabled = false;
            yield return new WaitForSeconds(0.2f);
        }
        mr.enabled = true;
    }

	public void startBlinking()
	{
		blink = true;
		blinking = StartCoroutine(blinkRoutine());
	}

    public void stopBlinking()
    {
        blink = false;
    }

}