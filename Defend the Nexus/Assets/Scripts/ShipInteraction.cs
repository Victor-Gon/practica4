using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipInteraction : MonoBehaviour {

    public int healthPoints = 10;
    private int score = 0;

    public Text scoreText;

    public AudioClip damageAudio;
    public AudioClip explosionAudio;
    private AudioSource audioSource;

    private bool onFire = false;
    private Vector3 resize = new Vector3 (0.5f, 0.5f, 0.5f);

    public Transform fireModel;
    public Transform explosionModel;
    public Transform targetImage;

    private bool lost = false;

    void Start () {
        audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update () {
        if (healthPoints <= 0) {
            StartCoroutine (LoadFinalScene ());
            lost = true;
            audioSource.PlayOneShot (explosionAudio, .5f);
            Transform explosion =
                Instantiate (explosionModel, this.transform.position, this.transform.rotation, targetImage);
            explosion.localScale = resize;
        }
    }

    IEnumerator LoadFinalScene () {
        yield return new WaitForSeconds (1.5f);

        SceneManager.LoadScene("FinalScene");
    }

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Enemy")) {
            Destroy (other.gameObject);
            if (!lost) {
                healthPoints -= Random.Range (1, 4);
                audioSource.PlayOneShot (damageAudio, .5f);

                if (!onFire && healthPoints <= 20) {
                    Transform fire =
                        Instantiate (fireModel, this.transform.position, this.transform.rotation, targetImage);
                    fire.localScale = new Vector3 (.02f, .02f, .02f);
                    onFire = true;
                }
            }
        }
    }

    public void IncreaseScore () {
        score++;
        ShowScore ();
    }

    private void ShowScore () {
        scoreText.text = "Score: " + score.ToString ();
    }
}