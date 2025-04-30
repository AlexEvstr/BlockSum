using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab; // Префаб стрелки
    [SerializeField] private Canvas canvas; // Canvas, в пределах которого будут создаваться стрелки
    private List<GameObject> letters;
    private List<Arrow> arrows;
    private bool initialized = false;

    private void Start()
    {
        StartCoroutine(InitializeArrows());
    }

    private IEnumerator InitializeArrows()
    {
        yield return new WaitForSeconds(0.5f);

        letters = new List<GameObject>(GameObject.FindGameObjectsWithTag("letter"));
        arrows = new List<Arrow>();

        foreach (var letter in letters)
        {
            if (letter != null)
            {
                GameObject arrowObject = Instantiate(arrowPrefab, canvas.transform);
                Arrow arrow = arrowObject.GetComponent<Arrow>();
                if (arrow != null)
                {
                    arrow.Initialize(letter, canvas);
                    arrows.Add(arrow);
                }
                else
                {
                    Destroy(arrowObject);
                }
            }
        }

        initialized = true;
    }

    private void Update()
    {
        if (!initialized) return;

        foreach (var arrow in arrows)
        {
            if (arrow != null)
            {
                arrow.CheckVisibilityAndMove();
            }
        }
    }
}
