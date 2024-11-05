
namespace EclosionHub.Entreprise
{
    partial class EntrepriseMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntrepriseMain));
            this.gunaElipse1 = new Guna.UI.WinForms.GunaElipse(this.components);
            this.gunaGradientPanel2 = new Guna.UI.WinForms.GunaGradientPanel();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.PanelADD = new Guna.UI.WinForms.GunaGradientPanel();
            this.guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.gunaButton2 = new Guna.UI.WinForms.GunaButton();
            this.TextBoxNomEntreprise = new Guna.UI2.WinForms.Guna2TextBox();
            this.ComboBoxProprietaire = new Guna.UI2.WinForms.Guna2ComboBox();
            this.TextBoxDescription = new Guna.UI2.WinForms.Guna2TextBox();
            this.ComboBoxSecteur = new Guna.UI2.WinForms.Guna2ComboBox();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaGradientPanel2.SuspendLayout();
            this.gunaPanel1.SuspendLayout();
            this.PanelADD.SuspendLayout();
            this.guna2ShadowPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gunaElipse1
            // 
            this.gunaElipse1.TargetControl = this;
            // 
            // gunaGradientPanel2
            // 
            this.gunaGradientPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gunaGradientPanel2.BackgroundImage")));
            this.gunaGradientPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaGradientPanel2.Controls.Add(this.gunaControlBox1);
            this.gunaGradientPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.gunaGradientPanel2.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(20)))), ((int)(((byte)(57)))));
            this.gunaGradientPanel2.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(20)))), ((int)(((byte)(57)))));
            this.gunaGradientPanel2.GradientColor3 = System.Drawing.Color.DeepSkyBlue;
            this.gunaGradientPanel2.GradientColor4 = System.Drawing.Color.HotPink;
            this.gunaGradientPanel2.Location = new System.Drawing.Point(0, 0);
            this.gunaGradientPanel2.Name = "gunaGradientPanel2";
            this.gunaGradientPanel2.Size = new System.Drawing.Size(800, 40);
            this.gunaGradientPanel2.TabIndex = 3;
            this.gunaGradientPanel2.Text = "gunaGradientPanel2";
            // 
            // gunaControlBox1
            // 
            this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox1.AnimationSpeed = 0.03F;
            this.gunaControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.gunaControlBox1.IconColor = System.Drawing.Color.White;
            this.gunaControlBox1.IconSize = 15F;
            this.gunaControlBox1.Location = new System.Drawing.Point(744, 5);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(45, 29);
            this.gunaControlBox1.TabIndex = 0;
            // 
            // gunaPanel1
            // 
            this.gunaPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.gunaPanel1.Controls.Add(this.PanelADD);
            this.gunaPanel1.Location = new System.Drawing.Point(12, 46);
            this.gunaPanel1.Name = "gunaPanel1";
            this.gunaPanel1.Size = new System.Drawing.Size(776, 452);
            this.gunaPanel1.TabIndex = 4;
            // 
            // PanelADD
            // 
            this.PanelADD.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelADD.BackgroundImage")));
            this.PanelADD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelADD.Controls.Add(this.guna2ShadowPanel2);
            this.PanelADD.Controls.Add(this.TextBoxNomEntreprise);
            this.PanelADD.Controls.Add(this.ComboBoxProprietaire);
            this.PanelADD.Controls.Add(this.TextBoxDescription);
            this.PanelADD.Controls.Add(this.ComboBoxSecteur);
            this.PanelADD.Controls.Add(this.gunaLabel1);
            this.PanelADD.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this.PanelADD.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.PanelADD.GradientColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this.PanelADD.GradientColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.PanelADD.Location = new System.Drawing.Point(7, 6);
            this.PanelADD.Name = "PanelADD";
            this.PanelADD.Size = new System.Drawing.Size(761, 435);
            this.PanelADD.TabIndex = 2;
            this.PanelADD.Text = "gunaGradientPanel1";
            // 
            // guna2ShadowPanel2
            // 
            this.guna2ShadowPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel2.Controls.Add(this.gunaButton2);
            this.guna2ShadowPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.guna2ShadowPanel2.Location = new System.Drawing.Point(38, 328);
            this.guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            this.guna2ShadowPanel2.Radius = 5;
            this.guna2ShadowPanel2.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(60)))), ((int)(((byte)(212)))));
            this.guna2ShadowPanel2.Size = new System.Drawing.Size(686, 82);
            this.guna2ShadowPanel2.TabIndex = 20;
            // 
            // gunaButton2
            // 
            this.gunaButton2.AnimationHoverSpeed = 0.07F;
            this.gunaButton2.AnimationSpeed = 0.03F;
            this.gunaButton2.BackColor = System.Drawing.Color.Transparent;
            this.gunaButton2.BaseColor = System.Drawing.Color.Transparent;
            this.gunaButton2.BorderColor = System.Drawing.Color.Transparent;
            this.gunaButton2.BorderSize = 3;
            this.gunaButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton2.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaButton2.ForeColor = System.Drawing.Color.White;
            this.gunaButton2.Image = ((System.Drawing.Image)(resources.GetObject("gunaButton2.Image")));
            this.gunaButton2.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton2.Location = new System.Drawing.Point(15, 15);
            this.gunaButton2.Name = "gunaButton2";
            this.gunaButton2.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton2.OnHoverBorderColor = System.Drawing.Color.Transparent;
            this.gunaButton2.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton2.OnHoverImage = null;
            this.gunaButton2.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton2.Radius = 10;
            this.gunaButton2.Size = new System.Drawing.Size(656, 52);
            this.gunaButton2.TabIndex = 0;
            this.gunaButton2.Text = "CONFIRMER L\'AJOUT";
            this.gunaButton2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaButton2.Click += new System.EventHandler(this.gunaButton2_Click);
            // 
            // TextBoxNomEntreprise
            // 
            this.TextBoxNomEntreprise.BackColor = System.Drawing.Color.Transparent;
            this.TextBoxNomEntreprise.BorderRadius = 15;
            this.TextBoxNomEntreprise.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBoxNomEntreprise.DefaultText = "";
            this.TextBoxNomEntreprise.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TextBoxNomEntreprise.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TextBoxNomEntreprise.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxNomEntreprise.DisabledState.Parent = this.TextBoxNomEntreprise;
            this.TextBoxNomEntreprise.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxNomEntreprise.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.TextBoxNomEntreprise.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxNomEntreprise.FocusedState.Parent = this.TextBoxNomEntreprise;
            this.TextBoxNomEntreprise.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxNomEntreprise.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxNomEntreprise.HoverState.Parent = this.TextBoxNomEntreprise;
            this.TextBoxNomEntreprise.Location = new System.Drawing.Point(38, 75);
            this.TextBoxNomEntreprise.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.TextBoxNomEntreprise.Name = "TextBoxNomEntreprise";
            this.TextBoxNomEntreprise.PasswordChar = '\0';
            this.TextBoxNomEntreprise.PlaceholderText = "Nom";
            this.TextBoxNomEntreprise.SelectedText = "";
            this.TextBoxNomEntreprise.ShadowDecoration.Parent = this.TextBoxNomEntreprise;
            this.TextBoxNomEntreprise.Size = new System.Drawing.Size(686, 45);
            this.TextBoxNomEntreprise.TabIndex = 7;
            // 
            // ComboBoxProprietaire
            // 
            this.ComboBoxProprietaire.BackColor = System.Drawing.Color.Transparent;
            this.ComboBoxProprietaire.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboBoxProprietaire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxProprietaire.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.ComboBoxProprietaire.FocusedColor = System.Drawing.Color.Empty;
            this.ComboBoxProprietaire.FocusedState.Parent = this.ComboBoxProprietaire;
            this.ComboBoxProprietaire.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBoxProprietaire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.ComboBoxProprietaire.FormattingEnabled = true;
            this.ComboBoxProprietaire.HoverState.Parent = this.ComboBoxProprietaire;
            this.ComboBoxProprietaire.ItemHeight = 30;
            this.ComboBoxProprietaire.ItemsAppearance.Parent = this.ComboBoxProprietaire;
            this.ComboBoxProprietaire.Location = new System.Drawing.Point(38, 135);
            this.ComboBoxProprietaire.Name = "ComboBoxProprietaire";
            this.ComboBoxProprietaire.ShadowDecoration.Parent = this.ComboBoxProprietaire;
            this.ComboBoxProprietaire.Size = new System.Drawing.Size(338, 36);
            this.ComboBoxProprietaire.TabIndex = 10;
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.BackColor = System.Drawing.Color.Transparent;
            this.TextBoxDescription.BorderRadius = 15;
            this.TextBoxDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBoxDescription.DefaultText = "";
            this.TextBoxDescription.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TextBoxDescription.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TextBoxDescription.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxDescription.DisabledState.Parent = this.TextBoxDescription;
            this.TextBoxDescription.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxDescription.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.TextBoxDescription.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxDescription.FocusedState.Parent = this.TextBoxDescription;
            this.TextBoxDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDescription.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxDescription.HoverState.Parent = this.TextBoxDescription;
            this.TextBoxDescription.Location = new System.Drawing.Point(40, 186);
            this.TextBoxDescription.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.TextBoxDescription.Multiline = true;
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.PasswordChar = '\0';
            this.TextBoxDescription.PlaceholderText = "Description";
            this.TextBoxDescription.SelectedText = "";
            this.TextBoxDescription.ShadowDecoration.Parent = this.TextBoxDescription;
            this.TextBoxDescription.Size = new System.Drawing.Size(684, 129);
            this.TextBoxDescription.TabIndex = 8;
            // 
            // ComboBoxSecteur
            // 
            this.ComboBoxSecteur.BackColor = System.Drawing.Color.Transparent;
            this.ComboBoxSecteur.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboBoxSecteur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSecteur.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.ComboBoxSecteur.FocusedColor = System.Drawing.Color.Empty;
            this.ComboBoxSecteur.FocusedState.Parent = this.ComboBoxSecteur;
            this.ComboBoxSecteur.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBoxSecteur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.ComboBoxSecteur.FormattingEnabled = true;
            this.ComboBoxSecteur.HoverState.Parent = this.ComboBoxSecteur;
            this.ComboBoxSecteur.ItemHeight = 30;
            this.ComboBoxSecteur.Items.AddRange(new object[] {
            "Audiovisuel, jeux video",
            "Audiovisuel, BD",
            "Audiovisuel, Plateforme Web, XR",
            "AI, Audiovisuel,",
            "Musique, Ecole de metier musical",
            "Bibliotheque artistique, Litterature, livre",
            "Espace Culturel",
            "Art de la scene,",
            "Danse, Ecole de danse",
            "imprimerie, XR",
            "Audiovisuel, Plateforme Web",
            "Artisanat, Design",
            ""});
            this.ComboBoxSecteur.ItemsAppearance.Parent = this.ComboBoxSecteur;
            this.ComboBoxSecteur.Location = new System.Drawing.Point(382, 135);
            this.ComboBoxSecteur.Name = "ComboBoxSecteur";
            this.ComboBoxSecteur.ShadowDecoration.Parent = this.ComboBoxSecteur;
            this.ComboBoxSecteur.Size = new System.Drawing.Size(342, 36);
            this.ComboBoxSecteur.TabIndex = 11;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel1.ForeColor = System.Drawing.Color.White;
            this.gunaLabel1.Location = new System.Drawing.Point(40, 16);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(684, 47);
            this.gunaLabel1.TabIndex = 6;
            this.gunaLabel1.Text = "ENREGISTRER UNE ENTREPRISE";
            this.gunaLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EntrepriseMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(26)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(800, 511);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.gunaGradientPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EntrepriseMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EntrepriseMain";
            this.Load += new System.EventHandler(this.EntrepriseMain_Load);
            this.gunaGradientPanel2.ResumeLayout(false);
            this.gunaPanel1.ResumeLayout(false);
            this.PanelADD.ResumeLayout(false);
            this.guna2ShadowPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI.WinForms.GunaElipse gunaElipse1;
        private Guna.UI.WinForms.GunaGradientPanel gunaGradientPanel2;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaGradientPanel PanelADD;
        private Guna.UI2.WinForms.Guna2TextBox TextBoxNomEntreprise;
        private Guna.UI2.WinForms.Guna2ComboBox ComboBoxProprietaire;
        private Guna.UI2.WinForms.Guna2TextBox TextBoxDescription;
        private Guna.UI2.WinForms.Guna2ComboBox ComboBoxSecteur;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private Guna.UI.WinForms.GunaButton gunaButton2;
    }
}