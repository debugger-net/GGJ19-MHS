namespace MHS.Player
{
    partial class _frmTest
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
            this.lblfTime = new System.Windows.Forms.Label();
            this.lbliCurrentTime = new System.Windows.Forms.Label();
            this.lblfSubscribers = new System.Windows.Forms.Label();
            this.lblfMoney = new System.Windows.Forms.Label();
            this.lbliSubscribers = new System.Windows.Forms.Label();
            this.lbliMoney = new System.Windows.Forms.Label();
            this.lbliGameSpeed = new System.Windows.Forms.Label();
            this.btnGameSpeedMinus = new System.Windows.Forms.Button();
            this.btnGameSpeedPlus = new System.Windows.Forms.Button();
            this.lblfHealth = new System.Windows.Forms.Label();
            this.lblfMental = new System.Windows.Forms.Label();
            this.lbliHealth = new System.Windows.Forms.Label();
            this.lbliMental = new System.Windows.Forms.Label();
            this.btnStreamingStart = new System.Windows.Forms.Button();
            this.lbliStreamingStatus = new System.Windows.Forms.Label();
            this.btnStreamingFinish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblfTime
            // 
            this.lblfTime.AutoSize = true;
            this.lblfTime.Location = new System.Drawing.Point(27, 39);
            this.lblfTime.Name = "lblfTime";
            this.lblfTime.Size = new System.Drawing.Size(149, 24);
            this.lblfTime.TabIndex = 0;
            this.lblfTime.Text = "Current Time:";
            // 
            // lbliCurrentTime
            // 
            this.lbliCurrentTime.AutoSize = true;
            this.lbliCurrentTime.Location = new System.Drawing.Point(182, 39);
            this.lbliCurrentTime.Name = "lbliCurrentTime";
            this.lbliCurrentTime.Size = new System.Drawing.Size(156, 24);
            this.lbliCurrentTime.TabIndex = 1;
            this.lbliCurrentTime.Text = "<Game Time>";
            // 
            // lblfSubscribers
            // 
            this.lblfSubscribers.AutoSize = true;
            this.lblfSubscribers.Location = new System.Drawing.Point(31, 201);
            this.lblfSubscribers.Name = "lblfSubscribers";
            this.lblfSubscribers.Size = new System.Drawing.Size(145, 24);
            this.lblfSubscribers.TabIndex = 2;
            this.lblfSubscribers.Text = "Subscribers: ";
            // 
            // lblfMoney
            // 
            this.lblfMoney.AutoSize = true;
            this.lblfMoney.Location = new System.Drawing.Point(79, 239);
            this.lblfMoney.Name = "lblfMoney";
            this.lblfMoney.Size = new System.Drawing.Size(87, 24);
            this.lblfMoney.TabIndex = 3;
            this.lblfMoney.Text = "Money:";
            // 
            // lbliSubscribers
            // 
            this.lbliSubscribers.AutoSize = true;
            this.lbliSubscribers.Location = new System.Drawing.Point(182, 201);
            this.lbliSubscribers.Name = "lbliSubscribers";
            this.lbliSubscribers.Size = new System.Drawing.Size(23, 24);
            this.lbliSubscribers.TabIndex = 4;
            this.lbliSubscribers.Text = "0";
            // 
            // lbliMoney
            // 
            this.lbliMoney.AutoSize = true;
            this.lbliMoney.Location = new System.Drawing.Point(182, 239);
            this.lbliMoney.Name = "lbliMoney";
            this.lbliMoney.Size = new System.Drawing.Size(23, 24);
            this.lbliMoney.TabIndex = 5;
            this.lbliMoney.Text = "0";
            // 
            // lbliGameSpeed
            // 
            this.lbliGameSpeed.AutoSize = true;
            this.lbliGameSpeed.Location = new System.Drawing.Point(182, 85);
            this.lbliGameSpeed.Name = "lbliGameSpeed";
            this.lbliGameSpeed.Size = new System.Drawing.Size(202, 24);
            this.lbliGameSpeed.TabIndex = 6;
            this.lbliGameSpeed.Text = "(Game Speed - 2)";
            // 
            // btnGameSpeedMinus
            // 
            this.btnGameSpeedMinus.Location = new System.Drawing.Point(146, 82);
            this.btnGameSpeedMinus.Name = "btnGameSpeedMinus";
            this.btnGameSpeedMinus.Size = new System.Drawing.Size(30, 31);
            this.btnGameSpeedMinus.TabIndex = 7;
            this.btnGameSpeedMinus.Text = "-";
            this.btnGameSpeedMinus.UseVisualStyleBackColor = true;
            this.btnGameSpeedMinus.Click += new System.EventHandler(this.btnGameSpeedMinus_Click);
            // 
            // btnGameSpeedPlus
            // 
            this.btnGameSpeedPlus.Location = new System.Drawing.Point(390, 81);
            this.btnGameSpeedPlus.Name = "btnGameSpeedPlus";
            this.btnGameSpeedPlus.Size = new System.Drawing.Size(30, 33);
            this.btnGameSpeedPlus.TabIndex = 8;
            this.btnGameSpeedPlus.Text = "+";
            this.btnGameSpeedPlus.UseVisualStyleBackColor = true;
            this.btnGameSpeedPlus.Click += new System.EventHandler(this.btnGameSpeedPlus_Click);
            // 
            // lblfHealth
            // 
            this.lblfHealth.AutoSize = true;
            this.lblfHealth.Location = new System.Drawing.Point(499, 201);
            this.lblfHealth.Name = "lblfHealth";
            this.lblfHealth.Size = new System.Drawing.Size(82, 24);
            this.lblfHealth.TabIndex = 9;
            this.lblfHealth.Text = "Health:";
            // 
            // lblfMental
            // 
            this.lblfMental.AutoSize = true;
            this.lblfMental.Location = new System.Drawing.Point(496, 239);
            this.lblfMental.Name = "lblfMental";
            this.lblfMental.Size = new System.Drawing.Size(85, 24);
            this.lblfMental.TabIndex = 10;
            this.lblfMental.Text = "Mental:";
            // 
            // lbliHealth
            // 
            this.lbliHealth.AutoSize = true;
            this.lbliHealth.Location = new System.Drawing.Point(587, 201);
            this.lbliHealth.Name = "lbliHealth";
            this.lbliHealth.Size = new System.Drawing.Size(23, 24);
            this.lbliHealth.TabIndex = 11;
            this.lbliHealth.Text = "0";
            // 
            // lbliMental
            // 
            this.lbliMental.AutoSize = true;
            this.lbliMental.Location = new System.Drawing.Point(587, 239);
            this.lbliMental.Name = "lbliMental";
            this.lbliMental.Size = new System.Drawing.Size(23, 24);
            this.lbliMental.TabIndex = 12;
            this.lbliMental.Text = "0";
            // 
            // btnStreamingStart
            // 
            this.btnStreamingStart.Location = new System.Drawing.Point(31, 446);
            this.btnStreamingStart.Name = "btnStreamingStart";
            this.btnStreamingStart.Size = new System.Drawing.Size(152, 92);
            this.btnStreamingStart.TabIndex = 13;
            this.btnStreamingStart.Text = "Start Streaming";
            this.btnStreamingStart.UseVisualStyleBackColor = true;
            this.btnStreamingStart.Click += new System.EventHandler(this.btnStreamingStart_Click);
            // 
            // lbliStreamingStatus
            // 
            this.lbliStreamingStatus.AutoSize = true;
            this.lbliStreamingStatus.BackColor = System.Drawing.Color.Silver;
            this.lbliStreamingStatus.Location = new System.Drawing.Point(31, 376);
            this.lbliStreamingStatus.Name = "lbliStreamingStatus";
            this.lbliStreamingStatus.Size = new System.Drawing.Size(155, 24);
            this.lbliStreamingStatus.TabIndex = 14;
            this.lbliStreamingStatus.Text = "Not Streaming";
            // 
            // btnStreamingFinish
            // 
            this.btnStreamingFinish.Location = new System.Drawing.Point(205, 446);
            this.btnStreamingFinish.Name = "btnStreamingFinish";
            this.btnStreamingFinish.Size = new System.Drawing.Size(152, 92);
            this.btnStreamingFinish.TabIndex = 15;
            this.btnStreamingFinish.Text = "Finish Streaming";
            this.btnStreamingFinish.UseVisualStyleBackColor = true;
            this.btnStreamingFinish.Click += new System.EventHandler(this.btnStreamingFinish_Click);
            // 
            // _frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 697);
            this.Controls.Add(this.btnStreamingFinish);
            this.Controls.Add(this.lbliStreamingStatus);
            this.Controls.Add(this.btnStreamingStart);
            this.Controls.Add(this.lbliMental);
            this.Controls.Add(this.lbliHealth);
            this.Controls.Add(this.lblfMental);
            this.Controls.Add(this.lblfHealth);
            this.Controls.Add(this.btnGameSpeedPlus);
            this.Controls.Add(this.btnGameSpeedMinus);
            this.Controls.Add(this.lbliGameSpeed);
            this.Controls.Add(this.lbliMoney);
            this.Controls.Add(this.lbliSubscribers);
            this.Controls.Add(this.lblfMoney);
            this.Controls.Add(this.lblfSubscribers);
            this.Controls.Add(this.lbliCurrentTime);
            this.Controls.Add(this.lblfTime);
            this.KeyPreview = true;
            this.Name = "_frmTest";
            this.Text = "TEST :: My Home Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._frmTest_FormClosing);
            this.Load += new System.EventHandler(this._frmTest_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._frmTest_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblfTime;
        private System.Windows.Forms.Label lbliCurrentTime;
        private System.Windows.Forms.Label lblfSubscribers;
        private System.Windows.Forms.Label lblfMoney;
        private System.Windows.Forms.Label lbliSubscribers;
        private System.Windows.Forms.Label lbliMoney;
        private System.Windows.Forms.Label lbliGameSpeed;
        private System.Windows.Forms.Button btnGameSpeedMinus;
        private System.Windows.Forms.Button btnGameSpeedPlus;
        private System.Windows.Forms.Label lblfHealth;
        private System.Windows.Forms.Label lblfMental;
        private System.Windows.Forms.Label lbliHealth;
        private System.Windows.Forms.Label lbliMental;
        private System.Windows.Forms.Button btnStreamingStart;
        private System.Windows.Forms.Label lbliStreamingStatus;
        private System.Windows.Forms.Button btnStreamingFinish;
    }
}