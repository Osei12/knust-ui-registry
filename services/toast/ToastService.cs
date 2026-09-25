using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace __MYUI_NAMESPACE__;

public enum ToastVariant
{
    Default,
    Success,
    Error,
    Info,
    Warning
}

public class ToastModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ToastVariant Variant { get; set; } = ToastVariant.Default;
    public DateTime Timestamp { get; } = DateTime.Now;
}

public class ToastService
{
    public event Action? OnToastsChanged;
    public event Action<Guid>? OnRequestClose;
    private readonly List<ToastModel> _toasts = new();

    public IReadOnlyList<ToastModel> Toasts => _toasts;

    public void Notify(string title, string message, ToastVariant variant = ToastVariant.Default)
    {
        var toast = new ToastModel { Title = title, Message = message, Variant = variant };
        _toasts.Add(toast);
        OnToastsChanged?.Invoke();

        // Signal closing after 5 seconds
        Task.Delay(5000).ContinueWith(_ => Close(toast.Id));
    }

    public void Success(string title, string message) => Notify(title, message, ToastVariant.Success);
    public void Error(string title, string message) => Notify(title, message, ToastVariant.Error);
    public void Warning(string title, string message) => Notify(title, message, ToastVariant.Warning);
    public void Info(string title, string message) => Notify(title, message, ToastVariant.Info);

    public void Close(Guid id)
    {
        OnRequestClose?.Invoke(id);
    }

    public void Remove(Guid id)
    {
        var toast = _toasts.Find(t => t.Id == id);
        if (toast != null)
        {
            _toasts.Remove(toast);
            OnToastsChanged?.Invoke();
        }
    }
}
