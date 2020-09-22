using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGarageButton : MonoBehaviour {

	public void EnterGarage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Garage");
    }
}
