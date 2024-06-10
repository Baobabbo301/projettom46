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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        OVZDatabaseEntities OVZ = new OVZDatabaseEntities();
        private int _id;
        public EmployeeWindow()
        {
            InitializeComponent();
        }
        public void GetUserID(int id)
        {
            _id = id;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EventGrid.ItemsSource = OVZ.Events.ToList();
            ChildGrid.ItemsSource = OVZ.Children.ToList();
            ChEveGrid.ItemsSource = OVZ.ChildEvents.ToList();
            EduGrid.ItemsSource = OVZ.EducationInfo.ToList();
            HealthGrid.ItemsSource = OVZ.HealthInfo.ToList();
            NoteGrid.ItemsSource = (from Note in OVZ.Notes where Note.user_id == _id select Note).ToList();
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

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Events events = (Events)EventGrid.SelectedItem;
                OVZ.Events.Add(events);
                OVZ.SaveChanges();

                EventGrid.ItemsSource = null;
                EventGrid.ItemsSource = OVZ.Events.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (EventGrid.SelectedItem as Events).id;
                var ev = (from index in OVZ.Events where index.id == ID select index).Single();
                ev.name = (EventGrid.SelectedItem as Events).name;
                ev.description = (EventGrid.SelectedItem as Events).description;
                ev.date = (EventGrid.SelectedItem as Events).date;
                ev.time = (EventGrid.SelectedItem as Events).time;
                OVZ.SaveChanges();

                EventGrid.ItemsSource = null;
                EventGrid.ItemsSource = OVZ.Events.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

        private void btnAddCheve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChildEvents childEvent = (ChildEvents)ChEveGrid.SelectedItem;
                OVZ.ChildEvents.Add(childEvent);
                OVZ.SaveChanges();

                ChEveGrid.ItemsSource = null;
                ChEveGrid.ItemsSource = OVZ.ChildEvents.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEditCheve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (ChEveGrid.SelectedItem as ChildEvents).id;
                var childEvent = (from index in OVZ.ChildEvents where index.id == ID select index).Single();
                childEvent.child_id = (ChEveGrid.SelectedItem as ChildEvents).child_id;
                childEvent.event_id = (ChEveGrid.SelectedItem as ChildEvents).event_id;
                childEvent.attendance = (ChEveGrid.SelectedItem as ChildEvents).attendance;
                childEvent.notes = (ChEveGrid.SelectedItem as ChildEvents).notes;
                OVZ.SaveChanges();

                ChEveGrid.ItemsSource = null;
                ChEveGrid.ItemsSource = OVZ.ChildEvents.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddEdu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EducationInfo educationInfo = (EducationInfo)EduGrid.SelectedItem;
                OVZ.EducationInfo.Add(educationInfo);
                OVZ.SaveChanges();

                EduGrid.ItemsSource = null;
                EduGrid.ItemsSource = OVZ.EducationInfo.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEditEdu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (EduGrid.SelectedItem as EducationInfo).id;
                var educationInfo = (from index in OVZ.EducationInfo where index.id == ID select index).Single();
                educationInfo.child_id = (EduGrid.SelectedItem as EducationInfo).child_id;
                educationInfo.school = (EduGrid.SelectedItem as EducationInfo).school;
                educationInfo.education_program = (EduGrid.SelectedItem as EducationInfo).education_program;
                OVZ.SaveChanges();

                EduGrid.ItemsSource = null;
                EduGrid.ItemsSource = OVZ.EducationInfo.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddHealth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HealthInfo healthInfo = (HealthInfo)HealthGrid.SelectedItem;
                OVZ.HealthInfo.Add(healthInfo);
                OVZ.SaveChanges();

                HealthGrid.ItemsSource = null;
                HealthGrid.ItemsSource = OVZ.HealthInfo.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEditHealth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (HealthGrid.SelectedItem as HealthInfo).id;
                var healthInfo = (from index in OVZ.HealthInfo where index.id == ID select index).Single();
                healthInfo.child_id = (HealthGrid.SelectedItem as HealthInfo).child_id;
                healthInfo.diagnosis = (HealthGrid.SelectedItem as HealthInfo).diagnosis;
                healthInfo.limitations = (HealthGrid.SelectedItem as HealthInfo).limitations;
                healthInfo.medical_history = (HealthGrid.SelectedItem as HealthInfo).medical_history;
                OVZ.SaveChanges();

                HealthGrid.ItemsSource = null;
                HealthGrid.ItemsSource = OVZ.HealthInfo.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Notes note = new Notes();
                note.child_id = (NoteGrid.SelectedItem as Notes).child_id;
                note.user_id = _id;
                note.note_text = (NoteGrid.SelectedItem as Notes).note_text;
                note.date_created = DateTime.Now;

                OVZ.Notes.Add(note);
                OVZ.SaveChanges();

                NoteGrid.ItemsSource = null;
                NoteGrid.ItemsSource = (from Note in OVZ.Notes where Note.user_id == _id select Note).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEditNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (NoteGrid.SelectedItem as Notes).id;
                var note = (from index in OVZ.Notes where index.id == ID select index).Single();
                note.child_id = (NoteGrid.SelectedItem as Notes).child_id;
                note.user_id = (NoteGrid.SelectedItem as Notes).user_id;
                note.note_text = (NoteGrid.SelectedItem as Notes).note_text;
                OVZ.SaveChanges();

                NoteGrid.ItemsSource = null;
                NoteGrid.ItemsSource = (from Note in OVZ.Notes where Note.user_id == _id select Note).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
