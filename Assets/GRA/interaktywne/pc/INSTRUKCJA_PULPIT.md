# Instrukcja przygotowania pulpitu z ikonami w Unity 6000.3.0f1 - KROK PO KROKU

## Krok 1: Przygotowanie Canvas dla pulpitu

### 1.1. Utwórz Canvas dla menu PC:
1. W hierarchii Unity (Hierarchy window): `Right Click` na pustym miejscu
2. Wybierz: `UI` → `Canvas`
3. W inspektorze (Inspector) zmień nazwę na `PC_Canvas`
4. W komponencie `Canvas`:
   - `Render Mode`: Ustaw na `Screen Space - Overlay`
   - `Sort Order`: Możesz zostawić domyślną wartość (0) lub ustawić wyższą, jeśli masz wiele Canvas

### 1.2. Dodaj Panel jako pulpit:
1. `Right Click` na `PC_Canvas` w hierarchii
2. Wybierz: `UI` → `Panel`
3. Zmień nazwę na `DesktopPanel`
4. W komponencie `Image` (Panel):
   - Kliknij na pole `Color`
   - Ustaw kolor tła na niebieski/szary (np. RGB: 50, 100, 150) lub użyj tekstury pulpitu
   - Jeśli chcesz użyć tekstury: W `Source Image` przeciągnij sprite tekstury pulpitu
5. W komponencie `Rect Transform`:
   - **Metoda 1 - Ręczne ustawienie (zalecane dla Unity 6000.3.0f1):**
     - Ustaw wartości bezpośrednio w polach:
       - `Left`: 0
       - `Right`: 0
       - `Top`: 0
       - `Bottom`: 0
     - To wypełni cały ekran panelem
   - **Metoda 2 - Anchor Presets (jeśli dostępne):**
     - Kliknij prawym przyciskiem myszy na komponencie `Rect Transform` w inspektorze
     - Wybierz `Anchor Presets` z menu kontekstowego
     - Przytrzymaj `Alt` i kliknij `Stretch-Stretch` (ostatni w prawym dolnym rogu)
     - To automatycznie ustawi wszystkie wartości na 0

## Krok 2: Utworzenie ikony terminala

### 2.1. Utwórz strukturę ikony:
1. `Right Click` na `DesktopPanel` w hierarchii
2. Wybierz: `UI` → `Button` (lub `Image` jeśli nie potrzebujesz funkcji przycisku)
3. Zmień nazwę na `TerminalIcon`
4. W inspektorze kliknij `Add Component`
5. Wyszukaj `DesktopIcon` i dodaj komponent

### 2.2. Utwórz etykietę tekstową:
1. `Right Click` na `TerminalIcon` w hierarchii
2. Wybierz: `UI` → `Text - TextMeshPro`
3. Jeśli pojawi się okno "TMP Importer", kliknij `Import TMP Essentials`
4. Zmień nazwę na `TerminalLabel`
5. W komponencie `TextMeshPro - Text (UI)`:
   - `Text`: Wpisz "Terminal"
   - `Font Size`: Ustaw na 12-14
   - `Alignment`: Wybierz `Center` (poziomo) i `Top` (pionowo)
   - `Color`: Ustaw na biały lub czarny (w zależności od tła)
6. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 0
     - `Right`: 0
     - `Top`: (zostaw domyślną wartość lub ustaw na odpowiednią pozycję)
     - `Bottom`: Ustaw na -85 (lub inną wartość, aby tekst był pod ikoną)
     - `Height`: Ustaw na 20-25
     - `Pos Y`: Możesz dostosować ręcznie, aby tekst był pod ikoną (np. -60)
   - **Uwaga:** Jeśli widzisz `Anchor Presets` w Rect Transform, możesz użyć `Bottom-Stretch` jako alternatywy

