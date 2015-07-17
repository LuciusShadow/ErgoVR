using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class AccSensor : MonoBehaviour{

	Thread receiveThread;

	UdpClient client;

	public int port;
	bool stop = false;
	float grav = 9.81f;
	Vector3 acceleration;

	public Vector3 Acceleration
	{
		get
		{	
			return acceleration;
		}
	}
	// Use this for initialization
	void Start () {

		init();

	}

	private void init()
	{
		port = 5555;
		
		receiveThread = new Thread(new ThreadStart(RecieveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}


	private void RecieveData()
	{

		client = new UdpClient(port);
		while(true)
		{	

			if(stop)return;
			try
			{
				//Get Bytes
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);
				if (data == null || data.Length == 0)
					return;
				string udpString = Encoding.UTF8.GetString(data);
				//print(">> " + udpString);

				//Parse and Split string
				udpString.Trim();
				string[] exData = udpString.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


				//Load Accelerometer data in new Vector
				acceleration.x = normStringToFloat(exData[2]);
				acceleration.y = normStringToFloat(exData[3]);
				acceleration.z = normStringToFloat(exData[4]);


			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}

	private float normStringToFloat(string value)
	{
		return float.Parse(value)/grav;
	}

	public void OnApplicationQuit() {

		stop = true;
		client.Close();
	}

	
	// Update is called once per frame
	void Update () {
		//print (acceleration.x + " " + acceleration.y + " " + acceleration.z);
		//print ("Count: " + count + " Zeit: " + Time.realtimeSinceStartup);
	}
}
