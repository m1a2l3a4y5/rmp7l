namespace rmp7l;

public partial class AddReminderPage : ContentPage
{
    private Action<Reminder> addReminderAction;//�������� ������ ����������� �� �� ��������
    public AddReminderPage(Action<Reminder> addReminderAction)
	{
		InitializeComponent();
        this.addReminderAction = addReminderAction;
    }
    private async void OnAddReminderClicked(object sender, EventArgs e)
    {
        DateTime reminderDate = datePicker.Date.Add(timePicker.Time);//������� ���� � ����� �����������
        string message = messageEntry.Text;//����� �����������

        if (string.IsNullOrWhiteSpace(message))//���� ���� ��������� ������
        {
            await DisplayAlert("������", "����������, ������� ��������� �����������.", "OK");
            return;
        }

        var reminderTimeSpan = reminderDate - DateTime.Now;//�������� ����� - �������

        if (reminderTimeSpan.TotalMilliseconds > 0)
        {
            var reminder = new Reminder { Message = message, ReminderTime = reminderDate };//��������� ������ Reminder   ���������� ������
            addReminderAction(reminder);//���������� ���������� ������
            await Navigation.PopModalAsync();  // ��������� ��������� ����
        }
        else
        {
            await DisplayAlert("������", "���� � ����� ������ ���� � �������.", "OK");
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(); // ��������� ��������� ����
    }
}