### 2.3. Skonfiguruj ikonę w inspektorze:
1. Wybierz `TerminalIcon` w hierarchii
2. W komponencie `DesktopIcon`:
   - `_iconImage`: Przeciągnij komponent `Image` z tego samego obiektu (znajdziesz go w inspektorze)
   - `_iconLabel`: Przeciągnij `TerminalLabel` z hierarchii
   - `_normalSprite`: Kliknij kółko obok pola i wybierz sprite ikony terminala (normalny) lub przeciągnij sprite z Project
   - `_highlightSprite`: Kliknij kółko obok pola i wybierz sprite ikony terminala (podświetlony) lub przeciągnij sprite z Project
   - `_iconName`: Wpisz "Terminal" (lub inną nazwę)

### 2.4. Skonfiguruj wygląd ikony:
1. Wybierz `TerminalIcon` w hierarchii
2. W komponencie `Rect Transform`:
   - `Width`: Ustaw na 64 lub 80
   - `Height`: Ustaw na 64 lub 80
   - `Pos X`: Ustaw na 50 (lub inną wartość dla lewego górnego rogu)
   - `Pos Y`: Ustaw na -50 (lub inną wartość dla lewego górnego rogu)
3. W komponencie `Image`:
   - `Source Image`: Jeśli nie ustawiłeś wcześniej, przeciągnij sprite ikony terminala
   - `Color`: Ustaw na biały (255, 255, 255, 255) aby sprite był widoczny w pełnych kolorach

## Krok 3: Przygotowanie terminala pełnoekranowego

### 3.1. Utwórz Panel dla terminala pełnoekranowego:
1. `Right Click` na `PC_Canvas` w hierarchii
2. Wybierz: `UI` → `Panel`
3. Zmień nazwę na `FullscreenTerminal`
4. W komponencie `Image`:
   - `Color`: Kliknij na pole koloru i ustaw na czarny (RGB: 0, 0, 0) lub ciemnozielony (RGB: 0, 50, 0)
5. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 0
     - `Right`: 0
     - `Top`: 0
     - `Bottom`: 50 (miejsce na InputField na dole)
   - To wypełni cały ekran z marginesem na dole dla InputField
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Stretch-Stretch` jako alternatywy

### 3.2. Dodaj Scroll View dla outputu:
1. `Right Click` na `FullscreenTerminal` w hierarchii
2. Wybierz: `UI` → `Scroll View`
3. Zmień nazwę na `FullscreenScrollView`
4. W hierarchii rozwiń `FullscreenScrollView`:
   - Zobaczysz: `Viewport` → `Content` → `Text`
5. Usuń domyślny `Text`:
   - `Right Click` na `Text` (dziecko `Content`)
   - Wybierz `Delete`
6. W komponencie `Rect Transform` na `FullscreenScrollView`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 0
     - `Right`: 0
     - `Top`: 0
     - `Bottom`: 50 (miejsce na InputField na dole)
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Stretch-Stretch` jako alternatywy

### 3.3. Skonfiguruj Content w ScrollView:
1. Wybierz `Content` (dziecko `Viewport`, które jest dzieckiem `FullscreenScrollView`)
2. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Width`: Ustaw na 1920 (lub szerokość ekranu)
     - `Height`: Ustaw na 2000 (duża wartość aby umożliwić przewijanie)
     - `Pos X`: 0
     - `Pos Y`: 0
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Top-Left` jako alternatywy
3. W komponencie `Content Size Fitter` (jeśli istnieje):
   - `Vertical Fit`: Ustaw na `Preferred Size`

### 3.4. Dodaj TextMeshPro Text dla outputu:
1. `Right Click` na `Content` (w `FullscreenScrollView` → `Viewport` → `Content`)
2. Wybierz: `UI` → `Text - TextMeshPro`
3. Zmień nazwę na `FullscreenTerminalOutput`
4. W komponencie `TextMeshPro - Text (UI)`:
   - `Text`: Zostaw puste
   - `Font Size`: Ustaw na 14-16
   - `Color`: Kliknij na pole koloru:
     - Zielony: RGB (0, 255, 0) dla klasycznego terminala
     - Biały: RGB (255, 255, 255) dla nowoczesnego terminala
   - `Alignment`: Wybierz `Left` (poziomo) i `Top` (pionowo)
   - `Wrapping`: Włącz jeśli chcesz zawijanie tekstu
5. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Width`: Ustaw na 1920 (lub szerokość ekranu minus marginesy)
     - `Height`: Ustaw na 2000
     - `Pos X`: 10 (mały margines z lewej)
     - `Pos Y`: -10 (mały margines z góry)
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Top-Left` jako alternatywy

### 3.5. Dodaj Scrollbar:
1. `Right Click` na `FullscreenScrollView` w hierarchii
2. Wybierz: `UI` → `Scrollbar`
3. Zmień nazwę na `FullscreenTerminalScrollbar`
4. W komponencie `Scrollbar`:
   - `Direction`: Ustaw na `Bottom To Top`
5. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Width`: Ustaw na 20
     - `Height`: Ustaw na odpowiednią wysokość (dopasuje się automatycznie lub ustaw ręcznie)
     - `Pos X`: -10 (margines od prawej krawędzi - ustaw względem prawej strony ekranu)
     - `Pos Y`: 0 (wyśrodkowany pionowo)
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Middle-Right` jako alternatywy
6. W komponencie `Scroll Rect` na `FullscreenScrollView`:
   - `Vertical Scrollbar`: Przeciągnij `FullscreenTerminalScrollbar` z hierarchii

### 3.6. Dodaj InputField dla inputu:
1. `Right Click` na `FullscreenTerminal` w hierarchii
2. Wybierz: `UI` → `Input Field - TextMeshPro`
3. Jeśli pojawi się okno "TMP Importer", kliknij `Import TMP Essentials`
4. Zmień nazwę na `FullscreenTerminalInput`
5. W komponencie `TMP Input Field`:
   - `Text`: Zostaw puste
   - `Placeholder`: Kliknij na `+` i dodaj `TextMeshPro - Text (UI)`
   - W `Placeholder` → `Text`: Wpisz "Wpisz komendę..."
   - `Font Size`: Ustaw na 14-16
   - `Character Limit`: Zostaw 0 (bez limitu) lub ustaw limit
   - `Line Type`: Ustaw na `Single Line`
   - `Content Type`: Ustaw na `Standard`
6. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 10
     - `Right`: 10
     - `Top`: (zostanie ustawione automatycznie na podstawie Height)
     - `Bottom`: 10
     - `Height`: 30-40
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Bottom-Stretch` jako alternatywy
7. W `Text Area` (dziecko `FullscreenTerminalInput`):
   - W komponencie `TextMeshPro - Text (UI)`:
     - `Color`: Ustaw tak samo jak w OutputText (zielony lub biały)
     - `Font Size`: 14-16

## Krok 4: Przygotowanie terminala okienkowego

### 4.1. Utwórz Panel dla terminala okienkowego:
1. `Right Click` na `PC_Canvas` w hierarchii
2. Wybierz: `UI` → `Panel`
3. Zmień nazwę na `WindowedTerminal`
4. W komponencie `Image`:
   - `Color`: Ustaw na czarny (RGB: 0, 0, 0) lub ciemnozielony (RGB: 0, 50, 0)
5. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Width`: Ustaw na 800
     - `Height`: Ustaw na 600
     - `Pos X`: 0 (wyśrodkowany poziomo)
     - `Pos Y`: 0 (wyśrodkowany pionowo)
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Middle-Center` jako alternatywy

### 4.2. Dodaj Scroll View dla outputu:
1. `Right Click` na `WindowedTerminal` w hierarchii
2. Wybierz: `UI` → `Scroll View`
3. Zmień nazwę na `WindowedScrollView`
4. Usuń domyślny `Text` (jak w kroku 3.2)
5. W komponencie `Rect Transform` na `WindowedScrollView`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 5
     - `Right`: 5
     - `Top`: 35 (miejsce na przycisk zamykania)
     - `Bottom`: 50 (miejsce na InputField)
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Stretch-Stretch` jako alternatywy

