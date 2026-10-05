using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController Controller;

    #region Camera rotation 
    private float sensitivity = 2.5f;
    private float limitForRotation = 70;
	private Vector2 rotation = Vector2.zero;
    private Camera mainCamera;
    #endregion

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainCamera = Camera.main;
    }
    public void Update()
    {
        Vector3 move = new Vector3(-Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, -Input.GetAxisRaw("Vertical") * Time.deltaTime);

        move = this.transform.TransformDirection(move);
        Controller.Move(move * 4); // speed
     

        // Camera rotation + player rotation (X axes)
        rotation.x += Input.GetAxis("Mouse X") * sensitivity;
		rotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
		rotation.y = Mathf.Clamp(rotation.y, -limitForRotation, limitForRotation);

		var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left) ;

		mainCamera.transform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.zero) * yQuat * Quaternion.Euler(0, 180, 0);

        transform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.zero) * xQuat ;
    }
  
}
