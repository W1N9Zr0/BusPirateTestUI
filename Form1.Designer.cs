namespace buspirateraw
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
		this.components = new System.ComponentModel.Container();
		this.txtOut = new System.Windows.Forms.TextBox();
		this.txtInASCII = new System.Windows.Forms.TextBox();
		this.txtInHex = new System.Windows.Forms.TextBox();
		this.txtInDec = new System.Windows.Forms.TextBox();
		this.txtInBin = new System.Windows.Forms.TextBox();
		this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
		this.btnProgVlt = new System.Windows.Forms.Button();
		this.btnReadProgram = new System.Windows.Forms.Button();
		this.btnPicConnect = new System.Windows.Forms.Button();
		this.btnReadData = new System.Windows.Forms.Button();
		this.btnProgTest = new System.Windows.Forms.Button();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.btnReadHex = new System.Windows.Forms.Button();
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.btnRead18Conf = new System.Windows.Forms.Button();
		this.SuspendLayout();
		// 
		// txtOut
		// 
		this.txtOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					| System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
		this.txtOut.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtOut.Location = new System.Drawing.Point(13, 12);
		this.txtOut.Multiline = true;
		this.txtOut.Name = "txtOut";
		this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.txtOut.Size = new System.Drawing.Size(401, 413);
		this.txtOut.TabIndex = 0;
		this.txtOut.WordWrap = false;
		// 
		// txtInASCII
		// 
		this.txtInASCII.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtInASCII.Location = new System.Drawing.Point(441, 12);
		this.txtInASCII.Name = "txtInASCII";
		this.txtInASCII.Size = new System.Drawing.Size(204, 20);
		this.txtInASCII.TabIndex = 1;
		// 
		// txtInHex
		// 
		this.txtInHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtInHex.Location = new System.Drawing.Point(441, 64);
		this.txtInHex.Name = "txtInHex";
		this.txtInHex.Size = new System.Drawing.Size(204, 20);
		this.txtInHex.TabIndex = 2;
		// 
		// txtInDec
		// 
		this.txtInDec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtInDec.Location = new System.Drawing.Point(441, 38);
		this.txtInDec.Name = "txtInDec";
		this.txtInDec.Size = new System.Drawing.Size(204, 20);
		this.txtInDec.TabIndex = 3;
		// 
		// txtInBin
		// 
		this.txtInBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtInBin.Location = new System.Drawing.Point(441, 90);
		this.txtInBin.Name = "txtInBin";
		this.txtInBin.Size = new System.Drawing.Size(204, 20);
		this.txtInBin.TabIndex = 4;
		// 
		// serialPort1
		// 
		this.serialPort1.BaudRate = 115200;
		this.serialPort1.PortName = "COM13";
		this.serialPort1.ReadBufferSize = 8192;
		this.serialPort1.WriteBufferSize = 8192;
		// 
		// btnProgVlt
		// 
		this.btnProgVlt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnProgVlt.Location = new System.Drawing.Point(441, 116);
		this.btnProgVlt.Name = "btnProgVlt";
		this.btnProgVlt.Size = new System.Drawing.Size(98, 23);
		this.btnProgVlt.TabIndex = 5;
		this.btnProgVlt.Text = "+13";
		this.btnProgVlt.UseVisualStyleBackColor = true;
		this.btnProgVlt.Click += new System.EventHandler(this.button1_Click);
		// 
		// btnReadProgram
		// 
		this.btnReadProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnReadProgram.Location = new System.Drawing.Point(547, 116);
		this.btnReadProgram.Name = "btnReadProgram";
		this.btnReadProgram.Size = new System.Drawing.Size(98, 23);
		this.btnReadProgram.TabIndex = 6;
		this.btnReadProgram.Text = "Read Program";
		this.btnReadProgram.UseVisualStyleBackColor = true;
		this.btnReadProgram.Click += new System.EventHandler(this.btnReadProgram_Click);
		// 
		// btnPicConnect
		// 
		this.btnPicConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnPicConnect.Location = new System.Drawing.Point(441, 145);
		this.btnPicConnect.Name = "btnPicConnect";
		this.btnPicConnect.Size = new System.Drawing.Size(98, 23);
		this.btnPicConnect.TabIndex = 7;
		this.btnPicConnect.Text = "PicConnect";
		this.btnPicConnect.UseVisualStyleBackColor = true;
		// 
		// btnReadData
		// 
		this.btnReadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnReadData.Location = new System.Drawing.Point(548, 146);
		this.btnReadData.Name = "btnReadData";
		this.btnReadData.Size = new System.Drawing.Size(97, 23);
		this.btnReadData.TabIndex = 8;
		this.btnReadData.Text = "Read Data";
		this.btnReadData.UseVisualStyleBackColor = true;
		this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
		// 
		// btnProgTest
		// 
		this.btnProgTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnProgTest.Location = new System.Drawing.Point(548, 175);
		this.btnProgTest.Name = "btnProgTest";
		this.btnProgTest.Size = new System.Drawing.Size(97, 23);
		this.btnProgTest.TabIndex = 9;
		this.btnProgTest.Text = "Program Test";
		this.btnProgTest.UseVisualStyleBackColor = true;
		this.btnProgTest.Click += new System.EventHandler(this.btnProgTest_Click);
		// 
		// timer1
		// 
		this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
		// 
		// btnReadHex
		// 
		this.btnReadHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnReadHex.Location = new System.Drawing.Point(441, 175);
		this.btnReadHex.Name = "btnReadHex";
		this.btnReadHex.Size = new System.Drawing.Size(98, 23);
		this.btnReadHex.TabIndex = 11;
		this.btnReadHex.Text = "Read Hex";
		this.btnReadHex.UseVisualStyleBackColor = true;
		this.btnReadHex.Click += new System.EventHandler(this.btnReadHex_Click);
		// 
		// progressBar1
		// 
		this.progressBar1.Location = new System.Drawing.Point(421, 402);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(224, 23);
		this.progressBar1.TabIndex = 12;
		// 
		// button1
		// 
		this.button1.Location = new System.Drawing.Point(441, 243);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(98, 23);
		this.button1.TabIndex = 13;
		this.button1.Text = "burn hex";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(this.button1_Click_1);
		// 
		// button2
		// 
		this.button2.Location = new System.Drawing.Point(441, 214);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(98, 23);
		this.button2.TabIndex = 14;
		this.button2.Text = "verify hex";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(this.button2_Click);
		// 
		// button3
		// 
		this.button3.Location = new System.Drawing.Point(441, 273);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(98, 23);
		this.button3.TabIndex = 15;
		this.button3.Text = "Bulk erase";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(this.button3_Click);
		// 
		// btnRead18Conf
		// 
		this.btnRead18Conf.Location = new System.Drawing.Point(548, 214);
		this.btnRead18Conf.Name = "btnRead18Conf";
		this.btnRead18Conf.Size = new System.Drawing.Size(97, 23);
		this.btnRead18Conf.TabIndex = 16;
		this.btnRead18Conf.Text = "Read Conf";
		this.btnRead18Conf.UseVisualStyleBackColor = true;
		this.btnRead18Conf.Click += new System.EventHandler(this.btnRead18Conf_Click);
		// 
		// Form1
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(657, 437);
		this.Controls.Add(this.btnRead18Conf);
		this.Controls.Add(this.button3);
		this.Controls.Add(this.button2);
		this.Controls.Add(this.button1);
		this.Controls.Add(this.progressBar1);
		this.Controls.Add(this.btnReadHex);
		this.Controls.Add(this.btnProgTest);
		this.Controls.Add(this.btnReadData);
		this.Controls.Add(this.btnPicConnect);
		this.Controls.Add(this.btnReadProgram);
		this.Controls.Add(this.btnProgVlt);
		this.Controls.Add(this.txtInBin);
		this.Controls.Add(this.txtInDec);
		this.Controls.Add(this.txtInHex);
		this.Controls.Add(this.txtInASCII);
		this.Controls.Add(this.txtOut);
		this.Name = "Form1";
		this.Text = "Form1";
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
		this.Load += new System.EventHandler(this.Form1_Load);
		this.ResumeLayout(false);
		this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.TextBox txtInASCII;
    private System.Windows.Forms.TextBox txtInHex;
    private System.Windows.Forms.TextBox txtInDec;
    private System.Windows.Forms.TextBox txtInBin;
    private System.IO.Ports.SerialPort serialPort1;
    private System.Windows.Forms.Button btnProgVlt;
    private System.Windows.Forms.Button btnReadProgram;
    private System.Windows.Forms.Button btnPicConnect;
    private System.Windows.Forms.Button btnReadData;
	private System.Windows.Forms.Button btnProgTest;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Button btnReadHex;
	private System.Windows.Forms.ProgressBar progressBar1;
	private System.Windows.Forms.Button button1;
	private System.Windows.Forms.Button button2;
	private System.Windows.Forms.Button button3;
	private System.Windows.Forms.Button btnRead18Conf;
  }
}

