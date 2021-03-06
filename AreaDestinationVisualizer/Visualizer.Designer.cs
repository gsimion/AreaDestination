﻿namespace AreaDestinationVisualizer
{
   partial class Visualizer
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizer));
         this.txtDestSet = new System.Windows.Forms.TextBox();
         this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
         this.btnPaint = new System.Windows.Forms.Button();
         this.lblRepresentation = new System.Windows.Forms.Label();
         this.lblDestinationSet = new System.Windows.Forms.Label();
         this.btnPopulate = new System.Windows.Forms.Button();
         this.lstDestination = new System.Windows.Forms.ListBox();
         this.lblDestination = new System.Windows.Forms.Label();
         this.tlpTooltip = new System.Windows.Forms.ToolTip(this.components);
         this.btnZoomPlus = new System.Windows.Forms.Button();
         this.btnZoomMinus = new System.Windows.Forms.Button();
         this.txtAreas = new System.Windows.Forms.TextBox();
         this.lblAreas = new System.Windows.Forms.Label();
         this.btnUpdate = new System.Windows.Forms.Button();
         this.lblDestinationID = new System.Windows.Forms.Label();
         this.lblDestAreas = new System.Windows.Forms.Label();
         this.txtDestID = new System.Windows.Forms.TextBox();
         this.picDestSet = new System.Windows.Forms.PictureBox();
         this.pnlDestSet = new System.Windows.Forms.Panel();
         this.worldMap = new AreaDestinationVisualizer.InteractiveWorldMap();
         ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.picDestSet)).BeginInit();
         this.pnlDestSet.SuspendLayout();
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
         // lblRepresentation
         // 
         this.lblRepresentation.AutoSize = true;
         this.lblRepresentation.Location = new System.Drawing.Point(369, 150);
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
         // btnZoomPlus
         // 
         this.btnZoomPlus.Location = new System.Drawing.Point(339, 169);
         this.btnZoomPlus.Name = "btnZoomPlus";
         this.btnZoomPlus.Size = new System.Drawing.Size(28, 25);
         this.btnZoomPlus.TabIndex = 17;
         this.btnZoomPlus.Tag = "Ceiling";
         this.btnZoomPlus.Text = "+";
         this.tlpTooltip.SetToolTip(this.btnZoomPlus, "Zoom In");
         this.btnZoomPlus.UseVisualStyleBackColor = true;
         this.btnZoomPlus.Click += new System.EventHandler(this.btnZoomPlus_Click);
         // 
         // btnZoomMinus
         // 
         this.btnZoomMinus.Location = new System.Drawing.Point(339, 201);
         this.btnZoomMinus.Name = "btnZoomMinus";
         this.btnZoomMinus.Size = new System.Drawing.Size(28, 25);
         this.btnZoomMinus.TabIndex = 18;
         this.btnZoomMinus.Tag = "Ceiling";
         this.btnZoomMinus.Text = "-";
         this.tlpTooltip.SetToolTip(this.btnZoomMinus, "Zoom Out");
         this.btnZoomMinus.UseVisualStyleBackColor = true;
         this.btnZoomMinus.Click += new System.EventHandler(this.btnZoomMinus_Click);
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
         // picDestSet
         // 
         this.picDestSet.Dock = System.Windows.Forms.DockStyle.Fill;
         this.picDestSet.Location = new System.Drawing.Point(0, 0);
         this.picDestSet.Name = "picDestSet";
         this.picDestSet.Size = new System.Drawing.Size(348, 259);
         this.picDestSet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.picDestSet.TabIndex = 15;
         this.picDestSet.TabStop = false;
         // 
         // pnlDestSet
         // 
         this.pnlDestSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.pnlDestSet.AutoScroll = true;
         this.pnlDestSet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.pnlDestSet.Controls.Add(this.picDestSet);
         this.pnlDestSet.Location = new System.Drawing.Point(372, 169);
         this.pnlDestSet.Name = "pnlDestSet";
         this.pnlDestSet.Size = new System.Drawing.Size(350, 261);
         this.pnlDestSet.TabIndex = 16;
         // 
         // worldMap
         // 
         this.worldMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
         this.worldMap.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("worldMap.BackgroundImage")));
         this.worldMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
         this.worldMap.Location = new System.Drawing.Point(15, 309);
         this.worldMap.Name = "worldMap";
         this.worldMap.Size = new System.Drawing.Size(316, 121);
         this.worldMap.TabIndex = 19;
         // 
         // Visualizer
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(734, 442);
         this.Controls.Add(this.worldMap);
         this.Controls.Add(this.btnZoomMinus);
         this.Controls.Add(this.btnZoomPlus);
         this.Controls.Add(this.pnlDestSet);
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
         this.Controls.Add(this.txtDestSet);
         this.MinimumSize = new System.Drawing.Size(640, 480);
         this.Name = "Visualizer";
         this.Text = "Area Destination Visualizer";
         ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.picDestSet)).EndInit();
         this.pnlDestSet.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox txtDestSet;
      private System.Windows.Forms.ErrorProvider errProvider;
      private System.Windows.Forms.Button btnPaint;
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
      private System.Windows.Forms.PictureBox picDestSet;
      private System.Windows.Forms.Panel pnlDestSet;
      private System.Windows.Forms.Button btnZoomPlus;
      private System.Windows.Forms.Button btnZoomMinus;
      private InteractiveWorldMap worldMap;
   }
}

