using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace socketnumber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                // Create a TCP/IP  socket.  
                Socket SOCKET = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    SOCKET.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        SOCKET.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>"); //messaggio che mando al server

                    // Send the data through the socket.  
                    int bytesSENT = SOCKET.Send(msg);  //effettivo invio

                    // Receive the response from the remote device.  
                    int bytesRECEIVE = SOCKET.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                      
                    textBoxReceive.Text=Encoding.ASCII.GetString(bytes, 0, bytesRECEIVE));

                    // Release the socket.  
                    SOCKET.Shutdown(SocketShutdown.Both);
                    SOCKET.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception a)
                {
                    Console.WriteLine("Unexpected exception : {0}", a.ToString());
                }

            }
            catch (Exception a)
            {
                Console.WriteLine(a.ToString());
            }
        }


    }
}
