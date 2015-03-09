using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMng : MonoBehaviour {


	public GameObject[] Characters;

	int playerCount;
	GamePadState state;
	PlayerIndex myIndex;

	// Use this for initialization
	void Start () {

		//playerCount = PlayerPrefs.GetInt ("PlayerCount");
		playerCount = 1;

		for(int i = 0; i < playerCount; i++) {
			GameObject player = (GameObject)Instantiate(Characters[i], new Vector2(5 * i, 5), Quaternion.identity);
			PlayerIndex index = (PlayerIndex)i;
			GamePadState state = GamePad.GetState(index);
	
			if(state.IsConnected)
			{
				print("Connected " + index.ToString());
				myIndex = index;
				player.GetComponent<PlayerInputV2>().MyIndex = index;
				//if(player.name == "Bob")
				//{
				//	player.GetComponent<BobScript>().gamepadState = state;
				//}
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
