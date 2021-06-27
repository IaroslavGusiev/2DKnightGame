using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSceneHUD : MonoBehaviour
{
    [SerializeField] private Text _winText;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlickerText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlickerText() 
    {
        while (true)
        {
            _winText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _winText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
