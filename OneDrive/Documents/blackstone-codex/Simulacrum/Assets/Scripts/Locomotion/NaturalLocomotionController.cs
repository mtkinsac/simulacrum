using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(CharacterController))]
public class NaturalLocomotionController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 2.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;

    [Header("Input Settings")]
    public XRNode inputSource = XRNode.LeftHand;
    private Vector2 inputAxis;

    private CharacterController characterController;
    private float currentSpeed = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if(characterController == null)
            Debug.LogError("CharacterController is missing on " + gameObject.name);
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if (!device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis))
            inputAxis = Vector2.zero;

        if (inputAxis.magnitude > 0.1f)
            currentSpeed = Mathf.Lerp(currentSpeed, movementSpeed, acceleration * Time.deltaTime);
        else
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);

        Vector3 move = new Vector3(inputAxis.x, 0, inputAxis.y);
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0f;

        characterController.Move(move * currentSpeed * Time.deltaTime);
    }
}
