/***********************************************************
 * Dateiname: SerialPortScript.cs
 * Autor: Sascha Bach
 * letzte Aenderung: 19.06.2015
 * Inhalt: enthaelt die Implementierung der Klasse SerialPortScript
 ***********************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using UnityEngine.UI;

/***********************************************************
 * Klasse: SerialPortScript
 * Beschreibung: Ein Objekt der Klasse SerialPortScript
 * stellt eine Verbindung mit einem BLE-Master Device
 * an einem Com-Port her und speichert die Beschleunigungs-
 * daten in einem öffentlichen Vektor
 ***********************************************************/
public class SerialPortScript : MonoBehaviour {

	//private string tagAdress="B4:99:4C:64:68:BF";
	
	public string comPortString = "COM6";
	static SerialPort port;
	StringBuilder sb;

	private bool initcomplete = false;

	byte xHandle = 0x3A;
	byte yHandle = 0x3e;
	byte zHandle = 0x42;

	//Deklaration der Acceleration-Daten für x,y,z
	public float xData = 0f;
	public float yData = 0f;
	public float zData = 0f;

	bool stop = false; //Wenn wahr wird RecieveThread beendet
	int count = 0;
	Thread receiveThread;
	//Faktor für Berechnung der Beschleunigungsdaten 
	float gFactor = 51f;

	public InputField comPort;
	public Button connectButton;
	public Button startButton;
	public bool InitComplete
	{
		get
		{
			return initcomplete;
		}
	}

