using System.Collections.ObjectModel;


namespace rmp7l
{
    public partial class MainPage : ContentPage
    {
        // коллекция, которая автоматически уведомляет интерфейс о любых изменениях
        private ObservableCollection<Reminder> reminders;

        public MainPage()
        {
            InitializeComponent();
            reminders = new ObservableCollection<Reminder>();//создание объекта типа Reminder
            RemindersListView.ItemsSource = reminders;//связываем коллекцию с элементом списка ListView

            CheckReminders();
        }

        private async void CheckReminders()
        {
          //  бесконечный цикл
            while (true)
            {
                var now = DateTime.Now;//текущая дата и время
                var dueReminders = reminders.Where(r => r.ReminderTime <= now).ToList();//тут все напомиания время, которых уже истекло

                foreach (var reminder in dueReminders)
                {
                    await DisplayAlert("Напоминание", reminder.Message, "OK");//отображаем
                    reminders.Remove(reminder);//удаляем
                }

                await Task.Delay(TimeSpan.FromMinutes(1));//задержка на 1 мин,чтобы избежать нагрузки
            }
        }
        //добавление
        private async void OnAddReminderClicked(object sender, EventArgs e)
        {
            var addReminderPage = new AddReminderPage(AddReminder);//передаем метод AddReminder в качестве колбэка
            await Navigation.PushModalAsync(addReminderPage);//открываем новую страницу
        }

        private void AddReminder(Reminder reminder)
        {
            reminders.Add(reminder);// добавляем напоминание в коллекцию
        }

        private void RemindersListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //при нажатие на элемент списка(напоминание) он удаляется
            if (e.Item is Reminder selectedReminder)
            {
                reminders.Remove(selectedReminder);
            }
        }
    }

    public class Reminder
    {
        public string Message { get; set; }//текст напоминания
        public DateTime ReminderTime { get; set; }//время, когда напоминание должно сработать
    }
}