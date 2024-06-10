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
using System.Windows.Shapes;

namespace stoneFenceProj
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        OVZDatabaseEntities OVZ = new OVZDatabaseEntities();
        public AdminWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserGrid.ItemsSource = OVZ.Users.ToList();
            ChildGrid.ItemsSource= OVZ.Children.ToList();
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    TabItem selectedTab = (TabItem)e.AddedItems[0];
                    if (IsExitTab(selectedTab))
                    {
                        ShowMainWindow();
                        CloseCurrentWindow();
                    }
                }
            }
            catch
            {
                
            }
        }

        private bool IsExitTab(TabItem tabItem)
        {
            return tabItem.Header.ToString() == "Выход";
        }

        private void ShowMainWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow main = new MainWindow();
                main.Show();
            });
        }

        private void CloseCurrentWindow()
        {
            this.Close();
        }
        private void btnAddChild_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Children child = (Children)ChildGrid.SelectedItem;
                OVZ.Children.Add(child);
                OVZ.SaveChanges();

                ChildGrid.ItemsSource = null;
                ChildGrid.ItemsSource = OVZ.Children.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEditChild_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (ChildGrid.SelectedItem as Children).id;
                var child = (from index in OVZ.Children where index.id == ID select index).Single();
                child.first_name = (ChildGrid.SelectedItem as Children).first_name;
                child.last_name = (ChildGrid.SelectedItem as Children).last_name;
                child.date_of_birth = (ChildGrid.SelectedItem as Children).date_of_birth;
                child.address = (ChildGrid.SelectedItem as Children).address;
                child.parent_contact = (ChildGrid.SelectedItem as Children).parent_contact;
                OVZ.SaveChanges();

                ChildGrid.ItemsSource = null;
                ChildGrid.ItemsSource = OVZ.Children.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelChild_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Children child = (Children)ChildGrid.SelectedItem;
                OVZ.Children.Attach(child);
                OVZ.Children.Remove(child);
                OVZ.SaveChanges();

                ChildGrid.ItemsSource = null;
                ChildGrid.ItemsSource = OVZ.Children.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    Users user = (Users)UserGrid.SelectedItem;
                    OVZ.Users.Add(user);
                    OVZ.SaveChanges();


                    UserGrid.ItemsSource = null;
                    UserGrid.ItemsSource = OVZ.Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (UserGrid.SelectedItem as Users).id;
                var user = (from index in OVZ.Users where index.id == ID select index).Single();
                user.username = (UserGrid.SelectedItem as Users).username;
                user.password = (UserGrid.SelectedItem as Users).password;
                user.role = (UserGrid.SelectedItem as Users).role;
                OVZ.SaveChanges();


                UserGrid.ItemsSource = null;
                UserGrid.ItemsSource = OVZ.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users user = (Users)UserGrid.SelectedItem;
                OVZ.Users.Attach(user);
                OVZ.Users.Remove(user);
                OVZ.SaveChanges();


                UserGrid.ItemsSource = null;
                UserGrid.ItemsSource = OVZ.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
