namespace OHTM
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerVehStatusData = new System.Windows.Forms.Timer(this.components);
            this.btnFromVehMCmd31_6Cycle = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txbMsgToVehicle = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txbMsgFromVeh = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txbMsgToVehM = new System.Windows.Forms.TextBox();
            this.txbMsgFromVehM = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.ckbOffLine = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txbSendDataRxStatus = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txbSendDataSenStatus = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txbRxDataRxStatus = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txbRxDataSendStatus = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnBlockLocked = new System.Windows.Forms.Button();
            this.btnBlockReleased = new System.Windows.Forms.Button();
            this.lbBlockCtrlReqst = new System.Windows.Forms.Label();
            this.btnToVehMCmd134 = new System.Windows.Forms.Button();
            this.btnFromVehMCmd39_1PauseMove = new System.Windows.Forms.Button();
            this.ckbBlockCtrl = new System.Windows.Forms.CheckBox();
            this.btnFromVehMCmd31_3LoadUnload = new System.Windows.Forms.Button();
            this.btnFromVehMCmd31_1Load = new System.Windows.Forms.Button();
            this.btnFromVehMCmd31_2Unload = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txbLoadAddress = new System.Windows.Forms.TextBox();
            this.txbToAddress = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txbGuideSections4Load = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txbCycleSections = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txbCstID4Load = new System.Windows.Forms.TextBox();
            this.ckbBlockCtlOn = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtDdsBcZone = new System.Windows.Forms.TextBox();
            this.txtDdsAcqBy = new System.Windows.Forms.TextBox();
            this.txtDdsLockBy = new System.Windows.Forms.TextBox();
            this.chkDdsEdit = new System.Windows.Forms.CheckBox();
            this.btnFromVehMCmd39_0RestartPausingMove = new System.Windows.Forms.Button();
            this.btnFromVehMCmd31_0Single = new System.Windows.Forms.Button();
            this.btnOhtmStopMovingCycle = new System.Windows.Forms.Button();
            this.btnFromVehMCmd31_5Continue = new System.Windows.Forms.Button();
            this.btnFromVehMCmd37_0Cancel = new System.Windows.Forms.Button();
            this.btnFromVehMCmd37_1Abort = new System.Windows.Forms.Button();
            this.txbGuideSections4Unload = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txbUnloadAddress = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txbGuideSections4PureMove = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txbEntryAddress = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txbCstID4Unload = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.FakeCstID = new System.Windows.Forms.CheckBox();
            this.XXXTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FakeMapCheckBox = new System.Windows.Forms.CheckBox();
            this.testLoadUnloadCmd = new System.Windows.Forms.Button();
            this.test_Veh_readMap = new System.Windows.Forms.Button();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.UnLoadBtn = new System.Windows.Forms.Button();
            this.fake144charge_check = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerVehStatusData
            // 
            this.timerVehStatusData.Tick += new System.EventHandler(this.timerVehStatusData_Tick);
            // 
            // btnFromVehMCmd31_6Cycle
            // 
            this.btnFromVehMCmd31_6Cycle.Enabled = false;
            this.btnFromVehMCmd31_6Cycle.Location = new System.Drawing.Point(1077, 433);
            this.btnFromVehMCmd31_6Cycle.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd31_6Cycle.Name = "btnFromVehMCmd31_6Cycle";
            this.btnFromVehMCmd31_6Cycle.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd31_6Cycle.TabIndex = 2;
            this.btnFromVehMCmd31_6Cycle.Text = "[VehM =>] Cmd#31-6 Cycle";
            this.btnFromVehMCmd31_6Cycle.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd31_6Cycle.Visible = false;
            this.btnFromVehMCmd31_6Cycle.Click += new System.EventHandler(this.btnFromVehMCmd31_6Cycle_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txbMsgToVehicle);
            this.groupBox2.Location = new System.Drawing.Point(8, 12);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(308, 274);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send Messages To Vehicle";
            // 
            // txbMsgToVehicle
            // 
            this.txbMsgToVehicle.Location = new System.Drawing.Point(14, 18);
            this.txbMsgToVehicle.Margin = new System.Windows.Forms.Padding(2);
            this.txbMsgToVehicle.Multiline = true;
            this.txbMsgToVehicle.Name = "txbMsgToVehicle";
            this.txbMsgToVehicle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbMsgToVehicle.Size = new System.Drawing.Size(277, 242);
            this.txbMsgToVehicle.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txbMsgFromVeh);
            this.groupBox3.Location = new System.Drawing.Point(8, 294);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(308, 246);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Recieve Message From Vehicle";
            // 
            // txbMsgFromVeh
            // 
            this.txbMsgFromVeh.Location = new System.Drawing.Point(19, 18);
            this.txbMsgFromVeh.Margin = new System.Windows.Forms.Padding(2);
            this.txbMsgFromVeh.Multiline = true;
            this.txbMsgFromVeh.Name = "txbMsgFromVeh";
            this.txbMsgFromVeh.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbMsgFromVeh.Size = new System.Drawing.Size(280, 218);
            this.txbMsgFromVeh.TabIndex = 5;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txbMsgToVehM);
            this.groupBox4.Location = new System.Drawing.Point(331, 12);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(342, 274);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Message Send To VehC";
            // 
            // txbMsgToVehM
            // 
            this.txbMsgToVehM.Location = new System.Drawing.Point(14, 18);
            this.txbMsgToVehM.Margin = new System.Windows.Forms.Padding(2);
            this.txbMsgToVehM.Multiline = true;
            this.txbMsgToVehM.Name = "txbMsgToVehM";
            this.txbMsgToVehM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbMsgToVehM.Size = new System.Drawing.Size(320, 244);
            this.txbMsgToVehM.TabIndex = 6;
            // 
            // txbMsgFromVehM
            // 
            this.txbMsgFromVehM.Location = new System.Drawing.Point(14, 18);
            this.txbMsgFromVehM.Margin = new System.Windows.Forms.Padding(2);
            this.txbMsgFromVehM.Multiline = true;
            this.txbMsgFromVehM.Name = "txbMsgFromVehM";
            this.txbMsgFromVehM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbMsgFromVehM.Size = new System.Drawing.Size(320, 218);
            this.txbMsgFromVehM.TabIndex = 6;
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(778, 78);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(182, 57);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Veh/C \r\nConnect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // ckbOffLine
            // 
            this.ckbOffLine.AutoSize = true;
            this.ckbOffLine.Location = new System.Drawing.Point(685, 211);
            this.ckbOffLine.Margin = new System.Windows.Forms.Padding(2);
            this.ckbOffLine.Name = "ckbOffLine";
            this.ckbOffLine.Size = new System.Drawing.Size(71, 16);
            this.ckbOffLine.TabIndex = 6;
            this.ckbOffLine.Text = "OFF_Line";
            this.ckbOffLine.UseVisualStyleBackColor = true;
            this.ckbOffLine.Visible = false;
            this.ckbOffLine.CheckedChanged += new System.EventHandler(this.ckbOffLine_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txbMsgFromVehM);
            this.groupBox5.Location = new System.Drawing.Point(331, 294);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(342, 246);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Message From VehM";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txbSendDataRxStatus);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.txbSendDataSenStatus);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Location = new System.Drawing.Point(685, 505);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(160, 74);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Send Data Status";
            // 
            // txbSendDataRxStatus
            // 
            this.txbSendDataRxStatus.Location = new System.Drawing.Point(80, 46);
            this.txbSendDataRxStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txbSendDataRxStatus.Name = "txbSendDataRxStatus";
            this.txbSendDataRxStatus.Size = new System.Drawing.Size(70, 22);
            this.txbSendDataRxStatus.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 49);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "Receive_Status";
            // 
            // txbSendDataSenStatus
            // 
            this.txbSendDataSenStatus.Location = new System.Drawing.Point(80, 20);
            this.txbSendDataSenStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txbSendDataSenStatus.Name = "txbSendDataSenStatus";
            this.txbSendDataSenStatus.Size = new System.Drawing.Size(70, 22);
            this.txbSendDataSenStatus.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 23);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "Send_Status";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txbRxDataRxStatus);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.txbRxDataSendStatus);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Location = new System.Drawing.Point(865, 505);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(160, 74);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Receive Data Status";
            // 
            // txbRxDataRxStatus
            // 
            this.txbRxDataRxStatus.Location = new System.Drawing.Point(80, 46);
            this.txbRxDataRxStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txbRxDataRxStatus.Name = "txbRxDataRxStatus";
            this.txbRxDataRxStatus.Size = new System.Drawing.Size(70, 22);
            this.txbRxDataRxStatus.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 49);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 12);
            this.label13.TabIndex = 21;
            this.label13.Text = "Receive_Status";
            // 
            // txbRxDataSendStatus
            // 
            this.txbRxDataSendStatus.Location = new System.Drawing.Point(80, 20);
            this.txbRxDataSendStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txbRxDataSendStatus.Name = "txbRxDataSendStatus";
            this.txbRxDataSendStatus.Size = new System.Drawing.Size(70, 22);
            this.txbRxDataSendStatus.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 23);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "Send_Status";
            // 
            // btnBlockLocked
            // 
            this.btnBlockLocked.Enabled = false;
            this.btnBlockLocked.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBlockLocked.Location = new System.Drawing.Point(939, 51);
            this.btnBlockLocked.Margin = new System.Windows.Forms.Padding(2);
            this.btnBlockLocked.Name = "btnBlockLocked";
            this.btnBlockLocked.Size = new System.Drawing.Size(111, 18);
            this.btnBlockLocked.TabIndex = 10;
            this.btnBlockLocked.Text = "VehM Block Locked";
            this.btnBlockLocked.UseVisualStyleBackColor = true;
            this.btnBlockLocked.Visible = false;
            this.btnBlockLocked.Click += new System.EventHandler(this.btnBlockLocked_Click);
            // 
            // btnBlockReleased
            // 
            this.btnBlockReleased.Enabled = false;
            this.btnBlockReleased.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBlockReleased.Location = new System.Drawing.Point(829, 14);
            this.btnBlockReleased.Margin = new System.Windows.Forms.Padding(2);
            this.btnBlockReleased.Name = "btnBlockReleased";
            this.btnBlockReleased.Size = new System.Drawing.Size(110, 18);
            this.btnBlockReleased.TabIndex = 11;
            this.btnBlockReleased.Text = "VehM Block Released";
            this.btnBlockReleased.UseVisualStyleBackColor = true;
            this.btnBlockReleased.Visible = false;
            this.btnBlockReleased.Click += new System.EventHandler(this.btnBlockReleased_Click);
            // 
            // lbBlockCtrlReqst
            // 
            this.lbBlockCtrlReqst.AutoSize = true;
            this.lbBlockCtrlReqst.Location = new System.Drawing.Point(510, 558);
            this.lbBlockCtrlReqst.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbBlockCtrlReqst.Name = "lbBlockCtrlReqst";
            this.lbBlockCtrlReqst.Size = new System.Drawing.Size(23, 12);
            this.lbBlockCtrlReqst.TabIndex = 12;
            this.lbBlockCtrlReqst.Text = "FYI";
            this.lbBlockCtrlReqst.Visible = false;
            // 
            // btnToVehMCmd134
            // 
            this.btnToVehMCmd134.Enabled = false;
            this.btnToVehMCmd134.Location = new System.Drawing.Point(578, 546);
            this.btnToVehMCmd134.Margin = new System.Windows.Forms.Padding(2);
            this.btnToVehMCmd134.Name = "btnToVehMCmd134";
            this.btnToVehMCmd134.Size = new System.Drawing.Size(94, 35);
            this.btnToVehMCmd134.TabIndex = 13;
            this.btnToVehMCmd134.Text = "[=> VehM] Cmd#134";
            this.btnToVehMCmd134.UseVisualStyleBackColor = true;
            this.btnToVehMCmd134.Visible = false;
            this.btnToVehMCmd134.Click += new System.EventHandler(this.btnToVehMCmd134_Click);
            // 
            // btnFromVehMCmd39_1PauseMove
            // 
            this.btnFromVehMCmd39_1PauseMove.Enabled = false;
            this.btnFromVehMCmd39_1PauseMove.Location = new System.Drawing.Point(1077, 319);
            this.btnFromVehMCmd39_1PauseMove.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd39_1PauseMove.Name = "btnFromVehMCmd39_1PauseMove";
            this.btnFromVehMCmd39_1PauseMove.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd39_1PauseMove.TabIndex = 14;
            this.btnFromVehMCmd39_1PauseMove.Text = "[VehM =>] Cmd#39-1 Pause";
            this.btnFromVehMCmd39_1PauseMove.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd39_1PauseMove.Visible = false;
            this.btnFromVehMCmd39_1PauseMove.Click += new System.EventHandler(this.btnFromVehMCmd39_1PauseMove_Click);
            // 
            // ckbBlockCtrl
            // 
            this.ckbBlockCtrl.AutoSize = true;
            this.ckbBlockCtrl.Location = new System.Drawing.Point(685, 196);
            this.ckbBlockCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.ckbBlockCtrl.Name = "ckbBlockCtrl";
            this.ckbBlockCtrl.Size = new System.Drawing.Size(110, 16);
            this.ckbBlockCtrl.TabIndex = 15;
            this.ckbBlockCtrl.Text = "Block Control OK";
            this.ckbBlockCtrl.UseVisualStyleBackColor = true;
            this.ckbBlockCtrl.Visible = false;
            this.ckbBlockCtrl.CheckedChanged += new System.EventHandler(this.ckbBlockCtrl_CheckedChanged);
            // 
            // btnFromVehMCmd31_3LoadUnload
            // 
            this.btnFromVehMCmd31_3LoadUnload.Enabled = false;
            this.btnFromVehMCmd31_3LoadUnload.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFromVehMCmd31_3LoadUnload.Location = new System.Drawing.Point(1077, 357);
            this.btnFromVehMCmd31_3LoadUnload.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd31_3LoadUnload.Name = "btnFromVehMCmd31_3LoadUnload";
            this.btnFromVehMCmd31_3LoadUnload.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd31_3LoadUnload.TabIndex = 16;
            this.btnFromVehMCmd31_3LoadUnload.Text = "[VehM =>] Cmd#31-3 Load&&Unload";
            this.btnFromVehMCmd31_3LoadUnload.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd31_3LoadUnload.Visible = false;
            this.btnFromVehMCmd31_3LoadUnload.Click += new System.EventHandler(this.btnFromVehMCmd31_3LoadUnload_Click);
            // 
            // btnFromVehMCmd31_1Load
            // 
            this.btnFromVehMCmd31_1Load.Enabled = false;
            this.btnFromVehMCmd31_1Load.Location = new System.Drawing.Point(1077, 241);
            this.btnFromVehMCmd31_1Load.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd31_1Load.Name = "btnFromVehMCmd31_1Load";
            this.btnFromVehMCmd31_1Load.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd31_1Load.TabIndex = 17;
            this.btnFromVehMCmd31_1Load.Text = "[VehM =>] Cmd#31-1 Load";
            this.btnFromVehMCmd31_1Load.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd31_1Load.Visible = false;
            this.btnFromVehMCmd31_1Load.Click += new System.EventHandler(this.btnFromVehMCmd31_1Load_Click);
            // 
            // btnFromVehMCmd31_2Unload
            // 
            this.btnFromVehMCmd31_2Unload.Enabled = false;
            this.btnFromVehMCmd31_2Unload.Location = new System.Drawing.Point(1077, 551);
            this.btnFromVehMCmd31_2Unload.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd31_2Unload.Name = "btnFromVehMCmd31_2Unload";
            this.btnFromVehMCmd31_2Unload.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd31_2Unload.TabIndex = 18;
            this.btnFromVehMCmd31_2Unload.Text = "VehM XXX =>VehC";
            this.btnFromVehMCmd31_2Unload.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd31_2Unload.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(693, 229);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 12);
            this.label15.TabIndex = 19;
            this.label15.Text = "Load Address";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(865, 229);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 12);
            this.label16.TabIndex = 20;
            this.label16.Text = "To Address";
            // 
            // txbLoadAddress
            // 
            this.txbLoadAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txbLoadAddress.Location = new System.Drawing.Point(696, 248);
            this.txbLoadAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txbLoadAddress.MaxLength = 5;
            this.txbLoadAddress.Name = "txbLoadAddress";
            this.txbLoadAddress.Size = new System.Drawing.Size(68, 22);
            this.txbLoadAddress.TabIndex = 21;
            this.txbLoadAddress.Text = "20311";
            // 
            // txbToAddress
            // 
            this.txbToAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txbToAddress.Location = new System.Drawing.Point(867, 248);
            this.txbToAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txbToAddress.MaxLength = 5;
            this.txbToAddress.Name = "txbToAddress";
            this.txbToAddress.Size = new System.Drawing.Size(68, 22);
            this.txbToAddress.TabIndex = 22;
            this.txbToAddress.Text = "20316";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(692, 279);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 24);
            this.label17.TabIndex = 23;
            this.label17.Text = "Guide Sections\r\n(for Load)";
            // 
            // txbGuideSections4Load
            // 
            this.txbGuideSections4Load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txbGuideSections4Load.Location = new System.Drawing.Point(695, 305);
            this.txbGuideSections4Load.Margin = new System.Windows.Forms.Padding(2);
            this.txbGuideSections4Load.Multiline = true;
            this.txbGuideSections4Load.Name = "txbGuideSections4Load";
            this.txbGuideSections4Load.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbGuideSections4Load.Size = new System.Drawing.Size(68, 141);
            this.txbGuideSections4Load.TabIndex = 24;
            this.txbGuideSections4Load.Text = "0004\r\n0005";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(946, 279);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 24);
            this.label18.TabIndex = 25;
            this.label18.Text = "Cycle Sections\r\n(pure Move)";
            // 
            // txbCycleSections
            // 
            this.txbCycleSections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txbCycleSections.Location = new System.Drawing.Point(946, 305);
            this.txbCycleSections.Margin = new System.Windows.Forms.Padding(2);
            this.txbCycleSections.Multiline = true;
            this.txbCycleSections.Name = "txbCycleSections";
            this.txbCycleSections.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbCycleSections.Size = new System.Drawing.Size(69, 185);
            this.txbCycleSections.TabIndex = 26;
            this.txbCycleSections.Text = "0202\r\n0006\r\n0002\r\n0004\r\n0005\r\n0201";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(694, 455);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 12);
            this.label19.TabIndex = 27;
            this.label19.Text = "Load Cst ID";
            // 
            // txbCstID4Load
            // 
            this.txbCstID4Load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txbCstID4Load.Location = new System.Drawing.Point(695, 469);
            this.txbCstID4Load.Margin = new System.Windows.Forms.Padding(2);
            this.txbCstID4Load.Name = "txbCstID4Load";
            this.txbCstID4Load.Size = new System.Drawing.Size(68, 22);
            this.txbCstID4Load.TabIndex = 28;
            this.txbCstID4Load.Text = "168";
            // 
            // ckbBlockCtlOn
            // 
            this.ckbBlockCtlOn.AutoSize = true;
            this.ckbBlockCtlOn.Location = new System.Drawing.Point(902, 196);
            this.ckbBlockCtlOn.Margin = new System.Windows.Forms.Padding(2);
            this.ckbBlockCtlOn.Name = "ckbBlockCtlOn";
            this.ckbBlockCtlOn.Size = new System.Drawing.Size(136, 16);
            this.ckbBlockCtlOn.TabIndex = 29;
            this.ckbBlockCtlOn.Text = "OHT/C: Block_Ctrl_On";
            this.ckbBlockCtlOn.UseVisualStyleBackColor = true;
            this.ckbBlockCtlOn.Visible = false;
            this.ckbBlockCtlOn.CheckedChanged += new System.EventHandler(this.ckbBlockCtlOn_CheckedChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(260, 542);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(37, 13);
            this.label26.TabIndex = 328;
            this.label26.Text = "Acq By";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(81, 542);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 13);
            this.label20.TabIndex = 326;
            this.label20.Text = "Lock By";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(10, 542);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 13);
            this.label21.TabIndex = 324;
            this.label21.Text = "B/C-Zone";
            // 
            // txtDdsBcZone
            // 
            this.txtDdsBcZone.BackColor = System.Drawing.Color.Lime;
            this.txtDdsBcZone.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDdsBcZone.ForeColor = System.Drawing.Color.Black;
            this.txtDdsBcZone.Location = new System.Drawing.Point(9, 558);
            this.txtDdsBcZone.Margin = new System.Windows.Forms.Padding(2);
            this.txtDdsBcZone.Name = "txtDdsBcZone";
            this.txtDdsBcZone.Size = new System.Drawing.Size(60, 25);
            this.txtDdsBcZone.TabIndex = 329;
            this.txtDdsBcZone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDdsAcqBy
            // 
            this.txtDdsAcqBy.BackColor = System.Drawing.Color.Lime;
            this.txtDdsAcqBy.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDdsAcqBy.ForeColor = System.Drawing.Color.Black;
            this.txtDdsAcqBy.Location = new System.Drawing.Point(259, 558);
            this.txtDdsAcqBy.Margin = new System.Windows.Forms.Padding(2);
            this.txtDdsAcqBy.Name = "txtDdsAcqBy";
            this.txtDdsAcqBy.Size = new System.Drawing.Size(164, 25);
            this.txtDdsAcqBy.TabIndex = 330;
            this.txtDdsAcqBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDdsLockBy
            // 
            this.txtDdsLockBy.BackColor = System.Drawing.Color.Lime;
            this.txtDdsLockBy.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDdsLockBy.ForeColor = System.Drawing.Color.Black;
            this.txtDdsLockBy.Location = new System.Drawing.Point(82, 558);
            this.txtDdsLockBy.Margin = new System.Windows.Forms.Padding(2);
            this.txtDdsLockBy.Name = "txtDdsLockBy";
            this.txtDdsLockBy.Size = new System.Drawing.Size(164, 25);
            this.txtDdsLockBy.TabIndex = 331;
            this.txtDdsLockBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkDdsEdit
            // 
            this.chkDdsEdit.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDdsEdit.AutoSize = true;
            this.chkDdsEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chkDdsEdit.Enabled = false;
            this.chkDdsEdit.Image = global::OHTM.Properties.Resources.edit_32;
            this.chkDdsEdit.Location = new System.Drawing.Point(434, 549);
            this.chkDdsEdit.Margin = new System.Windows.Forms.Padding(2);
            this.chkDdsEdit.Name = "chkDdsEdit";
            this.chkDdsEdit.Size = new System.Drawing.Size(38, 38);
            this.chkDdsEdit.TabIndex = 332;
            this.chkDdsEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDdsEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.chkDdsEdit.UseVisualStyleBackColor = true;
            this.chkDdsEdit.Visible = false;
            this.chkDdsEdit.CheckedChanged += new System.EventHandler(this.chkDdsEdit_CheckedChanged);
            // 
            // btnFromVehMCmd39_0RestartPausingMove
            // 
            this.btnFromVehMCmd39_0RestartPausingMove.Enabled = false;
            this.btnFromVehMCmd39_0RestartPausingMove.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFromVehMCmd39_0RestartPausingMove.Location = new System.Drawing.Point(1077, 279);
            this.btnFromVehMCmd39_0RestartPausingMove.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd39_0RestartPausingMove.Name = "btnFromVehMCmd39_0RestartPausingMove";
            this.btnFromVehMCmd39_0RestartPausingMove.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd39_0RestartPausingMove.TabIndex = 333;
            this.btnFromVehMCmd39_0RestartPausingMove.Text = "[VehM =>] Cmd#39-0 Restart";
            this.btnFromVehMCmd39_0RestartPausingMove.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd39_0RestartPausingMove.Visible = false;
            this.btnFromVehMCmd39_0RestartPausingMove.Click += new System.EventHandler(this.btnFromVehMCmd39_0RestartPausingMove_Click);
            // 
            // btnFromVehMCmd31_0Single
            // 
            this.btnFromVehMCmd31_0Single.Enabled = false;
            this.btnFromVehMCmd31_0Single.Location = new System.Drawing.Point(1077, 395);
            this.btnFromVehMCmd31_0Single.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd31_0Single.Name = "btnFromVehMCmd31_0Single";
            this.btnFromVehMCmd31_0Single.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd31_0Single.TabIndex = 334;
            this.btnFromVehMCmd31_0Single.Text = "[VehM =>] Cmd#31-0 Single";
            this.btnFromVehMCmd31_0Single.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd31_0Single.Visible = false;
            this.btnFromVehMCmd31_0Single.Click += new System.EventHandler(this.btnFromVehMCmd31_0Single_Click);
            // 
            // btnOhtmStopMovingCycle
            // 
            this.btnOhtmStopMovingCycle.Enabled = false;
            this.btnOhtmStopMovingCycle.Location = new System.Drawing.Point(1077, 471);
            this.btnOhtmStopMovingCycle.Margin = new System.Windows.Forms.Padding(2);
            this.btnOhtmStopMovingCycle.Name = "btnOhtmStopMovingCycle";
            this.btnOhtmStopMovingCycle.Size = new System.Drawing.Size(110, 34);
            this.btnOhtmStopMovingCycle.TabIndex = 335;
            this.btnOhtmStopMovingCycle.Text = "[OHTm] Stop Moving-Cycle";
            this.btnOhtmStopMovingCycle.UseVisualStyleBackColor = true;
            this.btnOhtmStopMovingCycle.Visible = false;
            this.btnOhtmStopMovingCycle.Click += new System.EventHandler(this.btnOhtmStopMovingCycle_Click);
            // 
            // btnFromVehMCmd31_5Continue
            // 
            this.btnFromVehMCmd31_5Continue.Enabled = false;
            this.btnFromVehMCmd31_5Continue.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFromVehMCmd31_5Continue.Location = new System.Drawing.Point(940, 14);
            this.btnFromVehMCmd31_5Continue.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd31_5Continue.Name = "btnFromVehMCmd31_5Continue";
            this.btnFromVehMCmd31_5Continue.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd31_5Continue.TabIndex = 336;
            this.btnFromVehMCmd31_5Continue.Text = "[VehM =>] Cmd#31-5 Continue";
            this.btnFromVehMCmd31_5Continue.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd31_5Continue.Visible = false;
            this.btnFromVehMCmd31_5Continue.Click += new System.EventHandler(this.btnFromVehMCmd31_5Continue_Click);
            // 
            // btnFromVehMCmd37_0Cancel
            // 
            this.btnFromVehMCmd37_0Cancel.Enabled = false;
            this.btnFromVehMCmd37_0Cancel.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFromVehMCmd37_0Cancel.Location = new System.Drawing.Point(1077, 202);
            this.btnFromVehMCmd37_0Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd37_0Cancel.Name = "btnFromVehMCmd37_0Cancel";
            this.btnFromVehMCmd37_0Cancel.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd37_0Cancel.TabIndex = 337;
            this.btnFromVehMCmd37_0Cancel.Text = "[VehM =>] Cmd#37-0 Cancel (w/o Tray)";
            this.btnFromVehMCmd37_0Cancel.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd37_0Cancel.Visible = false;
            this.btnFromVehMCmd37_0Cancel.Click += new System.EventHandler(this.btnFromVehMCmd37_0Cancel_Click);
            // 
            // btnFromVehMCmd37_1Abort
            // 
            this.btnFromVehMCmd37_1Abort.Enabled = false;
            this.btnFromVehMCmd37_1Abort.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFromVehMCmd37_1Abort.Location = new System.Drawing.Point(1077, 513);
            this.btnFromVehMCmd37_1Abort.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromVehMCmd37_1Abort.Name = "btnFromVehMCmd37_1Abort";
            this.btnFromVehMCmd37_1Abort.Size = new System.Drawing.Size(110, 34);
            this.btnFromVehMCmd37_1Abort.TabIndex = 338;
            this.btnFromVehMCmd37_1Abort.Text = "[VehM =>] Cmd#37-1 Abort (w/ Tray)";
            this.btnFromVehMCmd37_1Abort.UseVisualStyleBackColor = true;
            this.btnFromVehMCmd37_1Abort.Visible = false;
            this.btnFromVehMCmd37_1Abort.Click += new System.EventHandler(this.btnFromVehMCmd37_1Abort_Click);
            // 
            // txbGuideSections4Unload
            // 
            this.txbGuideSections4Unload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txbGuideSections4Unload.Location = new System.Drawing.Point(778, 305);
            this.txbGuideSections4Unload.Margin = new System.Windows.Forms.Padding(2);
            this.txbGuideSections4Unload.Multiline = true;
            this.txbGuideSections4Unload.Name = "txbGuideSections4Unload";
            this.txbGuideSections4Unload.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbGuideSections4Unload.Size = new System.Drawing.Size(68, 141);
            this.txbGuideSections4Unload.TabIndex = 339;
            this.txbGuideSections4Unload.Text = "0201";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(776, 279);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 24);
            this.label22.TabIndex = 340;
            this.label22.Text = "Guide Sections\r\n(for Unload)";
            // 
            // txbUnloadAddress
            // 
            this.txbUnloadAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txbUnloadAddress.Location = new System.Drawing.Point(778, 248);
            this.txbUnloadAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txbUnloadAddress.MaxLength = 5;
            this.txbUnloadAddress.Name = "txbUnloadAddress";
            this.txbUnloadAddress.Size = new System.Drawing.Size(68, 22);
            this.txbUnloadAddress.TabIndex = 342;
            this.txbUnloadAddress.Text = "20316";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(776, 229);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 12);
            this.label23.TabIndex = 341;
            this.label23.Text = "Unload Address";
            // 
            // txbGuideSections4PureMove
            // 
            this.txbGuideSections4PureMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txbGuideSections4PureMove.Location = new System.Drawing.Point(865, 305);
            this.txbGuideSections4PureMove.Margin = new System.Windows.Forms.Padding(2);
            this.txbGuideSections4PureMove.Multiline = true;
            this.txbGuideSections4PureMove.Name = "txbGuideSections4PureMove";
            this.txbGuideSections4PureMove.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbGuideSections4PureMove.Size = new System.Drawing.Size(69, 185);
            this.txbGuideSections4PureMove.TabIndex = 344;
            this.txbGuideSections4PureMove.Text = "0006\r\n0002\r\n0004\r\n0005\r\n0201";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(865, 279);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(74, 24);
            this.label24.TabIndex = 343;
            this.label24.Text = "Guide Sections\r\n(pure Move)";
            // 
            // txbEntryAddress
            // 
            this.txbEntryAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txbEntryAddress.Location = new System.Drawing.Point(946, 248);
            this.txbEntryAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txbEntryAddress.MaxLength = 5;
            this.txbEntryAddress.Name = "txbEntryAddress";
            this.txbEntryAddress.Size = new System.Drawing.Size(68, 22);
            this.txbEntryAddress.TabIndex = 346;
            this.txbEntryAddress.Text = "20316";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(942, 229);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(71, 12);
            this.label25.TabIndex = 345;
            this.label25.Text = "Entry Address";
            // 
            // txbCstID4Unload
            // 
            this.txbCstID4Unload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txbCstID4Unload.Location = new System.Drawing.Point(778, 469);
            this.txbCstID4Unload.Margin = new System.Windows.Forms.Padding(2);
            this.txbCstID4Unload.Name = "txbCstID4Unload";
            this.txbCstID4Unload.Size = new System.Drawing.Size(68, 22);
            this.txbCstID4Unload.TabIndex = 348;
            this.txbCstID4Unload.Text = "987";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(777, 455);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(72, 12);
            this.label27.TabIndex = 347;
            this.label27.Text = "Unload Cst ID";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(828, 51);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 18);
            this.button1.TabIndex = 349;
            this.button1.Text = "134send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FakeCstID
            // 
            this.FakeCstID.AutoSize = true;
            this.FakeCstID.Location = new System.Drawing.Point(829, 158);
            this.FakeCstID.Margin = new System.Windows.Forms.Padding(2);
            this.FakeCstID.Name = "FakeCstID";
            this.FakeCstID.Size = new System.Drawing.Size(73, 16);
            this.FakeCstID.TabIndex = 350;
            this.FakeCstID.Text = "FakeCstID";
            this.FakeCstID.UseVisualStyleBackColor = true;
            this.FakeCstID.CheckedChanged += new System.EventHandler(this.FakeCstID_CheckedChanged);
            // 
            // XXXTextBox
            // 
            this.XXXTextBox.Location = new System.Drawing.Point(1055, 169);
            this.XXXTextBox.Name = "XXXTextBox";
            this.XXXTextBox.Size = new System.Drawing.Size(100, 22);
            this.XXXTextBox.TabIndex = 351;
            this.XXXTextBox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(987, 172);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 352;
            this.label1.Text = "XXX will be";
            this.label1.Visible = false;
            // 
            // FakeMapCheckBox
            // 
            this.FakeMapCheckBox.AutoSize = true;
            this.FakeMapCheckBox.Location = new System.Drawing.Point(799, 211);
            this.FakeMapCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.FakeMapCheckBox.Name = "FakeMapCheckBox";
            this.FakeMapCheckBox.Size = new System.Drawing.Size(67, 16);
            this.FakeMapCheckBox.TabIndex = 353;
            this.FakeMapCheckBox.Text = "FakeMap";
            this.FakeMapCheckBox.UseVisualStyleBackColor = true;
            this.FakeMapCheckBox.Visible = false;
            this.FakeMapCheckBox.CheckedChanged += new System.EventHandler(this.FakeMapCheckBox_CheckedChanged);
            // 
            // testLoadUnloadCmd
            // 
            this.testLoadUnloadCmd.Location = new System.Drawing.Point(1054, 13);
            this.testLoadUnloadCmd.Margin = new System.Windows.Forms.Padding(2);
            this.testLoadUnloadCmd.Name = "testLoadUnloadCmd";
            this.testLoadUnloadCmd.Size = new System.Drawing.Size(110, 34);
            this.testLoadUnloadCmd.TabIndex = 354;
            this.testLoadUnloadCmd.Text = "testLoadUnloadCmd";
            this.testLoadUnloadCmd.UseVisualStyleBackColor = true;
            this.testLoadUnloadCmd.Visible = false;
            this.testLoadUnloadCmd.Click += new System.EventHandler(this.testLoadUnloadCmd_Click);
            // 
            // test_Veh_readMap
            // 
            this.test_Veh_readMap.Location = new System.Drawing.Point(1055, 51);
            this.test_Veh_readMap.Margin = new System.Windows.Forms.Padding(2);
            this.test_Veh_readMap.Name = "test_Veh_readMap";
            this.test_Veh_readMap.Size = new System.Drawing.Size(110, 34);
            this.test_Veh_readMap.TabIndex = 355;
            this.test_Veh_readMap.Text = "testReadVehMap";
            this.test_Veh_readMap.UseVisualStyleBackColor = true;
            this.test_Veh_readMap.Visible = false;
            this.test_Veh_readMap.Click += new System.EventHandler(this.test_Veh_readMap_Click);
            // 
            // LoadBtn
            // 
            this.LoadBtn.Enabled = false;
            this.LoadBtn.Location = new System.Drawing.Point(1054, 89);
            this.LoadBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(110, 34);
            this.LoadBtn.TabIndex = 356;
            this.LoadBtn.Text = "testVehLoad";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Visible = false;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // UnLoadBtn
            // 
            this.UnLoadBtn.Enabled = false;
            this.UnLoadBtn.Location = new System.Drawing.Point(1054, 130);
            this.UnLoadBtn.Margin = new System.Windows.Forms.Padding(2);
            this.UnLoadBtn.Name = "UnLoadBtn";
            this.UnLoadBtn.Size = new System.Drawing.Size(110, 34);
            this.UnLoadBtn.TabIndex = 357;
            this.UnLoadBtn.Text = "testVehUnload";
            this.UnLoadBtn.UseVisualStyleBackColor = true;
            this.UnLoadBtn.Visible = false;
            this.UnLoadBtn.Click += new System.EventHandler(this.UnLoadBtn_Click);
            // 
            // fake144charge_check
            // 
            this.fake144charge_check.AutoSize = true;
            this.fake144charge_check.Location = new System.Drawing.Point(902, 211);
            this.fake144charge_check.Margin = new System.Windows.Forms.Padding(2);
            this.fake144charge_check.Name = "fake144charge_check";
            this.fake144charge_check.Size = new System.Drawing.Size(109, 16);
            this.fake144charge_check.TabIndex = 358;
            this.fake144charge_check.Text = "fake144Forcharge";
            this.fake144charge_check.UseVisualStyleBackColor = true;
            this.fake144charge_check.Visible = false;
            this.fake144charge_check.CheckedChanged += new System.EventHandler(this.fake144charge_check_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1176, 599);
            this.Controls.Add(this.fake144charge_check);
            this.Controls.Add(this.UnLoadBtn);
            this.Controls.Add(this.LoadBtn);
            this.Controls.Add(this.test_Veh_readMap);
            this.Controls.Add(this.testLoadUnloadCmd);
            this.Controls.Add(this.FakeMapCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.XXXTextBox);
            this.Controls.Add(this.FakeCstID);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txbCstID4Unload);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txbEntryAddress);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txbGuideSections4PureMove);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txbUnloadAddress);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txbGuideSections4Unload);
            this.Controls.Add(this.btnFromVehMCmd37_1Abort);
            this.Controls.Add(this.btnFromVehMCmd37_0Cancel);
            this.Controls.Add(this.btnFromVehMCmd31_5Continue);
            this.Controls.Add(this.btnOhtmStopMovingCycle);
            this.Controls.Add(this.btnFromVehMCmd31_0Single);
            this.Controls.Add(this.btnFromVehMCmd39_0RestartPausingMove);
            this.Controls.Add(this.chkDdsEdit);
            this.Controls.Add(this.txtDdsLockBy);
            this.Controls.Add(this.txtDdsAcqBy);
            this.Controls.Add(this.txtDdsBcZone);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.ckbBlockCtlOn);
            this.Controls.Add(this.txbCstID4Load);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txbCycleSections);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txbGuideSections4Load);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txbToAddress);
            this.Controls.Add(this.txbLoadAddress);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnFromVehMCmd31_2Unload);
            this.Controls.Add(this.btnFromVehMCmd31_1Load);
            this.Controls.Add(this.btnFromVehMCmd31_3LoadUnload);
            this.Controls.Add(this.ckbBlockCtrl);
            this.Controls.Add(this.btnFromVehMCmd39_1PauseMove);
            this.Controls.Add(this.btnToVehMCmd134);
            this.Controls.Add(this.lbBlockCtrlReqst);
            this.Controls.Add(this.btnBlockReleased);
            this.Controls.Add(this.btnBlockLocked);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.ckbOffLine);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnFromVehMCmd31_6Cycle);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "0106_V2_LULADDR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerVehStatusData;
        private System.Windows.Forms.Button btnFromVehMCmd31_6Cycle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txbMsgToVehicle;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txbMsgFromVehM;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txbMsgFromVeh;
        private System.Windows.Forms.CheckBox ckbOffLine;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txbMsgToVehM;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txbSendDataRxStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txbSendDataSenStatus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txbRxDataRxStatus;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txbRxDataSendStatus;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnBlockLocked;
        private System.Windows.Forms.Button btnBlockReleased;
        private System.Windows.Forms.Label lbBlockCtrlReqst;
        private System.Windows.Forms.Button btnToVehMCmd134;
        private System.Windows.Forms.Button btnFromVehMCmd39_1PauseMove;
        private System.Windows.Forms.CheckBox ckbBlockCtrl;
        private System.Windows.Forms.Button btnFromVehMCmd31_3LoadUnload;
        private System.Windows.Forms.Button btnFromVehMCmd31_1Load;
        private System.Windows.Forms.Button btnFromVehMCmd31_2Unload;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txbLoadAddress;
        private System.Windows.Forms.TextBox txbToAddress;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txbGuideSections4Load;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txbCycleSections;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txbCstID4Load;
        private System.Windows.Forms.CheckBox ckbBlockCtlOn;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtDdsBcZone;
        private System.Windows.Forms.TextBox txtDdsAcqBy;
        private System.Windows.Forms.TextBox txtDdsLockBy;
        private System.Windows.Forms.CheckBox chkDdsEdit;
        private System.Windows.Forms.Button btnFromVehMCmd39_0RestartPausingMove;
        private System.Windows.Forms.Button btnFromVehMCmd31_0Single;
        private System.Windows.Forms.Button btnOhtmStopMovingCycle;
        private System.Windows.Forms.Button btnFromVehMCmd31_5Continue;
        private System.Windows.Forms.Button btnFromVehMCmd37_0Cancel;
        private System.Windows.Forms.Button btnFromVehMCmd37_1Abort;
        private System.Windows.Forms.TextBox txbGuideSections4Unload;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txbUnloadAddress;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txbGuideSections4PureMove;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txbEntryAddress;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txbCstID4Unload;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox FakeCstID;
        private System.Windows.Forms.TextBox XXXTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox FakeMapCheckBox;
        private System.Windows.Forms.Button testLoadUnloadCmd;
        private System.Windows.Forms.Button test_Veh_readMap;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.Button UnLoadBtn;
        private System.Windows.Forms.CheckBox fake144charge_check;
    }
}

