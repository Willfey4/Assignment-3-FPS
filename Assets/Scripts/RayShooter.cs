using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour 
{
	public Transform origin;
    private Camera _camera;
    private LineRenderer line;
    private bool cursorLocked = false;
    private Animator animator;
    public GameObject hand;

	void Start() 
    {
		_camera = GetComponent<Camera>();
        line = GetComponent<LineRenderer>();
        animator = hand.GetComponent<Animator>();
        LockCursor();	
	}


    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update() 
    {
        
		if (Input.GetMouseButtonDown(0) /* || Input.GetButton("Fire1")*/) 
        {
            //animator.SetBool("Attack", true);
			Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) 
            {
                line.SetPosition(0, origin.position);
                line.SetPosition(1, hit.point);
                StartCoroutine(lineDelete());
				GameObject hitObject = hit.transform.gameObject;

                IReactiveTarget reactiveTarget = hitObject.GetComponent<IReactiveTarget>();

                if (reactiveTarget != null)
                {
                    reactiveTarget.ReactToHit();
                }
            }
		}

        if (Input.GetKeyDown(KeyCode.C))
            LockCursor();
	}

    private IEnumerator lineDelete() 
    {
        yield return new WaitForSeconds(1);

		line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
	}
    void LockCursor()
    {
        if (!cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorLocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
        }
    }
}