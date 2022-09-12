namespace MobileStatisticsApp.Api;
/// <summary>
/// Счетчик.
/// </summary>
public class TimerManager
{
    private Timer? timer;
    private AutoResetEvent? autoResetEvent;
    private Action? action;
    /// <summary>
    /// Время начала счетчика.
    /// </summary>
    public DateTime TimerStarted { get; set; }
    /// <summary>
    /// Счетчик включен.
    /// </summary>
    public bool IsTimerStarted { get; set; }
    /// <summary>
    /// Подготовка счетчика.
    /// </summary>
    /// <param name="action">Событие вызова счетчика.</param>
    public void PrepareTimer(Action action)
    {
        this.action = action;
        this.autoResetEvent = new AutoResetEvent(false);
        this.timer = new Timer(Execute, this.autoResetEvent, 1000, 2000);
        TimerStarted = DateTime.Now;
        IsTimerStarted = true;
    }
    /// <summary>
    /// Хранение состояния времени.
    /// </summary>
    /// <param name="stateInfo">Состояние времени.</param>
    public void Execute(object? stateInfo)
    {
        this.action?.Invoke();
        if ((DateTime.Now - TimerStarted).TotalSeconds > 60)
        {
            IsTimerStarted = false;
            this.timer?.Dispose();
        }
    }
}
