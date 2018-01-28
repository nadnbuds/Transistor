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

    [SerializeField]
    private Transform rayCastPoint;

    private Tooltip toolTip;
    private bool canPickup = true;
    private Interactable focusObj;

    [HideInInspector]
    public bool CanMove = true;

    private void Awake()
    {
        toolTip = FindObjectOfType<Tooltip>();
    }

    public void Move(float horizontal, float vertical)
    {
        if (CanMove)
        {
            Vector3 dir = new Vector3(horizontal, 0.0f, vertical);

            Vector3 newVelocity = new Vector3(
            horizontal * moveSpeed,
            gameObject.GetComponent<Rigidbody>().velocity.y, // or 0
            vertical * moveSpeed);
            if(dir.magnitude > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), .15f);
            }

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
            obj.transform.position = GetPoint(a, c, b, t);
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
            Collider[] hits;
            Vector3 pos = rayCastPoint.position + transform.forward * interactionDistance;
            hits = Physics.OverlapSphere(pos, interactionDistance);
            foreach (Collider hit in hits)
            {
                if (hit.transform.gameObject.GetComponent<Interactable>() != null)
                {
                    focusObj = hit.transform.gameObject.GetComponent<Interactable>();
                    toolTip.ShowTooltip(hit.transform);
                    break;
                }
                else
                {
                    toolTip.HideTooltip();
                }

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
