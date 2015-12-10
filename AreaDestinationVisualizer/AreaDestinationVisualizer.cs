namespace AreaDestinationVisualizer
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Windows.Forms;
   using AreaDestination;
   
   /// <summary>
   /// Form visualizing areas.
   /// </summary>
   public partial class ADVisualizer : Form
   {
      private DestinationSet<string> _ds = null;
      private List<AreaRectangle<string>> _currentRectangles = new List<AreaRectangle<string>>();

      /// <summary>
      /// Creates a new form for visualizing areas.
      /// </summary>
      public ADVisualizer()
      {
         InitializeComponent();
         this.SizeChanged += new EventHandler(this.btnPaint_Click);
         this.pnlDestSet.MouseMove += new MouseEventHandler(this.pnlDestSet_MouseMove);
         this.lstDestination.SelectedIndexChanged += new EventHandler(this.lstDestination_SelectedIndexChanged);
      }

      /// <summary>
      /// Draws the destination set areas as rectangles.
      /// </summary>
      private void btnPaint_Click(object sender, EventArgs e)
      {
         if (_ds == null)
            return;

         int scale = pnlDestSet.Width;
         _currentRectangles = Painter.GetRepresentation<Destination<string>, string>(_ds, scale);

         Color[] cols = { Color.LightGray, Color.Yellow, Color.Red, Color.IndianRed, Color.Indigo, Color.Ivory, Color.Khaki, Color.Lavender, Color.LavenderBlush, Color.LawnGreen, Color.LightBlue, Color.LightCyan, Color.LightGray, Color.LightSalmon };

         ResetbackGround();

         int init = -1;
         decimal rank = pnlDestSet.Height;
         foreach (AreaRectangle<string> r in _currentRectangles)
         {
            r.ScaleY = pnlDestSet.Height;
            if (rank != r.Height)
            {
               rank = r.Height;
               init++;
            }
            SolidBrush myBrush = new System.Drawing.SolidBrush(cols[init]);
            Graphics formGraphics;
            formGraphics = pnlDestSet.CreateGraphics();
            Rectangle show = new Rectangle(r.X, 0, r.Width, r.Height);
            formGraphics.FillRectangle(myBrush, show);
            myBrush.Dispose();
            formGraphics.Dispose();
         }
      }

      /// <summary>
      /// Resets background of the panel containing the graph.
      /// </summary>
      private void ResetbackGround()
      {
         SolidBrush myBrush = new System.Drawing.SolidBrush(Color.White);
         Graphics formGraphics;
         formGraphics = pnlDestSet.CreateGraphics();
         formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, pnlDestSet.Width, pnlDestSet.Height));
         myBrush.Dispose();
         formGraphics.Dispose();
      }

      /// <summary>
      /// Handles mouse move event within the panel containing the graph.
      /// </summary>
      private void pnlDestSet_MouseMove(object sender, MouseEventArgs e)
      {
         if (_ds == null)
            return;

         string dest = GetDestinationID(e.X, e.Y);
         if (String.IsNullOrEmpty(dest))
         {
            lstDestination.SelectedItem = _ds.UndefinedDestinationId;
         }
         else if (lstDestination.Items.Contains(dest))
         {
            lstDestination.SelectedItem = dest;
         }
      }

      /// <summary>
      /// Populates the destination set.
      /// </summary>
      private void btnPopulate_Click(object sender, EventArgs e)
      {
         lstDestination.Items.Clear();
         ResetbackGround();
         if (txtDestSet.Text.Length == 0)
            return;

         _ds = new DestinationSet<string>();
         btnUpdate_Click(sender, e);
      }

      /// <summary>
      /// Gets destination id based on the relative coordinates of the graph.
      /// </summary>
      /// <param name="X">Mouse relative X coordinates</param>
      /// <param name="Y">Mouse relative Y coordinates</param>
      /// <returns>Destination id, if found</returns>
      private string GetDestinationID(int X, int Y)
      {
         string id = null;
         if (_currentRectangles != null && _currentRectangles.Any())
         {
            id = (from r in _currentRectangles where r.X <= X && r.X + r.Width > X select r.Id).LastOrDefault();
         }
         return id;
      }

      /// <summary>
      /// Shows current/selected destination areas.
      /// </summary>
      private void lstDestination_SelectedIndexChanged(object sender, EventArgs e)
      {
         txtAreas.Text = String.Empty;
         string id = Convert.ToString(lstDestination.SelectedItem);
         if (_ds != null && _ds.Destinations.ContainsKey(id))
            txtAreas.Text = _ds[id].ToString();
      }

      /// <summary>
      /// Updates destination.
      /// </summary>
      private void btnUpdate_Click(object sender, EventArgs e)
      {
         errProvider.Clear();
         bool valid = true;
         if (String.IsNullOrWhiteSpace(txtDestID.Text))
         {
            errProvider.SetError(txtDestID, "Destination must have a valid Id");
            valid = false;
         }
         if (String.IsNullOrWhiteSpace(txtDestID.Text))
         {
            errProvider.SetError(txtDestSet, "Destination must have a valid area");
            valid = false;
         }
         if (!valid)
            return;

         if (_ds == null)
            _ds = new DestinationSet<string>();

         try
         {
            _ds.UpdateDestination(txtDestID.Text, txtDestSet.Text);
         }
         catch (Exception ex)
         {
            MessageBox.Show("Destination not updated" + Environment.NewLine + ex.Message);
            _ds = null;
            return;
         }
         _currentRectangles.Clear();
         lstDestination.Items.Clear();
         lstDestination.Items.AddRange(_ds.Destinations.Select(x => x.Key).ToArray()); 
      }

      /// <summary>
      /// Destroys GUI handlers.
      /// </summary>
      protected override void DestroyHandle()
      {
         base.DestroyHandle();
         this.SizeChanged -= new EventHandler(this.btnPaint_Click);
         this.pnlDestSet.MouseMove -= new MouseEventHandler(this.pnlDestSet_MouseMove);
         this.lstDestination.SelectedIndexChanged -= new EventHandler(this.lstDestination_SelectedIndexChanged);
      }
   }
}
