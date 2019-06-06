using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producteur_Consomateur_WPF
{
    public class Consommateur : INotifyPropertyChanged
    {
        public MainWindow mw { get; set; }
        private string _nom;
        private bool _pause;
        private BackgroundWorker worker_Conso;

        public Consommateur(string nom, MainWindow m)
        {
            this.mw = m;
            Nom = nom;
            Pause = false;
            Worker_Conso = new BackgroundWorker();
            Worker_Conso.WorkerReportsProgress = true;
            Worker_Conso.WorkerSupportsCancellation = true;
            Worker_Conso.DoWork += Worker_Conso_DoWork;
            Worker_Conso.ProgressChanged += Worker_Conso_ProgressChanged;
            Worker_Conso.RunWorkerCompleted += Worker_Conso_RunWorkerCompleted;
        }

        private void Worker_Conso_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Worker_Conso_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (mw.Pipe.Value >= 0)
            {
                mw.Pipe.Value -= e.ProgressPercentage;
            }
        }

        private void Worker_Conso_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Pause == false)
            {
                int i = 0;
                i++;
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(30);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public bool Pause
        {
            get { return _pause; }
            set {
                if (this._pause != value)
                {
                    this._pause = value;
                    this.NotifyPropertyChanged("Pause");
                }
            }
        }

        public BackgroundWorker Worker_Conso
        {
            get { return worker_Conso; }
            set { worker_Conso = value; }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

}
