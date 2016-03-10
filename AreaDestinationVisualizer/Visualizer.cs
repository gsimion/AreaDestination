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
   public partial class Visualizer : Form
   {
      private DestinationSet<string> _ds = null;
      private List<AreaRectangle<string>> _currentRectangles = new List<AreaRectangle<string>>();
      private double _zoom;
      FormWindowState _lastWindowState;
      /// <summary>
      /// Array containing different brushes for different digits.
      /// </summary>
      private readonly Brush[] _digitsBrushes = { Brushes.LightGray, Brushes.Yellow, Brushes.Red, Brushes.IndianRed, Brushes.Indigo, Brushes.Ivory, Brushes.Khaki, Brushes.Lavender, Brushes.LavenderBlush, Brushes.LawnGreen, Brushes.LightBlue, Brushes.LightCyan, Brushes.LightGray, Brushes.LightSalmon };
      /// <summary>
      /// Vector of distinct RGB colors.
      /// </summary>
      private readonly string[] _colorsRgb = new string[] { 
         "FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "000000", 
         "800000", "008000", "000080", "808000", "800080", "008080", "808080", 
         "C00000", "00C000", "0000C0", "C0C000", "C000C0", "00C0C0", "C0C0C0", 
         "400000", "004000", "000040", "404000", "400040", "004040", "404040", 
         "200000", "002000", "000020", "202000", "200020", "002020", "202020", 
         "600000", "006000", "000060", "606000", "600060", "006060", "606060", 
         "A00000", "00A000", "0000A0", "A0A000", "A000A0", "00A0A0", "A0A0A0", 
         "E00000", "00E000", "0000E0", "E0E000", "E000E0", "00E0E0", "E0E0E0", 
      };
      /// <summary>
      /// Sets the zoom step.
      /// </summary>
      private const double ZoomStepIncrease = 0.2;
      /// <summary>
      /// Zoom default 100%.
      /// </summary>
      private const double ZoomDefault = 1;

      /// <summary>
      /// Creates a new form for visualizing areas.
      /// </summary>
      public Visualizer()
      {
         InitializeComponent();
         this.ResizeEnd += new EventHandler(this.btnPaint_Click);
         this.Resize += new EventHandler(this.Visualizer_Resize);
         this.picDestSet.MouseMove += new MouseEventHandler(this.picDestSet_MouseMove);
         this.lstDestination.SelectedIndexChanged += new EventHandler(this.lstDestination_SelectedIndexChanged);
         _zoom = ZoomDefault;
         _lastWindowState = this.WindowState;
      }

      /// <summary>
      /// Draws the destination set areas as rectangles.
      /// </summary>
      private void btnPaint_Click(object sender, EventArgs e)
      {
         if (_ds == null)
            return;

         if (_zoom.Equals(ZoomDefault) && picDestSet.Dock != DockStyle.Fill)
         {
            picDestSet.Dock = DockStyle.Fill;
            picDestSet.Anchor = AnchorStyles.None;
            picDestSet.Width = pnlDestSet.Width;
            picDestSet.Height = pnlDestSet.Height;
         }
         else if (!_zoom.Equals(ZoomDefault))
         {
            picDestSet.Dock = DockStyle.None;
            picDestSet.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            picDestSet.Width = Convert.ToInt32(pnlDestSet.Width * _zoom);
            picDestSet.Height = Convert.ToInt32(pnlDestSet.Height * _zoom);
         }

         int scale = picDestSet.Width;
         _currentRectangles = Painter.GetRepresentation<Destination<string>, string>(_ds, scale);

         int init = -1;
         decimal rank = picDestSet.Height;
         if (picDestSet.Image != null)
            picDestSet.Image.Dispose();
         picDestSet.Image = new Bitmap(picDestSet.Width, picDestSet.Height);
         Graphics.FromImage(picDestSet.Image).FillRectangle(Brushes.White, picDestSet.DisplayRectangle); 
         foreach (AreaRectangle<string> r in _currentRectangles)
         {
            r.Translate = scale / 10;
            r.ScaleY = picDestSet.Height;
            if (rank != r.Height)
            {
               rank = r.Height;
               init++;
            }
            Rectangle AreaToDisplay = new Rectangle(r.X, 0, r.Width, r.Height);
            Graphics.FromImage(picDestSet.Image).FillRectangle(_digitsBrushes[init], AreaToDisplay);
            Graphics.FromImage(picDestSet.Image).FillRectangle(Brushes.Black, AreaToDisplay.X, AreaToDisplay.Y, AreaToDisplay.Width, 5);
         }
         picDestSet.Refresh();
      }

      /// <summary>
      /// Handles reseize event, triggering painting when the window state changes. 
      /// </summary>
      private void Visualizer_Resize(object sender, EventArgs e)
      {
         if (this.WindowState != _lastWindowState)
         {
            _lastWindowState = WindowState;
            btnPaint_Click(sender, e);
         }
      }

      /// <summary>
      /// Handles mouse move event within the panel containing the graph.
      /// </summary>
      private void picDestSet_MouseMove(object sender, MouseEventArgs e)
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
         {
            txtAreas.Text = _ds[id].ToString();
            if (id != null && !id.Equals(_ds.UndefinedDestinationId))
               worldMap.SetCountry(Convert.ToUInt64(_ds[id].Areas.First().Start.ToString().Substring(2)));
         }
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
         this.ResizeEnd -= new EventHandler(this.btnPaint_Click);
         this.Resize -= new EventHandler(this.Visualizer_Resize);
         this.picDestSet.MouseMove -= new MouseEventHandler(this.picDestSet_MouseMove);
         this.lstDestination.SelectedIndexChanged -= new EventHandler(this.lstDestination_SelectedIndexChanged);
      }

      /// <summary>
      /// Zoom in.
      /// </summary>
      private void btnZoomPlus_Click(object sender, EventArgs e)
      {
         _zoom += ZoomStepIncrease;
         btnPaint_Click(sender, e);
      }

      /// <summary>
      /// Zoom out.
      /// </summary>
      private void btnZoomMinus_Click(object sender, EventArgs e)
      {
         _zoom = Math.Max(ZoomDefault, _zoom - ZoomStepIncrease);
         btnPaint_Click(sender, e);
      }
   }
}
