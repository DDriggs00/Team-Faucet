using UnityEngine;

public class DC_HighScore : MonoBehaviour
{
	private string IP;
	private int gameID;
	
	public DC_HighScore()
	{
		IP = "52.160.46.238";
		gameID = 5;
	}

	public DC_HighScore(string overrideIP, int overrideGameID)
	{
		IP = overrideIP;
		gameID = overrideGameID;
	}
	
	public void UploadScore(string name, int score)
	{
		var highscore = new System.Diagnostics.Process();
		highscore.StartInfo.FileName = "hssclient.exe";
		highscore.StartInfo.Arguments = IP + " " + gameID + " " + name + " " + score;
		highscore.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		highscore.Start();
	}
}
