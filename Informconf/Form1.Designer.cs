namespace Informconf
{
    partial class FmStart
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
            this.lblTxt = new System.Windows.Forms.Label();
            this.bntStart = new System.Windows.Forms.Button();
            this.dtg = new System.Windows.Forms.DataGridView();
            this.btnStop = new System.Windows.Forms.Button();
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTxt
            // 
            this.lblTxt.AutoSize = true;
            this.lblTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTxt.Location = new System.Drawing.Point(12, 52);
            this.lblTxt.Name = "lblTxt";
            this.lblTxt.Size = new System.Drawing.Size(449, 31);
            this.lblTxt.TabIndex = 0;
            this.lblTxt.Text = "INTEGRACION DATOS MOROSIDAD";
            this.lblTxt.Click += new System.EventHandler(this.lblTxt_Click);
            // 
            // bntStart
            // 
            this.bntStart.AccessibleName = "bntStart";
            this.bntStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntStart.Location = new System.Drawing.Point(516, 47);
            this.bntStart.Name = "bntStart";
            this.bntStart.Size = new System.Drawing.Size(209, 40);
            this.bntStart.TabIndex = 1;
            this.bntStart.Text = "INICIAR";
            this.bntStart.UseVisualStyleBackColor = true;
            this.bntStart.Click += new System.EventHandler(this.bntStart_Click);
            // 
            // dtg
            // 
            this.dtg.AllowUserToOrderColumns = true;
            this.dtg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtg.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg.Location = new System.Drawing.Point(21, 146);
            this.dtg.Name = "dtg";
            this.dtg.RowHeadersWidth = 51;
            this.dtg.RowTemplate.Height = 24;
            this.dtg.Size = new System.Drawing.Size(1517, 476);
            this.dtg.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.AccessibleName = "btnStop";
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(774, 47);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(209, 40);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "DETENER";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timerStart
            // 
            this.timerStart.Enabled = true;
            this.timerStart.Interval = 86400000;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // FmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1610, 647);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.bntStart);
            this.Controls.Add(this.lblTxt);
            this.Name = "FmStart";
            this.Text = "SAP INFORMCONF V1";
            this.Load += new System.EventHandler(this.FmStart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTxt;
        private System.Windows.Forms.Button bntStart;
        private System.Windows.Forms.DataGridView dtg;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timerStart;
    }
}

