using UnityEngine;
using System.IO.Ports;
using TMPro;
using System.Collections;
using System.Threading;
using System;

public class ComController : MonoBehaviour
{
    private bool abort;
    private SerialPort serialPort;
    private Thread serialThread;
    private SynchronizationContext mainThread;

    private char incomingChar;
    private string incomingString;

    public delegate void SerialEvent(string incomingString);
    public static event SerialEvent WhenReceiveDataCall;

    [SerializeField] private TMP_Dropdown dropdownPort;
    [SerializeField] private TextMeshProUGUI msg;

    private void Awake()
    {
        string[] ports = SerialPort.GetPortNames();
        foreach(string port in ports)
        {
            this.dropdownPort.options.Add(new TMP_Dropdown.OptionData(port));
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void CreatePort()
    {
        string portValue = this.dropdownPort.options[this.dropdownPort.value].text;
        this.StartPort(portValue);
    }

    public void StartPort(string portName)
    {
        // init port
        serialPort = new SerialPort(portName, 9600);
        bool isOK = false;
        if(serialPort != null)
        {
            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();

                    this.mainThread = SynchronizationContext.Current;
                    if (this.mainThread == null)
                        this.mainThread = new SynchronizationContext();

                    this.serialThread = new Thread(Receive);
                    this.serialThread.Start(); 

                    serialPort.ReadTimeout = 500;
                    if(serialPort.ReadByte() > 0)
                    {
                        Debug.Log(portName + " Opened");
                        // Start Game
                        GameController.instance.StartGame();
                        isOK = true;
                        
                        //StartCoroutine(ReadDataFromSerialPort());
                    }
                }
                catch
                {
                    msg.text = "Port is not ready in catch";
                }
            }
        }
        if(!isOK) msg.text = "Port is not ready";
    }

    void Receive()
    {
        while (true)
        {
            if (this.abort)
            {
                this.serialThread.Abort();
                break;
            }

            try
            {
                this.incomingChar = (char)serialPort.ReadChar();
            }
            catch(Exception e) { }

            if (!this.incomingChar.Equals('\n'))
            {
                this.incomingString += this.incomingChar;
            }
            else
            {
                this.mainThread.Send((object state) =>
                {
                    WhenReceiveDataCall?.Invoke(this.incomingString);
                }, null);
                this.incomingString = "";
            }
        }
    }

    IEnumerator ReadDataFromSerialPort()
    {
        yield return new WaitForSeconds(.1f);
        //Debug.Log("Start Cour");
        string[] val = serialPort.ReadLine().Split(',');

        /*float steerVal = float.Parse(val[0], CultureInfo.InvariantCulture.NumberFormat);
        float speed = float.Parse(val[1], CultureInfo.InvariantCulture.NumberFormat);

        GameController.instance.SteerSpeedBike = steerVal;
        GameController.instance.MaxSpeedBike = speed;*/
        float steerVal = 0, speed = 0;
        if (val[0] != "")
            steerVal = float.Parse(val[0], System.Globalization.NumberStyles.Float);
        if (val[1] != null && val[1] != "")
            speed = float.Parse(val[1], System.Globalization.NumberStyles.Float);

        GameController.instance.SteerSpeedBike = steerVal;
        GameController.instance.MaxSpeedBike = speed;

        print("Steer val: " + steerVal);
        print("Speed val: " + speed);

        StartCoroutine(ReadDataFromSerialPort());
    }

    private void OnApplicationQuit()
    {
        this.abort = true;
        serialPort.DiscardInBuffer();
        serialPort.DiscardOutBuffer();
        serialPort.Close();
    }
}