### 4.3. Skonfiguruj Content w ScrollView:
1. Wybierz `Content` (w `WindowedScrollView` → `Viewport` → `Content`)
2. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Width`: Ustaw na 790 (szerokość okna minus marginesy)
     - `Height`: Ustaw na 2000
     - `Pos X`: 0
     - `Pos Y`: 0
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Top-Left` jako alternatywy

### 4.4. Dodaj TextMeshPro Text dla outputu:
1. `Right Click` na `Content` (w `WindowedScrollView`)
2. Wybierz: `UI` → `Text - TextMeshPro`
3. Zmień nazwę na `WindowedTerminalOutput`
4. Skonfiguruj tak samo jak `FullscreenTerminalOutput` (krok 3.4)
5. W komponencie `Rect Transform`:
   - `Width`: Ustaw na 790
   - `Height`: Ustaw na 2000

### 4.5. Dodaj Scrollbar:
1. `Right Click` na `WindowedScrollView` w hierarchii
2. Wybierz: `UI` → `Scrollbar`
3. Zmień nazwę na `WindowedTerminalScrollbar`
4. Skonfiguruj tak samo jak `FullscreenTerminalScrollbar` (krok 3.5)
5. W komponencie `Scroll Rect` na `WindowedScrollView`:
   - `Vertical Scrollbar`: Przeciągnij `WindowedTerminalScrollbar`

### 4.6. Dodaj InputField dla inputu:
1. `Right Click` na `WindowedTerminal` w hierarchii
2. Wybierz: `UI` → `Input Field - TextMeshPro`
3. Zmień nazwę na `WindowedTerminalInput`
4. Skonfiguruj tak samo jak `FullscreenTerminalInput` (krok 3.6)
5. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 10
     - `Right`: 10
     - `Top`: (zostanie ustawione automatycznie)
     - `Bottom`: 10
     - `Height`: 30-40
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Bottom-Stretch` jako alternatywy

### 4.7. Dodaj przycisk zamykania okna (opcjonalne):
1. `Right Click` na `WindowedTerminal` w hierarchii
2. Wybierz: `UI` → `Button`
3. Zmień nazwę na `CloseWindowButton`
4. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Width`: 30
     - `Height`: 30
     - `Pos X`: -15 (lub ustaw pozycję od prawej krawędzi okna)
     - `Pos Y`: -15 (lub ustaw pozycję od górnej krawędzi okna)
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Top-Right` jako alternatywy
5. W komponencie `Image` (Button):
   - `Color`: Ustaw na czerwony (RGB: 255, 0, 0) lub szary (RGB: 100, 100, 100)
6. Dodaj tekst "X":
   - `Right Click` na `CloseWindowButton` w hierarchii
   - Wybierz: `UI` → `Text - TextMeshPro`
   - W komponencie `TextMeshPro - Text (UI)`:
     - `Text`: Wpisz "X"
     - `Font Size`: 20
     - `Alignment`: `Center` (poziomo i pionowo)
     - `Color`: Biały (RGB: 255, 255, 255)

## Krok 5: Utworzenie Panelu opcji wyboru

### 5.1. Utwórz Panel opcji:
1. `Right Click` na `PC_Canvas` w hierarchii
2. Wybierz: `UI` → `Panel`
3. Zmień nazwę na `TerminalOptionsPanel`
4. W komponencie `Image`:
   - `Color`: Kliknij na pole koloru
   - Ustaw na szary z przezroczystością (np. RGB: 50, 50, 50, Alpha: 200)
5. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Width`: 300
     - `Height`: 200
     - `Pos X`: 0 (wyśrodkowany poziomo)
     - `Pos Y`: 0 (wyśrodkowany pionowo)
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Middle-Center` jako alternatywy

### 5.2. Dodaj przycisk "Pełny ekran":
1. `Right Click` na `TerminalOptionsPanel` w hierarchii
2. Wybierz: `UI` → `Button`
3. Zmień nazwę na `FullscreenButton`
4. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 20
     - `Right`: 20
     - `Top`: 20
     - `Height`: 40
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Top-Stretch` jako alternatywy
5. Dodaj tekst:
   - Wybierz `Text (TMP)` (dziecko `FullscreenButton`)
   - W komponencie `TextMeshPro - Text (UI)`:
     - `Text`: Wpisz "Pełny ekran"
     - `Font Size`: 16
     - `Alignment`: `Center` (poziomo i pionowo)

