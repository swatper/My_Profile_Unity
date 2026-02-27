using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpen : MonoBehaviour
{
    [SerializeField] string[] urlList;

    public void OpenURL(int index) {
        Application.OpenURL(urlList[index]);
    }
}
