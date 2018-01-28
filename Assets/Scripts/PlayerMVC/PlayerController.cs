using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float interactionDistance;

    [Space(10)]

    [SerializeField]
    private AudioInfo pickUpSound = null;

    [SerializeField]
    private AudioInfo dropSound = null;

    [SerializeField]
    private float hoistRate;

    [SerializeField]
    private Transform BackPos;

    [SerializeField]
    private Transform ArcPoint;

    private Tooltip toolTip;
    private bool canPickup = true;
    private Interactable focusObj;

    [HideInInspector]
    public bool CanMove = true;

    private Animator animator;

    private void Awake()
    {
        toolTip = FindObjectOfType<Tooltip>();
        animator = GetComponent<Animator>();
    }

    public void Move(float horizontal, float vertical)
    {
        if (CanMove)
        {
            if (horizontal == 0f && vertical == 0f)
            {
                //Animation
                animator.SetInteger("State", 1);
            }
            else
            {
                //Animation
                animator.SetInteger("State", 0);
            }

            Vector3 newVelocity = new Vector3(
            horizontal * moveSpeed,
            gameObject.GetComponent<Rigidbody>().velocity.y, // or 0
            vertical * moveSpeed);

            this.gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        }
    }

    public void Action()
    {
        if (canPickup)
            pickupObj();
        else
            putDownObj();
    }

    public void HoistObj(GameObject obj)
    {
        StartCoroutine(hoistObj(obj));
    }

    private IEnumerator hoistObj(GameObject obj)
    {
        yield return null;
        Vector3 a = obj.transform.position;
        Vector3 b = BackPos.position;
        Vector3 c = ArcPoint.position;

        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime * hoistRate;
            obj.transform.position = GetPoint(a, b, c, t);
            yield return null;
        }

    }

    private void pickupObj()
    {
        if(focusObj != null)
        {
            focusObj.ToggleInteract(this);
            canPickup = false;
            toolTip.HideTooltip();

            AudioManager.Instance.PlayAudioAtPoint(pickUpSound, transform.position);
        }
    }

    private void putDownObj()
    {
        focusObj.ToggleInteract(this);
        canPickup = true;

        AudioManager.Instance.PlayAudioAtPoint(dropSound, transform.position);
    }

    private void Update()
    {
        if (canPickup)
        {
            focusObj = null;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                if (hit.transform.gameObject.GetComponent<Interactable>() != null)
                {
                    focusObj = hit.transform.gameObject.GetComponent<Interactable>();
                    toolTip.ShowTooltip(hit.transform);
                }
            }
            else
            {
                toolTip.HideTooltip();
            }
        }
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
            2f * oneMinusT * t * p1 +
            t * t * p2;
    }
}
