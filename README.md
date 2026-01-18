# 📈 Stock Analysis Application

A comprehensive Windows Forms application for loading, displaying, and analyzing stock market data with candlestick charting, pattern recognition, and real-time ticker simulation.

## 🎯 Overview

This application was developed as a semester-long project, evolving through three phases to create a full-featured stock analysis tool. It demonstrates object-oriented programming principles, data binding, inheritance, abstract classes, and design patterns in C#.

### Key Capabilities

- **Load & Display** — Import stock data from CSV files (Daily, Weekly, Monthly)
- **Candlestick Charts** — Visualize OHLC data with properly colored candlesticks (green for bullish, red for bearish)
- **Volume Analysis** — Display trading volume as a column chart
- **Multi-Stock Support** — Open multiple stocks simultaneously, each in its own window
- **Pattern Recognition** — Automatically detect 1-candlestick and 2-candlestick patterns
- **Real-Time Simulation** — Animate candlesticks appearing sequentially with adjustable speed

---

## 📸 Screenshots

### Daily Chart View (ABBV)
<img width="1440" height="900" alt="Screenshot 2026-01-17 at 21 27 27" src="https://github.com/user-attachments/assets/f723fca0-b9de-4394-86f3-1a2d0925346f" />
*Main application window showing ABBV daily candlestick data with volume chart below. Features animation speed controls and pattern selection dropdown.*

### Pattern Detection (AAPL Weekly - Bullish Harami)
<img width="1440" height="900" alt="Screenshot 2026-01-17 at 21 27 42" src="https://github.com/user-attachments/assets/351e246c-2d53-4c61-aac9-549529235cee" />
*Weekly AAPL chart with Bullish Harami pattern detection enabled. Detected patterns are highlighted with rectangle annotations and labeled.*

---

## 🖼️ Features

### Candlestick Charting
- OHLC (Open, High, Low, Close) data displayed in standard candlestick format
- Dynamic Y-axis scaling with 2% padding for optimal chart utilization
- Weekend/holiday gap elimination for daily data
- Chart titles displaying stock symbol, period, and date range

### Pattern Recognition

The application recognizes the following candlestick patterns:

**1-Candlestick Patterns:**
| Pattern | Variations |
|---------|------------|
| Doji | Regular, Dragonfly, Gravestone |
| Marubozu | Bullish, Bearish |
| Hammer | Bullish, Bearish |
| Inverted Hammer | Bullish, Bearish |

**2-Candlestick Patterns:**
| Pattern | Description |
|---------|-------------|
| Engulfing | Bullish & Bearish variants |
| Harami | Bullish & Bearish variants |

Detected patterns are highlighted using chart annotations (rectangles, arrows, or lines).

### Real-Time Ticker Simulation
- Simulates live market data by displaying candlesticks sequentially
- Adjustable animation speed with slider control
- Start/Stop buttons for playback control
- Speed displayed in milliseconds (e.g., 500 ms)

---

## 🏗️ Architecture

### Class Hierarchy

```
Candlestick
    └── SmartCandlestick
            ├── Anatomical Properties (range, bodyRange, upperTailRange, lowerTailRange)
            ├── Directional Properties (isBullish, isBearish)
            └── Pattern Properties (isDoji, isMarubozu, isHammer, etc.)

Recognizer (Abstract Base Class)
    ├── Recognizer_Doji
    ├── Recognizer_Marubozu
    ├── Recognizer_BullishMarubozu
    ├── Recognizer_BearishMarubozu
    ├── Recognizer_Hammer
    ├── Recognizer_BullishHammer
    ├── Recognizer_BearishHammer
    ├── Recognizer_InvertedHammer
    ├── Recognizer_Engulfing
    ├── Recognizer_BullishEngulfing
    ├── Recognizer_BearishEngulfing
    ├── Recognizer_Harami
    ├── Recognizer_BullishHarami
    └── Recognizer_BearishHarami
```

### Key Design Patterns

- **Data Binding** — `BindingList<T>` connects data to UI controls
- **Template Method** — Abstract `Recognizer` class defines pattern recognition interface
- **Factory Pattern** — Recognizer instantiation for pattern detection

### Method Design Convention

All major operations follow a dual-method pattern:

```csharp
// Version 1: Takes arguments, returns result
List<Candlestick> filterCandlesticks(List<Candlestick> unfilteredList, DateTime startDate, DateTime endDate)

// Version 2: Uses form-level variables, updates state
void filterCandlesticks()
```

