﻿namespace AreaDestinationVisualizer
{
   partial class InteractiveWorldMap
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.picCountry = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.picCountry)).BeginInit();
         this.SuspendLayout();
         // 
         // picCountry
         // 
         this.picCountry.Location = new System.Drawing.Point(0, 0);
         this.picCountry.Margin = new System.Windows.Forms.Padding(0);
         this.picCountry.Name = "picCountry";
         this.picCountry.Size = new System.Drawing.Size(24, 26);
         this.picCountry.TabIndex = 0;
         this.picCountry.TabStop = false;
         // 
         // InteractiveWorldMap
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.picCountry);
         this.DoubleBuffered = true;
         this.Name = "InteractiveWorldMap";
         this.Size = new System.Drawing.Size(405, 273);
         ((System.ComponentModel.ISupportInitialize)(this.picCountry)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.PictureBox picCountry;

   }
}
