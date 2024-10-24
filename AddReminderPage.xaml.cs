namespace rmp7l;

public partial class AddReminderPage : ContentPage
{
    private Action<Reminder> addReminderAction;//передача нового напоминания на гл страницу
    public AddReminderPage(Action<Reminder> addReminderAction)
	{
		InitializeComponent();
        this.addReminderAction = addReminderAction;
    }
    private async void OnAddReminderClicked(object sender, EventArgs e)
    {
        DateTime reminderDate = datePicker.Date.Add(timePicker.Time);//создаем дату и время напоминания
        string message = messageEntry.Text;//текст напоминания

        if (string.IsNullOrWhiteSpace(message))//если поле сообщения пустое
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите сообщение напоминания.", "OK");
            return;
        }

        var reminderTimeSpan = reminderDate - DateTime.Now;//указаное время - текущее

        if (reminderTimeSpan.TotalMilliseconds > 0)
        {
            var reminder = new Reminder { Message = message, ReminderTime = reminderDate };//создается объект Reminder   передается колбэк
            addReminderAction(reminder);//вызывается переданный колбэк
            await Navigation.PopModalAsync();  // Закрываем модальное окно
        }
        else
        {
            await DisplayAlert("Ошибка", "Дата и время должны быть в будущем.", "OK");
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(); // Закрывает модальное окно
    }
}