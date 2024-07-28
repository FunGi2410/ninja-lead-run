using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	[SerializeField] private bool rotate;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private uint amount;

	[SerializeField] private AudioClip collectSound;
	[SerializeField] private GameObject collectEffect;

	void Update()
	{
		if (rotate)
			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bike")
		{
			Collect();
		}
	}

	public void Collect()
	{
		if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if (collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		Bike.Instance.CollectCoin(this.amount);

		Destroy(gameObject);
	}
}
