namespace PgSql
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
            this.button1 = new System.Windows.Forms.Button();
            this.tbDataItems = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.firstname_txt = new System.Windows.Forms.TextBox();
            this.secondname_txt = new System.Windows.Forms.TextBox();
            this.meadlename_txt = new System.Windows.Forms.TextBox();
            this.studying_txt = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.textBox_sheet = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.id_text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(21, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get data  from table student";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbDataItems
            // 
            this.tbDataItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDataItems.Location = new System.Drawing.Point(21, 301);
            this.tbDataItems.Multiline = true;
            this.tbDataItems.Name = "tbDataItems";
            this.tbDataItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDataItems.Size = new System.Drawing.Size(491, 205);
            this.tbDataItems.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(295, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(217, 59);
            this.button2.TabIndex = 2;
            this.button2.Text = "Get filtered data from table student";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Last Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Meadle Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Studying";
            // 
            // firstname_txt
            // 
            this.firstname_txt.Location = new System.Drawing.Point(118, 22);
            this.firstname_txt.Name = "firstname_txt";
            this.firstname_txt.Size = new System.Drawing.Size(142, 20);
            this.firstname_txt.TabIndex = 9;
            // 
            // secondname_txt
            // 
            this.secondname_txt.Location = new System.Drawing.Point(118, 50);
            this.secondname_txt.Name = "secondname_txt";
            this.secondname_txt.Size = new System.Drawing.Size(142, 20);
            this.secondname_txt.TabIndex = 10;
            // 
            // meadlename_txt
            // 
            this.meadlename_txt.Location = new System.Drawing.Point(118, 83);
            this.meadlename_txt.Name = "meadlename_txt";
            this.meadlename_txt.Size = new System.Drawing.Size(142, 20);
            this.meadlename_txt.TabIndex = 11;
            // 
            // studying_txt
            // 
            this.studying_txt.Location = new System.Drawing.Point(118, 116);
            this.studying_txt.Name = "studying_txt";
            this.studying_txt.Size = new System.Drawing.Size(391, 20);
            this.studying_txt.TabIndex = 12;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(288, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(221, 64);
            this.button3.TabIndex = 10;
            this.button3.Text = "Insert data into table student";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(730, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 44);
            this.button4.TabIndex = 13;
            this.button4.Text = "Save data to File Excel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(564, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(558, 313);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cellclick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(564, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(160, 44);
            this.button5.TabIndex = 15;
            this.button5.Text = "Import From Data Base and Show";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(666, 403);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(272, 20);
            this.textBox_path.TabIndex = 16;
            // 
            // textBox_sheet
            // 
            this.textBox_sheet.Location = new System.Drawing.Point(666, 433);
            this.textBox_sheet.Name = "textBox_sheet";
            this.textBox_sheet.Size = new System.Drawing.Size(272, 20);
            this.textBox_sheet.TabIndex = 17;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(564, 400);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 18;
            this.button6.Text = "Choose File";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(564, 459);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(420, 27);
            this.button7.TabIndex = 19;
            this.button7.Text = "Load File to the  GridView";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(561, 436);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Write Sheet Name";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(871, 12);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(113, 44);
            this.button8.TabIndex = 21;
            this.button8.Text = "ADD data to DataBase";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(295, 165);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(194, 34);
            this.button9.TabIndex = 22;
            this.button9.Text = "Export DataBase to Text file";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(12, 165);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(220, 29);
            this.button10.TabIndex = 23;
            this.button10.Text = "Update slected row in grid view";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(990, 12);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(132, 44);
            this.button11.TabIndex = 24;
            this.button11.Text = "free button";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // id_text
            // 
            this.id_text.Location = new System.Drawing.Point(118, 139);
            this.id_text.Name = "id_text";
            this.id_text.Size = new System.Drawing.Size(100, 20);
            this.id_text.TabIndex = 25;
            this.id_text.TextChanged += new System.EventHandler(this.id_text_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Id";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1134, 508);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.id_text);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox_sheet);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.studying_txt);
            this.Controls.Add(this.meadlename_txt);
            this.Controls.Add(this.secondname_txt);
            this.Controls.Add(this.firstname_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbDataItems);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.TextBox tbDataItems;
      private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox firstname_txt;
        private System.Windows.Forms.TextBox secondname_txt;
        private System.Windows.Forms.TextBox meadlename_txt;
        private System.Windows.Forms.TextBox studying_txt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.TextBox textBox_sheet;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TextBox id_text;
        private System.Windows.Forms.Label label1;
    }
}

