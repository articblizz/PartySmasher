using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMng : MonoBehaviour {


	public GameObject[] Characters;

	int playerCount;
	GamePadState state;

	// Use this for initialization
	void Start () {

		//playerCount = PlayerPrefs.GetInt ("PlayerCount");
		playerCount = Characters.Length;

		for(int i = 0; i < playerCount; i++) {
			PlayerIndex index = (PlayerIndex)i;
			GamePadState state = GamePad.GetState(index);
			GameObject player = (GameObject)Instantiate(Characters[i], new Vector2(5 * i, 5), Quaternion.identity);
	
			if(state.IsConnected)
			{
				var plyScript = player.GetComponent<PlayerInputV2>();
				plyScript.MyIndex = index;
				plyScript.isUsingController = true;

			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
