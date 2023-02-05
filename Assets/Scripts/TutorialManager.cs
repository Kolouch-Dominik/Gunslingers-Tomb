using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [field: SerializeField] public GameObject Message { get; set; }
    [field: SerializeField] public List<GameObject> Panels { get; set; }
    private bool canSelect;
    private int selectedPanel = -1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect && Input.GetKeyDown(KeyCode.E))
        {
            selectedPanel = (selectedPanel + 1) % Panels.Count;
            Panels.ForEach(x => x.SetActive(false));
            Panels[selectedPanel].SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Message.SetActive(true);
            canSelect = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Message.SetActive(false);
            canSelect = false;
            Panels.ForEach((x) => x.SetActive(false));
        }
    }
}
