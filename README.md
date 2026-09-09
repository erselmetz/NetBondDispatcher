# NetBond Dispatcher

<p align="center">
  <img src="Assets/app.ico" alt="NetBond Dispatcher Logo" width="96" height="96" />
</p>

<p align="center">
  <strong>Multi-Adapter Network Aggregator & SOCKS5 Load Balancing Dispatcher for Windows</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Platform-Windows%2010%20%7C%2011-blue?style=flat-square" alt="Platform" />
  <img src="https://img.shields.io/badge/.NET-8.0%20%2F%209.0-purple?style=flat-square" alt=".NET" />
  <img src="https://img.shields.io/badge/Release-v1.0.0-brightgreen?style=flat-square" alt="Release" />
  <img src="https://img.shields.io/badge/License-MIT-green?style=flat-square" alt="License" />
</p>

---

## Overview

**NetBond Dispatcher** is a lightweight Windows utility designed to utilize multiple active network connections (such as **LAN + Wi-Fi** or **Dual Wi-Fi adapters**) simultaneously. 

By binding outgoing sockets directly to specific local network adapters, it distributes web traffic and accelerates large file transfers through parallel HTTP chunking.

> **Note on Gaming:** NetBond Dispatcher operates at Layer 7 (Application Socket Level). It does not bond single-socket UDP game connections (e.g., Valorant, Roblox), but allows you to dedicate one connection entirely to low-latency gaming while routing downloads, streams, and browsers through secondary adapters.

---

## Key Features

* **Multi-Adapter Detection & Monitoring:** Automatically detects active IPv4 network interfaces and displays real-time per-adapter upload/download throughput.
* **Local SOCKS5 Proxy Server:** Runs locally on `127.0.0.1:10808` and dispatches outbound browser/application connections across your active adapters using Round-Robin scheduling.
* **Parallel Chunk Downloader:** Splits single-file HTTP downloads into multiple concurrent ranges, assigning chunks to different physical adapters to maximize combined throughput.
* **Real-time Metrics Dashboard:** Live gauges for total upload/download speeds, active adapter counters, and session tracking.
* **Seamless Background Integration:** Minimize to Windows System Tray (`NotifyIcon`), optional start on Windows boot, and automatic silent updates via **Velopack**.

---

## Screenshots

| System Dashboard | Bonded Network Adapters |
| :---: | :---: |
| *(Add your screenshot here)* | *(Add your screenshot here)* |

| Parallel Downloader | Settings & Updates |
| :---: | :---: |
| *(Add your screenshot here)* | *(Add your screenshot here)* |

---

## Installation & Setup

### Method 1: Using the Installer (Recommended)
1. Go to the [Releases](https://github.com/YOUR_GITHUB_USERNAME/NetBondDispatcher/releases) tab.
2. Download **`NetBondDispatcher-Setup.exe`**.
3. Run the installer. The app will install and create desktop/start menu shortcuts automatically.

### Method 2: Portable Build
Download the standalone executable archive from the Releases page, extract it to any folder, and launch `NetBondDispatcher.exe`.

---

## How to Use

### 1. Multi-Adapter SOCKS5 Proxy (Browser / Streaming)
1. Launch **NetBond Dispatcher** and ensure your adapters (e.g., Ethernet + Wi-Fi) show an **Up** status under the **Network Adapters** tab.
2. Go to the **Dashboard** or **SOCKS5 Proxy** tab and click **Toggle SOCKS5 Proxy**.
3. In your browser (e.g., Chrome/Firefox) or streaming software (e.g., OBS), set your SOCKS5 proxy to:
   * **Host:** `127.0.0.1`
   * **Port:** `10808`

### 2. Parallel Chunk Downloader
1. Open the **Parallel Downloader** tab.
2. Paste any direct download URL supporting HTTP Range requests (e.g., direct `.mp4`, `.zip`, or `.iso` links).
3. Select your destination directory and choose the chunk count.
4. Click **Start Download** to observe concurrent streams running across different network cards simultaneously.

---

## Tech Stack & Dependencies

* **Language/Framework:** C# / .NET 8.0 (Windows Forms)
* **Packaging & Updates:** [Velopack](https://velopack.io/)
* **Networking:** `System.Net.Sockets`, `System.Net.Http`, `System.Net.NetworkInformation`

---

## License

This project is licensed under the [MIT License](LICENSE).