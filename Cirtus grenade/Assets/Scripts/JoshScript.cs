using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoshScript : MonoBehaviour
{
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = 0;
    [SerializeField] float moveSmoothTime = .3f;
    float timeTillReturn;
    bool ReturnNow = false;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    [SerializeField] Transform thePlayer;

    [SerializeField] Transform target;
    [SerializeField] Transform returntarget;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        UpdateMovement();
    }

   public void SetTarget(Transform targettoset)
    {
        target = targettoset;
    }

    public bool GetTarget()
    {
        if (!target && !returntarget)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    void UpdateMovement()
    {
        if (target != null || returntarget != null)
        {
            if (target && target.name.ToLower().Contains("startthendone") && returntarget == null)
            {
                transform.rotation = target.rotation;
                transform.position = target.position;
                returntarget = target;
                target = target.parent;

            }


            if (target != null)
            {
                Vector3 targetDir = target.position - transform.position;
                if (targetDir.magnitude > 0.05f)
                {
                    targetDir.Normalize();

                    currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, moveSmoothTime);

                    velocityY += gravity * Time.deltaTime;


                    Vector3 newvector = targetDir.normalized;
                    Vector3 velocity = newvector * walkSpeed + Vector3.up * velocityY;

                    controller.Move(velocity * Time.deltaTime);
                }
                else
                {

                    transform.rotation = target.rotation;
                    transform.position = target.position;
                    target = null;

                }
            }
            else
            {
                StartCoroutine(ReturnTime(returntarget.parent.GetComponent<JoshSpawnSO>().timeToVanish));
                if ((thePlayer.position - transform.position).magnitude <= returntarget.parent.GetComponent<JoshSpawnSO>().distanceToVanish || ReturnNow)
                {
                    if (returntarget && (returntarget.position - transform.position).magnitude > .05f)
                    {
                        Vector3 newtargetDir = returntarget.position - transform.position;
                        newtargetDir.Normalize();

                        currentDir = Vector2.SmoothDamp(currentDir, newtargetDir, ref currentDirVelocity, moveSmoothTime);
                        transform.rotation = Quaternion.Slerp(transform.rotation, returntarget.rotation, moveSmoothTime);

                        velocityY += gravity * Time.deltaTime;



                        Vector3 velocity = newtargetDir * walkSpeed + Vector3.up * velocityY;

                        controller.Move(velocity * Time.deltaTime);
                    }
                    else
                    {
                        
                        transform.rotation = returntarget.rotation;
                        transform.position = returntarget.position;
                        returntarget = null;
                        ReturnNow = false;


                    }
                }

            }
        }
    }



    IEnumerator ReturnTime(float timetoreturn)
    {
        yield return new WaitForSeconds(timetoreturn);
        ReturnNow = true;
    }
}