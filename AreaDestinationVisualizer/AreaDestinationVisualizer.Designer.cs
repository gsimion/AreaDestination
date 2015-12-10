namespace AreaDestinationVisualizer
{
   partial class ADVisualizer
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
         this.txtDestSet = new System.Windows.Forms.TextBox();
         this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
         this.btnPaint = new System.Windows.Forms.Button();
         this.pnlDestSet = new System.Windows.Forms.Panel();
         this.lblRepresentation = new System.Windows.Forms.Label();
         this.lblDestinationSet = new System.Windows.Forms.Label();
         this.btnPopulate = new System.Windows.Forms.Button();
         this.lstDestination = new System.Windows.Forms.ListBox();
         this.lblDestination = new System.Windows.Forms.Label();
         this.tlpTooltip = new System.Windows.Forms.ToolTip(this.components);
         this.txtAreas = new System.Windows.Forms.TextBox();
         this.lblAreas = new System.Windows.Forms.Label();
         this.btnUpdate = new System.Windows.Forms.Button();
         this.lblDestinationID = new System.Windows.Forms.Label();
         this.lblDestAreas = new System.Windows.Forms.Label();
         this.txtDestID = new System.Windows.Forms.TextBox();
         ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // txtDestSet
         // 
         this.txtDestSet.Location = new System.Drawing.Point(145, 57);
         this.txtDestSet.Multiline = true;
         this.txtDestSet.Name = "txtDestSet";
         this.txtDestSet.Size = new System.Drawing.Size(358, 80);
         this.txtDestSet.TabIndex = 0;
         // 
         // errProvider
         // 
         this.errProvider.ContainerControl = this;
         // 
         // btnPaint
         // 
         this.btnPaint.Location = new System.Drawing.Point(509, 114);
         this.btnPaint.Name = "btnPaint";
         this.btnPaint.Size = new System.Drawing.Size(75, 23);
         this.btnPaint.TabIndex = 5;
         this.btnPaint.Text = "Paint";
         this.btnPaint.UseVisualStyleBackColor = true;
         this.btnPaint.Click += new System.EventHandler(this.btnPaint_Click);
         // 
         // pnlDestSet
         // 
         this.pnlDestSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.pnlDestSet.BackColor = System.Drawing.SystemColors.Window;
         this.pnlDestSet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.pnlDestSet.Location = new System.Drawing.Point(388, 169);
         this.pnlDestSet.Name = "pnlDestSet";
         this.pnlDestSet.Size = new System.Drawing.Size(334, 265);
         this.pnlDestSet.TabIndex = 1;
         // 
         // lblRepresentation
         // 
         this.lblRepresentation.AutoSize = true;
         this.lblRepresentation.Location = new System.Drawing.Point(388, 150);
         this.lblRepresentation.Name = "lblRepresentation";
         this.lblRepresentation.Size = new System.Drawing.Size(82, 13);
         this.lblRepresentation.TabIndex = 3;
         this.lblRepresentation.Text = "Representation:";
         // 
         // lblDestinationSet
         // 
         this.lblDestinationSet.AutoSize = true;
         this.lblDestinationSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblDestinationSet.Location = new System.Drawing.Point(12, 9);
         this.lblDestinationSet.Name = "lblDestinationSet";
         this.lblDestinationSet.Size = new System.Drawing.Size(119, 17);
         this.lblDestinationSet.TabIndex = 4;
         this.lblDestinationSet.Text = "Destination Set";
         // 
         // btnPopulate
         // 
         this.btnPopulate.Location = new System.Drawing.Point(509, 86);
         this.btnPopulate.Name = "btnPopulate";
         this.btnPopulate.Size = new System.Drawing.Size(75, 23);
         this.btnPopulate.TabIndex = 3;
         this.btnPopulate.Text = "Populate";
         this.btnPopulate.UseVisualStyleBackColor = true;
         this.btnPopulate.Click += new System.EventHandler(this.btnPopulate_Click);
         // 
         // lstDestination
         // 
         this.lstDestination.FormattingEnabled = true;
         this.lstDestination.Location = new System.Drawing.Point(15, 169);
         this.lstDestination.Name = "lstDestination";
         this.lstDestination.Size = new System.Drawing.Size(123, 134);
         this.lstDestination.TabIndex = 6;
         // 
         // lblDestination
         // 
         this.lblDestination.AutoSize = true;
         this.lblDestination.Location = new System.Drawing.Point(12, 150);
         this.lblDestination.Name = "lblDestination";
         this.lblDestination.Size = new System.Drawing.Size(68, 13);
         this.lblDestination.TabIndex = 7;
         this.lblDestination.Text = "Destinations:";
         // 
         // txtAreas
         // 
         this.txtAreas.Location = new System.Drawing.Point(145, 169);
         this.txtAreas.Multiline = true;
         this.txtAreas.Name = "txtAreas";
         this.txtAreas.ReadOnly = true;
         this.txtAreas.Size = new System.Drawing.Size(186, 134);
         this.txtAreas.TabIndex = 8;
         // 
         // lblAreas
         // 
         this.lblAreas.AutoSize = true;
         this.lblAreas.Location = new System.Drawing.Point(142, 150);
         this.lblAreas.Name = "lblAreas";
         this.lblAreas.Size = new System.Drawing.Size(37, 13);
         this.lblAreas.TabIndex = 9;
         this.lblAreas.Text = "Areas:";
         // 
         // btnUpdate
         // 
         this.btnUpdate.Location = new System.Drawing.Point(509, 57);
         this.btnUpdate.Name = "btnUpdate";
         this.btnUpdate.Size = new System.Drawing.Size(75, 23);
         this.btnUpdate.TabIndex = 2;
         this.btnUpdate.Text = "Update";
         this.btnUpdate.UseVisualStyleBackColor = true;
         this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
         // 
         // lblDestinationID
         // 
         this.lblDestinationID.AutoSize = true;
         this.lblDestinationID.Location = new System.Drawing.Point(12, 41);
         this.lblDestinationID.Name = "lblDestinationID";
         this.lblDestinationID.Size = new System.Drawing.Size(19, 13);
         this.lblDestinationID.TabIndex = 12;
         this.lblDestinationID.Text = "Id:";
         // 
         // lblDestAreas
         // 
         this.lblDestAreas.AutoSize = true;
         this.lblDestAreas.Location = new System.Drawing.Point(142, 41);
         this.lblDestAreas.Name = "lblDestAreas";
         this.lblDestAreas.Size = new System.Drawing.Size(37, 13);
         this.lblDestAreas.TabIndex = 13;
         this.lblDestAreas.Text = "Areas:";
         // 
         // txtDestID
         // 
         this.txtDestID.Location = new System.Drawing.Point(15, 57);
         this.txtDestID.Name = "txtDestID";
         this.txtDestID.Size = new System.Drawing.Size(100, 20);
         this.txtDestID.TabIndex = 14;
         // 
         // ADVisualizer
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(734, 442);
         this.Controls.Add(this.txtDestID);
         this.Controls.Add(this.lblDestAreas);
         this.Controls.Add(this.lblDestinationID);
         this.Controls.Add(this.btnUpdate);
         this.Controls.Add(this.lblAreas);
         this.Controls.Add(this.txtAreas);
         this.Controls.Add(this.lblDestination);
         this.Controls.Add(this.lstDestination);
         this.Controls.Add(this.btnPopulate);
         this.Controls.Add(this.lblDestinationSet);
         this.Controls.Add(this.lblRepresentation);
         this.Controls.Add(this.btnPaint);
         this.Controls.Add(this.pnlDestSet);
         this.Controls.Add(this.txtDestSet);
         this.MinimumSize = new System.Drawing.Size(640, 480);
         this.Name = "ADVisualizer";
         this.Text = "Area Destination Visualizer";
         ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox txtDestSet;
      private System.Windows.Forms.ErrorProvider errProvider;
      private System.Windows.Forms.Button btnPaint;
      private System.Windows.Forms.Panel pnlDestSet;
      private System.Windows.Forms.Label lblRepresentation;
      private System.Windows.Forms.Label lblDestinationSet;
      private System.Windows.Forms.Button btnPopulate;
      private System.Windows.Forms.ListBox lstDestination;
      private System.Windows.Forms.Label lblDestination;
      private System.Windows.Forms.ToolTip tlpTooltip;
      private System.Windows.Forms.Label lblAreas;
      private System.Windows.Forms.TextBox txtAreas;
      private System.Windows.Forms.Button btnUpdate;
      private System.Windows.Forms.Label lblDestAreas;
      private System.Windows.Forms.Label lblDestinationID;
      private System.Windows.Forms.TextBox txtDestID;
   }
}