This pattern applies to: `readCandlesticksFromFile`, `filterCandlesticks`, `normalize`, `displayCandlesticks`

---

## 📁 Project Structure

```
StockAnalysis/
├── Stock Data/                    # CSV data files
│   ├── AAPL-Day.csv
│   ├── AAPL-Week.csv
│   ├── AAPL-Month.csv
│   └── ...
├── Forms/
│   ├── Form_Main                  # Main input form with first chart
│   └── Form_StockChart            # Secondary chart windows
├── Models/
│   ├── Candlestick.cs             # Base candlestick class
│   └── SmartCandlestick.cs        # Extended candlestick with pattern detection
├── Recognizers/
│   ├── Recognizer.cs              # Abstract base class
│   ├── Recognizer_Doji.cs
│   ├── Recognizer_Marubozu.cs
│   └── ...
└── README.md
```

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2+ or .NET 6.0+
- Windows OS

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/stock-analysis.git
   ```

2. **Open the solution**
   ```
   Open StockAnalysis.sln in Visual Studio
   ```

3. **Prepare stock data**
   - Create a folder named `Stock Data` in the project directory
   - Download CSV files from Yahoo Finance
   - Name files using convention: `{SYMBOL}-{Period}.csv`
     - Example: `AAPL-Day.csv`, `MSFT-Week.csv`, `GOOGL-Month.csv`

4. **Build and run**
   ```
   Build → Build Solution (Ctrl+Shift+B)
   Debug → Start Debugging (F5)
   ```

### CSV File Format

Stock data CSV files should follow Yahoo Finance format:

```csv
Date,Open,High,Low,Close,Adj Close,Volume
2024-01-02,185.32,186.04,184.21,185.64,185.64,45678900
...
```

---

## 📖 Usage

1. **Load Stock Data**
   - Click the Load button to open the file dialog
   - Navigate to `Stock Data` folder
   - Select one or more CSV files (use Ctrl+Click for multiple)

2. **Set Date Range**
   - Use the date pickers to specify start and end dates
   - Start date is preset for convenience

3. **View Charts**
   - First selected stock displays in main form
   - Additional stocks open in separate windows

4. **Refresh Data**
   - Modify date range
   - Click Refresh to update all charts without reloading files

5. **Detect Patterns**
   - Select a pattern from the ComboBox dropdown
   - Matching patterns are highlighted with annotations

6. **Ticker Simulation**
   - Adjust speed using the scrollbar (100ms - 2000ms)
   - Watch candlesticks appear sequentially

---

## 🎨 Naming Conventions

All controls follow the `controlType_descriptiveName` convention:

| Control Type | Examples from Application |
|--------------|---------------------------|
| Button | `button_loadStockFiles`, `button_refreshAll`, `button_start`, `button_stop` |
| DateTimePicker | `dateTimePicker_startDate`, `dateTimePicker_endDate` |
| ComboBox | `comboBox_selectPattern` |
| Chart | `chart_stockChart` |
| TrackBar/Slider | `trackBar_animationSpeed` |
| TextBox | `textBox_speedValue` |
| Label | `label_startDate`, `label_endDate`, `label_animationSpeed` |

---

## 🔧 Technical Details

### Candlestick Anatomy Calculations

```
range = High - Low
bodyRange = |Close - Open|
topPrice = max(Open, Close)
bottomPrice = min(Open, Close)
upperTailRange = High - topPrice
lowerTailRange = bottomPrice - Low
```

### Chart Normalization

The Y-axis is dynamically scaled to maximize chart utilization:

```csharp
double maxHigh = candlesticks.Max(c => c.High);
double minLow = candlesticks.Min(c => c.Low);
double padding = (maxHigh - minLow) * 0.02;

chartArea.AxisY.Maximum = maxHigh + padding;
chartArea.AxisY.Minimum = minLow - padding;
```

---

## 📚 Resources & References

- [C# Syntax Reference (W3Schools)](https://www.w3schools.com/cs/)
- [Stock Chart Tutorial (FoxLearn)](https://www.youtube.com/watch?v=example)
- [Data Binding in Windows Forms (Microsoft Docs)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/data-binding)
- [C-SharpCorner Data Binding Tutorial](https://www.c-sharpcorner.com/)

---

## 📄 License

This project was developed for educational purposes as part of a university course.

---

## 🙏 Acknowledgments

- Course instructor for project specifications and guidance
- Yahoo Finance for historical stock data
- Microsoft documentation for Windows Forms and Chart controls
