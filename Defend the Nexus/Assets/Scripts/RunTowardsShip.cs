using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTowardsShip : MonoBehaviour {

    public float speed = 3f;

    public Transform target;
    public GameObject scripts;

    // Start is called before the first frame update
    void Start () {
        scripts = GameObject.FindGameObjectsWithTag ("Target") [0];
        target = scripts.transform;
    }

    // Update is called once per frame
    void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards (transform.position, target.position, step);
    }

    private void OnMouseDown () {
        Destroy (this.gameObject);
        scripts.GetComponent<ShipInteraction> ().IncreaseScore ();
    }
}