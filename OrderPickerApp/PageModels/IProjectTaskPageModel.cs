using CommunityToolkit.Mvvm.Input;
using OrderPickerApp.Models;

namespace OrderPickerApp.PageModels;

public interface IProjectTaskPageModel
{
    IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
    bool IsBusy { get; }
}