using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionHandler : MonoBehaviour
{
    const int transitionDuration = 1; //Duration of animation
    public void DoAnimationIntoScene(string scene) => StartCoroutine(AnimateIntoScene(scene)); //Start animation
    //Do the animation where the scene is closing
    IEnumerator AnimateIntoScene(string scene)
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Fade_In");
        }
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }
}