### 5.3. Dodaj przycisk "Okno":
1. `Right Click` na `TerminalOptionsPanel` w hierarchii
2. Wybierz: `UI` → `Button`
3. Zmień nazwę na `WindowedButton`
4. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 20
     - `Right`: 20
     - `Top`: 70 (lub dostosuj pozycję)
     - `Height`: 40
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Middle-Stretch` jako alternatywy
5. Dodaj tekst "Okno" (jak w kroku 5.2)

### 5.4. Dodaj przycisk "Anuluj":
1. `Right Click` na `TerminalOptionsPanel` w hierarchii
2. Wybierz: `UI` → `Button`
3. Zmień nazwę na `CancelButton`
4. W komponencie `Rect Transform`:
   - **Dla Unity 6000.3.0f1 - ustaw wartości ręcznie:**
     - `Left`: 20
     - `Right`: 20
     - `Bottom`: 20
     - `Height`: 40
   - **Uwaga:** Jeśli widzisz `Anchor Presets`, możesz użyć `Bottom-Stretch` jako alternatywy
5. Dodaj tekst "Anuluj" (jak w kroku 5.2)

## Krok 6: Konfiguracja skryptów

### 6.1. Dodaj skrypt PcDesktop:
1. W hierarchii: `Right Click` → `Create Empty`
2. Zmień nazwę na `PC_Desktop_Object`
3. W inspektorze kliknij `Add Component`
4. Wyszukaj `Pc Desktop` i dodaj komponent
5. W komponencie `Pc Desktop`:
   - `_desktop Panel`: Przeciągnij `DesktopPanel` z hierarchii do tego pola

### 6.2. Skonfiguruj DesktopIcon:
1. Wybierz `TerminalIcon` w hierarchii
2. W komponencie `Desktop Icon`:
   - `_desktop`: Przeciągnij `PC_Desktop_Object` z hierarchii
   - `_terminal`: Zostaw puste na razie (przypiszesz później)

### 6.3. Dodaj skrypt PcTerminal:
1. W hierarchii: `Right Click` → `Create Empty`
2. Zmień nazwę na `PC_Terminal_Object`
3. W inspektorze kliknij `Add Component`
4. Wyszukaj `Pc Terminal` i dodaj komponent
5. W komponencie `Pc Terminal` przypisz wszystkie pola:

   **Podstawowe pola (opcjonalne):**
   - `_input Field`: Możesz zostawić puste
   - `_output Text`: Możesz zostawić puste
   - `_scrollbar`: Możesz zostawić puste

   **Terminal Windows:**
   - `_fullscreen Terminal`: Przeciągnij `FullscreenTerminal` (Panel) z hierarchii
   - `_windowed Terminal`: Przeciągnij `WindowedTerminal` (Panel) z hierarchii
   - `_terminal Options Panel`: Przeciągnij `TerminalOptionsPanel` (Panel) z hierarchii
   - `_fullscreen Button`: Przeciągnij `FullscreenButton` (Button) z hierarchii
   - `_windowed Button`: Przeciągnij `WindowedButton` (Button) z hierarchii
   - `_cancel Button`: Przeciągnij `CancelButton` (Button) z hierarchii
   - `_close Window Button`: Przeciągnij `CloseWindowButton` (Button) z hierarchii (jeśli utworzyłeś)

   **Terminal Components - Fullscreen:**
   - `_fullscreen Input Field`: Przeciągnij `FullscreenTerminalInput` (TMP_InputField) z hierarchii
   - `_fullscreen Output Text`: Przeciągnij `FullscreenTerminalOutput` (TMP_Text) z hierarchii
   - `_fullscreen Scrollbar`: Przeciągnij `FullscreenTerminalScrollbar` (Scrollbar) z hierarchii

   **Terminal Components - Windowed:**
   - `_windowed Input Field`: Przeciągnij `WindowedTerminalInput` (TMP_InputField) z hierarchii
   - `_windowed Output Text`: Przeciągnij `WindowedTerminalOutput` (TMP_Text) z hierarchii
   - `_windowed Scrollbar`: Przeciągnij `WindowedTerminalScrollbar` (Scrollbar) z hierarchii

### 6.4. Zaktualizuj DesktopIcon:
1. Wybierz `TerminalIcon` w hierarchii
2. W komponencie `Desktop Icon`:
   - `_terminal`: Przeciągnij `PC_Terminal_Object` z hierarchii

### 6.5. Zaktualizuj PcMenu:
1. Znajdź obiekt z komponentem `Pc Menu` (lub utwórz nowy: `Right Click` → `Create Empty`, dodaj komponent `Pc Menu`)
2. Zmień nazwę na `PC_Menu_Object`
3. W komponencie `Pc Menu`:
   - `_menu`: Przeciągnij `PC_Canvas` z hierarchii
   - `_desktop`: Przeciągnij `PC_Desktop_Object` z hierarchii
   - `_terminal`: Przeciągnij `PC_Terminal_Object` z hierarchii

### 6.6. Połącz PcCORE z PcMenu:
1. Znajdź obiekt z komponentem `Pc CORE` w scenie
2. W komponencie `Pc CORE`:
   - `_pc Menu`: Przeciągnij `PC_Menu_Object` z hierarchii

## Krok 7: Finalne ustawienia i testowanie

### 7.1. Ustawienie warstw (z-order):
1. W hierarchii upewnij się, że kolejność obiektów jest następująca (od góry do dołu):
   - `TerminalOptionsPanel` (najwyżej - będzie widoczny nad wszystkim)
   - `FullscreenTerminal` (drugi - będzie widoczny nad pulpitem)
   - `WindowedTerminal` (trzeci)
   - `DesktopPanel` (najniżej - tło)

### 7.2. Ukryj terminale na starcie:
1. Wybierz `FullscreenTerminal` w hierarchii
2. W inspektorze odznacz checkbox obok nazwy obiektu (ukryje go)
3. Zrób to samo dla `WindowedTerminal` i `TerminalOptionsPanel`

### 7.3. Testowanie:
1. Naciśnij `Play` w Unity
2. Interakcja z PC powinna otworzyć pulpit
3. Kliknij dwukrotnie na ikonę terminala
4. Powinno pojawić się okno wyboru
5. Kliknij "Pełny ekran" lub "Okno"
6. Terminal powinien się otworzyć
7. Wpisz `help` i naciśnij Enter
8. Sprawdź czy komendy działają
9. Wpisz `exit` aby zamknąć terminal

## Struktura hierarchii (finalna):

```
PC_Canvas
├── DesktopPanel
│   └── TerminalIcon (z DesktopIcon)
│       └── TerminalLabel (TextMeshPro)
├── FullscreenTerminal (Panel) [ukryty na starcie]
│   ├── FullscreenScrollView (Scroll View)
│   │   ├── Viewport
│   │   │   └── Content (GameObject)
│   │   │       └── FullscreenTerminalOutput (TextMeshPro)
│   │   └── FullscreenTerminalScrollbar (Scrollbar)
│   └── FullscreenTerminalInput (InputField - TextMeshPro)
│       └── Text Area
│           └── Text (TextMeshPro)
├── WindowedTerminal (Panel) [ukryty na starcie]
│   ├── WindowedScrollView (Scroll View)
│   │   ├── Viewport
│   │   │   └── Content (GameObject)
│   │   │       └── WindowedTerminalOutput (TextMeshPro)
│   │   └── WindowedTerminalScrollbar (Scrollbar)
│   ├── WindowedTerminalInput (InputField - TextMeshPro)
│   │   └── Text Area
│   │       └── Text (TextMeshPro)
│   └── CloseWindowButton (Button) [opcjonalne]
│       └── Text (TextMeshPro) - "X"
└── TerminalOptionsPanel (Panel) [ukryty na starcie]
    ├── FullscreenButton (Button)
    │   └── Text (TextMeshPro) - "Pełny ekran"
    ├── WindowedButton (Button)
    │   └── Text (TextMeshPro) - "Okno"
    └── CancelButton (Button)
        └── Text (TextMeshPro) - "Anuluj"
