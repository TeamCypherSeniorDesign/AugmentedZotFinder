using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is what gets our location (hopefully)

public class GPSLocation : MonoBehaviour
{
	public static GPSLocation Instance { set; get; }
	public float latitude;
	public float longitude;
	
    // Start is called before the first frame update
    void Start()
    {
		Instance = this;
		DontDestroyOnLoad(gameObject);
		if(Input.location.isEnabledByUser)
		{
			StartCoroutine (GetLocation ());
		}
    }
	
	private IEnumerator GetLocation()
	{
		if (!Input.location.isEnabledByUser)
		{
			Debug.Log("GPS not enabled");
		}
		
		Input.location.Start();
		
		while(Input.location.status == LocationServiceStatus.Initializing)
		{
			yield return new WaitForSeconds(0.5f);
		}
		
		if(Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log("Unable to find device location");
			yield break;
		}
		
		
		latitude = Input.location.lastData.latitude;
		longitude = Input.location.lastData.longitude;
		
		yield break;
	}

    // Update is called once per frame
    void Update()
    {
		latitude = Input.location.lastData.latitude;
		longitude = Input.location.lastData.longitude; 
		
    }
}
