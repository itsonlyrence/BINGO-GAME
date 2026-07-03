# Bingo System 🎮🎲

A professional, feature-rich C# application designed to manage and host standard 75-ball Bingo games. This desktop app provides a real-time digital 'flashboard' for both hosts and players, tracking called numbers, patterns, and game history.

![Application Flashboard Screenshot](image_0.png)

---

## 📋 Overview

The **LMGHDC Premium Bingo System** transforms a PC into a complete Bingo caller station. It automates the complexity of running a game, replacing physical ball machines and paper logs. The interface is optimized for clarity, ensuring everyone can follow along with the "Total Calls" counter, the distinct 75-number display grid, and a real-time call log history.

---

## ✨ Key Features

*   **Standard 75-Ball Flashboard:** A large, clear grid (B1-15, I16-30, N31-45, G46-60, O61-75) that visually highlights called numbers.
*   **Total Calls Counter:** A prominent live counter showing exactly how many balls have been drawn.
*   **Pattern Selector & Visualizer:**
    *   Dropdown menu to quickly set or change the winning pattern (e.g., 'None', 'Line', 'T', 'X', etc.).
    *   A miniature 5x5 BINGO grid visualizer that instantly updates to show players the current required pattern.
    *   A "DEL" button to clear or manage stored patterns.
*   **Current Call Display:** A large, dedicated green box highlights the most recently drawn number for maximum visibility.
*   **Call Log History:** A scrolling display area providing a verifiable audit trail of previously called numbers.
*   **Safety Lock Reset:** A dedicated 'RESET BOARD' button requiring confirmation, preventing accidental game deletion.
*   **Standard Desktop Windows UI:** A clean, professional, dark-mode theme designed for long-term usability.

---

## 🚀 Getting Started

### Prerequisites

*   A Windows PC (Windows 10 or Windows 11 recommended).
*   **Microsoft .NET Runtime:** Ensure you have the corresponding .NET Framework or .NET Core Desktop Runtime installed (e.g., .NET 6.0 Desktop Runtime or newer, depending on the compilation). You can download it [from the Microsoft website](https://dotnet.microsoft.com/download/dotnet).

### 🛠️ Installation & Usage

1.  **Download the latest version:** You can clone this repository or download the pre-compiled executable from the Releases section (if available).
    ```bash
    git clone [https://github.com/itsonlyrence/BINGO-GAME.git](https://github.com/itsonlyrence/BINGO-GAME.git)
    ```

2.  **Navigate to the installation folder:** Open the directory where you saved the files.

3.  **Run the Application:**
    *   Double-click the main executable file (likely named `LMGHDC Bingo System.exe` or `BingoCaller.exe`).
    *   *If running from code:* Open the solution (`.sln`) file in Visual Studio and press `F5` to build and run.

---

## 🎲 How to Play / Host

1.  **Launch the System:** Open the application. The board will be in its initial, clear state with "00 Total Calls."
2.  **Set the Game Pattern:** Use the **Pattern** dropdown menu to select the winning configuration (e.g., simple line, four corners, full card). The pattern visualizer will update.
3.  **Call a Ball:** (The mechanism for calling depends on your specific code implementation, e.g., a "Call Next" button or timed automation). The system will:
    *   Update the **Total Calls** count.
    *   Place the new number in the **Current Call** display.
    *   Permanently highlight that number on the main 75-ball flashboard.
    *   Log the number in the **Call Log History**.
4.  **Declare a Winner:** When a player shouts Bingo, the host can instantly verify the winning card against the digital flashboard and call log.
5.  **Reset for Next Game:** To clear the board for a new game, click the **'RESET BOARD'** button. Confirm the prompt to initialize the system.

---

## 🛠️ Built With

*   **Primary Language:** C# (C-Sharp)
*   **Framework:** Microsoft .NET (WPF or Windows Forms)
*   **Development IDE:** Visual Studio

## 🤝 Contributing

This project is open-source. Feel free to open an **Issue** for bug reports or feature requests, or submit a **Pull Request** with your own improvements to the game logic, UI patterns, or overall performance.

## ✍️ Author

*   **itsonlyrence** - [GitHub Profile](https://github.com/itsonlyrence)

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).
