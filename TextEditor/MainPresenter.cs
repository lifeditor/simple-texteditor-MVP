using System;
using TextEditor.Logic;

namespace TextEditor
{
    public class MainPresenter
    {
        private readonly IMainForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;

        public MainPresenter(IMainForm view, IFileManager manager, IMessageService service)
        {
            _view = view;
            _manager = manager;
            _messageService = service;

            _view.SetSymbolCount(0);
            _view.ContentChanged += new EventHandler(_view_ContentChanged);
            _view.FileOpenClick += new EventHandler(_view_FileOpenClick);
            _view.FileSaveClick += new EventHandler(_view_FileSaveClick);
        }

        void _view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.filePath;

                if (!_manager.IsExist(filePath)) return;

                string content = _manager.GetContent(filePath);

                _view.content = content;
                _view.SetSymbolCount(_manager.GetSymbolCount(content));
            }
            catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                _manager.SaveContent(_view.content, _view.filePath);
                _messageService.ShowMessage("Файл успешно сохранен");
            }
            catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        void _view_ContentChanged(object sender, EventArgs e)
        {
            _view.SetSymbolCount(_manager.GetSymbolCount(_view.content));
        }
    }
}
