using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeBoard : MonoBehaviour
{
    [SerializeField] private GameObject _loadBar;

    private void Start()
    {
        StartCoroutine(ActivateChildrenSequentially());
    }

    private IEnumerator ActivateChildrenSequentially()
    {
        int childCount = _loadBar.transform.childCount;
        float delay = 3.0f / childCount;

        for (int i = 0; i < childCount; i++)
        {
            _loadBar.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
        //yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MenuScene");
    }
}
