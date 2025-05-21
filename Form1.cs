using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Configuration;

namespace game
{
    public partial class Form1 : Form
    {
        static IPAddress ip = IPAddress.Parse("127.0.0.1");
        static IPEndPoint remoteEP = new IPEndPoint(ip, 53311);
        Socket toSrvSocket;
        byte[] buffer = new byte[1024];
        Panel thisbox, opponentbox, objective;
        bool connected = false;
        ushort x, y;
        int gridX = 15, gridY = 9;
        int pointsThis, pointsOpponent;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += keyDown;
         //   boxes = new Panel[gridX, gridY];
            chboxPanelsBorders.Enabled = false;
        }
        Panel[,] boxes;


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void findMatch()
        {
            toSrvSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                toSrvSocket.Connect(remoteEP);
            }
            catch(SocketException ex) { lbErrors.Items.Add("error connecting to remote server endpoint " + remoteEP +": " + ex.Message); connectButton.Enabled = true; return; }
            connected = true;
            new Thread(ReceiveMessages).Start();
        }
        private void gameStartSetup()
        {
            boxes = new Panel[gridX, gridY];
            thisbox = new Panel { Location = new Point(0, 0), Size = new Size(50, 50), BackColor = Color.Green };
            opponentbox = new Panel { Location = new Point(0, 0), Size = new Size(50, 50), BackColor = Color.Red };
            objective = new Panel { Location = new Point(0, 0), Size = new Size(50, 50), BackColor = Color.Yellow };
            x = 0; y = 0; pointsThis = 0; pointsOpponent=0;
            int w, h, ws, hs;

            w = GamePanel.Width;
            h = GamePanel.Height;
            ws = w / gridX;
            hs = h / gridY;
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    Panel p = new Panel { Width = ws, Height = hs, BackColor = Color.FromArgb(255, 255, 255), Location = new Point(i * ws, j * hs)};
                    boxes[i, j] = p;
                    GamePanel.Controls.Add(p);
                    p.Click += (object s, EventArgs e) => { (s as Panel).BackColor = Color.FromArgb(255, 128, 128, 0); };
                }
            }
            lbThisPts.Text = "this: 0";
            lbOpponentPts.Text = "opponent: 0";
            chboxPanelsBorders.Enabled = true;
          
        }
        private void gameDesetup()
        {
           
            pointsThis = 0;
            pointsOpponent = 0;
            boxes = new Panel[gridX,gridY];
            this.Invoke(new Action(() =>
            {
                chboxPanelsBorders.Enabled = false;
                objective.Dispose();
                thisbox.Dispose();
                opponentbox.Dispose();
                GamePanel.Controls.Clear();
                NewMsgLabel.Text = "GAME ENDED";
                lbThisPts.Text = "";
                lbOpponentPts.Text = "";
                connectButton.Enabled = true;
            }));

        }

        private void keyDown(object s, KeyEventArgs e)
        {
            var key = e.KeyCode;
            switch (key) //MOVE:DIRECTION
            {
                case Keys.W: if (!(y <= 0)) send("MOVE|"+JsonConvert.SerializeObject(new MessageToServerMove(MoveDirection.UP))); break;
                case Keys.S: if (!(y >= gridY - 1)) send("MOVE|" +JsonConvert.SerializeObject(new MessageToServerMove(MoveDirection.DOWN))); break;
                case Keys.A: if (!(x <= 0)) send("MOVE|"+JsonConvert.SerializeObject(new MessageToServerMove(MoveDirection.LEFT))); break;
                case Keys.D: if (!(x >= gridX - 1)) send("MOVE|"+JsonConvert.SerializeObject(new MessageToServerMove(MoveDirection.RIGHT))); break;

            }
        }

        private void chboxPanelsBorders_CheckedChanged(object sender, EventArgs e)
        {
            if (chboxPanelsBorders.Checked)
            {
                foreach (Panel ct in GamePanel.Controls)
                {
                    ct.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            else
            {
                foreach (Panel ct in GamePanel.Controls)
                {
                    ct.BorderStyle = BorderStyle.None;
                }
            }
        }

        private void ReceiveMessages()
        {
            

            while (connected) { 
            string data = receiveMessage();
            if (data == "rcmsgerr") return;
            string[] messages = data.Split('!');
                foreach (string singleMessage in messages)
                {
                    if (singleMessage == "") continue;
                    string[] splitMessage = singleMessage.Split('|');
                    string mtype = splitMessage[0];
                    switch (mtype)
                    {

                        case "TEXT":
                            var msg = JsonConvert.DeserializeObject<MessageFromServerText>(splitMessage[1]).text;
                            this.Invoke(new Action(() => { NewMsgLabel.Text = msg; if (msg == "GAME_STARTED") gameStartSetup(); }));
                            break;
                        case "LOCATION":
                            {
                                MessageFromServerLocation msgf = JsonConvert.DeserializeObject<MessageFromServerLocation>(splitMessage[1]);
                                switch (msgf.moveWho)
                                {
                                    case MoveWho.THIS:
                                        this.Invoke(new Action(() => { changePosition(thisbox, msgf.x, msgf.y); x = msgf.x; y = msgf.y; }));
                                        break;
                                    case MoveWho.OPPONENT:
                                        this.Invoke(new Action(() => { changePosition(opponentbox, msgf.x, msgf.y); }));
                                        break;
                                    case MoveWho.OBJECTIVE:
                                        this.Invoke(new Action(() => { changePosition(objective, msgf.x, msgf.y); }));
                                        break;
                                }
                            }
                            break;
                        case "POINT":
                            {
                                var msgp = JsonConvert.DeserializeObject<MessageFromServerPoints>(splitMessage[1]);
                                switch (msgp.pointsWho)
                                {
                                    case PointsWho.THIS:
                                        pointsThis++;
                                        this.Invoke(new Action(() => lbThisPts.Text = "this: " + pointsThis.ToString()));

                                        break;
                                    case PointsWho.OPPONENT:
                                        pointsOpponent++;
                                        this.Invoke(new Action(() => lbOpponentPts.Text = "opponent: " + pointsOpponent.ToString()));

                                        break;
                                }
                                break;
                            }
                        case "ERROR":
                            {
                                this.Invoke(new Action(() => lbErrors.Items.Add(splitMessage[1])));

                                toSrvSocket.Shutdown(SocketShutdown.Both);
                                connected = false;
                                gameDesetup();
                                break;
                            }
                        case "WINS":
                            {
                                var msgwins = JsonConvert.DeserializeObject<MessageFromServerWins>(splitMessage[1]);
                                if (msgwins.whoWins == WhoPlayer.THIS)
                                {
                                    this.Invoke(new Action(() => NewMsgLabel.Text = "You win."));
                                }
                                if (msgwins.whoWins == WhoPlayer.OPPONENT)
                                {
                                    this.Invoke(new Action(() => NewMsgLabel.Text = "You lost."));
                                }
                                disconnect(toSrvSocket);
                                Thread.Sleep(2000);
                                gameDesetup();
                                break;
                            }


                    }
                }
            }
        }

        private void disconnect(Socket s)
        {
            s.Shutdown(SocketShutdown.Both);
            s.Close();
        }
        private void changePosition(Panel p, ushort x, ushort y)
        {
            if (x >= gridX) throw new Exception("outside x: " + x);
            if (y >= gridY) throw new Exception("outside y: " + y);
            var previousBox = p.Parent;
            if (previousBox != null) previousBox.Controls.Remove(p);
            boxes[x, y].Controls.Add(p);
        }

        private string receiveMessage()
        {
            string s = "";
            do
            {
                int nbytes;
                try
                {
                    nbytes = toSrvSocket.Receive(buffer);
                }
                catch(Exception ex) { lbErrors.Items.Add(ex.Message); return "rcmgserr"; }
                s += Encoding.ASCII.GetString(buffer, 0, nbytes);
            }
            while (s[s.Length - 1] != '!');
            s = s.Remove(s.Length - 1);
            return s;
        }
        private void send(string message)
        {
            if (connected)
                try
                {
                    toSrvSocket.Send(Encoding.ASCII.GetBytes(message + '!'));
                }
                catch (Exception ex) { lbErrors.Items.Add(ex.Message); return; } 
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            findMatch();
        }
    }
}    

