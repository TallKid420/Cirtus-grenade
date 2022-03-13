using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJoshScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> JoshSpawnSpots;
    [SerializeField] Transform Josh;
    JoshScript JoshScript;
    void Start()
    {
        JoshScript = Josh.GetComponent<JoshScript>();
        if (JoshSpawnSpots.Count == 0)
        {
            foreach (Transform Child in transform.parent.GetComponentsInChildren<Transform>())
            {
                if (Child.GetComponent<MeshRenderer>())
                {
                    Destroy(Child.GetComponent<MeshRenderer>());
                }
                if (Child.name.ToLower().Contains("spawn"))
                {
                    JoshSpawnSpots.Add(Child);
                    foreach (Transform cChild in Child.GetComponentsInChildren<Transform>())
                    {
                        Destroy(cChild.GetComponent<MeshRenderer>());
                    }
                   
                }
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower().Contains("player") && !JoshScript.GetTarget())
        {
            int RandomTarget = Random.Range(0, JoshSpawnSpots.Count);
          //  print(RandomTarget);
            JoshScript.SetTarget(JoshSpawnSpots[RandomTarget].GetChild(0));
        }
    }

}
