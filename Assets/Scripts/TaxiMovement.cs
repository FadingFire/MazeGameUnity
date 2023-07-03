using UnityEngine;

public class TaxiMovement : MonoBehaviour {

	public float moveSpeed = 5f;
	public GameObject EndPoint;

	private float maxSpeed = 10f;
	private float curSpeed;
	private Rigidbody rb;
	private int progress;
 
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
 
	void FixedUpdate()
    {
        curSpeed = moveSpeed;
        maxSpeed = curSpeed;
    
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
    
        rb.velocity = new Vector3(
            horizontalInput * curSpeed,
            rb.velocity.y,
            verticalInput * curSpeed
        );
    
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);
            Quaternion targetRotation = Quaternion.LookRotation(-direction); // Invert the direction
            transform.rotation = targetRotation;
        }
    }
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Maze"){
			Destroy(other.gameObject);
		}
		progress++;
		if (progress == 3)
		{
			EndPoint.SetActive(true);
		}
		if (progress == 4)
		{
			Application.Quit();
		}
	}
}
