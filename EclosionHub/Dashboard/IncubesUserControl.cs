using EclosionHub.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EclosionHub.incubes
{
    public partial class UserControlIncubes : UserControl
    {
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }
        public UserControlIncubes()
        {
            InitializeComponent();
        }

        NewDashboard dash = new NewDashboard();

        public event EventHandler OnSelected = null;
        protected virtual void ButtonPlus_Click(object sender, EventArgs e)
        {
            OnSelected?.Invoke(this, e);
        }

        public int id { get => Convert.ToInt32(labelId.Text); set => labelId.Text = Convert.ToInt32(value).ToString(); }
        public string Nom { get => LabelNomIncube.Text; set => LabelNomIncube.Text = value; }
        public string Postnom { get => LabelPostNomIncube.Text; set => LabelPostNomIncube.Text = value; }
        public string Prenom { get => LabelPrenomIncube.Text; set => LabelPrenomIncube.Text = value; }
        public string Projet { get => LabelProjetIncube.Text; set => LabelProjetIncube.Text = value; }
        public Image Icon { get => PictureBoxIncube.Image; set => PictureBoxIncube.Image = value; }

        public void data1(int id, string Nom, string postnom, string Prenom, string Projet)
        {
            id = Convert.ToInt32(labelId.Text);
            Nom = LabelNomIncube.Text;
            postnom = LabelPostNomIncube.Text;
            Prenom = LabelPrenomIncube.Text;
            Projet = LabelProjetIncube.Text;
        }
    }


}
