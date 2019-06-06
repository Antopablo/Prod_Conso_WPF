using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Producteur_Consomateur_WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Producteur> Liste_Producteur;
        public ObservableCollection<Consommateur> Liste_Consommateur;
        int compteur = 0;
        int compteurConso = 0;

        public MainWindow()
        {
            InitializeComponent();
            Liste_Producteur = new ObservableCollection<Producteur>();
            Liste_Consommateur = new ObservableCollection<Consommateur>();
            ListeView_Prod.ItemsSource = Liste_Producteur;
            ListeView_Conso.ItemsSource = Liste_Consommateur;
        }

        private void Bouton_Ajouter_Prod_Click(object sender, RoutedEventArgs e)
        {
            if (Choix_nom_Prod.Text == "")
            {
                Choix_nom_Prod.Text = "Saisir un nom";
            }
            else
            {
                
                Producteur p = new Producteur(Choix_nom_Prod.Text, this);
                compteur++;
                Liste_Producteur.Add(p);
                Compteur_Prod.Text = compteur.ToString();
                Liste_Producteur.Last().Worker_Prod.RunWorkerAsync();
                Choix_nom_Prod.Text = "";
            }
        }

        private void Bouton_Ajouter_Conso_Click(object sender, RoutedEventArgs e)
        {
            
            if (Choix_nom_Conso.Text == "")
            {
                Choix_nom_Conso.Text = "Saisir un nom";
            }
            else
            {
                
                Consommateur c = new Consommateur(Choix_nom_Conso.Text, this);
                compteurConso++;
                Liste_Consommateur.Add(c);
                Compteur_Conso.Text = compteurConso.ToString();
                Liste_Consommateur.Last().Worker_Conso.RunWorkerAsync();
                Choix_nom_Conso.Text = "";
            }
        }

        private void Bouton_Pause_Prod_Click(object sender, RoutedEventArgs e)
        {
            if (ListeView_Prod.SelectedItems.Count > 0)
            {
                ((Producteur)ListeView_Prod.SelectedItems[0]).Pause = true;

            }
        }

        private void Bouton_Pause_Conso_Click(object sender, RoutedEventArgs e)
        {
            if (ListeView_Conso.SelectedItems.Count > 0)
            {
                ((Consommateur)ListeView_Conso.SelectedItems[0]).Pause = true;
            }
        }
    }
}
