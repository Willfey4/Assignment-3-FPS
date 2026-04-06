using UnityEngine;
using System.Collections;
public class handAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private AudioClip fireball;
    [SerializeField] private AudioClip lightning;
    private Animator animator;
    private AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            StartCoroutine(fire());
            if (Input.GetMouseButtonDown(0))
            {
                audioSource.clip = lightning;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = fireball;
                audioSource.Play(); 
            }
        }
    }

    private IEnumerator fire() 
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Rest", true);
        animator.SetBool("Attack", false);
	}
}
