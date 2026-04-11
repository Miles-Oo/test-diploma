using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PcTerminal : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _outputText;
    [SerializeField] private Scrollbar _scrollbar;
    
    [Header("Terminal Windows")]
    [SerializeField] private GameObject _fullscreenTerminal;
    [SerializeField] private GameObject _windowedTerminal;
    [SerializeField] private GameObject _terminalOptionsPanel;
    [SerializeField] private Button _fullscreenButton;
    [SerializeField] private Button _windowedButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _closeWindowButton;
    
    [Header("Terminal Components - Fullscreen")]
    [SerializeField] private TMP_InputField _fullscreenInputField;
    [SerializeField] private TMP_Text _fullscreenOutputText;
    [SerializeField] private Scrollbar _fullscreenScrollbar;
    
    [Header("Terminal Components - Windowed")]
    [SerializeField] private TMP_InputField _windowedInputField;
    [SerializeField] private TMP_Text _windowedOutputText;
    [SerializeField] private Scrollbar _windowedScrollbar;
    
    private List<string> _commandHistory = new List<string>();
    private int _historyIndex = -1;
    private string _currentDirectory = "C:\\";
    private bool _isFullscreen = false;
    private bool _isOpen = false;
    
    private Dictionary<string, System.Action<string[]>> _commands;

    private void Start()
    {
        InitializeCommands();
        ClearOutput();
        PrintWelcomeMessage();
        
        // Setup input fields for both modes
        SetupInputField(_inputField);
        SetupInputField(_fullscreenInputField);
        SetupInputField(_windowedInputField);
        
        // Setup buttons
        if (_fullscreenButton != null)
            _fullscreenButton.onClick.AddListener(OpenFullscreen);
        
        if (_windowedButton != null)
            _windowedButton.onClick.AddListener(OpenWindowed);
        
        if (_cancelButton != null)
            _cancelButton.onClick.AddListener(HideTerminalOptions);
        
        if (_closeWindowButton != null)
            _closeWindowButton.onClick.AddListener(CloseTerminal);
        
        // Hide terminals initially
        if (_fullscreenTerminal != null)
            _fullscreenTerminal.SetActive(false);
        
        if (_windowedTerminal != null)
            _windowedTerminal.SetActive(false);
        
        if (_terminalOptionsPanel != null)
            _terminalOptionsPanel.SetActive(false);
    }
    
    private void SetupInputField(TMP_InputField field)
    {
        if (field != null)
        {
            field.onSubmit.AddListener(OnCommandSubmitted);
            field.onSelect.AddListener(OnInputSelected);
        }
    }

    private void OnDestroy()
    {
        // Remove listeners from all input fields
        RemoveInputFieldListeners(_inputField);
        RemoveInputFieldListeners(_fullscreenInputField);
        RemoveInputFieldListeners(_windowedInputField);
        
        // Remove button listeners
        if (_fullscreenButton != null)
            _fullscreenButton.onClick.RemoveAllListeners();
        
        if (_windowedButton != null)
            _windowedButton.onClick.RemoveAllListeners();
        
        if (_cancelButton != null)
            _cancelButton.onClick.RemoveAllListeners();
        
        if (_closeWindowButton != null)
            _closeWindowButton.onClick.RemoveAllListeners();
    }
    
    private void RemoveInputFieldListeners(TMP_InputField field)
    {
        if (field != null)
        {
            field.onSubmit.RemoveListener(OnCommandSubmitted);
            field.onSelect.RemoveListener(OnInputSelected);
        }
    }

    private void Update()
    {
        // Nawigacja po historii komend strzałkami
        TMP_InputField activeInput = GetActiveInputField();
        if (activeInput != null && activeInput.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NavigateHistory(-1, activeInput);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                NavigateHistory(1, activeInput);
            }
        }
    }
    
    private TMP_InputField GetActiveInputField()
    {
        if (_isFullscreen && _fullscreenInputField != null)
            return _fullscreenInputField;
        else if (!_isFullscreen && _windowedInputField != null)
            return _windowedInputField;
        else if (_inputField != null)
            return _inputField;
        return null;
    }

    private void InitializeCommands()
    {
        _commands = new Dictionary<string, System.Action<string[]>>
        {
            { "help", ExecuteHelp },
            { "clear", ExecuteClear },
            { "cls", ExecuteClear },
            { "pwd", ExecutePwd },
            { "cd", ExecuteCd },
            { "ls", ExecuteLs },
            { "dir", ExecuteLs },
            { "echo", ExecuteEcho },
            { "whoami", ExecuteWhoami },
            { "date", ExecuteDate },
            { "time", ExecuteTime },
            { "exit", ExecuteExit },
            { "quit", ExecuteExit }
        };
    }

    private void OnCommandSubmitted(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return;

        // Dodaj komendę do historii
        _commandHistory.Add(command);
        _historyIndex = _commandHistory.Count;
        
        // Wyświetl komendę w output
        AppendOutput($"{_currentDirectory}> {command}");
        
        // Wykonaj komendę
        ExecuteCommand(command);
        
        // Wyczyść input field
        TMP_InputField activeInput = GetActiveInputField();
        if (activeInput != null)
        {
            activeInput.text = "";
            activeInput.ActivateInputField();
        }
        
        // Przewiń do dołu
        Scrollbar activeScrollbar = GetActiveScrollbar();
        if (activeScrollbar != null)
            activeScrollbar.value = 0;
    }

    private void OnInputSelected(string text)
    {
        _inputField.ActivateInputField();
    }

    private void NavigateHistory(int direction, TMP_InputField field = null)
    {
        if (_commandHistory.Count == 0)
            return;

        if (field == null)
            field = GetActiveInputField();
        
        if (field == null)
            return;

        _historyIndex += direction;
        
        if (_historyIndex < 0)
            _historyIndex = 0;
        else if (_historyIndex >= _commandHistory.Count)
            _historyIndex = _commandHistory.Count - 1;
        
        if (_historyIndex >= 0 && _historyIndex < _commandHistory.Count)
        {
            field.text = _commandHistory[_historyIndex];
            field.caretPosition = field.text.Length;
        }
    }

    private void ExecuteCommand(string command)
    {
        string[] parts = command.Trim().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 0)
            return;

        string commandName = parts[0].ToLower();
        string[] args = parts.Skip(1).ToArray();

        if (_commands.ContainsKey(commandName))
        {
            _commands[commandName](args);
        }
        else
        {
            AppendOutput($"'{commandName}' nie jest rozpoznawany jako komenda wewnętrzna lub zewnętrzna.");
            AppendOutput("Wpisz 'help' aby zobaczyć listę dostępnych komend.");
        }
    }

    private void ExecuteHelp(string[] args)
    {
        AppendOutput("Dostępne komendy:");
        AppendOutput("  help     - Wyświetla listę dostępnych komend");
        AppendOutput("  clear    - Czyści ekran terminala");
        AppendOutput("  cls      - Czyści ekran terminala");
        AppendOutput("  pwd      - Wyświetla aktualny katalog");
        AppendOutput("  cd [dir] - Zmienia katalog");
        AppendOutput("  ls       - Wyświetla zawartość katalogu");
        AppendOutput("  dir      - Wyświetla zawartość katalogu");
        AppendOutput("  echo     - Wyświetla tekst");
        AppendOutput("  whoami   - Wyświetla informacje o użytkowniku");
        AppendOutput("  date     - Wyświetla datę");
        AppendOutput("  time     - Wyświetla czas");
        AppendOutput("  exit     - Zamyka terminal");
        AppendOutput("  quit     - Zamyka terminal");
    }

    private void ExecuteClear(string[] args)
    {
        ClearOutput();
        PrintWelcomeMessage();
    }

    private void ExecutePwd(string[] args)
    {
        AppendOutput(_currentDirectory);
    }

    private void ExecuteCd(string[] args)
    {
        if (args.Length == 0)
        {
            _currentDirectory = "C:\\";
            AppendOutput(_currentDirectory);
        }
        else
        {
            string newDir = args[0];
            
            // Symulacja zmiany katalogu
            if (newDir == "..")
            {
                if (_currentDirectory != "C:\\")
                {
                    int lastSlash = _currentDirectory.LastIndexOf('\\');
                    if (lastSlash > 0)
                        _currentDirectory = _currentDirectory.Substring(0, lastSlash);
                    else
                        _currentDirectory = "C:\\";
                }
            }
            else if (newDir == "C:" || newDir == "C:\\")
            {
                _currentDirectory = "C:\\";
            }
            else if (!newDir.Contains(":"))
            {
                // Dodaj do aktualnego katalogu
                if (_currentDirectory.EndsWith("\\"))
                    _currentDirectory += newDir;
                else
                    _currentDirectory += "\\" + newDir;
            }
            else
            {
                _currentDirectory = newDir;
            }
            
            AppendOutput(_currentDirectory);
        }
    }

    private void ExecuteLs(string[] args)
    {
        // Symulacja zawartości katalogu
        AppendOutput("Katalog " + _currentDirectory);
        AppendOutput("");
        AppendOutput("  <DIR>          Documents");
        AppendOutput("  <DIR>          Downloads");
        AppendOutput("  <DIR>          Program Files");
        AppendOutput("  <DIR>          Users");
        AppendOutput("  <DIR>          Windows");
        AppendOutput("  <FILE>         readme.txt");
        AppendOutput("  <FILE>         config.ini");
    }

    private void ExecuteEcho(string[] args)
    {
        if (args.Length > 0)
        {
            AppendOutput(string.Join(" ", args));
        }
    }

    private void ExecuteWhoami(string[] args)
    {
        AppendOutput("Użytkownik: Administrator");
        AppendOutput("System: Windows Terminal v1.0");
    }

    private void ExecuteDate(string[] args)
    {
        System.DateTime now = System.DateTime.Now;
        AppendOutput(now.ToString("dd.MM.yyyy"));
    }

    private void ExecuteTime(string[] args)
    {
        System.DateTime now = System.DateTime.Now;
        AppendOutput(now.ToString("HH:mm:ss"));
    }

    private void ExecuteExit(string[] args)
    {
        AppendOutput("Zamykanie terminala...");
        CloseTerminal();
    }
    
    public void ShowTerminalOptions()
    {
        if (_terminalOptionsPanel != null)
        {
            _terminalOptionsPanel.SetActive(true);
        }
    }
    
    public void HideTerminalOptions()
    {
        if (_terminalOptionsPanel != null)
        {
            _terminalOptionsPanel.SetActive(false);
        }
    }
    
    public void OpenFullscreen()
    {
        _isFullscreen = true;
        _isOpen = true;
        
        HideTerminalOptions();
        
        if (_windowedTerminal != null)
            _windowedTerminal.SetActive(false);
        
        if (_fullscreenTerminal != null)
        {
            _fullscreenTerminal.SetActive(true);
            ActivateTerminal();
        }
    }
    
    public void OpenWindowed()
    {
        _isFullscreen = false;
        _isOpen = true;
        
        HideTerminalOptions();
        
        if (_fullscreenTerminal != null)
            _fullscreenTerminal.SetActive(false);
        
        if (_windowedTerminal != null)
        {
            _windowedTerminal.SetActive(true);
            ActivateTerminal();
        }
    }
    
    public void CloseTerminal()
    {
        _isOpen = false;
        
        if (_fullscreenTerminal != null)
            _fullscreenTerminal.SetActive(false);
        
        if (_windowedTerminal != null)
            _windowedTerminal.SetActive(false);
        
        DeactivateTerminal();
    }
    
    public bool IsOpen()
    {
        return _isOpen;
    }
    
    public bool IsFullscreen()
    {
        return _isFullscreen;
    }

    private void AppendOutput(string text)
    {
        TMP_Text activeOutput = GetActiveOutputText();
        if (activeOutput != null)
        {
            if (string.IsNullOrEmpty(activeOutput.text))
                activeOutput.text = text;
            else
                activeOutput.text += "\n" + text;
        }
    }

    private void ClearOutput()
    {
        TMP_Text activeOutput = GetActiveOutputText();
        if (activeOutput != null)
            activeOutput.text = "";
    }
    
    private TMP_Text GetActiveOutputText()
    {
        if (_isFullscreen && _fullscreenOutputText != null)
            return _fullscreenOutputText;
        else if (!_isFullscreen && _windowedOutputText != null)
            return _windowedOutputText;
        else if (_outputText != null)
            return _outputText;
        return null;
    }
    
    private Scrollbar GetActiveScrollbar()
    {
        if (_isFullscreen && _fullscreenScrollbar != null)
            return _fullscreenScrollbar;
        else if (!_isFullscreen && _windowedScrollbar != null)
            return _windowedScrollbar;
        else if (_scrollbar != null)
            return _scrollbar;
        return null;
    }

    private void PrintWelcomeMessage()
    {
        AppendOutput("Microsoft Windows [Wersja 10.0.19042.1234]");
        AppendOutput("(c) 2025 Microsoft Corporation. Wszelkie prawa zastrzeżone.");
        AppendOutput("");
        AppendOutput("Wpisz 'help' aby zobaczyć listę dostępnych komend.");
        AppendOutput("");
    }

    public void ActivateTerminal()
    {
        TMP_InputField activeInput = GetActiveInputField();
        if (activeInput != null)
        {
            activeInput.ActivateInputField();
            activeInput.Select();
        }
    }

    public void DeactivateTerminal()
    {
        TMP_InputField activeInput = GetActiveInputField();
        if (activeInput != null)
        {
            activeInput.DeactivateInputField();
        }
    }
}
