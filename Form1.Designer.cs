namespace game
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        // <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectButton = new System.Windows.Forms.Button();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.NewMsgLabel = new System.Windows.Forms.Label();
            this.lbThisPts = new System.Windows.Forms.Label();
            this.lbOpponentPts = new System.Windows.Forms.Label();
            this.lbErrors = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chboxPanelsBorders = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(371, 440);
            this.connectButton.Margin = new System.Windows.Forms.Padding(2);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(232, 81);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "find match";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Location = new System.Drawing.Point(190, 31);
            this.GamePanel.Margin = new System.Windows.Forms.Padding(2);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(585, 310);
            this.GamePanel.TabIndex = 1;
            // 
            // NewMsgLabel
            // 
            this.NewMsgLabel.AutoSize = true;
            this.NewMsgLabel.Location = new System.Drawing.Point(188, 343);
            this.NewMsgLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NewMsgLabel.Name = "NewMsgLabel";
            this.NewMsgLabel.Size = new System.Drawing.Size(0, 13);
            this.NewMsgLabel.TabIndex = 2;
            // 
            // lbThisPts
            // 
            this.lbThisPts.AutoSize = true;
            this.lbThisPts.Location = new System.Drawing.Point(187, 356);
            this.lbThisPts.Name = "lbThisPts";
            this.lbThisPts.Size = new System.Drawing.Size(0, 13);
            this.lbThisPts.TabIndex = 3;
            this.lbThisPts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbOpponentPts
            // 
            this.lbOpponentPts.AutoSize = true;
            this.lbOpponentPts.Location = new System.Drawing.Point(740, 356);
            this.lbOpponentPts.Name = "lbOpponentPts";
            this.lbOpponentPts.Size = new System.Drawing.Size(0, 13);
            this.lbOpponentPts.TabIndex = 4;
            this.lbOpponentPts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbErrors
            // 
            this.lbErrors.FormattingEnabled = true;
            this.lbErrors.Location = new System.Drawing.Point(24, 526);
            this.lbErrors.Name = "lbErrors";
            this.lbErrors.Size = new System.Drawing.Size(763, 95);
            this.lbErrors.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Errors";
            // 
            // chboxPanelsBorders
            // 
            this.chboxPanelsBorders.AutoSize = true;
            this.chboxPanelsBorders.Location = new System.Drawing.Point(820, 150);
            this.chboxPanelsBorders.Name = "chboxPanelsBorders";
            this.chboxPanelsBorders.Size = new System.Drawing.Size(95, 17);
            this.chboxPanelsBorders.TabIndex = 7;
            this.chboxPanelsBorders.Text = "panels borders";
            this.chboxPanelsBorders.UseVisualStyleBackColor = true;
            this.chboxPanelsBorders.CheckedChanged += new System.EventHandler(this.chboxPanelsBorders_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 646);
            this.Controls.Add(this.chboxPanelsBorders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbErrors);
            this.Controls.Add(this.lbOpponentPts);
            this.Controls.Add(this.lbThisPts);
            this.Controls.Add(this.NewMsgLabel);
            this.Controls.Add(this.GamePanel);
            this.Controls.Add(this.connectButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.Label NewMsgLabel;
        private System.Windows.Forms.Label lbThisPts;
        private System.Windows.Forms.Label lbOpponentPts;
        private System.Windows.Forms.ListBox lbErrors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chboxPanelsBorders;
    }
}

