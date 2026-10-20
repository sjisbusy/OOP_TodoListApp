using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TodoListApp
{
    public partial class MainWindow : Window
    {
        private TodoService _todoService;

        public MainWindow()
        {
            InitializeComponent();

            ITaskRepository repository = new JsonTaskRepository();
            _todoService = new TodoService(repository);

            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(TaskPriority));
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(TaskStatus));

            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            TasksListBox.ItemsSource = null;
            TasksListBox.ItemsSource = _todoService.GetAllTasks();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TodoModel new_task = new TodoModel
                {
                    Title = TitleTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    CreatedAt = DateTime.Now,
                    DueDate = DueDatePicker.SelectedDate ?? DateTime.Now.AddDays(1),
                    Priority = (TaskPriority)(PriorityComboBox.SelectedItem ?? TaskPriority.Medium),
                    Status = (TaskStatus)(StatusComboBox.SelectedItem ?? TaskStatus.NotStarted)
                };

                _todoService.add_task(new_task);

                RefreshTaskList();

                TitleTextBox.Text = "";
                DescriptionTextBox.Text = "";
                PriorityComboBox.SelectedIndex = -1;
                StatusComboBox.SelectedIndex = -1;
                DueDatePicker.SelectedDate = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "!!! ошибка ввода данных !!!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TasksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TodoModel selected_task = TasksListBox.SelectedItem as TodoModel;

            if (selected_task == null)
            {
                return;
            }

            TitleTextBox.Text = selected_task.Title;
            DescriptionTextBox.Text = selected_task.Description;
            PriorityComboBox.SelectedItem = selected_task.Priority;
            StatusComboBox.SelectedItem = selected_task.Status;
            DueDatePicker.SelectedDate = selected_task.DueDate;
        }

        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            TodoModel selected_task = TasksListBox.SelectedItem as TodoModel;

            if (selected_task == null)
            {
                MessageBox.Show("выберите задачу из списка для ее редактирования", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("!!! ошибка ввода данных !!!\nназвание НЕ может быть пустым !", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            selected_task.Title = TitleTextBox.Text;
            selected_task.Description = DescriptionTextBox.Text;
            selected_task.Priority = (TaskPriority)(PriorityComboBox.SelectedItem ?? TaskPriority.Medium);
            selected_task.Status = (TaskStatus)(StatusComboBox.SelectedItem ?? TaskStatus.NotStarted);
            selected_task.DueDate = DueDatePicker.SelectedDate ?? DateTime.Now.AddDays(1);

            _todoService.upd_task();
            RefreshTaskList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TodoModel selected_task = TasksListBox.SelectedItem as TodoModel;

            if (selected_task == null)
            {
                MessageBox.Show("выберите задачу для ее удаления", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _todoService.delete_task(selected_task);
            RefreshTaskList();

            TitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
            PriorityComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = -1;
            DueDatePicker.SelectedDate = null;
        }
    }
}
