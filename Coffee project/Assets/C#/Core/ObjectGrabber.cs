using UnityEngine; 
public class ObjectGrabber : MonoBehaviour
{
    # region Fields
        private readonly float minGrabDistance = 1;
        private readonly float maxGrabDistance = 2;
        private readonly float forceDamping = 50;
        private readonly float linearDragWhenGrabbed = 10;
        private readonly float angularDragWhenGrabbed = 10;
     
        private readonly float rotateTorque = 2000;
        private readonly float maxGrabReleaseDistance = 3f;
       
 
        private Rigidbody currentObject;
        private float currentGrabDistance;
        private Camera camera;
        private float originalLinearDrag;
        private float originalAngularDrag;
        private bool isGrabbing = false;

        private UIManager uimanager;
    # endregion

       
        void Start()
        {
            currentGrabDistance = 2;
 
            uimanager = FindObjectOfType<UIManager>();
            
            camera = Camera.main;
        }
 
        
 
        void Update()
        { 
            if (currentObject != null)
            {
                WheelRotated();
                Rotation();
            }       
 
            if (Input.GetMouseButtonDown(1)) TryToGrab();
            

            if (isGrabbing && currentObject != null)
            {
                if (Vector3.Distance(camera.transform.position, currentObject.position) > maxGrabReleaseDistance)
                {
                    StopGrabbing();
                    return;
                }
            }
            
            if ((Input.GetMouseButtonUp(1)) && isGrabbing)   StopGrabbing();

            if (Input.GetMouseButtonDown(0) && isGrabbing)  currentGrabDistance = Mathf.Max(3, currentGrabDistance - 1f);
 
            if (Input.GetMouseButtonUp(0) && isGrabbing)    Throw();
 
        }

        void FixedUpdate()
        {
            if (isGrabbing && currentObject != null) MovePhysics();
        }
        void MovePhysics()
        {
            if (currentObject == null) return;
 
            Vector3 targetPoint = camera.transform.position + camera.transform.forward * currentGrabDistance;
            Vector3 toTarget = targetPoint - currentObject.worldCenterOfMass;
            Vector3 force = toTarget * forceDamping;
 
            currentObject.AddForce(force, ForceMode.Force);
            currentObject.angularVelocity = Vector3.Lerp(currentObject.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 3f);
        }
        void WheelRotated()
        {
            float scroll = Input.mouseScrollDelta.y;

            if (scroll != 0f) currentGrabDistance = Mathf.Clamp(currentGrabDistance + scroll, minGrabDistance, maxGrabDistance);
              
        }
        void TryToGrab()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 6f))
            {
                Rigidbody rb = hit.rigidbody;
                if (rb != null && !rb.isKinematic)
                {
                    uimanager.ShowObjectTips(); 
                    uimanager.ShowInteractImage();
                    currentObject = rb;
                    isGrabbing = true;
 
                    originalLinearDrag = rb.linearDamping;
                    originalAngularDrag = rb.angularDamping;
 
                    rb.linearDamping = linearDragWhenGrabbed;
                    rb.angularDamping = angularDragWhenGrabbed;
                    rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    rb.interpolation = RigidbodyInterpolation.Interpolate;
                  
                }
            }
        }
        void Rotation()
        {   
 
            if (Input.GetKey(KeyCode.Z))currentObject.AddTorque(camera.transform.right * -rotateTorque* Time.deltaTime, ForceMode.Force);
            if (Input.GetKey(KeyCode.X)) currentObject.AddTorque(camera.transform.right * rotateTorque* Time.deltaTime, ForceMode.Force);
          
            if (Input.GetKey(KeyCode.Q))currentObject.AddTorque(Vector3.up * -rotateTorque * Time.deltaTime, ForceMode.Force);

            if (Input.GetKey(KeyCode.E))currentObject.AddTorque(Vector3.up * rotateTorque* Time.deltaTime, ForceMode.Force);
        }
 
        void StopGrabbing()
        {
            if (currentObject != null)
            {
                currentObject.linearDamping = originalLinearDrag;
                currentObject.angularDamping = originalAngularDrag;
                currentObject = null;
                uimanager.HideInteractImage();
                uimanager.HideObjectTips();
            }
            isGrabbing = false;
        }
 
        void Throw()
        {
            if (currentObject != null)
            {
                currentObject.linearDamping = originalAngularDrag;
                currentObject.angularDamping = originalAngularDrag;
 
                Vector3 directionToThrow = camera.transform.forward;
                currentObject.AddForce(directionToThrow * 25, ForceMode.Impulse); // Throw force
                currentObject = null;
                uimanager.HideInteractImage();
                uimanager.HideObjectTips();
            }
            isGrabbing = false;
        }
 
    }
 

