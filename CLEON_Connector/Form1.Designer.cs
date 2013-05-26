namespace CLEON_Connector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_connect = new System.Windows.Forms.Button();
            this.comboBox_availableComPort = new System.Windows.Forms.ComboBox();
            this.groupBox_connection = new System.Windows.Forms.GroupBox();
            this.button_updateCLEON = new System.Windows.Forms.Button();
            this.groupBox_time = new System.Windows.Forms.GroupBox();
            this.label_currentTimeTick = new System.Windows.Forms.Label();
            this.label_currentTime = new System.Windows.Forms.Label();
            this.label_currentUTCTimeTick = new System.Windows.Forms.Label();
            this.label_currentTimeInfo = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer_currentTime = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_numberOfSamples = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox_parameter = new System.Windows.Forms.GroupBox();
            this.textBox_batteryCapacity = new System.Windows.Forms.TextBox();
            this.label_batteryCapacity = new System.Windows.Forms.Label();
            this.textBox_chunkGap = new System.Windows.Forms.TextBox();
            this.label_chunkGap = new System.Windows.Forms.Label();
            this.textBox_ChunkCount = new System.Windows.Forms.TextBox();
            this.textBox_sampleGap = new System.Windows.Forms.TextBox();
            this.label_numberOfChunksInASample = new System.Windows.Forms.Label();
            this.label_sampleGap = new System.Windows.Forms.Label();
            this.textBox_SampleCount = new System.Windows.Forms.TextBox();
            this.groupBox_connection.SuspendLayout();
            this.groupBox_time.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.groupBox_parameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(150, 17);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(109, 23);
            this.button_connect.TabIndex = 0;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // comboBox_availableComPort
            // 
            this.comboBox_availableComPort.FormattingEnabled = true;
            this.comboBox_availableComPort.Location = new System.Drawing.Point(6, 20);
            this.comboBox_availableComPort.Name = "comboBox_availableComPort";
            this.comboBox_availableComPort.Size = new System.Drawing.Size(138, 20);
            this.comboBox_availableComPort.TabIndex = 1;
            this.toolTip1.SetToolTip(this.comboBox_availableComPort, "Select a COM port that CLEON is connected");
            // 
            // groupBox_connection
            // 
            this.groupBox_connection.Controls.Add(this.button_connect);
            this.groupBox_connection.Controls.Add(this.comboBox_availableComPort);
            this.groupBox_connection.Location = new System.Drawing.Point(12, 12);
            this.groupBox_connection.Name = "groupBox_connection";
            this.groupBox_connection.Size = new System.Drawing.Size(265, 52);
            this.groupBox_connection.TabIndex = 2;
            this.groupBox_connection.TabStop = false;
            this.groupBox_connection.Text = "Connection";
            // 
            // button_updateCLEON
            // 
            this.button_updateCLEON.Enabled = false;
            this.button_updateCLEON.Location = new System.Drawing.Point(12, 481);
            this.button_updateCLEON.Name = "button_updateCLEON";
            this.button_updateCLEON.Size = new System.Drawing.Size(265, 23);
            this.button_updateCLEON.TabIndex = 3;
            this.button_updateCLEON.Text = "Connect to CLEON (1/3)";
            this.toolTip1.SetToolTip(this.button_updateCLEON, "Update CLEON\'s time and parameters");
            this.button_updateCLEON.UseVisualStyleBackColor = true;
            this.button_updateCLEON.Click += new System.EventHandler(this.button_updateCLEON_Click);
            // 
            // groupBox_time
            // 
            this.groupBox_time.Controls.Add(this.label_currentTimeTick);
            this.groupBox_time.Controls.Add(this.label_currentTime);
            this.groupBox_time.Controls.Add(this.label_currentUTCTimeTick);
            this.groupBox_time.Controls.Add(this.label_currentTimeInfo);
            this.groupBox_time.Location = new System.Drawing.Point(12, 70);
            this.groupBox_time.Name = "groupBox_time";
            this.groupBox_time.Size = new System.Drawing.Size(265, 129);
            this.groupBox_time.TabIndex = 4;
            this.groupBox_time.TabStop = false;
            this.groupBox_time.Text = "Time";
            // 
            // label_currentTimeTick
            // 
            this.label_currentTimeTick.AutoSize = true;
            this.label_currentTimeTick.Location = new System.Drawing.Point(6, 105);
            this.label_currentTimeTick.Name = "label_currentTimeTick";
            this.label_currentTimeTick.Size = new System.Drawing.Size(49, 12);
            this.label_currentTimeTick.TabIndex = 9;
            this.label_currentTimeTick.Text = "00:00:00";
            // 
            // label_currentTime
            // 
            this.label_currentTime.AutoSize = true;
            this.label_currentTime.Location = new System.Drawing.Point(6, 55);
            this.label_currentTime.Name = "label_currentTime";
            this.label_currentTime.Size = new System.Drawing.Size(49, 12);
            this.label_currentTime.TabIndex = 8;
            this.label_currentTime.Text = "00:00:00";
            // 
            // label_currentUTCTimeTick
            // 
            this.label_currentUTCTimeTick.AutoSize = true;
            this.label_currentUTCTimeTick.Location = new System.Drawing.Point(6, 80);
            this.label_currentUTCTimeTick.Name = "label_currentUTCTimeTick";
            this.label_currentUTCTimeTick.Size = new System.Drawing.Size(126, 12);
            this.label_currentUTCTimeTick.TabIndex = 7;
            this.label_currentUTCTimeTick.Text = "Current UTC time tick";
            // 
            // label_currentTimeInfo
            // 
            this.label_currentTimeInfo.AutoSize = true;
            this.label_currentTimeInfo.Location = new System.Drawing.Point(6, 30);
            this.label_currentTimeInfo.Name = "label_currentTimeInfo";
            this.label_currentTimeInfo.Size = new System.Drawing.Size(74, 12);
            this.label_currentTimeInfo.TabIndex = 6;
            this.label_currentTimeInfo.Text = "Current time";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 57600;
            this.serialPort1.PortName = "COM9";
            // 
            // timer_currentTime
            // 
            this.timer_currentTime.Enabled = true;
            this.timer_currentTime.Interval = 1;
            this.timer_currentTime.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(236, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Click to see the explanation");
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label_numberOfSamples
            // 
            this.label_numberOfSamples.AutoSize = true;
            this.label_numberOfSamples.Location = new System.Drawing.Point(6, 29);
            this.label_numberOfSamples.Name = "label_numberOfSamples";
            this.label_numberOfSamples.Size = new System.Drawing.Size(224, 12);
            this.label_numberOfSamples.TabIndex = 0;
            this.label_numberOfSamples.Text = "Sample count (\'1\' means on-demand)";
            this.toolTip1.SetToolTip(this.label_numberOfSamples, "\'1\' means collecting on demand");
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 517);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(289, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // groupBox_parameter
            // 
            this.groupBox_parameter.Controls.Add(this.textBox_batteryCapacity);
            this.groupBox_parameter.Controls.Add(this.label_batteryCapacity);
            this.groupBox_parameter.Controls.Add(this.pictureBox1);
            this.groupBox_parameter.Controls.Add(this.textBox_chunkGap);
            this.groupBox_parameter.Controls.Add(this.label_chunkGap);
            this.groupBox_parameter.Controls.Add(this.textBox_ChunkCount);
            this.groupBox_parameter.Controls.Add(this.textBox_sampleGap);
            this.groupBox_parameter.Controls.Add(this.label_numberOfChunksInASample);
            this.groupBox_parameter.Controls.Add(this.label_sampleGap);
            this.groupBox_parameter.Controls.Add(this.textBox_SampleCount);
            this.groupBox_parameter.Controls.Add(this.label_numberOfSamples);
            this.groupBox_parameter.Location = new System.Drawing.Point(12, 205);
            this.groupBox_parameter.Name = "groupBox_parameter";
            this.groupBox_parameter.Size = new System.Drawing.Size(265, 265);
            this.groupBox_parameter.TabIndex = 7;
            this.groupBox_parameter.TabStop = false;
            this.groupBox_parameter.Text = "Parameters";
            // 
            // textBox_batteryCapacity
            // 
            this.textBox_batteryCapacity.Location = new System.Drawing.Point(8, 236);
            this.textBox_batteryCapacity.Name = "textBox_batteryCapacity";
            this.textBox_batteryCapacity.Size = new System.Drawing.Size(251, 21);
            this.textBox_batteryCapacity.TabIndex = 10;
            this.textBox_batteryCapacity.Text = "850";
            this.textBox_batteryCapacity.TextChanged += new System.EventHandler(this.textBox_batteryCapacity_TextChanged);
            // 
            // label_batteryCapacity
            // 
            this.label_batteryCapacity.AutoSize = true;
            this.label_batteryCapacity.Location = new System.Drawing.Point(6, 221);
            this.label_batteryCapacity.Name = "label_batteryCapacity";
            this.label_batteryCapacity.Size = new System.Drawing.Size(136, 12);
            this.label_batteryCapacity.TabIndex = 9;
            this.label_batteryCapacity.Text = "Battery capacity (mAh)";
            // 
            // textBox_chunkGap
            // 
            this.textBox_chunkGap.Location = new System.Drawing.Point(8, 188);
            this.textBox_chunkGap.Name = "textBox_chunkGap";
            this.textBox_chunkGap.Size = new System.Drawing.Size(251, 21);
            this.textBox_chunkGap.TabIndex = 7;
            this.textBox_chunkGap.Text = "50";
            this.textBox_chunkGap.TextChanged += new System.EventHandler(this.textBox_chunkGap_TextChanged);
            // 
            // label_chunkGap
            // 
            this.label_chunkGap.AutoSize = true;
            this.label_chunkGap.Location = new System.Drawing.Point(6, 173);
            this.label_chunkGap.Name = "label_chunkGap";
            this.label_chunkGap.Size = new System.Drawing.Size(98, 12);
            this.label_chunkGap.TabIndex = 6;
            this.label_chunkGap.Text = "Chunk gap (ms)";
            this.toolTip1.SetToolTip(this.label_chunkGap, "Minimum chunk gap is 50 ms");
            // 
            // textBox_ChunkCount
            // 
            this.textBox_ChunkCount.Location = new System.Drawing.Point(8, 140);
            this.textBox_ChunkCount.Name = "textBox_ChunkCount";
            this.textBox_ChunkCount.Size = new System.Drawing.Size(251, 21);
            this.textBox_ChunkCount.TabIndex = 5;
            this.textBox_ChunkCount.Text = "5";
            this.textBox_ChunkCount.TextChanged += new System.EventHandler(this.textBox_numberOfChunks_TextChanged);
            // 
            // textBox_sampleGap
            // 
            this.textBox_sampleGap.Location = new System.Drawing.Point(8, 92);
            this.textBox_sampleGap.Name = "textBox_sampleGap";
            this.textBox_sampleGap.Size = new System.Drawing.Size(251, 21);
            this.textBox_sampleGap.TabIndex = 4;
            this.textBox_sampleGap.Text = "3000";
            this.textBox_sampleGap.TextChanged += new System.EventHandler(this.textBox_sampleGap_TextChanged);
            // 
            // label_numberOfChunksInASample
            // 
            this.label_numberOfChunksInASample.AutoSize = true;
            this.label_numberOfChunksInASample.Location = new System.Drawing.Point(6, 125);
            this.label_numberOfChunksInASample.Name = "label_numberOfChunksInASample";
            this.label_numberOfChunksInASample.Size = new System.Drawing.Size(157, 12);
            this.label_numberOfChunksInASample.TabIndex = 3;
            this.label_numberOfChunksInASample.Text = "Chunk count (in a sample)";
            // 
            // label_sampleGap
            // 
            this.label_sampleGap.AutoSize = true;
            this.label_sampleGap.Location = new System.Drawing.Point(6, 77);
            this.label_sampleGap.Name = "label_sampleGap";
            this.label_sampleGap.Size = new System.Drawing.Size(105, 12);
            this.label_sampleGap.TabIndex = 2;
            this.label_sampleGap.Text = "Sample gap (ms)";
            this.toolTip1.SetToolTip(this.label_sampleGap, "It should be multiple of 1000");
            // 
            // textBox_SampleCount
            // 
            this.textBox_SampleCount.Location = new System.Drawing.Point(8, 44);
            this.textBox_SampleCount.Name = "textBox_SampleCount";
            this.textBox_SampleCount.Size = new System.Drawing.Size(253, 21);
            this.textBox_SampleCount.TabIndex = 1;
            this.textBox_SampleCount.Text = "1";
            this.textBox_SampleCount.TextChanged += new System.EventHandler(this.textBox_numberOfSamples_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 539);
            this.Controls.Add(this.groupBox_parameter);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox_time);
            this.Controls.Add(this.groupBox_connection);
            this.Controls.Add(this.button_updateCLEON);
            this.Name = "Form1";
            this.Text = "CLEON Connector";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox_connection.ResumeLayout(false);
            this.groupBox_time.ResumeLayout(false);
            this.groupBox_time.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox_parameter.ResumeLayout(false);
            this.groupBox_parameter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.ComboBox comboBox_availableComPort;
        private System.Windows.Forms.GroupBox groupBox_connection;
        private System.Windows.Forms.Button button_updateCLEON;
        private System.Windows.Forms.GroupBox groupBox_time;
        private System.Windows.Forms.Label label_currentTimeInfo;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label_currentUTCTimeTick;
        private System.Windows.Forms.Label label_currentTimeTick;
        private System.Windows.Forms.Label label_currentTime;
        private System.Windows.Forms.Timer timer_currentTime;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBox_parameter;
        private System.Windows.Forms.Label label_numberOfSamples;
        private System.Windows.Forms.TextBox textBox_SampleCount;
        private System.Windows.Forms.Label label_numberOfChunksInASample;
        private System.Windows.Forms.Label label_sampleGap;
        private System.Windows.Forms.TextBox textBox_sampleGap;
        private System.Windows.Forms.TextBox textBox_chunkGap;
        private System.Windows.Forms.Label label_chunkGap;
        private System.Windows.Forms.TextBox textBox_ChunkCount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox_batteryCapacity;
        private System.Windows.Forms.Label label_batteryCapacity;
    }
}

