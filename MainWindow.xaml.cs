using System;
using System.Collections.Generic;
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

namespace Afkgame
{
    public partial class MainWindow : Window
    {
        private GameManager gameManager;
        int cost = 300;

        public MainWindow()
        {

            InitializeComponent();
            gameManager = new GameManager();
            gameManager.MoneyGenerated += GameManager_MoneyGenerated;
            UpdateUI();
            AnimateProgressBar();
        }

        private void GameManager_MoneyGenerated(object sender, EventArgs e)
        {
          
            Application.Current.Dispatcher.Invoke(() =>
            {
                UpdateUI();
            });
        }

        private void IncreaseWorkersLimit(object sender, RoutedEventArgs e)
        {
           
            increasWorkersLimitBtn.IsEnabled = false;
            if (gameManager.Money >= cost)
            {
                gameManager.MaxWorkers += 10;
                gameManager.Money -= 300;
                gameManager.Rent *= 3;
                cost = cost * 3;
                UpdateUI();
            } else
            {
              
                increasWorkersLimitBtn.IsEnabled = false;
                UpdateUI();
            }
           
        }

        private void BuyJuniorDev(object sender, RoutedEventArgs e)
        {
            Worker newJuniorDev = new Worker("Junior Dev", 5, 3, 18);
            if (gameManager.BuyWorker(newJuniorDev))
            {
                MessageBox.Show("Junior bought!");
                UpdateUI();
            }
            else
            {
                buyJuniorDevBtn.IsEnabled = false;
            }
        }

        private void BuySeniorDev(object sender, RoutedEventArgs e)
        {
            Worker newSeniorDev = new Worker("Senior Dev", 25, 9, 60);
            if (gameManager.BuyWorker(newSeniorDev))
            {
                MessageBox.Show("Senior bought!");
                UpdateUI();
            }
            else
            {
                buyJuniorDevBtn.IsEnabled = false;
            }
        }

        private void BuyDesigner(object sender, RoutedEventArgs e)
        {
            Worker newDesigner = new Worker("Designer", 32, 13, 700);
            if (gameManager.BuyWorker(newDesigner))
            {
                MessageBox.Show("Senior bought!");
                UpdateUI();
            }
            else
            {
                buyJuniorDevBtn.IsEnabled = false;
            }
        }




        // Met à jour l'interface utilisateur
        private void UpdateUI()
        {
            cycleLabel.Content = gameManager.Cycles;
            moneyLabel.Content = $"Money: {gameManager.Money}$";
            buyJuniorDevBtn.IsEnabled = gameManager.Money >= 5;
            buySeniorDevBtn.IsEnabled = gameManager.Money >= 25;
            totalDevJuniorsLabel.Content = $"Dev Juniors: {gameManager.TotalDevJuniors}";
            totalDevSeniorsLabel.Content = $"Dev Seniors: {gameManager.TotalDevSeniors}";
            totalDesignerLabel.Content = $"Designers: {gameManager.TotalDesigners}";
            workerLimitLabel.Content = $"{gameManager.Workers.Count} / {gameManager.MaxWorkers} Workers";
            rentLabel.Content = $"Rent : {gameManager.Rent} ";

            increasWorkersLimitBtn.Content = $"Add 10 slots ({cost}$)";

            bool canBuyMoreWorkers = (gameManager.TotalDevJuniors + gameManager.TotalDevSeniors +gameManager.TotalDesigners) < gameManager.MaxWorkers;
            buyJuniorDevBtn.IsEnabled = canBuyMoreWorkers && gameManager.Money >= 5; 
            buySeniorDevBtn.IsEnabled = canBuyMoreWorkers && gameManager.Money >= 25;

            if( cost >= gameManager.Money )
            {
                increasWorkersLimitBtn.IsEnabled = false;
            } else
            {
                increasWorkersLimitBtn.IsEnabled = true;

            }
        }

        private void AnimateProgressBar()
        {
            var duration = TimeSpan.FromSeconds(10); 
            var doubleAnimation = new System.Windows.Media.Animation.DoubleAnimation(100, duration)
            {
                AutoReverse = false, 
                RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever
            };

            moneyProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
