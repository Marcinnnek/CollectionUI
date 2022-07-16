using System.ComponentModel;

public abstract class ObservedObject : INotifyPropertyChanged //klasa którą możemy przenosić z projektu do projektu
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public void onPropertyChanged(params string[] properties)
    {
        if (PropertyChanged != null)//  jeśli mam jakąś subskrybcje
        {
            foreach (var prop in properties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
            
    }
}