 /***********************************************************
 * Methode: Acceleration
 * Beschreibung: Gibt aktuellen Beschleunigungsvektor zurück
 * Parameter: keine
 * Rückgabewert: Beschleunigungsvektor
 ***********************************************************/
	public Vector3 Acceleration
	{
		get{	
			return new Vector3 (xData, yData, zData);
		}
	}


/***********************************************************
 * Methode: InitDongle
 * Beschreibung: Ruft Methoden zum Setup des USB-Dongle
 * auf
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	IEnumerator InitDongle(){
		port = new SerialPort(comPortString, 115200, Parity.None, 8, StopBits.One);
		
		OpenConnection(port);
		if(port.IsOpen){
			
			StartCoroutine(InitalizeDongle());
			yield return new WaitForSeconds(3);
			
			
		} else Debug.Log("Initialization failed");
		init();
	}
/***********************************************************
 * Methode: init
 * Beschreibung: Started Recieve-Thread
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	private void init(){
		receiveThread = new Thread(
			new ThreadStart(RecieveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}


/***********************************************************
 * Methode: Update
 * Beschreibung: Wird in jedem Frame aufgerufen
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	void Update(){
//		print ("Count: " + count + " Zeit: " + Time.realtimeSinceStartup);
	}

/***********************************************************
 * Methode: recieveData
 * Beschreibung: Empfängt Daten, die vom SensorTag an den
 * USB-Dongle und schließlich zum Com-Port gesendet wurden
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	private void RecieveData () {
//		string filePath = "E:\\log2.csv";
//		if (!File.Exists(filePath)){
//			File.Create(filePath).Close();
//		}
//		string delimiter = ",";

		byte inputByte;		
		sbyte byteVal = 0;
		string gValue ="";
		string incData = "";

		while(stop == false){	

			byte[] eventMsg = new byte[50]; // Stores whole data package
			int dataLength = 8;				// Default input data length

			sb = new StringBuilder();		// String builder for debug purposes
			
			char axis = '0';				// Which axis does the data belong to
			if(initcomplete && port.IsOpen){
	
					try{
						//Handle for x: 003A
						//Handle for y: 003E
						//Handle for z: 0042
						
						for(int i=0; i<=dataLength; i++){
							inputByte = (byte)port.ReadByte();		//Read Byte from port

							
							if(i==2){								//Adapt loop to data Length in package (should be constant)
								//Debug.Log ("Datenlänge: " + Convert.ToInt16(tmpByte));
								dataLength = i + Convert.ToInt16(inputByte);
							}

							//Convert incoming Data to hex
							incData = Convert.ToString(inputByte, 16).PadLeft(2, '0').PadRight(3, ' ');
							eventMsg[i] = inputByte;

							//Decide which axis the data belongs to
							if(i==9){
								if(incData.Equals(Convert.ToString(xHandle, 16).PadLeft(2, '0').PadRight(3, ' '))){
									axis = 'x';
								} 
								if(incData.Equals(Convert.ToString(yHandle, 16).PadLeft(2, '0').PadRight(3, ' '))){
									axis ='y';
								} 
								if(incData.Equals(Convert.ToString(zHandle, 16).PadLeft(2, '0').PadRight(3, ' '))){
									axis = 'z';
								}
								
							} 
							//Extract Accelerometer-Data from package
							gValue = Convert.ToString(eventMsg[dataLength],16).PadLeft(2, '0');
							
							//accVal = eventMsg[dataLength]; //Debug line
							
							//Convert to signed value
							byteVal = Convert.ToSByte(gValue, 16); 

						}//end for
					switch(axis){
					case 'x': 
						xData = byteVal/gFactor;
						break;
					case 'y': 
						yData = byteVal/gFactor;
						count++;
						break;
					case 'z': 
						zData = byteVal/gFactor;
						break;
					default:
						
						break;
					}
					if(axis != '0'){
//						File.AppendAllText(filePath, Convert.ToString(byteVal/gFactor)+delimiter+"\r\n");
//						sb.Append(incData); // Debug Code
						//Debug.Log ( "in hex: " + gValue + " in Byte: " + byteVal + " G auf Achse " + axis + ": " + byteVal/gFactor);
					}

						//Convert.ToString (Convert.ToInt32(value,16),2).PadLeft (value.Length * 4, '0') 
						
						
						//if(incData.Equals(Convert.ToString(eventByte, 16).PadLeft(2, '0').PadRight(3, ' ')))
						
						//Debug.Log("Received: " + sb.ToString());

						
					} 
					catch(TimeoutException){}
					catch (System.Exception e) {
						print ("System.Exception in serial.ReadLine: " + e.ToString ());
					}

			}
			else {
				
				//port.DiscardInBuffer();
			}
		}//while end

	}

/***********************************************************
 * Methode: sendByte
 * Beschreibung: Schreibt einen ByteArray auf den gegebenen
 * Com-Port
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	void sendByte(byte[] message){
		port.Write (message, 0, message.Length);

	}
/***********************************************************
 * Methode: OpenConnection
 * Beschreibung: Öffnet den Port
 * Parameter: port
 * Rückgabewert: keinen
 ***********************************************************/
	public void OpenConnection(SerialPort port) {
		if (port != null){
			if (port.IsOpen){
				port.Close();
				print("Port schliessen, weil bereits offen");
			} else {
				port.DtrEnable = true;
				port.RtsEnable = true;
				port.WriteTimeout = 2000;
				port.ReadTimeout = 5; 
				port.Open(); // COM-Port Verbindung öffnen
				
				print("Port geoeffnet um " + DateTime.Now + "\n");


			  }
		} else {
			if (port.IsOpen)
				print("Port bereits offen!");
			else
				print("Port == null");
		}
	}
	/***********************************************************
 * Methode: OnApplicationQuit
 * Beschreibung: Wird beim Schließen des Spiels aufgerufen
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	void OnApplicationQuit() {
		TerminateConnection();
		stop = true;
	}
/***********************************************************
 * Methode: TerminateConnection
 * Beschreibung: Sendet einen ByteBefehl an den Dongle die
 * Verbindung mit dem Sensortag zu trennen und schließt
 * den Com-Port
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	void TerminateConnection(){
		byte[] disconnect = { 0x01, 0x0A, 0xFE, 0x03, 0x00, 0x00, 0x13 };
		sendByte(disconnect);
		port.Close();
		
		print ("Port geschlossen");
	}

/***********************************************************
 * Methode: InitalizeDongle
 * Beschreibung: Initalisiert den USB-Dongle
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	IEnumerator InitalizeDongle (){
		print ("Initalize");
		byte[] init = { 0x01, 0x00, 0xFE, 0x26, 0x08, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00,
			0x00, 0x00 }; //Initalize Dongle
		sendByte(init);
		yield return new WaitForSeconds(0.5f);
		

		
	}
/***********************************************************
 * Methode: ConnectWithTag
 * Beschreibung: Schreibt Anweisungen auf den Com-Port um
 * den Dongle mit dem SensorTag zu verbinden
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	IEnumerator ConnectWithTag(){
		//byte[] disc = { 0x01, 0x30, 0xFE, 0x03, 0x15, 0x06, 0x00 }; // Discovery
		sendByte(new byte[]{ 0x01, 0x30, 0xFE, 0x03, 0x15, 0x06, 0x00 });
		yield return new WaitForSeconds(0.5f);
		print ("Connect");
		
		//byte[] canceldisc = { 0x01, 0x05, 0xFE, 0x00 }; //Cancel Discovery
		sendByte(new byte[]{ 0x01, 0x05, 0xFE, 0x00 });
		
		yield return new WaitForSeconds(0.5f);
		//byte[] connectTag = {0x01, 0x09,  0xFE, 0x09, 0x00, 0x00, 0x00, 0xBF, 0x68, 0x64, 0x4C, 0x99, 0xB4 }; //Connect with SensorTag
		sendByte(new byte[]{0x01, 0x09,  0xFE, 0x09, 0x00, 0x00, 0x00, 0xBF, 0x68, 0x64, 0x4C, 0x99, 0xB4 });
		
		yield return new WaitForSeconds(0.5f);

		StartCoroutine(ActivateAccelerometer());
	}
/***********************************************************
 * Methode: ActivateAccelerometer
 * Beschreibung: Enabled den Accelerometer und die
 * Notifications für x,y und z Achse
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	IEnumerator ActivateAccelerometer(){
		//Befehlsstruktur
		byte[] command = {0x01, 0x92, 0xFD, 0x06, 0x00, 0x00, 0x34, 0x00, 0x01, 0x00};
		int pos = 6;
		//Enable Acceleromenter
		command[pos] = 0x34;
		//sendByte(new byte[]{0x01, 0x92, 0xFD, 0x06, 0x00, 0x00, 0x34, 0x00, 0x01, 0x00});
		sendByte(command);

		yield return new WaitForSeconds(0.5f);
		//Activate Notifications for x-axis
		command[pos] = 0x3B;
		//sendByte(new byte[]{0x01, 0x92, 0xFD, 0x06, 0x00, 0x00, 0x3B, 0x00, 0x01, 0x00});
		sendByte(command);
		
		yield return new WaitForSeconds(0.5f);
		//Activate Notifications for y-axis
		command[pos] = 0x3F;
		//sendByte(new byte[]{0x01, 0x92, 0xFD, 0x06, 0x00, 0x00, 0x3F, 0x00, 0x01, 0x00 });
		sendByte(command);
		
		yield return new WaitForSeconds(0.5f);
		//Activate Notifications for z-axis
		command[pos] = 0x43;
		//sendByte(new byte[]{0x01, 0x92, 0xFD, 0x06, 0x00, 0x00, 0x43, 0x00, 0x01, 0x00 });
		sendByte(command);
		
		yield return new WaitForSeconds(1f);
		Debug.Log("Connection established at: " + DateTime.Now);
		initcomplete = true;
	}

/***********************************************************
 * Methode: EnableAcc
 * Beschreibung: Funktion für ButtonOnClickEvent um die
 * SensorTagverbindung herzustellen
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	public void EnableAcc(){

		StartCoroutine(ConnectWithTag());
		if(port.IsOpen){
			print ("Enable");
			startButton.gameObject.SetActive(true);
		}
	}


/***********************************************************
 * Methode: ConnectUSB
 * Beschreibung: Stellt Verbindung mit ComPort her, der im
 * Eingabefeld im Menü angegeben wurde
 * Parameter: keine
 * Rückgabewert: keinen
 ***********************************************************/
	public void ConnectUSB(){
		comPortString = comPort.text;
		StartCoroutine(InitDongle());
		if(port.IsOpen){
			connectButton.gameObject.SetActive(true);
		}
			
	}


}
