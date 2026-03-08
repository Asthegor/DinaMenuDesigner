namespace DinaMenuDesigner.Services
{
    public interface IFileDialogService
    {
        public string? GetSaveFilePath(string filter);
        public string? GetOpenFilePath(string filter);
    }
}
