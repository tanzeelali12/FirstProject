namespace ClashFreeTimetable
{
    partial class Generate
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rooms = new System.Windows.Forms.NumericUpDown();
            this.startHour = new System.Windows.Forms.NumericUpDown();
            this.startMin = new System.Windows.Forms.NumericUpDown();
            this.endHour = new System.Windows.Forms.NumericUpDown();
            this.endMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.open = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.update = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.export = new System.Windows.Forms.Button();
            this.code = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endMin)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rooms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Starting Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ending Time";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // rooms
            // 
            this.rooms.Location = new System.Drawing.Point(52, 36);
            this.rooms.Name = "rooms";
            this.rooms.Size = new System.Drawing.Size(45, 20);
            this.rooms.TabIndex = 8;
            // 
            // startHour
            // 
            this.startHour.Location = new System.Drawing.Point(227, 36);
            this.startHour.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.startHour.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.startHour.Name = "startHour";
            this.startHour.Size = new System.Drawing.Size(45, 20);
            this.startHour.TabIndex = 9;
            this.startHour.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // startMin
            // 
            this.startMin.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.startMin.Location = new System.Drawing.Point(293, 36);
            this.startMin.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.startMin.Name = "startMin";
            this.startMin.Size = new System.Drawing.Size(45, 20);
            this.startMin.TabIndex = 10;
            // 
            // endHour
            // 
            this.endHour.Location = new System.Drawing.Point(416, 36);
            this.endHour.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.endHour.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.endHour.Name = "endHour";
            this.endHour.Size = new System.Drawing.Size(45, 20);
            this.endHour.TabIndex = 11;
            this.endHour.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // endMin
            // 
            this.endMin.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.endMin.Location = new System.Drawing.Point(479, 36);
            this.endMin.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.endMin.Name = "endMin";
            this.endMin.Size = new System.Drawing.Size(45, 20);
            this.endMin.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(273, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(459, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = ":";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(52, 105);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(198, 84);
            this.flowLayoutPanel1.TabIndex = 15;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // open
            // 
            this.open.BackColor = System.Drawing.Color.Maroon;
            this.open.Font = new System.Drawing.Font("Papyrus", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.open.ForeColor = System.Drawing.Color.Ivory;
            this.open.Location = new System.Drawing.Point(666, 26);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(87, 38);
            this.open.TabIndex = 20;
            this.open.Text = "Generate";
            this.open.UseVisualStyleBackColor = false;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(52, 223);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel2.TabIndex = 21;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(52, 344);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel3.TabIndex = 22;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoScroll = true;
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(52, 473);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel4.TabIndex = 23;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoScroll = true;
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(52, 580);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel5.TabIndex = 24;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Maroon;
            this.button2.Font = new System.Drawing.Font("Papyrus", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Ivory;
            this.button2.Location = new System.Drawing.Point(1059, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 38);
            this.button2.TabIndex = 25;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(700, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = " ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(426, 687);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "  ";
            // 
            // update
            // 
            this.update.BackColor = System.Drawing.Color.Maroon;
            this.update.Font = new System.Drawing.Font("Papyrus", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update.ForeColor = System.Drawing.Color.Ivory;
            this.update.Location = new System.Drawing.Point(783, 26);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(126, 38);
            this.update.TabIndex = 28;
            this.update.Text = "Update Database";
            this.update.UseVisualStyleBackColor = false;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.Maroon;
            this.Close.Font = new System.Drawing.Font("Papyrus", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.ForeColor = System.Drawing.Color.Ivory;
            this.Close.Location = new System.Drawing.Point(1172, 26);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(87, 38);
            this.Close.TabIndex = 29;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // export
            // 
            this.export.BackColor = System.Drawing.Color.Maroon;
            this.export.Font = new System.Drawing.Font("Papyrus", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.export.ForeColor = System.Drawing.Color.Ivory;
            this.export.Location = new System.Drawing.Point(937, 26);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(103, 38);
            this.export.TabIndex = 30;
            this.export.Text = "Export Excel";
            this.export.UseVisualStyleBackColor = false;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // code
            // 
            this.code.Location = new System.Drawing.Point(1073, 105);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(100, 20);
            this.code.TabIndex = 31;
            this.code.TextChanged += new System.EventHandler(this.code_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(780, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(393, 17);
            this.label8.TabIndex = 33;
            this.label8.Text = "Enter Course Code and click on the column to Check Clashes";
            // 
            // Generate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ClientSize = new System.Drawing.Size(1292, 741);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.code);
            this.Controls.Add(this.export);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.update);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.flowLayoutPanel5);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.open);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.endMin);
            this.Controls.Add(this.endHour);
            this.Controls.Add(this.startMin);
            this.Controls.Add(this.startHour);
            this.Controls.Add(this.rooms);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Generate";
            this.Text = "Generate";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Generate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown rooms;
        private System.Windows.Forms.NumericUpDown startHour;
        private System.Windows.Forms.NumericUpDown startMin;
        private System.Windows.Forms.NumericUpDown endHour;
        private System.Windows.Forms.NumericUpDown endMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.TextBox code;
        private System.Windows.Forms.Label label8;
    }
}