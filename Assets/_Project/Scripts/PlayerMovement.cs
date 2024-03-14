using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	public Rigidbody rb;

	void Update()
	{
		GetMovementInput();
	}

	public void GetMovementInput()
	{
		////Obtenemos los movimientos del jugador
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

		//Aplicamos el normalized para que los diagonales no sean m�s r�pido
		//Vector2 inputDirection = playerActions.Player.Move.ReadValue<Vector2>();
		//Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

		MovePlayer(direction);
	}

	private void MovePlayer(Vector3 direction)
	{
		rb.angularVelocity = Vector3.zero;
		rb.velocity = direction * speed;
	}

	//private void StopMovement()
	//{
	//	rb.velocity = Vector3.zero;

	//	//Evitamos que se mueva si ha chocado con otro objeto con colliders y rigidiBody
	//	rb.angularVelocity = Vector3.zero;
	//}
}