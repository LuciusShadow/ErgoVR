/***********************************************************
* Dateiname: PhoneSensor.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse PathScript
***********************************************************/
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

/***********************************************************
* Klasse: PhoneSensor
* Beschreibung: Verarbeitet die Gyroskopdaten des Smartphones
* am Lenker
***********************************************************/
public class PhoneSensor : MonoBehaviour{

	Thread receiveThread;
	UdpClient client;

	public int port;
	bool stop = false;
	float grav = 9.81f;
	Vector3 gyrodata;

	//Getter der Gyrodaten
	public Vector3 Gyrodata
	{
		get
		{	
			return gyrodata;
		}
	}
	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Aufruf von init()
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		init();

	}

	/***********************************************************
	 * Methode: init
	 * Beschreibung: Initialisierung des Threads für 
	 * Gyroskopdatenempfang
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	private void init()
	{
		
		receiveThread = new Thread(new ThreadStart(RecieveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}

	/***********************************************************
	 * Methode: ReceiveData
	 * Beschreibung: Empfängt die UDP-Packete des Smartphones
	 * über WLAN
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	private void RecieveData()
	{

		client = new UdpClient(port);
		while(true)
		{	

			if(stop)return;
			try
			{
				//Empfange Bytes
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);

				//print (data);

				if (data == null || data.Length == 0)
					return;

				string udpString = Encoding.UTF8.GetString(data);
				print(">> " + udpString);

				//Parsen und splitten des Strings
				udpString.Trim();
				string[] exData = udpString.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


				//Lade Gyroskopdaten in Vektor
				gyrodata.x = NormStringToFloat(exData[6]);
				gyrodata.y = NormStringToFloat(exData[7]);
				gyrodata.z = NormStringToFloat(exData[8]);
			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}

	/***********************************************************
	 * Methode: NormStringToFloat
	 * Beschreibung: Konvertiert String in Float
	 * Parameter: String value
	 * Rückgabewert: float value
	 ***********************************************************/
	private float NormStringToFloat(string value)
	{
		return float.Parse(value)/grav;
	}

	/***********************************************************
	 * Methode: OnApplicationQuit
	 * Beschreibung: Stoppt den RecieveThread
	 * Parameter: -
	 * Rückgabewert: -
	 ***********************************************************/
	public void OnApplicationQuit() {

		stop = true;
		client.Close();
	}

	
	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Verwendet für Debug Code
	 * Parameter: -
	 * Rückgabewert: -
	 ***********************************************************/
	void Update () {
		//print (acceleration.x + " " + acceleration.y + " " + acceleration.z);
		//print ("Count: " + count + " Zeit: " + Time.realtimeSinceStartup);
	}
}
