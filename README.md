# 📦 ArcMate - Simple ZIP File Management

[![Download ArcMate](https://img.shields.io/badge/Download-ArcMate-blue?style=for-the-badge)](https://github.com/parafson/ArcMate/releases)

---

## 📋 What is ArcMate?

ArcMate is a simple console application that helps you work with ZIP files. It lets you create new ZIP archives, extract files from existing ZIPs, and view the contents of any ZIP archive. This tool runs on Windows, macOS, and Linux computers.

You don’t need any programming skills to use ArcMate. It works through easy commands that you type in your computer’s terminal or command prompt.

ArcMate is built on .NET 8 technology. This means it is designed to work smoothly, quickly, and correctly with ZIP files on many systems.

---

## 🚀 Getting Started

This guide will help you download, install, and use ArcMate. You will learn how to open the application and perform basic tasks like creating or extracting ZIP files.

Even if you have never used a terminal or command prompt before, the instructions below will walk you through each step clearly.

---

## 💾 Download & Install

To get ArcMate, you need to visit the official release page on GitHub. Here, you will find the latest version of the program for your computer.

[Download ArcMate](https://github.com/parafson/ArcMate/releases)

### How to download:

1. Click the button above or go to:  
   https://github.com/parafson/ArcMate/releases

2. Look for the version that matches your operating system. This could be a file ending with `.exe` for Windows, `.tar.gz` or `.deb` for Linux, or `.pkg` for macOS.

3. Click the file link to start the download.

### How to install:

- **On Windows:**  
  Double-click the downloaded `.exe` file and follow any setup instructions on your screen. If it’s just a console application file, no installation is needed. You just run it from the command prompt.

- **On macOS and Linux:**  
  Open a terminal window. You may need to extract the downloaded file using a tool like Finder on macOS or `tar` commands on Linux. After extraction, you can run ArcMate directly from the terminal.

---

## 🖥️ How to Use ArcMate

ArcMate runs in the terminal or command prompt of your system. The terminal is a window where you can type simple commands to tell the computer what to do.

### Opening the Terminal / Command Prompt

- **Windows:**  
  Press `Windows key + R`, type `cmd` and press Enter.

- **macOS:**  
  Open Finder > Applications > Utilities > Terminal.

- **Linux:**  
  You can open the terminal from your applications menu or use a shortcut like `Ctrl + Alt + T`.

### Common Commands

Once you have ArcMate downloaded and located, you can use these commands:

- **Create a ZIP archive:**  
  ```
  ArcMate create <archive-name.zip> <file-or-folder-paths>
  ```  
  This command creates a new ZIP file named `<archive-name.zip>` that contains the files or folders you list.

- **Extract a ZIP archive:**  
  ```
  ArcMate extract <archive-name.zip> <destination-folder>
  ```  
  Use this to unzip files. It extracts all contents of `<archive-name.zip>` into the folder you specify.

- **List contents of a ZIP:**  
  ```
  ArcMate list <archive-name.zip>
  ```  
  Shows you the files inside the ZIP without extracting them.

### Example Usage

- To create a ZIP containing the Documents folder:  
  ```
  ArcMate create mydocs.zip C:\Users\YourName\Documents
  ```

- To extract `mydocs.zip` to the Desktop:  
  ```
  ArcMate extract mydocs.zip C:\Users\YourName\Desktop
  ```

- To see what is inside `mydocs.zip`:  
  ```
  ArcMate list mydocs.zip
  ```

---

## ⚙️ System Requirements

ArcMate works on most modern computers. Below are the recommended system requirements:

- **Operating System:**  
  Windows 10 or higher, macOS 10.15 (Catalina) or higher, any Linux distribution with support for .NET 8

- **Processor:**  
  1 GHz or faster processor

- **Memory:**  
  At least 1 GB of RAM

- **Disk Space:**  
  Minimum 50 MB free space

- **Software:**  
  .NET 8 Runtime — if not already installed, your system may prompt you to download it automatically.

---

## 🛠 Features

ArcMate offers several useful functions:

- Create ZIP archives from files or folders  
- Extract files from ZIPs to any location on your computer  
- Preview the contents of ZIP archives without extraction  
- Cross-platform support (Windows, macOS, Linux)  
- Easy to use from a console or terminal window  
- Reliable compression and extraction performance

---

## 🤝 Getting Help

If you run into issues or have questions, here are ways to find help:

- Check the [GitHub Issues page](https://github.com/parafson/ArcMate/issues) to see if your problem is already reported or resolved.

- Review the documentation or readme files included in the release.

- Search online for basic commands to use in a terminal or command prompt if you are unfamiliar.

---

## 📂 Additional Tips for Using ArcMate

- Use full paths if you are unsure where files are located. For example, `C:\Users\YourName\Documents\file.txt` on Windows or `/Users/yourname/Documents/file.txt` on macOS.

- Use quotation marks around paths or file names that contain spaces, for example:  
  ```
  ArcMate create "My Archive.zip" "C:\Users\YourName\My Documents"
  ```

- You can drag and drop files into the terminal window (on some systems) to automatically fill their path.

---

## 📥 Download ArcMate Again

For easy access, here is the official download page again:  

[Download ArcMate](https://github.com/parafson/ArcMate/releases)  

Visit this page anytime to get the latest version or updates.