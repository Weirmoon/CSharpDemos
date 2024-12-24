using CommunityToolkit.Mvvm.Input;
using MauiDemo.Models;

namespace MauiDemo.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}