```

## Funkcjonalność:

1. **Kliknięcie na ikonę terminala:**
   - Pojedyncze kliknięcie: Zaznacza ikonę (podświetla ją)
   - Podwójne kliknięcie: Otwiera okno wyboru trybu (`TerminalOptionsPanel`)

2. **Okno wyboru:**
   - "Pełny ekran" - otwiera terminal na pełnym ekranie (`FullscreenTerminal`)
   - "Okno" - otwiera terminal w oknie (`WindowedTerminal`)
   - "Anuluj" - zamyka okno wyboru bez otwierania terminala

3. **Terminal:**
   - Działa tak samo w obu trybach
   - Komenda `exit` lub `quit` zamyka terminal
   - Komenda `help` wyświetla listę dostępnych komend
   - Strzałki góra/dół nawigują po historii komend

## Dodatkowe wskazówki:

1. **Czcionka monospace:**
   - Dla lepszego efektu terminala użyj czcionki monospace
   - W TextMeshPro: Window → TextMeshPro → Font Asset Creator
   - Wybierz czcionkę systemową (np. Courier New, Consolas)
   - Kliknij `Generate Font Atlas`
   - Zapisz jako nowy Font Asset
   - Ustaw ten Font Asset w `Font Asset` w obu OutputText i InputField

2. **Kolory terminala:**
   - Zielony tekst na czarnym tle = klasyczny terminal (RGB: 0, 255, 0)
   - Biały tekst na czarnym tle = nowoczesny terminal (RGB: 255, 255, 255)
   - Możesz użyć gradientów dla lepszego efektu

3. **Automatyczne przewijanie:**
   - Scrollbar automatycznie przewija się do dołu po każdej komendzie
   - Upewnij się, że `Content` w ScrollView ma odpowiednią wysokość (2000+)

4. **InputField:**
   - Ustaw `Line Type` na `Single Line` w InputField
   - Ustaw `Content Type` na `Standard` lub `Alphanumeric`
   - Włącz `Rich Text` jeśli chcesz używać kolorów w output

5. **Rozwiązywanie problemów:**
   - Jeśli terminal się nie otwiera: Sprawdź czy wszystkie referencje są przypisane w `PcTerminal`
   - Jeśli tekst się nie wyświetla: Sprawdź czy `Content` w ScrollView ma odpowiednią wysokość
   - Jeśli scrollbar nie działa: Sprawdź czy jest przypisany w `Scroll Rect`
   - Jeśli input nie działa: Sprawdź czy InputField jest przypisany w `PcTerminal`

## Uwagi końcowe:

- Upewnij się, że wszystkie panele są na odpowiednich warstwach (z-order)
- Terminal pełnoekranowy powinien być na najwyższej warstwie
- Terminal okienkowy może mieć możliwość przeciągania (dodaj `ObjFollowMouse` jeśli chcesz)
- Możesz dodać animacje otwierania/zamykania okien używając `Animator` lub `DOTween`
- Wszystkie obiekty powinny być ukryte na starcie (oprócz `DesktopPanel`)
