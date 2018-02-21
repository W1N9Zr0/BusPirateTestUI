using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using BusPirateLibCS;
using BusPiratePICProgrammer;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;

namespace buspirateraw
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}



		private ManagementEventWatcher _DeviceChangeWatcher;
		private void Form1_Load(object sender, EventArgs e)
		{
			FindSerialPorts();

			_DeviceChangeWatcher = new ManagementEventWatcher(@"root\CIMV2", "SELECT * FROM Win32_DeviceChangeEvent");

			_DeviceChangeWatcher.Start();
			_DeviceChangeWatcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
		}

		private void connectProgrammer()
		{
			//pp = new PIC16Programmer(serialPort1, false);
			var failsLeft = 10;
			while (pp == null && failsLeft > 0)
			{
				try
				{
					pp = new PIC18Programmer(serialPort1, true);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
					serialPort1.Close();
					Thread.Sleep(10);

				}
			}

			if (failsLeft == 0)
				Application.Exit();
			//pp = new DsPICProgrammer(serialPort1);
		}

		PicProgrammer pp;

		public class PortInfo
		{
			public string DeviceName { get; set; }
			public string PortName { get; set; }
		}

		private void FindSerialPorts()
		{
			var ports = new List<PortInfo>();

			using (var searcher = new ManagementObjectSearcher(
				@"root\CIMV2",
				"SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\""))
			{
				var reCom = new Regex(@"\((COM\d+)\)");
				foreach (ManagementObject queryObj in searcher.Get())
				{
					var deviceName = queryObj["Name"] as string;
					var m = reCom.Match(deviceName);

					if (m.Success)
					{
						var portName = m.Groups[1].Value;
						ports.Add(new PortInfo() { PortName = portName, DeviceName = deviceName });
					}
				}
			}

			ports = ports.OrderBy(x => x.PortName).ToList();

			Invoke((MethodInvoker) delegate {
				lstPorts.DataSource = ports;
				lstPorts.Refresh();
			});
		}

		void watcher_EventArrived(object sender, EventArrivedEventArgs e)
		{
			var ev = e.NewEvent;
			var type = Convert.ToInt32(ev.Properties["EventType"].Value);
			if (type == 2)
			{
				Debug.WriteLine("connect!");
				FindSerialPorts();
			}
			else if (type == 3)
			{
				Debug.WriteLine("disconnect!");
				FindSerialPorts();
			}
		}
		
		private void button1_Click(object sender, EventArgs e)
		{
			pp.Program = !pp.Program;
		}



		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			_DeviceChangeWatcher.Stop();

			timer1.Stop();
			if (pp != null)
				pp.close();
			//w.exitMode();
			//b.close();
			
		}


	   
		private void btnProgTest_Click(object sender, EventArgs e)
		{
			pp.bulkErase();

			var uids = new byte[] {
				0x01, 0x02,
				0x03, 0x04,
				0x05, 0x06,
				0x07, 0x08
			};
			pp.writeConfig(0x2000, uids, 0, uids.Length);

			var conf = new byte[] {
				0x10, 0x3f
			};
			pp.writeConfig(0x2007, conf, 0, conf.Length);

			var code = new byte[96];

			code[0] = (byte)DateTime.Now.Hour;
			code[2] = (byte)DateTime.Now.Minute;
			code[4] = (byte)DateTime.Now.Second;

			for (int i = 0; i < ('z' - 'a') * 2; i += 2)
			{
				code[6 + i] = (byte)(i + 'a');
			}

			pp.writeCode(0x0, code, 0, code.Length);
			
			var data = new byte[16];

			data[0] = (byte)DateTime.Now.Hour;
			data[1] = (byte)DateTime.Now.Minute;
			data[3] = (byte)DateTime.Now.Second;
			
			for (int i = 0; i < 9; i++) {
				data[i + 4] = (byte)(i * i);
			}

			pp.writeData(0x0, data, 0, data.Length);
			
			var confRead = new byte[16];

			
			pp.readCode(0x2000, confRead, 0, confRead.Length);

			for (int i = 0; i < confRead.Length; i += 2) {
				txtOut.AppendText(String.Format("{0} {1}\n"
					, (i/2 + 0x2000).ToString("X4")
					, (confRead[i] | (confRead[i + 1] << 8)).ToString("X4")));
			}

		
		}



		private void timer1_Tick(object sender, EventArgs e)
		{
			//txtOut.AppendText(b.PinValues.ToString() + "\r\n");
			//txtOut.AppendText(b.ADCProbe + "v"+ "\r\n");


		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			var tb = sender as TrackBar;
			//b.pwmSetup((1 << tb.Value), 0.2);
		}

		private void btnReadHex_Click(object sender, EventArgs e)
		{
			var bgw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

			var confRead = new byte[16];

			var file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
				@"\Code\microchip\remoteShutter\output\remoteShutter.hex";
			
			bgw.DoWork += (obj,param) => {
				byte[] code;
				byte[] uid;
				byte[] data;
				byte[] conf;
				parseHex(file, out code, out uid, out data, out conf);

				pp.bulkErase();
				pp.writeConfig(0x2000, uid, 0, uid.Length);
				if (conf.Length < 2)
				{
					conf = new byte[] { 0x10, 0x3f };
				}
				pp.writeConfig(0x2007, conf, 0, conf.Length);
				pp.writeCode(0x0000, code, 0, code.Length, x=>{bgw.ReportProgress(x);});
					
				pp.writeData(0x0000, data, 0, data.Length);

				pp.readCode(0x2000, confRead, 0, confRead.Length);
				
			};

			bgw.ProgressChanged += (s, ev) => {
				progressBar1.Value = ev.ProgressPercentage;
			};

			bgw.RunWorkerCompleted += (s, ev) => {
				progressBar1.Value = 100;
				this.Enabled = true;
				txtOut.AppendText("\r\n");
				for (int i = 0; i < confRead.Length; i += 2)
				{
					txtOut.AppendText(String.Format("{0} {1}\r\n"
						, (i / 2 + 0x2000).ToString("X4")
						, (confRead[i] | (confRead[i + 1] << 8)).ToString("X4")));
				}
			};
			this.Enabled = false;
			bgw.RunWorkerAsync();

		}

		void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private static void parseHex(string file, out byte[] code, out byte[] uid, out byte[] data, out byte[] conf)
		{
			var f = File.Open(
								file,
								FileMode.Open, FileAccess.Read);
			var h = new HexParser(f);

			code = new byte[0x1000];
			uid = new byte[8];
			data = new byte[0x80];
			conf = new byte[2];

			long addr = -1;
			while (h.Address <= code.Length * 2)
			{
				addr = h.Address;
				code[addr] = h.Value;
				h.nextAddress();
			}
			Array.Resize<byte>(ref code, (int)addr + 1);

			addr = -1;
			while (h.Address <= 0x2004 * 2)
			{
				addr = h.Address - 0x2000 * 2;
				uid[addr] = h.Value;
				h.nextAddress();
			}
			Array.Resize<byte>(ref uid, (int)addr + 1);

			addr = -1;
			while (h.Address <= (0x2007 * 2) + 1)
			{
				addr = h.Address - 0x2007 * 2;
				conf[addr] = h.Value;
				h.nextAddress();
			}
			Array.Resize<byte>(ref conf, (int)addr + 1);

			addr = -1;
			while (h.Address < (0x2100 + 0x80 * 2) * 2)
			{
				addr = (h.Address - 0x2100 * 2) / 2;
				data[addr] = h.Value;
				h.nextAddress();
				h.nextAddress();
			}
			Array.Resize<byte>(ref data, (int)addr + 1);
			f.Close();
		}


		private void btnReadProgram_Click(object sender, EventArgs e)
		{
			byte[] data = new byte[320];

			pp.readCode(0x0, data, 0, data.Length);

			for (int i = 0; i < data.Length; i += 2)
			{
				if (i % 16 == 0)
				{
					txtOut.AppendText("\r\n");
				}
				txtOut.AppendText((data[i] | data[i + 1] << 8).ToString("X4") + " ");
			}
		}

		private void btnReadData_Click(object sender, EventArgs e)
		{
			byte[] data = new byte[64];

			pp.readData(0x0, data, 0, data.Length);

			for (int i = 0; i < data.Length; i += 1)
			{
				if (i % 16 == 0)
				{
					txtOut.AppendText("\r\n");
				}
				txtOut.AppendText((data[i]).ToString("X2") + " ");
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{

			var bgw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

			
			
			
			var sb = new StringBuilder();



			this.Enabled = false;
			bgw.DoWork += (obj, param) =>
			{

				var file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
				@"\Code\microchip\testCprog.X\dist\default\production\testCprog.X.production.hex";
				var f = new FileStream(file, FileMode.Open);
				var h = new HexParser(f);
				long lastAddr = 0;
				long startAddr = 0;


				Queue<Tuple<int, byte[]>> blocks = new Queue<Tuple<int, byte[]>>();


				Queue<byte> block = new Queue<byte>();

				block.Enqueue(h.Value);

				while (h.nextAddress() != long.MaxValue)
				{
					if (h.Address > lastAddr + 1)
					{
						blocks.Enqueue(new Tuple<int, byte[]>((int)startAddr, block.ToArray()));
						block = new Queue<byte>();
						startAddr = h.Address;
					}

					block.Enqueue(h.Value);
					lastAddr = h.Address;
				}

				blocks.Enqueue(new Tuple<int, byte[]>((int)startAddr, block.ToArray()));

				f.Close();
				var myset = from x in blocks
							select x.Item1;

				pp.bulkErase();
				sb.AppendLine("erased");

				//foreach (var st in myset)
				//{
				//    readCodeToSB(bgw, sb, st, 0x10);
				//}
				//sb.AppendLine();

				var blankData = new byte[64];

				Random r = new Random();
				r.NextBytes(blankData);

				foreach (var b in blocks)
				{
					var address = b.Item1;
					var data = b.Item2;


					if (address < 0x300000)
					{
						sb.AppendLine(string.Format("Writing code @ {0:X6} - {1:X6}", address, address + data.Length));
						pp.writeCode(address, data, 0, data.Length, bgw.ReportProgress);
						
						//pp.writeCode(address, blankData, 0, blankData.Length, bgw.ReportProgress);
					}
					else
					{
						sb.AppendLine(string.Format("Writing conf @ {0:X6} - {1:X6}", address, address + data.Length));
						pp.writeConfig(address, data, 0, data.Length, bgw.ReportProgress);
						//pp.writeConfig(address, blankData, 0, blankData.Length, bgw.ReportProgress);
					}
				}

				//foreach (var st in myset)
				//{
				//    readCodeToSB(bgw, sb, st, 0x10);
				//}
				sb.AppendLine();

			};


			bgw.ProgressChanged += (s, ev) =>
			{
				progressBar1.Value = ev.ProgressPercentage;
			};


			bgw.RunWorkerCompleted += (s, ev) =>
			{
				txtOut.Text = sb.ToString();
				
				
				this.Enabled = true;

			};

			bgw.RunWorkerAsync();
		}

		private byte[] readCodeToSB(BackgroundWorker bgw, StringBuilder sb, int targetAddr, int targetLen)
		{

			//sb.Append(string.Format("Address: {0:X6} - {1:X6}", targetAddr, targetAddr + targetLen));
			if (targetAddr % 2 != 0)
			{
				targetAddr -= 1;
				targetLen += 1;
			}
			if (targetLen % 2 == 1)
			{
				targetLen += 1;
			}

			byte[] dataBuff = new byte[targetLen];
			
			pp.readCode(targetAddr, dataBuff, 0, dataBuff.Length, bgw.ReportProgress);

			for (int i = 0; i < dataBuff.Length; i += 2)
			{
				if (i % 16 == 0)
				{
					sb.AppendLine();
					sb.AppendFormat("{0:X8} : ", targetAddr + i);
				}
				sb.Append((dataBuff[i] | dataBuff[i + 1] << 8).ToString("X4") + " ");
			}
			return dataBuff;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var bgw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };




			var sb = new StringBuilder();



			this.Enabled = false;
			bgw.DoWork += (obj, param) =>
			{

				var file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
				@"\Code\microchip\testCprog.X\dist\default\production\testCprog.X.production.hex";
				var f = new FileStream(file, FileMode.Open);
				var h = new HexParser(f);
				long lastAddr = 0;
				long startAddr = 0;


				Queue<Tuple<int, byte[]>> blocks = new Queue<Tuple<int, byte[]>>();


				Queue<byte> block = new Queue<byte>();

				block.Enqueue(h.Value);

				while (h.nextAddress() != long.MaxValue)
				{
					if (h.Address > lastAddr + 1)
					{
						blocks.Enqueue(new Tuple<int, byte[]>((int)startAddr, block.ToArray()));
						block = new Queue<byte>();
						startAddr = h.Address;
					}

					block.Enqueue(h.Value);
					lastAddr = h.Address;
				}

				blocks.Enqueue(new Tuple<int, byte[]>((int)startAddr, block.ToArray()));

				f.Close();

				foreach (var st in blocks)
				{
					readCodeToSB(bgw, sb, st.Item1, st.Item2.Length);
				}
				sb.AppendLine();


			};


			bgw.ProgressChanged += (s, ev) =>
			{
				progressBar1.Value = ev.ProgressPercentage;
			};


			bgw.RunWorkerCompleted += (s, ev) =>
			{
				txtOut.Text = sb.ToString();


				this.Enabled = true;

			};

			bgw.RunWorkerAsync();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			pp.bulkErase();
		}

		private void btnRead18Conf_Click(object sender, EventArgs e)
		{
			var bgw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

			var sb = new StringBuilder();

			this.Enabled = false;
			bgw.DoWork += (obj, param) =>
			{
				var blocks = new Queue<Tuple<int, int>>();
				blocks.Enqueue(new Tuple<int, int>(0x300000, 0x30000D - 0x300000 + 1));
				blocks.Enqueue(new Tuple<int, int>(0x3FFFFE, 2));

				foreach (var st in blocks)
				{
					readCodeToSB(bgw, sb, st.Item1, st.Item2);
				}
				sb.AppendLine();


			};


			bgw.ProgressChanged += (s, ev) =>
			{
				progressBar1.Value = ev.ProgressPercentage;
			};


			bgw.RunWorkerCompleted += (s, ev) =>
			{
				txtOut.Text = sb.ToString();


				this.Enabled = true;

			};

			bgw.RunWorkerAsync();
		}

		private void lstPorts_SelectedIndexChanged(object sender, EventArgs e)
		{
			serialPort1.PortName = lstPorts.SelectedValue as string;
		}

		private void btnPicConnect_Click(object sender, EventArgs e)
		{
			connectProgrammer();
		}




	}
}
