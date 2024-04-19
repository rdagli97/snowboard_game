using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSound;
    CircleCollider2D playerHead;
    AudioSource crashSFX;
    PlayerController playerController;
    bool playCrashSFX = true;
    void Start()
    {
        playerHead = GetComponent<CircleCollider2D>();
        crashSFX = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && playerHead.IsTouching(other.collider) && playCrashSFX)
        {
            playerController.DisableController();
            crashEffect.Play();
            crashSFX.PlayOneShot(crashSound);
            playCrashSFX = false;
            Invoke("ReloadScene", delayTime);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
