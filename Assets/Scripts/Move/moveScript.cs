using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScript : MonoBehaviour
{
    public float speed = 6.0f;
    public float boost = 1.7f;
    public float maxBoost = 3;
    public float sensitivity = 2f;
    public float boostCharge;

    private float mouseX = 0.0f;
    private Vector3 inputVector = Vector3.zero;
    [SerializeField]
    private Rigidbody tankBody;
    private Transform canon;
    private canonScript canonScript;

    // Use this for initialization
    void Start()
    {
        tankBody = GetComponent<Rigidbody>();
        canon = transform.Find("canon");
        canonScript = canon.GetComponent<canonScript>();
        boostCharge = maxBoost;
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();
        moveController();
        canonController();
        actionController();
    }

    private void checkMove()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal") * speed, tankBody.velocity.y, Input.GetAxis("Vertical") * speed);
        mouseX = sensitivity * Input.GetAxis("Mouse X");
        if (Input.GetKey(KeyCode.LeftShift) && boostCharge > 0)
        {
            boostCharge -= Time.deltaTime;
            inputVector.z *= boost;
        }
        else if (boostCharge <= maxBoost)
        {
            boostCharge += Time.deltaTime / 2;
        }
    }

    private void moveController()
    {
        transform.Translate(new Vector3(0, 0, inputVector.z) * Time.deltaTime);
        transform.Rotate(new Vector3(0, inputVector.x, 0));
    }

    private void canonController()
    {
        canon.Rotate(new Vector3(0, mouseX, 0));
    }

    private void actionController()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            canonScript.rifleShot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            canonScript.missileShot();
        }
    }
}
